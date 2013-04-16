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

        public List<Station> getPrixCommune(string codePostal)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationCodePostalSansPrix(string codePostal) avec codePostal = " + codePostal);
            List<Station> listStation = daoReadDonneeStation.recupererStationCodePostalSansPrix(codePostal);
            if(listStation != null)
            {
                foreach(Station uneStation in listStation)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStation.getIdStation());
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count);
            }
            return listStation;
        }

        public List<StationAndDistance> getPrixPosition(int distance, float longitude, float latitude)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationParRapportPosition((int distance, float longitude, float latitude) avec distance = " + distance + " & longitude = " + " & latitude = " + latitude);
            List<StationAndDistance> listStation = daoReadDonneeStation.recupererStationParRapportPosition(distance, longitude, latitude);
            if (listStation != null)
            {
                foreach (StationAndDistance uneStationAndDistance in listStation)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStationAndDistance.getIdStation());
                    uneStationAndDistance.setPrice(daoReadDonneePrix.readPrixByStation(uneStationAndDistance.getIdStation()));
                }
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count);
            }
            return listStation;
        }

        public List<Station> getPrixDepartement(string departement)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationDepartementSansPrix(string departement) avec departement = " + departement);
            List<Station> listStation = daoReadDonneeStation.recupererStationDepartementSansPrix(departement);
            if (listStation != null)
            {
                foreach (Station uneStation in listStation)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStation.getIdStation());
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count);
            }
            return listStation;
        }

        public List<Station> getPrixVille(string ville, string departement)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationVilleSansPrix(string ville, string departement) avec ville = " + ville + " & departement = " + departement);
            List<Station> listStation = daoReadDonneeStation.recupererStationVilleSansPrix(ville, departement);
            if (listStation != null)
            {
                foreach (Station uneStation in listStation)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneePrix.readPrixByStation(Station uneStation) avec uneStation = " + uneStation.getIdStation());
                    uneStation.setPrice(daoReadDonneePrix.readPrixByStation(uneStation.getIdStation()));
                }
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count);
            }
            return listStation;
        }
    }
}