using Logger_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using User_Lib;
using WcfService1.Outil;
using WcfService1.ReadBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "UserService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez UserService.svc ou UserService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class UserService : IUserService
    {
        private DelegateEcritureUserService delegateEcritureUserService;
        private DelegateReadUserInfo delegateReadUserInfo;
        public static Logger logger;
        private bool activationUserService;
        private DictionnaireReponseUpdateBase drub;
        private int id_user_base;

        public UserService()
        {
            delegateEcritureUserService = new DelegateEcritureUserService();
            delegateReadUserInfo = new DelegateReadUserInfo();
            drub = new DictionnaireReponseUpdateBase();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "UserService");
            try
            {
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["activationUserService"]);
                id_user_base = Convert.ToInt32(ConfigurationManager.AppSettings["id_user_base"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
        }

        public ReponseUpdateBase InscriptionUser(string civilite, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref)
        {
            logger.ecrireInfoLogger("Accès aux services InscriptionUser("+ civilite + ", " + nom + ", " + prenom + ", " + pseudo + ", " + email + ", " + mdp + ", " + adresse + ", " + code_postal + ", " + ville + ", " + url_avatar + ", " + string_id_station_favorite + ", " + string_id_carburant_pref+ ")", activationUserService);
            int idRole = id_user_base;
            int id_station_favorite = -1;
            int id_carburant_pref = -1;
            try
            {
                if (string_id_station_favorite != null && !string_id_station_favorite.Equals(""))
                {
                    id_station_favorite = Convert.ToInt32(string_id_station_favorite);
                }
                if (string_id_carburant_pref != null && !string_id_carburant_pref.Equals(""))
                {
                    id_carburant_pref = Convert.ToInt32(string_id_carburant_pref);
                }
                if (!email.Contains("@") && !email.Contains("."))
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                return drub.getReponseUpdateBase(8);
            }
            if (idRole > -1 && !pseudo.Equals("") && !email.Equals("") && !mdp.Equals(""))
            {
                return delegateEcritureUserService.inscriptionUser(idRole, civilite, nom, prenom, pseudo, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
            }
            return drub.getReponseUpdateBase(8);
        }

        public ReponseUpdateBase ChangementMDP(string identifiant, string ancienMDP, string nouveauMDP)
        {
            logger.ecrireInfoLogger("Accès aux services ChangementMDP avec" + identifiant + " et " + ancienMDP + " et " + nouveauMDP, activationUserService);
            if (!identifiant.Equals("") && identifiant != null && !ancienMDP.Equals("") && ancienMDP != null && !nouveauMDP.Equals("") && nouveauMDP != null)
            {
                return delegateEcritureUserService.changementMDP(identifiant, ancienMDP, nouveauMDP);
            }
            return drub.getReponseUpdateBase(17);
        }

        public ReponseUpdateBase ReinitialisationMDP(string identifiant, string cle, string nouveauMDP)
        {
            logger.ecrireInfoLogger("Accès aux services ChangementMDP avec" + identifiant + " et " + cle + " et " + nouveauMDP, activationUserService);
            if (!identifiant.Equals("") && identifiant != null && !cle.Equals("") && cle != null && !nouveauMDP.Equals("") && nouveauMDP != null)
            {
                return delegateEcritureUserService.ReinitialisationMDP(identifiant, cle, nouveauMDP);
            }
            return drub.getReponseUpdateBase(28);
        }

        public ReponseUpdateBase MotDePasseOublie(string identifiant)
        {
            logger.ecrireInfoLogger("Accès aux services MotDePasseOublie avec" + identifiant, activationUserService);
            if (!identifiant.Equals("") && identifiant != null)
            {
                return delegateEcritureUserService.motDePasseOublie(identifiant);
            }
            return drub.getReponseUpdateBase(25);
        }

        public ReponseConnectionUser Identification(string identifiant, string mdp)
        {
            logger.ecrireInfoLogger("Accès aux services IdentificationUser avec" + identifiant + " et " + mdp, activationUserService);
            if (!identifiant.Equals("") && identifiant != null && !mdp.Equals("") && mdp!= null)
            {
                return delegateReadUserInfo.identificationUser(identifiant, mdp);
            }
            return new ReponseConnectionUser(1, null);
        }

        public ReponseUpdateBase Desinscription(string identifiant, string mdp)
        {
            logger.ecrireInfoLogger("Accès aux services Desincription avec" + identifiant + " et " + mdp, activationUserService);
            if (!identifiant.Equals("") && identifiant != null && !mdp.Equals("") && mdp != null)
            {
                return delegateEcritureUserService.desinscription(identifiant, mdp);
            }
            return drub.getReponseUpdateBase(17);
        }

        public ReponseUpdateBase MiseAJourProfilUser(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref)
        {
            int id_station_favorite = -1;
            int id_carburant_pref = -1;
            try
            {
                if (string_id_station_favorite != null && !string_id_station_favorite.Equals(""))
                {
                    id_station_favorite = Convert.ToInt32(string_id_station_favorite);
                }
                if (string_id_carburant_pref != null && !string_id_carburant_pref.Equals(""))
                {
                    id_carburant_pref = Convert.ToInt32(string_id_carburant_pref);
                }
                if (!email.Equals("") && !email.Contains("@") && !email.Contains("."))
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                return drub.getReponseUpdateBase(8);
            }
            logger.ecrireInfoLogger("Accès aux services miseAJourProfilUser(" + civilite + ", " + nom + ", " + prenom + ", " + pseudo + ", " + email + ", " + adresse + ", " + code_postal + ", " + ville + ", " + url_avatar + ", " + id_station_favorite + ", " + id_carburant_pref + ")", activationUserService);
            return delegateEcritureUserService.miseAJourProfilUser(civilite, nom, prenom, pseudo, email, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }
    }
}
