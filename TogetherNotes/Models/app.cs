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
    
    public partial class app
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public app()
        {
            this.chats = new HashSet<chats>();
            this.chats1 = new HashSet<chats>();
            this.contracts = new HashSet<contracts>();
            this.contracts1 = new HashSet<contracts>();
            this.incidences = new HashSet<incidences>();
            this.matches = new HashSet<matches>();
            this.matches1 = new HashSet<matches>();
            this.rating1 = new HashSet<rating>();
            this.rating2 = new HashSet<rating>();
            this.temp_match = new HashSet<temp_match>();
            this.temp_match1 = new HashSet<temp_match>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public Nullable<int> rating { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> language_id { get; set; }
        public Nullable<int> file_id { get; set; }
        public Nullable<int> notification_id { get; set; }
    
        public virtual files files { get; set; }
        public virtual languages languages { get; set; }
        public virtual notifications notifications { get; set; }
        public virtual artists artists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chats> chats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chats> chats1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contracts> contracts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contracts> contracts1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<incidences> incidences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matches> matches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matches> matches1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rating> rating1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rating> rating2 { get; set; }
        public virtual spaces spaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temp_match> temp_match { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temp_match> temp_match1 { get; set; }
    }
}
