using FuelTracker_Lib;
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
using WcfService1.WriteBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ActionAdmin" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ActionAdmin.svc ou ActionAdmin.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ActionAdmin : IActionAdmin
    {
        private DelegateActionAdminRead delegateActionAdminRead;
        private DelegateActionAdminWrite delegateActionAdminWrite;
        public static Logger logger;
        private DictionnaireReponseUpdateBase drub;
        private bool activationActionAdmin;

        public ActionAdmin()
        {
            delegateActionAdminWrite = new DelegateActionAdminWrite();
            delegateActionAdminRead = new DelegateActionAdminRead();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "ActionAdmin");
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                activationActionAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionAdmin"]);
            }
            catch (FormatException e)
            {
                activationActionAdmin = false;
            }
        }

        public List<Station> ListStationAValider()
        {
            logger.ecrireInfoLogger("Appel web service : ActionAdmin, fonction : ListStationAValider()", activationActionAdmin);
            return delegateActionAdminRead.ListStationAValider();
        }

        public Station GetStationByID(string id_station)
        {
            logger.ecrireInfoLogger("Appel web service : ActionAdmin, fonction : GetStationByID(" + id_station + ")", activationActionAdmin);
            if (!id_station.Equals("") && id_station != null)
            {
                return delegateActionAdminRead.GetStationByID(id_station);
            }
            return null;
        }

        public ReponseUpdateBase ValiderStation(string id_station)
        {
            logger.ecrireInfoLogger("Appel web service : ActionAdmin, fonction : ValiderStation() avec l'id_station : " + id_station, activationActionAdmin);
            if (!id_station.Equals("") && id_station != null)
            {
                ActionAdmin.logger.ecrireInfoLogger("Accès à delegateActionAdmin.ValiderStation(id_station)", activationActionAdmin);
                return delegateActionAdminWrite.ValiderStation(id_station);
            }
            return drub.getReponseUpdateBase(8);
        }

        public ReponseUpdateBase SupprimerStation(string id_station)
        {
            logger.ecrireInfoLogger("Appel web service : ActionAdmin, fonction : SupprimerStation() avec l'id_station : " + id_station, activationActionAdmin);
            if (!id_station.Equals("") && id_station != null)
            {
                ActionAdmin.logger.ecrireInfoLogger("Accès à delegateActionAdmin.SupprimerStation(id_station)", activationActionAdmin);
                return delegateActionAdminWrite.SupprimerStation(id_station);
            }
            return drub.getReponseUpdateBase(8);
        }

        public ReponseUpdateBase ModififierStation(string id_station, string address, string code_postal, string city, string tel, string id_enseigne)
        {
            int int_id_enseigne = -1;
            try
            {
                int_id_enseigne = Convert.ToInt32(id_enseigne);
            }
            catch (FormatException e)
            {
                return drub.getReponseUpdateBase(8);
            }
            if (address != null && !address.Equals("") && code_postal != null && !code_postal.Equals("") && city != null && !city.Equals("") && int_id_enseigne != -1)
            {
                logger.ecrireInfoLogger("Accès à delegateActionAdmin.modififierStation(" + id_station + ", " + address + ", " + code_postal + ", " + city + ", " + tel + ", " + int_id_enseigne + ")", activationActionAdmin);
                return delegateActionAdminWrite.modififierStation(id_station, address, code_postal, city, tel, int_id_enseigne);
            }
            return drub.getReponseUpdateBase(1);
        }

        public ReponseUpdateBase AjouterStationAdmin(string address, string code_postal, string city, string tel, string id_enseigne, List<Prix> price_list)
        {
            int int_id_enseigne = 0;
            try
            {
                int_id_enseigne = Convert.ToInt32(id_enseigne);
            }
            catch (FormatException e)
            {
                return drub.getReponseUpdateBase(8);
            }
            if (address != null && !address.Equals("") && code_postal != null && !code_postal.Equals("") && city != null && !city.Equals("") && int_id_enseigne != 0 && price_list != null)
            {
                logger.ecrireInfoLogger("Accès à delegateActionAdmin.pushStationAdress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionAdmin);
                return delegateActionAdminWrite.pushStationAdress(address, code_postal, city, tel, int_id_enseigne, price_list, true);
            }
            return drub.getReponseUpdateBase(1);
        }

        public ReponseUpdateBase MiseAJourProfilUserByAdmin(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref)
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
                if (!email.Contains("@") && !email.Contains("."))
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                return drub.getReponseUpdateBase(8);
            }
            logger.ecrireInfoLogger("Accès aux services MiseAJourProfilUserByAdmin(" + civilite + ", " + nom + ", " + prenom + ", " + pseudo + ", " + email + ", " + adresse + ", " + code_postal + ", " + ville + ", " + url_avatar + ", " + id_station_favorite + ", " + id_carburant_pref + ")", activationActionAdmin);
            return delegateActionAdminWrite.miseAJourProfilUser(civilite, nom, prenom, pseudo, email, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }

        public ReponseUpdateBase SuppressionCompteByAdmin(string identifiant)
        {
            logger.ecrireInfoLogger("Accès aux services SuppressionCompteByAdmin avec" + identifiant, activationActionAdmin);
            if (!identifiant.Equals("") && identifiant != null)
            {
                return delegateActionAdminWrite.suppressionCompteByAdmin(identifiant);
            }
            return drub.getReponseUpdateBase(1);
        }

        public List<User> ListUtilisateur()
        {
            logger.ecrireInfoLogger("Accès aux services ListUtilisateur()", activationActionAdmin);
            return delegateActionAdminRead.listUtilisateur();
        }
    }
}
