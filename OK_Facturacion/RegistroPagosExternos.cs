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
    
    public partial class RegistroPagosExternos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistroPagosExternos()
        {
            this.RegistroPagosExternosDetalles = new HashSet<RegistroPagosExternosDetalles>();
        }
    
        public string IdRegistroPagoRemoto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<double> Cambio { get; set; }
        public Nullable<double> Efectivo { get; set; }
        public Nullable<double> Cheque { get; set; }
        public Nullable<double> Deposito { get; set; }
        public Nullable<double> Cheque2 { get; set; }
        public Nullable<double> Deposito2 { get; set; }
        public string NumeroCheque { get; set; }
        public string NumeroDeposito { get; set; }
        public string NumeroCheque2 { get; set; }
        public string NumeroDeposito2 { get; set; }
        public string BancoCheque { get; set; }
        public string BancoCheque2 { get; set; }
        public string BancoDeposito { get; set; }
        public string BancoDeposito2 { get; set; }
        public string Detalles { get; set; }
        public string Equipo { get; set; }
        public string Facturas { get; set; }
        public Nullable<bool> Aplicado { get; set; }
        public Nullable<bool> Aplicar { get; set; }
        public string IdTercero { get; set; }
        public string RazonSocial { get; set; }
        public Nullable<double> Monto { get; set; }
        public Nullable<double> SaldoAnterior { get; set; }
        public Nullable<double> SaldoAcual { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroPagosExternosDetalles> RegistroPagosExternosDetalles { get; set; }
    }
}
