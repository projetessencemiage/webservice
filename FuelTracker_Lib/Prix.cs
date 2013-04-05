using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    class Prix
    {
        public int id_type { get; set; }
        public int id_station { get; set; }
        public float price { get; set; }
        public Carburant_type carburant_type { get; set; }

        public Prix() { }
    }
}
