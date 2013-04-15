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

        public ActionCommunaute()
        {
            delegateActionCommunaute = new DelegateActionCommunaute();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "ActionCommunaute");
        }

        public ReponseUpdateBase PushPrice(string id_station, int id_price, double price)
        {
            if (id_station != null && !id_station.Equals("") && id_price != null && price != null && price != 0)
            {
                logger.ecrireInfoLogger("Accès à delegateActionCommunaute.pushPrice(string id_station, int id_price, double price) avec id_station = " + id_station + " & id_price = " + id_price + " & price = " + price);
                return delegateActionCommunaute.pushPrice(id_station, id_price, price);
            }
            DictionnaireReponseUpdateBase drub = new DictionnaireReponseUpdateBase();
            return drub.getReponseUdateBase(1);
        }
    }
}
