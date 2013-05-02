using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Lib
{
    public class User
    {
        public Role role;
        public string nom;
        public string prenom;
        public string pseudo;
        public string adresse;
        public string code_postal;
        public string ville;
        public string mdp;
        public string avatar;
        public string email;
        public int id_station_favorite;
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
