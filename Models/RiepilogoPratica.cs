using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDM.Models.Database;

namespace SDM.Models
{
    public class RiepilogoPratica
    {
        public Pratica pratica { get; set; }
        public List<Attachment> attachment { get; set; }
        public string type { get; set; }
    }
}