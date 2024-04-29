using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remindo.Class
{
    public class Tache
    {
        public int TacheId { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateRealisation { get; set; }
        public string Statut { get; set; }
    }
}
