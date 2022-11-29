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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Agenzia = new HashSet<Agenzia>();
            this.Archivio = new HashSet<Archivio>();
            this.AssistenzaLegale = new HashSet<AssistenzaLegale>();
            this.AttachmentsAgenzia = new HashSet<AttachmentsAgenzia>();
            this.AttachmentsArchivio = new HashSet<AttachmentsArchivio>();
            this.AttachmentsAssistenzaLegale = new HashSet<AttachmentsAssistenzaLegale>();
            this.AttachmentsCredito = new HashSet<AttachmentsCredito>();
            this.AttachmentsEventi = new HashSet<AttachmentsEventi>();
            this.AttachmentsFormazione = new HashSet<AttachmentsFormazione>();
            this.AttachmentsNoleggio = new HashSet<AttachmentsNoleggio>();
            this.AttachmentsPatronato = new HashSet<AttachmentsPatronato>();
            this.AttachmentsSindacato = new HashSet<AttachmentsSindacato>();
            this.AttachmentsStudioProfessionale = new HashSet<AttachmentsStudioProfessionale>();
            this.Credito = new HashSet<Credito>();
            this.DownloadFile = new HashSet<DownloadFile>();
            this.Eventi = new HashSet<Eventi>();
            this.Formazione = new HashSet<Formazione>();
            this.Noleggio = new HashSet<Noleggio>();
            this.Patronato = new HashSet<Patronato>();
            this.Sindacato = new HashSet<Sindacato>();
            this.StudioProfessionale = new HashSet<StudioProfessionale>();
        }
    
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdSede { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Nullable<int> IdRuolo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenzia> Agenzia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Archivio> Archivio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssistenzaLegale> AssistenzaLegale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsAgenzia> AttachmentsAgenzia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsArchivio> AttachmentsArchivio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsAssistenzaLegale> AttachmentsAssistenzaLegale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsCredito> AttachmentsCredito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsEventi> AttachmentsEventi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsFormazione> AttachmentsFormazione { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsNoleggio> AttachmentsNoleggio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsPatronato> AttachmentsPatronato { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsSindacato> AttachmentsSindacato { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentsStudioProfessionale> AttachmentsStudioProfessionale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credito> Credito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DownloadFile> DownloadFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eventi> Eventi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Formazione> Formazione { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Noleggio> Noleggio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patronato> Patronato { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Sedi Sedi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sindacato> Sindacato { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudioProfessionale> StudioProfessionale { get; set; }
    }
}
