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
    
    public partial class RetencionesDetalles
    {
        public string IdRetencionDetalle { get; set; }
        public string IdRetencion { get; set; }
        public string IdDocumento { get; set; }
        public string Numero { get; set; }
        public Nullable<double> BaseImponible { get; set; }
        public Nullable<double> Iva { get; set; }
        public Nullable<double> PorcentajeRetenido { get; set; }
        public Nullable<double> MontoRetenido { get; set; }
    
        public virtual Retenciones Retenciones { get; set; }
    }
}
