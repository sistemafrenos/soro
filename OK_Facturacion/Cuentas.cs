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
    
    public partial class Cuentas
    {
        public string IdCuenta { get; set; }
        public string IdSesion { get; set; }
        public Nullable<System.DateTime> Momento { get; set; }
        public string IdDocumento { get; set; }
        public string IdTercero { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> Vence { get; set; }
        public string Tipo { get; set; }
        public string TipoDocumento { get; set; }
        public string Concepto { get; set; }
        public string Numero { get; set; }
        public Nullable<double> Monto { get; set; }
        public Nullable<double> Saldo { get; set; }
        public Nullable<bool> Seleccion { get; set; }
    }
}
