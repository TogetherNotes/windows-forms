//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TogetherNotes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class artists
    {
        public int app_user_id { get; set; }
        public Nullable<int> genre_id { get; set; }
    
        public virtual app app { get; set; }
        public virtual genres genres { get; set; }
    }
}
