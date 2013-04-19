using FuelTracker_Lib;
using Logger_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.Outil;
using WcfService1.WriteBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ActionCommunaute" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ActionCommunaute.svc ou ActionCommunaute.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ActionCommunaute : IActionCommunaute
    {
        private DelegateActionCommunaute delegateActionCommunaute;
        public static Logger logger;
        private DictionnaireReponseUpdateBase drub;

        public ActionCommunaute()
        {
            delegateActionCommunaute = new DelegateActionCommunaute();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "ActionCommunaute");
            drub = new DictionnaireReponseUpdateBase();
        }

        public ReponseUpdateBase PushPrice(string id_station, int id_price, double price)
        {
            logger.ecrireInfoLogger("Appel web service avec : id_station = " + id_station + " & id_price = " + id_price + " & price = " + price);
            if (id_station != null && !id_station.Equals("") && price != 0)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushPrice(string id_station, int id_price, double price) avec id_station = " + id_station + " & id_price = " + id_price + " & price = " + price);
                return delegateActionCommunaute.pushPrice(id_station, id_price, price);
            }
            return drub.getReponseUdateBase(1);
        }

        public ReponseUpdateBase PushStationWithAddress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list)
        {
            if (address != null && !address.Equals("") && code_postal != null && !code_postal.Equals("") && city != null && !city.Equals("") && id_enseigne != 0 && price_list != null)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushStationAdress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString());
                return delegateActionCommunaute.pushStationAdress(address, code_postal, city, tel, id_enseigne, price_list);
            }
            return drub.getReponseUdateBase(1);
        }

        public ReponseUpdateBase PushStationWithGPS(string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list)
        {
            if (latitude != 0 && longitude !=0 && price_list != null && id_enseigne != 0)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushPrice(string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list) avec tel = " + tel + " & latitude = " + latitude + " & longitude = " + longitude + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString());
                return delegateActionCommunaute.pushStationGPS(tel, latitude, longitude, id_enseigne, price_list);
            }
            return drub.getReponseUdateBase(1);
        }
    }
}
