using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class Station
    {
        public string id_station;
        public List<Prix> price_list;
        public string address;
        public string code_postal;
        public string city;
        public float longitude;
        public float lattitude;
        public string id_enseigne;
        public Enseigne enseigne;
        public string tel;

        public Station() { }

        public Station(string id_station, List<Prix> price_list, string address, string city, string code_postal, float longitude, float lattitude, string id_enseigne, string enseigne_marque, string tel)
        {
            this.id_station = id_station;
            this.price_list = price_list;
            this.address = address;
            this.city = city;
            this.code_postal = code_postal;
            this.longitude = longitude;
            this.lattitude = lattitude;
            this.id_enseigne = id_enseigne;
            this.enseigne = new Enseigne(id_enseigne, enseigne_marque);
            this.tel = tel;
        }

        public void setPrice(List<Prix> price_list)
        {
            this.price_list = price_list;
        }

        public string getIdStation()
        {
            return id_station;
        }

        public void setEnseigne(Enseigne enseigne)
        {
            this.enseigne = enseigne;
        }
    }
}
