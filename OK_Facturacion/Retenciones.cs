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
    
    public partial class Retenciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Retenciones()
        {
            this.RetencionesDetalles = new HashSet<RetencionesDetalles>();
        }
    
        public string IdRetencion { get; set; }
        public string IdTercero { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public Nullable<double> Monto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RetencionesDetalles> RetencionesDetalles { get; set; }
    }
}