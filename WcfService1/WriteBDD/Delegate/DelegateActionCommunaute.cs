using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.Outil;
using WcfService1.WriteBDD.DAO;

namespace WcfService1.WriteBDD.Delegate
{
    public class DelegateActionCommunaute
    {
        private WriteActionCommunaute daoWriteActionCommunaute;

        public DelegateActionCommunaute()
        {
            daoWriteActionCommunaute = new WriteActionCommunaute();
        }


        public ReponseUpdateBase pushPrice(string id_station, int id_price, double price)
        {
            ActionCommunaute.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.writePushPrice(string id_station, int id_price, double price) avec id_station = " + id_station + " & id_price = " + id_price + " & price = " + price);
            return daoWriteActionCommunaute.writePushPrice(id_station, id_price, price);
        }
    }
}