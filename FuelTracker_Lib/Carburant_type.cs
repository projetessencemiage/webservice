using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class Carburant_type
    {
        public string id_type;
        public string type_nom;

        public Carburant_type() { }

        public Carburant_type(string id_type, string type_nom)
        {
            this.id_type = id_type;
            this.type_nom = type_nom;
        }
    }
}
