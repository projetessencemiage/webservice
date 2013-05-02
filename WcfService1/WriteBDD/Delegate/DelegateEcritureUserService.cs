using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using User_Lib;
using WcfService1.Outil;
using WcfService1.WriteBDD.DAO;

namespace WcfService1
{
    class DelegateEcritureUserService
    {
        private WriteUserService daoUserService;

        public DelegateEcritureUserService()
        {
            daoUserService = new WriteUserService();
        }

        internal ReponseUpdateBase inscriptionUser(int idRole, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            return daoUserService.inscriptionUser(idRole, nom, prenom, pseudo, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }
    }
}
