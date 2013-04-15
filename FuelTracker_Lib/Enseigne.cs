using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    [DataContract]
    public class Enseigne
    {
        [DataMember]
        public string id_enseigne;
        [DataMember]
        public string enseigne_name;

        public Enseigne() { }

        public Enseigne(string id_enseigne, string enseigne_name) 
        {
            this.id_enseigne = id_enseigne;
            this.enseigne_name = enseigne_name;
        }
    }
}
