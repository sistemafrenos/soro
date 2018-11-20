using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HK
{
    partial class DocumentosProducto
    {
        private static Parametro parametros = FactoryParametros.Item();
        private double? minimo = 0;

        public double? Minimo
        {
            get { return minimo; }
            set { minimo = value; }
        }
        private double? maximo = 0;

        public double? Maximo
        {
            get { return maximo; }
            set { maximo = value; }
        }
        private string linea;

        public string Linea
        {
            get { return linea; }
            set { linea = value; }
        }
    }
    partial class Documento
    {        
        public void CalcularTotales()
        {
            this.MontoIva = 0;
            this.MontoTotal = 0;
            this.MontoExento = 0;
            this.BaseImponible = 0;
            this.SubTotal = 0;
            var Base = (from p in this.DocumentosProductos
                        where p.Iva != 0 && Activo == true
                        select p.MontoNeto * p.Cantidad).Sum();
            if (Base.HasValue)
                this.BaseImponible = Base.Value;
            var Exento = (from p in this.DocumentosProductos
                          where p.Iva == 0 && Activo == true
                          select p.Total).Sum();
            if (Exento.HasValue)
                this.MontoExento = Exento.Value;
            var Iva = (from p in this.DocumentosProductos
                       where p.Iva != 0 && Activo==true
                       select p.Iva * p.Cantidad).Sum();
            if (Iva.HasValue)
                this.MontoIva = Iva.Value;
            var Total = (from p in this.DocumentosProductos
                         where Activo == true
                         select p.Total).Sum();

            var SubTotalPrecio  =  (from p in this.DocumentosProductos
                              where Activo == true
                              select (p.Cantidad * p.Precio)).Sum();
            var SubTotalCosto = (from p in this.DocumentosProductos
                                  where Activo == true
                                  select (p.Cantidad * p.Costo)).Sum();
            //var SubDescuentoBs = (from p in this.DocumentosProductos
            //                      where Activo == true
            //                      select (p.Cantidad * p.DescuentoBs)).Sum();
            if (this.Tipo == "COMPRA" || this.Tipo =="PEDIDO PROVEEDOR")
            {
                if (SubTotalCosto.HasValue)
                this.SubTotal = SubTotalCosto.Value;
            }
            else
            {
                if (SubTotalPrecio.HasValue)
                this.SubTotal = SubTotalPrecio.Value;
            }
            //if( SubDescuentoBs.HasValue )
            //        this.DescuentoBs = SubDescuentoBs.Value;
            if (!DescuentoBs.HasValue)
            {
                DescuentoBs = 0;
            }
            if (!Fletes.HasValue)
            {
                Fletes = 0;
            }
            if (Total.HasValue ) 
                this.MontoTotal = cBasicas.Round( Total.Value - DescuentoBs+Fletes);
        }
        public double Pagos()
        {
            if( !this.Efectivo.HasValue )
                this.Efectivo = 0;
            if( !this.Cheque.HasValue )
                this.Cheque = 0;
            if( !this.TarjetaCredito.HasValue )
                this.TarjetaCredito = 0;
            if( !this.TarjetaDebito.HasValue )
                this.TarjetaDebito = 0;
            if( !this.Deposito.HasValue )
                this.Deposito = 0;
            if (!this.Transferencias.HasValue)
                this.Transferencias = 0;
            if (!this.Cambio.HasValue)
                this.Cambio = 0;
            return Efectivo.Value + Cheque.Value + TarjetaCredito.Value + TarjetaDebito.Value + Deposito.Value + Transferencias.Value - Cambio.Value;
        }
    }
    partial class Documento
    {

        public double? CantidadItems
        {
            get 
            { 
                
                Documento Doc = FactoryDocumentos.Item(new Entities(), this.IdDocumento);
                return Doc.DocumentosProductos.Sum(x => x.Cantidad);
            }
            set {  }
        }
        public double? Peso
        {
            get
            {

                Documento Doc = FactoryDocumentos.Item(new Entities(), this.IdDocumento);
                return Doc.DocumentosProductos.Sum(x => x.PesoUnitario * x.Cantidad);
            }
            set { }
        }
    }

    class FactoryDocumentos
    {
        public static List<Documento> PedidosPorFacturar()
        {
            using(var dc = new Entities())
            {
                var q = from p in dc.Documentos
                        where p.Tipo == "PEDIDO" && p.Status != "FACTURADO"
                        orderby p.Fecha 
                        select p;
                return q.ToList();
            }
        }
        public static int Count()
        {
            using (var oEntidades = new Entities())
            {
                return oEntidades.Documentos.Count();
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new Entities())
            {
                Documento Item = oEntidades.Documentos.FirstOrDefault(x => x.IdDocumento == Id);
                if (Item == null)
                    return false;
                try
                {
                    oEntidades.Documentos.Remove(Item); ;
                    try
                    {
                        oEntidades.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        string s = x.Message;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
        }
        public static List<DocumentosProducto> DetallesDocumento(Entities dc, string IdDocumento)
        {
            var Consulta = from p in dc.DocumentosProductos
                           where (p.IdDocumento == IdDocumento)
                           select p;
            return Consulta.ToList();
        }
        public static IEnumerable<DocumentosProducto> DetallesDocumento(Entities dc, string IdDocumento, string _filtro)
        {
            var Consulta = from p in dc.DocumentosProductos
                           where (p.IdDocumento == IdDocumento) && (p.Tipo == _filtro)
                           select p;
            return Consulta;
        }
       public static List<DocumentosProducto> Detalles(Entities dc, string IdDocumento, string _filtro)
        {
            string BuscarFiltro = "";
            if (_filtro != "" && _filtro != null)
            {
                string[] s = _filtro.Split(',');
                BuscarFiltro = " and ( ";
                foreach (string Item in s)
                {
                    if (Item != s[0])
                    {
                        BuscarFiltro += " or it.Tipo='" + Item + "'";
                    }
                    else
                    {
                        BuscarFiltro += "it.Tipo='" + Item + "'";
                    }
                }
                BuscarFiltro += ")";
            }
            if (IdDocumento == null)
            {
                IdDocumento = "";
            }
            var Consulta = from p in dc.DocumentosProductos
                           where (p.IdDocumento == IdDocumento) && (p.Tipo == _filtro)
                           select p;
            return Consulta.ToList();
        }
        public static List<Documento> Buscar(Entities dc, string Texto, string _filtro, string Status)
        {
            var Q = dc.Documentos.OrderByDescending(x=>x.Fecha).OrderByDescending(x=> x.Numero).Where(x =>
                (x.CedulaRif == Texto ||
                x.Razonsocial.Contains(Texto) ||
                x.Numero == Texto) && (x.Tipo == _filtro) && (x.Status == Status)
                
                );
            return Q.ToList();
        }
        public static List<Documento> Buscar(Entities dc, string Texto, string _filtro, bool Activo)
        {
             var Consulta = from p in dc.Documentos
                           where (p.Tipo == _filtro) && (p.Activo == true) &&
                           (p.Razonsocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto)) 
                           orderby p.Fecha descending
                           select p;

            return Consulta.ToList();
        }
        public static List<Documento> BuscarVentas(string Texto,string Filtro)
        {
            using (var dc = new Entities())
            {
                var Consulta = from p in dc.Documentos
                               where (p.Tipo ==Filtro ) && (p.Activo == true) &&
                               (p.Razonsocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;

                return Consulta.ToList();
            }
        }
        public static List<Documento> BuscarCompras(string Texto)
        {
            using (var dc = new Entities())
            {
                var Consulta = from p in dc.Documentos
                               where (p.Tipo == "COMPRA" || p.Tipo == "AJUSTE") && (p.Activo == true) &&
                               (p.Razonsocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;
                return Consulta.ToList();
            }
        }
        public static List<Documento> BuscarCompras(string Texto, string _filtro)
        {
            using (var dc = new Entities())
            {

                var Consulta = from p in dc.Documentos
                               where (p.Tipo == _filtro ) && (p.Activo == true) &&
                               (p.Razonsocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;
                return Consulta.ToList();
            }
        }
        public static Documento Item(Entities dc, string IdDocumento)
        {
            return dc.Documentos.FirstOrDefault(x => x.IdDocumento == IdDocumento);
        }
        public static Documento ItemxNumero(Entities dc, string Numero)
        {
            return dc.Documentos.FirstOrDefault(x => x.Numero == Numero && x.Tipo == "FACTURA");
        }
        public static bool Save(Entities dc, Documento Doc, Tercero Tercero)
        {
            if (string.IsNullOrEmpty(Doc.IdDocumento))
            {
                Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                dc.Documentos.Add(Doc);
            }
            if (string.IsNullOrEmpty(Doc.Numero))
            {
                Doc.Numero = "00"+FactoryContadores.GetLast("Numero"+Doc.Tipo);
            }
            Doc.Momento = DateTime.Now;
            Doc.IdTercero = Tercero.IdTercero;
            foreach (DocumentosProducto Detalle in Doc.DocumentosProductos)
            {
                if (string.IsNullOrEmpty(Detalle.IdDocumento))
                {
                    Detalle.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento");
                }
            }
            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public static bool Save(Entities dc, Documento Doc)
        {
            if (string.IsNullOrEmpty(Doc.IdDocumento))
            {
                Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                if (Doc.Tipo == "PRESUPUESTO" || Doc.Tipo == "PEDIDO")
                {
                    if(string.IsNullOrEmpty( Doc.Numero ))
                    {
                        Doc.Numero = "00"+FactoryContadores.GetLast("Numero" + Doc.Tipo);
                    }
                }
                dc.Documentos.Add(Doc);
            }
            Doc.Momento = DateTime.Now;
            foreach (DocumentosProducto Detalle in Doc.DocumentosProductos)
            {
                if (string.IsNullOrEmpty(Detalle.IdDetalleDocumento))
                {
                    Detalle.IdDetalleDocumento = FactoryContadores.GetLast("IdDetalleDocumento");
                }
                if (Detalle.IdProducto == null)
                {
                    Doc.DocumentosProductos.Remove(Detalle);
                    dc.DocumentosProductos.Remove(Detalle);
                }
            }
            try
            {
               dc.SaveChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
            return true;
        }
    }
    }

