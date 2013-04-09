using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class Prix
    {
        public string id_station;
        public float price;
        public Carburant_type carburant_type;
        public string dateMiseAjour;

        public Prix() { }

        public Prix(string id_station, string type_id, string type_nom, float price, string dateMiseAjour)
        {
            this.id_station = id_station;
            this.price = price;
            this.carburant_type = new Carburant_type(type_id, type_nom);
            this.dateMiseAjour = dateMiseAjour;
        }
    }
}
