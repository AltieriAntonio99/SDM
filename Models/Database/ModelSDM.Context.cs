﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SDMEntities : DbContext
    {
        public SDMEntities()
            : base("name=SDMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agenzia> Agenzia { get; set; }
        public virtual DbSet<AgenziaDownload> AgenziaDownload { get; set; }
        public virtual DbSet<Archivio> Archivio { get; set; }
        public virtual DbSet<ArchivioDownload> ArchivioDownload { get; set; }
        public virtual DbSet<AssistenzaContabile> AssistenzaContabile { get; set; }
        public virtual DbSet<AssistenzaDownload> AssistenzaDownload { get; set; }
        public virtual DbSet<AssistenzaLegale> AssistenzaLegale { get; set; }
        public virtual DbSet<AttachmentsAgenzia> AttachmentsAgenzia { get; set; }
        public virtual DbSet<AttachmentsArchivio> AttachmentsArchivio { get; set; }
        public virtual DbSet<AttachmentsAssistenzaContabile> AttachmentsAssistenzaContabile { get; set; }
        public virtual DbSet<AttachmentsAssistenzaLegale> AttachmentsAssistenzaLegale { get; set; }
        public virtual DbSet<AttachmentsCredito> AttachmentsCredito { get; set; }
        public virtual DbSet<AttachmentsEventi> AttachmentsEventi { get; set; }
        public virtual DbSet<AttachmentsFormazione> AttachmentsFormazione { get; set; }
        public virtual DbSet<AttachmentsNoleggio> AttachmentsNoleggio { get; set; }
        public virtual DbSet<AttachmentsPatronato> AttachmentsPatronato { get; set; }
        public virtual DbSet<AttachmentsSindacato> AttachmentsSindacato { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Credito> Credito { get; set; }
        public virtual DbSet<CreditoDownload> CreditoDownload { get; set; }
        public virtual DbSet<DownloadFile> DownloadFile { get; set; }
        public virtual DbSet<Eventi> Eventi { get; set; }
        public virtual DbSet<EventiDownload> EventiDownload { get; set; }
        public virtual DbSet<Formazione> Formazione { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Noleggio> Noleggio { get; set; }
        public virtual DbSet<NoleggioDownload> NoleggioDownload { get; set; }
        public virtual DbSet<NumeroPratiche> NumeroPratiche { get; set; }
        public virtual DbSet<Patronato> Patronato { get; set; }
        public virtual DbSet<PatronatoDownload> PatronatoDownload { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sedi> Sedi { get; set; }
        public virtual DbSet<Sindacato> Sindacato { get; set; }
        public virtual DbSet<SindacatoDownload> SindacatoDownload { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
