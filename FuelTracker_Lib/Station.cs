using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class Station
    {
        public int id_station { get; set; }
        public List<Prix> price_list {get; set;}
        public string address { get; set; }
        public string city { get; set; }
        public string department { get; set; }
        public float longitutde { get; set; }
        public float lattitude { get; set; }
        public int id_enseigne { get; set; }
        public Enseigne enseigne { get; set; }

        public Station() { }
    }
}
