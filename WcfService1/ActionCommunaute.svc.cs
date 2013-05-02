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
        private bool activationActionCommunaute;

        public ActionCommunaute()
        {
            delegateActionCommunaute = new DelegateActionCommunaute();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "ActionCommunaute");
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                activationActionCommunaute = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionCommunaute"]);
            }
            catch (FormatException e)
            {
                activationActionCommunaute = false;
            }
        }

        public ReponseUpdateBase PushPrice(string id_station, string id_price, string price)
        {
            int int_id_price = 0;
            double double_price = 0;
            logger.ecrireInfoLogger("Appel web service avec : id_station = " + id_station + " & id_price = " + id_price + " & price = " + price, activationActionCommunaute);
            try
            {
                int_id_price = Convert.ToInt32(id_price);
                double_price = Convert.ToDouble(price.Replace(".", ","));
            }
            catch (FormatException e)
            {
                return drub.getReponseUdateBase(8);
            }
            if (id_station != null && !id_station.Equals("") && double_price != 0)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushPrice(string id_station, int id_price, double price) avec id_station = " + id_station + " & id_price = " + id_price + " & price = " + price, activationActionCommunaute);
                return delegateActionCommunaute.pushPrice(id_station, int_id_price, double_price);
            }
            
            return drub.getReponseUdateBase(1);
        }

        public ReponseUpdateBase PushStationWithAddress(string address, string code_postal, string city, string tel, string id_enseigne, List<Prix> price_list)
        {
            int int_id_enseigne = 0;
            try
            {
                int_id_enseigne = Convert.ToInt32(id_enseigne);
            }
            catch (FormatException e)
            {
                return drub.getReponseUdateBase(8);
            }
            if (address != null && !address.Equals("") && code_postal != null && !code_postal.Equals("") && city != null && !city.Equals("") && int_id_enseigne != 0 && price_list != null)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushStationAdress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionCommunaute);
                return delegateActionCommunaute.pushStationAdress(address, code_postal, city, tel, int_id_enseigne, price_list);
            }
            return drub.getReponseUdateBase(1);
        }

        public ReponseUpdateBase PushStationWithGPS(string tel, string latitude, string longitude, string id_enseigne, List<Prix> price_list)
        {
            int int_id_enseigne = 0;
            double double_latitude = 0;
            double double_longitude = 0;
            try
            {
                int_id_enseigne = Convert.ToInt32(id_enseigne);
                double_latitude = Convert.ToDouble(latitude.Replace(".", ","));
                double_longitude = Convert.ToDouble(longitude.Replace(".", ","));
            }
            catch (FormatException e)
            {
                return drub.getReponseUdateBase(8);
            }
            if (double_latitude != 0 && double_longitude != 0 && price_list != null && int_id_enseigne != 0)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushPrice(string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list) avec tel = " + tel + " & latitude = " + latitude + " & longitude = " + longitude + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionCommunaute);
                return delegateActionCommunaute.pushStationGPS(tel, double_latitude, double_longitude, int_id_enseigne, price_list);
            }
            return drub.getReponseUdateBase(1);
        }
    }
}
