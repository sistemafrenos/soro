//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HK
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cuenta
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
