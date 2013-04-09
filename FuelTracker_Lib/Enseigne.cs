using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class Enseigne
    {
        public string id_enseigne;
        public string enseigne_name;

        public Enseigne() { }

        public Enseigne(string id_enseigne, string enseigne_name) 
        {
            this.id_enseigne = id_enseigne;
            this.enseigne_name = enseigne_name;
        }
    }
}
