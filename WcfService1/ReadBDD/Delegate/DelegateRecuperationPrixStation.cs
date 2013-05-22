using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WcfService1.Outil;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateRecuperationPrixStation
    {
        private ReadDonneePrix daoReadDonneePrix;
        private bool activationReadPrix;

        public DelegateRecuperationPrixStation()
        {
            daoReadDonneePrix = new ReadDonneePrix();
            try
            {
                activationReadPrix = Convert.ToBoolean(ConfigurationManager.AppSettings["activationReadPrix"]);
            }
            catch (FormatException e)
            {
                activationReadPrix = false;
            }
        }

        public List<StationAndDistance> recupererPrixStationAndDistance(List<StationAndDistance> list_station)
        {
            foreach (StationAndDistance uneStationAndDistance in list_station)
            {
                AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStationAndDistance.getIdStation(), activationReadPrix);
                uneStationAndDistance.setPrice(daoReadDonneePrix.readPrixByStation(uneStationAndDistance.getIdStation()));
            }
            return list_station;
        }

        public List<Station> recupererPrixStation(List<Station> list_station)
        {
            foreach (Station uneStation in list_station)
            {
                AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStation.getIdStation(), activationReadPrix);
                uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
            }
            return list_station;
        }
    }
}