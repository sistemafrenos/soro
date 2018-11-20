using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Linq;

namespace HK
{
    class FiscalBixolon 
    {        
        ~ FiscalBixolon()
        {
         //   this.AuditMensaje.Hide();

        }
    //    FrmInformacion AuditMensaje = new FrmInformacion();
        #region Delaraciones
        //declaracion de funciones de la DLL
        [DllImport("tfhkaif.dll")]
        public static extern bool OpenFpctrl(string lpPortName);

        [DllImport("tfhkaif.dll")]
        public static extern bool CloseFpctrl();

        [DllImport("tfhkaif.dll")]
        public static extern bool CheckFprinter();

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool ReadFpStatus(int* status, int* error);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool SendCmd(int* status, int* error, string cmd);

        [DllImport("tfhkaif.dll")]
        public static extern int SendNcmd(int status, int error, string buffer);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern int SendFileCmd(int* status, int* error, string file);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool UploadStatusCmd(int* status, int* error, string cmd, string file);

        // Variables para monitorear retorno de funciones de la dll
        public static int iError=0;
        public static int iStatus=0;
        public static bool bRet;
        #endregion
        Parametros Parametros = null;
        public bool IsOK()
        {
            return bRet;
        }
        public FiscalBixolon()
        { 
            Parametros = FactoryParametros.Item();            
        }

        public bool ImprimeFactura(string IdDocumento)
        {
            int error;
            int status;
            string sCmd;
            bool bRet;
          //  AuditMensaje.SetTexto("");
            unsafe
            {
                SoroEntities dc = new SoroEntities();
                Documentos Documento = FactoryDocumentos.Item(dc, IdDocumento);
                if (Documento == null)
                {
                    return false;
                }
                if( !this.ConectarImpresora() )
                {
                    return false;                   
                }
                Terceros Titular = FactoryTerceros.Item(Documento.IdTercero);
                Terceros Vendedor = FactoryTerceros.Item(Documento.IdVendedor);
                double SubTotal = 0;
                double MontoIva = 0;
                bRet = SendCmd(&status, &error, "i01Cedula/Rif:" + Titular.CedulaRif) ;
                bRet = SendCmd(&status, &error, "i02Razon Social:");
                sCmd = "i03" + Titular.RazonSocial;
                bRet = SendCmd(&status, &error, sCmd);
                bRet = SendCmd(&status, &error, "i04Direccion:");
                sCmd = "i05" + Titular.Direccion;
                bRet = SendCmd(&status, &error, sCmd);
                if( Vendedor != null)
                {
                    sCmd = "i06" + Vendedor.RazonSocial;
                    bRet = SendCmd(&status, &error, sCmd);
                }                
                //if (Documento[0].Direccion.Length > 40)
                //{
                //    bRet = SendCmd(&status, &error, "i07" + Documento[0].Direccion.Substring(40, Documento[0].Direccion.Length - 40));
                //}
                foreach (DocumentosProductos d in Documento.DocumentosProductos)
                {
                    try
                    {
                        if (!d.Cantidad.HasValue)
                        {
                            d.Cantidad = 0;
                        }
                        if (!d.Iva.HasValue)
                        {
                            d.Iva = 0;
                        }
                        if (!d.MontoNeto.HasValue)
                        {
                            d.MontoNeto = 0;
                        }
                        if ((double)d.Iva == 0)
                        {
                            sCmd = " ";
                        }
                        else
                        {
                            sCmd = "!";
                        }
                        SubTotal += ((double)d.Cantidad * (double)d.MontoNeto);
                        MontoIva += ((double)d.Cantidad * (double)d.Iva);
                        string Precio = "0000000000";
                        if (Parametros.TipoIva == "INCLUIDO")
                        {
                            Precio= (Convert.ToDouble(d.MontoNeto + d.Iva) * 100).ToString("0000000000");
                        }
                        else
                        {
                            Precio = (Convert.ToDouble(d.MontoNeto) * 100).ToString("0000000000");
                        }
                        string Cantidad = (Convert.ToDouble(d.Cantidad) * 1000).ToString("00000000");
                        string Descripcion = d.Descripcion.PadRight(37);
                        if (d.Descripcion.Length <= 37)
                        {                            
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + d.Descripcion);
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}", d.Descripcion));
                            }
                        }
                        else
                        {
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + Descripcion.Substring(0,37));
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}",d.Descripcion));
                            }
                            string Descripcion2 = d.Descripcion.Substring(37, (d.Descripcion.Length - 37));
                            bRet = SendCmd(&status, &error, "@" + Descripcion2);
                        }
#region custom
                        switch (FactoryParametros.Item().Empresa)
                        {
                            case "CENTRO AUTOMOTRIZ SUCRE,C.A.":
                                    break;
                            default:
                                    bRet = SendCmd(&status, &error, "@" + d.Codigo);
                                    break;
                        }
#endregion                    
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                        return false;
                    }
                }
                if (Documento.DescuentoBs != 0)
                {
                    
                    Documento.DescuentoBs = Documento.MontoTotal - SubTotal - MontoIva;
                    Documento.DescuentoBs = Documento.DescuentoBs * -1;
                    Documento.DescuentoPorcentaje = (Documento.DescuentoBs * 100) / (SubTotal + MontoIva);
                    bRet = SendCmd(&status, &error, "3");
                    string DescuentoPorcentaje = ((double)Documento.DescuentoPorcentaje * 100).ToString("0000");
                    bRet = SendCmd(&status, &error, "p-"+DescuentoPorcentaje);
                }
                //Pagos
                double TotalPagos = 0;
                try
                {
                    if (Documento.Efectivo != 0)
                    {
                        sCmd = "201" + ((double)Documento.Efectivo * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Efectivo.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Deposito != 0)
                    {
                        sCmd = "207" + ((double)Documento.Deposito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Deposito.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Transferencias != 0)
                    {
                        sCmd = "208" + ((double)Documento.Transferencias * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Transferencias.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Cheque != 0)
                    {
                        sCmd = "205" + ((double)Documento.Cheque * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Cheque.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.TarjetaCredito != 0)
                    {
                        sCmd = "210" + ((double)Documento.TarjetaCredito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.TarjetaCredito.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.TarjetaDebito != 0)
                    {
                        sCmd = "209" + ((double)Documento.TarjetaDebito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.TarjetaDebito.Value;
                    }
                }
                catch { }
                if( Documento.Saldo > 0 )
                {
                    sCmd = "216" + ((double)Documento.Saldo * 100).ToString("000000000000");
                    bRet = SendCmd(&status, &error, sCmd);
                    TotalPagos += Documento.Saldo.Value; 
                }
                if (Documento.MontoTotal >TotalPagos)
                {
                    sCmd = "216" + (((double)Documento.MontoTotal - (double)TotalPagos) * 100).ToString("000000000000");
                    bRet = SendCmd(&status, &error, sCmd);

                }
            }
            System.Threading.Thread.Sleep(1000);
            this.LiberarImpresora();
            return bRet;
        }        
        public void ImprimeComentarios(string s)
        {
            int error;
            int status;
            bool bRet;

            unsafe
            {
                if (s.Length <= 40 && s.Length > 0)
                    bRet = SendCmd(&status, &error, "@" + s);
                if (s.Length > 40)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(0, 40));
                    bRet = SendCmd(&status, &error, "@" + s.Substring(40, s.Length - 40));
                }
                if (s.Length > 80)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(80, s.Length - 80));
                }
                if (s.Length > 120)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(120, s.Length - 120));
                }
                if (s.Length > 160)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(160, s.Length - 160));
                }

            }
        }
        public bool ImprimeDevolucion(string IdDocumento)
        {
            Recibos Recibo = new Recibos();
            SoroEntities dc = new SoroEntities();
            Documentos Documento = FactoryDocumentos.Item(dc, IdDocumento);
            if (Documento == null)
            {
                return false;
            }
            if (!this.ConectarImpresora())
            {
                return false;
            }
            Terceros Titular = FactoryTerceros.Item(Documento.IdTercero);
            Terceros Vendedor = FactoryTerceros.Item(Documento.IdVendedor);
            unsafe
            {
                int error;
                int status;
                string sCmd;
                bool bRet=false;
                bRet = SendCmd(&status, &error, "i01Cedula/Rif:" + Titular.CedulaRif);
                bRet = SendCmd(&status, &error, "i02Razon Social:");
                sCmd = "i03" + Titular.RazonSocial;
                bRet = SendCmd(&status, &error, sCmd);
                bRet = SendCmd(&status, &error, "i04Direccion:");
                sCmd = "i05" + Titular.Direccion;
                bRet = SendCmd(&status, &error, sCmd);
                if (Titular.Direccion.Length > 40)
                {
                    bRet = SendCmd(&status, &error, "i06" + Titular.Direccion.Substring(40, Titular.Direccion.Length - 40));
                }
                sCmd = "i07 # FACTURA AFECTADA:" + Documento.Comentarios;
                bRet = SendCmd(&status, &error, sCmd);                
                // Agrego el servicio en la ultima fila
               // DS.ImpresionTicket.AddImpresionTicketRow(1, 1, 1, 1, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 1, "", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, "SERVICIO 10%", 1, Documento[0].MontoServicio, 0, "", "", "");
                double SubTotal = 0;
                double MontoIva = 0;
                bRet = SendCmd(&status, &error, sCmd);
                sCmd = "i06" + Titular.Direccion.PadRight(40);
                foreach (DocumentosProductos d in Documento.DocumentosProductos)
                {
                    try
                    {
                        if (!d.Cantidad.HasValue)
                        {
                            d.Cantidad = 0;
                        }
                        if (!d.Iva.HasValue)
                        {
                            d.Iva = 0;
                        }
                        if (!d.MontoNeto.HasValue)
                        {
                            d.MontoNeto = 0;
                        }
                        if ((double)d.Iva == 0)
                            sCmd = "d0";
                        else
                            sCmd = "d1";
                        SubTotal += ((double)d.Cantidad * (double)d.MontoNeto);
                        MontoIva += ((double)d.Cantidad * (double)d.Iva);
                        string Precio = "0000000000";
                        if (Parametros.TipoIva == "INCLUIDO")
                        {
                            Precio = (Convert.ToDouble(d.MontoNeto + d.Iva) * 100).ToString("0000000000");
                        }
                        else
                        {
                            Precio = (Convert.ToDouble(d.MontoNeto) * 100).ToString("0000000000");
                        }
                        string Cantidad = (Convert.ToDouble(d.Cantidad) * 1000).ToString("00000000");
                        string Descripcion = d.Descripcion.PadRight(37);
                        bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + Descripcion);
                        if (!bRet)
                        {
                            return false;
                        }
                        if (d.Descripcion.Length > 37)
                        {
                            string Descripcion2 = d.Descripcion.Substring(37, (d.Descripcion.Length - 37));
                            SendCmd(&status, &error, "@" + Descripcion2);
                        }
#region custom
                        switch (FactoryParametros.Item().Empresa)
                        {
                            case "CENTRO AUTOMOTRIZ SUCRE,C.A.":
                                    break;
                            default:
                                    bRet = SendCmd(&status, &error, "@" + d.Codigo);
                                    break;
                        }
#endregion                   
                    
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                    }
                }
                if (Documento.DescuentoBs != 0)
                {

                    Documento.DescuentoBs = Documento.MontoTotal - SubTotal - MontoIva;
                    Documento.DescuentoBs = Documento.DescuentoBs * -1;
                    Documento.DescuentoPorcentaje = (Documento.DescuentoBs * 100) / (SubTotal + MontoIva);
                    bRet = SendCmd(&status, &error, "3");
                    string DescuentoPorcentaje = ((double)Documento.DescuentoPorcentaje * 100).ToString("0000");
                    bRet = SendCmd(&status, &error, "p-" + DescuentoPorcentaje);
                }
                //Pagos
                double TotalPagos = 0;
                try
                {
                    if (Documento.Efectivo != 0)
                    {
                        sCmd = "f01" + ((double)Documento.Efectivo * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Efectivo.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Deposito != 0)
                    {
                        sCmd = "f07" + ((double)Documento.Deposito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Deposito.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Transferencias != 0)
                    {
                        sCmd = "f08" + ((double)Documento.Transferencias * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Transferencias.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.Cheque != 0)
                    {
                        sCmd = "f05" + ((double)Documento.Cheque * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.Cheque.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.TarjetaCredito != 0)
                    {
                        sCmd = "f10" + ((double)Documento.TarjetaCredito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.TarjetaCredito.Value;
                    }
                }
                catch { }
                try
                {
                    if (Documento.TarjetaDebito != 0)
                    {
                        sCmd = "f09" + ((double)Documento.TarjetaDebito * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += Documento.TarjetaDebito.Value;
                    }
                }
                catch { }
                if (Documento.Saldo > 0)
                {
                    sCmd = "f16" + ((double)Documento.Saldo * 100).ToString("000000000000");
                    bRet = SendCmd(&status, &error, sCmd);
                }
                if (Documento.MontoTotal > TotalPagos)
                {
                    sCmd = "f16" + ((double)Documento.MontoTotal - (double)TotalPagos * 100).ToString("000000000000");
                    bRet = SendCmd(&status, &error, sCmd);

                }
                this.LiberarImpresora();
                return true;
            }
        }
        public bool ImprimeFacturaCopia(string Numero)
        {
            int error;
            int status;
            bool bRet;

            unsafe
            {
                if (!CheckFprinter())
                {                    
                    throw new Exception("Impresora no encontrada");                    
                }
             //   AuditMensaje.SetTexto("Comunicacion con impresora OK");
             //   AuditMensaje.SetTexto("Enviado Comandos....");
                bRet = SendCmd(&status, &error, "RF" +  Numero + Numero);
                if (!bRet)
                {
               //     AuditMensaje.SetTextoModal("Imposible imprimir copia de esa factura....");
                }
              //  AuditMensaje.SetTexto("");
                return bRet;
            }
        }
        public bool ConectarImpresora()
        {            
            bRet = OpenFpctrl(Parametros.PuertoImpresora);            
            if (!bRet)
            {                
                throw( new Exception("Error de conexión, verifique el puerto por favor..."));                
            }
            cBasicas.ImpresoraOcupada = true;
            return bRet;
        }
        public void LiberarImpresora()
        {
            //Comando de apertura de puerto e inicio de conexión
            //AuditMensaje.SetTexto("Estableciendo Comunicación... Por favor espere");
            cBasicas.ImpresoraOcupada = false;
            bRet = CloseFpctrl();
            if (bRet)
            {
              //  AuditMensaje.SetTexto("Desconexión Exitosa....");
            }
            else
            {                
                throw (new Exception("Error de desconexión, verifique el puerto por favor..."));
            }
            // AuditMensaje.SetTexto("");
        }
        public void ReporteX()
        {
            int error;
            int status;
            string sCmd;
            bool bRet;
            if (!this.ConectarImpresora())
                return; unsafe
            {
                //************ Imprimir Reporte X *******************
                sCmd = "I0X";
                bRet = SendCmd(&status, &error, sCmd);
            }
            this.LiberarImpresora();

        }
        public void ReporteZ()
        {
            int error;
            int status;
            string sCmd;
            bool bRet;
            if (!this.ConectarImpresora())
                return;
            unsafe
            {
                //************ Imprimir Reporte Z *******************
                sCmd = "I0Z";
                bRet = SendCmd(&status, &error, sCmd);
           }
            this.LiberarImpresora();
        }
        public string GetNumeroFactura()
        {
            int error;
            int status;
            bool bRet;
            string Retorno = "";
            if (!ConectarImpresora())
            {
                throw new Exception("Imposible leer datos desde Impresora Fiscal");
            }
            try
            {
                unsafe
                {
                    bRet = UploadStatusCmd(&status, &error, "S1", "Status.TXT");
                    if (!bRet)
                    {
                        throw new Exception("Imposible leer datos desde Impresora Fiscal");
                    }
                    using (StreamReader sr = new StreamReader("Status.TXT"))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            Retorno= "0"+line.Substring(22, 7);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Imposible leer datos desde Impresora Fiscal\n" + e.Message );
            }
            this.LiberarImpresora();
            return Retorno;
        }
    }
}
