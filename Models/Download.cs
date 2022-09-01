using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDM.Models
{
    public class Download
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Blob { get; set; }
        public string Type { get; set; }
        public int IdSezione { get; set; }
        public int IdUserUpdate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}