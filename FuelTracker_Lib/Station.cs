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
        private string id_station1;
        private List<Prix> list_prix;
        private string address1;
        private string tel;

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

        public Station(string id_station1, List<Prix> list_prix, string address1)
        {
            this.id_station1 = id_station1;
            this.list_prix = list_prix;
            this.address1 = address1;
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
