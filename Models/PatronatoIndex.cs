using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDM.Models.Database;

namespace SDM.Models
{
    public class PatronatoIndex
    {
        public List<Patronato> PatronatoList { get; set; }
        public List<Categorie> Categorie { get; set; }
    }
}