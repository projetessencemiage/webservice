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

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "UserService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez UserService.svc ou UserService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class UserService : IUserService
    {
        private DelegateUserService delegateUserService;
        public static Logger logger;
        private bool activationUserService;
        private DictionnaireReponseUpdateBase drub;
        private int id_user_base;

        public UserService()
        {
            delegateUserService = new DelegateUserService();
            drub = new DictionnaireReponseUpdateBase();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "InscriptionUser");
            try
            {
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionUser"]);
                id_user_base = Convert.ToInt32(ConfigurationManager.AppSettings["id_user_base"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
        }

        public ReponseUpdateBase InscriptionUser(string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref)
        {
            logger.ecrireInfoLogger("Accès aux services InscriptionUser.", activationUserService);
            int idRole = id_user_base;
            int id_station_favorite = 1;
            int id_carburant_pref;
            try
            {
                id_station_favorite = Convert.ToInt32(string_id_station_favorite);
                id_carburant_pref = Convert.ToInt32(string_id_carburant_pref);
                if (!email.Contains("@") && !email.Contains("."))
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                return drub.getReponseUdateBase(8);
            }
            if (idRole > -1 && !pseudo.Equals("") && !email.Equals("") && !mdp.Equals(""))
            {
                return delegateUserService.inscriptionUser(idRole, nom, prenom, pseudo, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
            }
            return drub.getReponseUdateBase(8);
        }

        public User Identification(string identifiant, string mdp)
        {
            logger.ecrireInfoLogger("Accès aux services IdentificationUser.", activationUserService);
            if (!identifiant.Equals("") && !mdp.Equals(""))
            {
                return delegateUserService.identificationUser(identifiant, mdp);
            }
            return null;
        }
    }
}
