//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HK
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecibosPagos
    {
        public int IdPago { get; set; }
        public string IdRecibo { get; set; }
        public string FormaPago { get; set; }
        public Nullable<double> Monto { get; set; }
        public string Detalles { get; set; }
        public string Banco { get; set; }
        public string Numero { get; set; }
    
        public virtual Recibos Recibos { get; set; }
    }
}
