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
        public virtual DbSet<AttachmentsAgenzia> AttachmentsAgenzia { get; set; }
        public virtual DbSet<AttachmentsEventi> AttachmentsEventi { get; set; }
        public virtual DbSet<AttachmentsFinanza> AttachmentsFinanza { get; set; }
        public virtual DbSet<AttachmentsPatronato> AttachmentsPatronato { get; set; }
        public virtual DbSet<AttachmentsPraticheAuto> AttachmentsPraticheAuto { get; set; }
        public virtual DbSet<AttachmentsSindacato> AttachmentsSindacato { get; set; }
        public virtual DbSet<AttachmentsStudioProfessionale> AttachmentsStudioProfessionale { get; set; }
        public virtual DbSet<AttachmentsVisure> AttachmentsVisure { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<DownloadFile> DownloadFile { get; set; }
        public virtual DbSet<Eventi> Eventi { get; set; }
        public virtual DbSet<Finanza> Finanza { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<NumeroPratiche> NumeroPratiche { get; set; }
        public virtual DbSet<Patronato> Patronato { get; set; }
        public virtual DbSet<PraticheAuto> PraticheAuto { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sedi> Sedi { get; set; }
        public virtual DbSet<Sindacato> Sindacato { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StudioProfessionale> StudioProfessionale { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visure> Visure { get; set; }
        public virtual DbSet<AttachmentsNoleggio> AttachmentsNoleggio { get; set; }
        public virtual DbSet<Noleggio> Noleggio { get; set; }
    }
}
