using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FuelTracker_Lib
{
    [DataContract]
    public class Station
    {
        [DataMember]
        public string id_station;
        [DataMember]
        public List<Prix> price_list;
        [DataMember]
        public string address;
        [DataMember]
        public string code_postal;
        [DataMember]
        public string city;
        [DataMember]
        public float longitude;
        [DataMember]
        public float lattitude;
        [DataMember]
        public string id_enseigne;
        [DataMember]
        public Enseigne enseigne;
        [DataMember]
        public string tel;
        [DataMember]
        public string dateCreation;

        public Station() { }

        public Station(string id_station, List<Prix> price_list, string address, string city, string code_postal, float longitude, float lattitude, string id_enseigne, string enseigne_marque, string tel, string dateCreation)
        {
            this.id_station = id_station;
            this.price_list = price_list;
            this.address = address;
            this.city = city;
            this.code_postal = code_postal;
            this.longitude = longitude;
            this.lattitude = lattitude;
            this.id_enseigne = id_enseigne;
            this.dateCreation = dateCreation;
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
