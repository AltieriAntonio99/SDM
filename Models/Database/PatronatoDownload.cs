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
    
    public partial class PatronatoDownload
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Type { get; set; }
        public byte[] Blob { get; set; }
        public int IdUserUpdate { get; set; }
        public System.DateTime LastUpdate { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
