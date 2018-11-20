using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HK
{
    partial class DocumentosProductos
    {
        private static Parametros parametros = FactoryParametros.Item();
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
    partial class Documentos
    {        
        public void CalcularTotales()
        {
            this.MontoIva = 0;
            this.MontoTotal = 0;
            this.MontoExento = 0;
            this.BaseImponible = 0;
            this.SubTotal = 0;
            this.PesoFactura = (from p in this.DocumentosProductos
                            where p.PesoTotal != 0 && Activo == true
                            select p.PesoTotal).Sum();
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
                       where p.Iva != 0 && Activo == true
                       select p.MontoNeto * (p.TasaIva/100)  * p.Cantidad).Sum();            
           // var Iva = Base * 0.12;
            if (Iva.HasValue)
                this.MontoIva = Iva.Value;
            var Total = (from p in this.DocumentosProductos
                         where Activo == true
                         select p.Total).Sum();

            var SubTotalPrecio  =  (from p in this.DocumentosProductos
                              where Activo == true
                              select (p.Cantidad * p.MontoNeto)).Sum();
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
        public double? CantidadItems
        {
            get { return DocumentosProductos.Sum( x=> x.Cantidad); }
            set {  }
        }

    }
    partial class VistaDocumento
    {

        public double? CantidadItems
        {
            get 
            { 
                
                Documentos Doc = FactoryDocumentos.Item(new SoroEntities(), this.IdDocumento);
                return Doc.DocumentosProductos.Sum(x => x.Cantidad);
            }
            set {  }
        }
        public double? Peso
        {
            get
            {

                Documentos Doc = FactoryDocumentos.Item(new SoroEntities(), this.IdDocumento);
                return Doc.DocumentosProductos.Sum(x => x.PesoUnitario * x.Cantidad);
            }
            set { }
        }
    }

    class FactoryDocumentos
    {
        public static List<VistaDocumento> PedidosPorFacturar()
        {
            using(var dc = new SoroEntities())
            {
                var q = from p in dc.VistaDocumento
                        where p.Tipo == "PEDIDO" && p.Status != "FACTURADO"
                        orderby p.Fecha 
                        select p;
                return q.ToList();
            }
        }
        public static int Count()
        {
            using (var oEntidades = new SoroEntities())
            {
                return oEntidades.Documentos.Count();
            }
        }
        public static bool Delete(string Id)
        {
            using (var oEntidades = new SoroEntities())
            {
                Documentos Item = oEntidades.Documentos.FirstOrDefault(x => x.IdDocumento == Id);
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
        public static List<DocumentosProductos> DetallesDocumento(SoroEntities dc, string IdDocumento)
        {
            var Consulta = from p in dc.DocumentosProductos
                           where (p.IdDocumento == IdDocumento)
                           select p;
            return Consulta.ToList();
        }
        public static IEnumerable<DocumentosProductos> DetallesDocumento(SoroEntities dc, string IdDocumento, string _filtro)
        {
            var Consulta = from p in dc.DocumentosProductos
                           where (p.IdDocumento == IdDocumento) && (p.Tipo == _filtro)
                           select p;
            return Consulta;
        }
        public static VistaDocumento ItemVistaDocumento(string IdDocumento)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.VistaDocumento
                               where (p.IdDocumento == IdDocumento)
                               select p;
                return Consulta.FirstOrDefault();
            }
        }
        public static List<VistaFactura> VistaFacturas(SoroEntities dc, string IdDocumento)
        {
            var Consulta = from p in dc.VistaFactura
                           where (p.IdDocumento == IdDocumento)
                           select p;
            return Consulta.ToList();
        }
        public static List<VistaCompras> VistaCompras(SoroEntities dc, string IdDocumento)
        {
            var Consulta = from p in dc.VistaCompras
                           where (p.IdDocumento == IdDocumento)
                           select p;
            return Consulta.ToList();
        }
       public static List<DocumentosProductos> Detalles(SoroEntities dc, string IdDocumento, string _filtro)
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
        public static List<VistaDocumento> Buscar(SoroEntities dc, string Texto, string _filtro, string Status)
        {
            var Q = dc.VistaDocumento.OrderByDescending(x=>x.Fecha).OrderByDescending(x=> x.Numero).Where(x =>
                (x.CedulaRif == Texto ||
                x.RazonSocial.Contains(Texto) ||
                x.Numero == Texto) && (x.Tipo == _filtro) && (x.Status == Status)
                
                );
            return Q.ToList();
        }
        public static List<VistaDocumento> Buscar(SoroEntities dc, string Texto, string _filtro, bool Activo)
        {
             var Consulta = from p in dc.VistaDocumento
                           where (p.Tipo == _filtro) && (p.Activo == true) &&
                           (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto)) 
                           orderby p.Fecha descending
                           select p;

            return Consulta.ToList();
        }
        public static List<VistaDocumento> BuscarVentas2(string Texto, string Filtro)
        {
            string[] Filtros = Filtro.Split(',');
            var predicate = PredicateBuilder.False<VistaDocumento>();
            foreach (string keyword in Filtros)
            {
                string temp = keyword;
                predicate = predicate.Or(p => p.Tipo.Contains(temp));
            }
            predicate = predicate.And(p=> (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto)));            
            predicate = predicate.And(p => p.Activo == true);
            SoroEntities dataContext = new SoroEntities(); 
            var query =
                    from p in dataContext.VistaDocumento.Where(predicate)
                    orderby p.Numero
                    descending
                    select p;
            return query.ToList();
        }
        public static List<VistaDocumento> BuscarVentas(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.VistaDocumento
                               where (p.Tipo == "PRESUPUESTO" || p.Tipo == "FACTURA" || p.Tipo == "PEDIDO") && (p.Activo == true) &&
                               (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;

                return Consulta.ToList();
            }
        }
        public static List<VistaDocumento> BuscarVentas(string Texto,string Filtro)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.VistaDocumento
                               where (p.Tipo ==Filtro ) && (p.Activo == true) &&
                               (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;

                return Consulta.ToList();
            }
        }
        public static List<VistaDocumento> BuscarCompras(string Texto)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = from p in dc.VistaDocumento
                               where (p.Tipo == "COMPRA" || p.Tipo == "AJUSTE") && (p.Activo == true) &&
                               (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;
                return Consulta.ToList();
            }
        }
        public static List<VistaDocumento> BuscarCompras(string Texto, string _filtro)
        {
            using (var dc = new SoroEntities())
            {

                var Consulta = from p in dc.VistaDocumento
                               where (p.Tipo == _filtro ) && (p.Activo == true) &&
                               (p.RazonSocial.Contains(Texto) || p.CedulaRif.Contains(Texto) || p.Numero.Contains(Texto))
                               orderby p.Fecha descending
                               select p;
                return Consulta.ToList();
            }
        }
        public static Documentos Item(SoroEntities dc, string IdDocumento)
        {
            return dc.Documentos.FirstOrDefault(x => x.IdDocumento == IdDocumento);
        }
        public static Documentos ItemxNumero(SoroEntities dc, string Numero)
        {
            return dc.Documentos.FirstOrDefault(x => x.Numero == Numero && x.Tipo == "FACTURA");
        }
        public static bool Save(SoroEntities dc, Documentos Doc, Terceros Tercero)
        {
            try
            {
                FactoryTerceros.Guardar(Tercero);
            }
            catch (Exception x)
            {
                throw x;
            }
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
            Doc.IdSesion = FactorySesiones.SesionActiva.IdSesion;
            Doc.IdTercero = Tercero.IdTercero;
            foreach (DocumentosProductos Detalle in Doc.DocumentosProductos)
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
        public static bool Save(SoroEntities dc, Documentos Doc, RegistroPagos Recibo, Terceros Tercero)
        {
            try
            {
                FactoryTerceros.Guardar(Tercero);
            }
            catch (Exception x)
            {
                throw x;
            }
            Doc.IdTercero = Tercero.IdTercero;
            if (string.IsNullOrEmpty(Doc.IdDocumento))
            {
                Doc.IdDocumento = FactoryContadores.GetLast("IdDocumento");
                if (Doc.Tipo == "PRESUPUESTO" || Doc.Tipo == "PEDIDO" || Doc.Tipo == "NOTA ENTREGA")
                {
                    if(string.IsNullOrEmpty( Doc.Numero ))
                    {
                        Doc.Numero = "00"+FactoryContadores.GetLast("Numero" + Doc.Tipo);
                    }
                }
                dc.Documentos.Add(Doc);
            }
            if (!FactoryParametros.Item().TipoImpresora.Contains("FISCAL"))
            {
                if (Doc.Tipo == "FACTURA")
                {
                    Parametros Parametro = FactoryParametros.Item(dc);
                    Parametro.Correlativofactura++;
                    Doc.Numero = "00" + Parametro.Correlativofactura.ToString().PadLeft(6, '0');
                }
            }
            Doc.Momento = DateTime.Now;
            Doc.IdSesion = FactorySesiones.SesionActiva.IdSesion;            
            foreach (DocumentosProductos Detalle in Doc.DocumentosProductos)
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
            if (Recibo != null)
            {
                if (Recibo.MontoPagado.HasValue)
                {
                    if (string.IsNullOrEmpty(Recibo.IdRegistroPago))
                    {
                        Recibo.IdRegistroPago = FactoryContadores.GetLast("IdRegistroPago");
                        Recibo.Documento = Doc.Numero;
                        Recibo.Tipo = Doc.Tipo;
                        Recibo.IdDocumento = Doc.IdDocumento;
                        Recibo.Documento = Doc.Numero;
                        Recibo.RazonSocial = Tercero.RazonSocial;
                        Recibo.IdTercero = Tercero.IdTercero;
                        Recibo.Modulo = Doc.Tipo;
                        Recibo.RazonSocial = Tercero.RazonSocial;
                        Recibo.Documento = Doc.Numero;
                        dc.RegistroPagos.Add(Recibo);
                    }
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
        public static bool TieneRetencionIVA(string IdDocumento)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = (from p in dc.RetencionesDetalles
                                join r in dc.Retenciones on p.IdRetencion equals r.IdRetencion
                                where p.IdDocumento == IdDocumento && r.Tipo == "RETENCION IVA"
                                select r).FirstOrDefault();
                if (Consulta== null)
                    return false;
                else
                    return true;
            }
        }
        public static bool TieneRetencionISLR(string IdDocumento)
        {
            using (var dc = new SoroEntities())
            {
                var Consulta = (from p in dc.RetencionesDetalles
                                join r in dc.Retenciones on p.IdRetencion equals r.IdRetencion
                                where p.IdDocumento == IdDocumento && r.Tipo == "RETENCION ISLR"
                                select r).FirstOrDefault();
                if (Consulta== null)
                    return false;
                else
                    return true;
            }
        }
    }
    }

