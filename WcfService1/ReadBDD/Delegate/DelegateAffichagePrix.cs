using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.Outil;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateAffichagePrix
    {
        private ReadDonneePrix daoReadDonneePrix;
        private ReadDonneeStation daoReadDonneeStation;

        public DelegateAffichagePrix()
        {
            daoReadDonneePrix = new ReadDonneePrix();
            daoReadDonneeStation = new ReadDonneeStation();
        }

        public List<Station> getPrixCommune(int codePostal)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à delegateAffichagePrix.recupererStationCodePostalSansPrix(int codePostal) avec codePostal = " + codePostal);
            List<Station> listStation = daoReadDonneeStation.recupererStationCodePostalSansPrix(codePostal);
            if(listStation != null)
            {
                foreach(Station uneStation in listStation)
                {
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
            }
            return listStation;
        }

        public List<StationAndDistance> getPrixPosition(int distance, float longitude, float latitude)
        {
            List<StationAndDistance> listStation = daoReadDonneeStation.recupererStationParRapportPosition(distance, longitude, latitude);
            if (listStation != null)
            {
                foreach (StationAndDistance uneStationAndDistance in listStation)
                {
                    uneStationAndDistance.setPrice(daoReadDonneePrix.readPrixByStation(uneStationAndDistance.getIdStation()));
                }
            }
            return listStation;
        }

        public List<Station> getPrixDepartement(int departement)
        {
            List<Station> listStation = daoReadDonneeStation.recupererStationDepartementSansPrix(departement);
            if (listStation != null)
            {
                foreach (Station uneStation in listStation)
                {
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
            }
            return listStation;
        }

        public List<Station> getPrixVille(string ville)
        {
            List<Station> listStation = daoReadDonneeStation.recupererStationVilleSansPrix(ville);
            if (listStation != null)
            {
                foreach (Station uneStation in listStation)
                {
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
            }
            return listStation;
        }
    }
}