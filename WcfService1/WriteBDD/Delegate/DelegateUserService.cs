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
    class DelegateUserService
    {
        private WriteUserService daoUserService;
        private DictionnaireReponseUpdateBase drub;
        private bool activationUserService;

        public DelegateUserService()
        {
            daoUserService = new WriteUserService();
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionUser"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
        }

        internal ReponseUpdateBase inscriptionUser(int idRole, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            return daoUserService.inscriptionUser(idRole, nom, prenom, pseudo, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }

        internal User identificationUser(string identifiant, string mdp)
        {
            return daoUserService.identificationUser(identifiant, mdp);
        }
    }
}
