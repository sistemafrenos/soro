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
    
    public partial class LibroInventarios
    {
        public string Id { get; set; }
        public string IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Año { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<double> Inicial { get; set; }
        public Nullable<double> InicialBs { get; set; }
        public Nullable<double> Entradas { get; set; }
        public Nullable<double> EntradasBs { get; set; }
        public Nullable<double> Salidas { get; set; }
        public Nullable<double> SalidasBs { get; set; }
        public Nullable<double> Final { get; set; }
        public Nullable<double> FinalBs { get; set; }
    }
}