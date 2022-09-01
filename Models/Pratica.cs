using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SDM.Models.Database;

namespace SDM.Models
{
    public class Pratica
    {
        public int Id { get; set; }
        public string Anno { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public List<Categorie> SottocategoriaList { get; set; }
        public string Sottocategoria { get; set; }

        [DisplayName("Tipologia pratica")]
        public string TipologiaPratica { get; set; }
        public string Note { get; set; }
        public string NumPratica { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? IdUserUpdate { get; set; }
        public int? IdSede { get; set; }
        public int? IdStato { get; set; }
        public List<Status> StatoList { get; set; }

    }
}