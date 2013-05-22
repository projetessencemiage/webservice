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
        private DictionnaireReponseUpdateBase drub;

        public DelegateEcritureUserService()
        {
            daoUserService = new WriteUserService();
            drub = new DictionnaireReponseUpdateBase();
        }

        internal ReponseUpdateBase inscriptionUser(int idRole, string civilite, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            return daoUserService.inscriptionUser(idRole, civilite, nom, prenom, pseudo, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }

        internal ReponseUpdateBase desinscription(string identifiant, string mdp)
        {
            return daoUserService.desinscription(identifiant, mdp);
        }

        internal ReponseUpdateBase miseAJourProfilUser(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            return daoUserService.miseAJourProfilUser(civilite, nom, prenom, pseudo, email, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }

        internal ReponseUpdateBase changementMDP(string identifiant, string ancienMDP, string nouveauMDP)
        {
            return daoUserService.changementMDP(identifiant, ancienMDP, nouveauMDP);
        }

        internal ReponseUpdateBase motDePasseOublie(string identifiant)
        {
            return daoUserService.motDePasseOublie(identifiant);
        }

        internal ReponseUpdateBase ReinitialisationMDP(string identifiant, string cle, string nouveauMDP)
        {
            bool cle_active = daoUserService.verificationCle(cle, identifiant);
            if (cle_active)
            {
                return daoUserService.reinitialisationMDP(identifiant, cle, nouveauMDP);
            }
            return drub.getReponseUpdateBase(28);
        }
    }
}
