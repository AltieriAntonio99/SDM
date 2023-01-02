//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDM.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Visure()
        {
            this.AttachmentsVisure = new HashSet<AttachmentsVisure>();
        }
    
        public int Id { get; set; }
        public string Anno { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Sottocategoria { get; set; }
        public string NumPratica { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<int> IdUserUpdate { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdStato { get; set; }
        public string TipologiaPratica { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsVisure> AttachmentsVisure { get; set; }
        public virtual Sedi Sedi { get; set; }
        public virtual Users Users { get; set; }
    }
}