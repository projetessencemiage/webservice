using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace User_Lib
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Role role;
        [DataMember]
        public string nom;
        [DataMember]
        public string prenom;
        [DataMember]
        public string pseudo;
        [DataMember]
        public string adresse;
        [DataMember]
        public string code_postal;
        [DataMember]
        public string ville;
        [DataMember]
        public string mdp;
        [DataMember]
        public string avatar;
        [DataMember]
        public string email;
        [DataMember]
        public int id_station_favorite;
        [DataMember]
        public int id_carburant_pref;


        public User(int idRole, string nomRole, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            role = new Role(idRole, nomRole);
            this.nom = nom;
            this.prenom = prenom;
            this.pseudo = pseudo;
            this.adresse = adresse;
            this.code_postal = code_postal;
            this.ville = ville;
            this.mdp = mdp;
            avatar = url_avatar;
            this.email = email;
            this.id_carburant_pref = id_carburant_pref;
            this.id_station_favorite = id_station_favorite;
        }
    }
}
