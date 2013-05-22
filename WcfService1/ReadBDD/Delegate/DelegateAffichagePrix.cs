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
    public class DelegateAffichagePrix
    {
        
        private ReadDonneeStation daoReadDonneeStation;
        private bool activationReadStation;
        private DelegateRecuperationPrixStation delegateRecuperationPrixStation;

        public DelegateAffichagePrix()
        {
            daoReadDonneeStation = new ReadDonneeStation();
            try
            {
                activationReadStation = Convert.ToBoolean(ConfigurationManager.AppSettings["activationReadStation"]);
            }
            catch (FormatException e)
            {
                activationReadStation = false;
            }
            delegateRecuperationPrixStation = new DelegateRecuperationPrixStation();
        }

        public List<Station> getPrixCommune(string codePostal)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationCodePostalSansPrix(string codePostal) avec codePostal = " + codePostal, activationReadStation);
            List<Station> listStation = daoReadDonneeStation.recupererStationCodePostalSansPrix(codePostal);
            if(listStation != null)
            {
                listStation = delegateRecuperationPrixStation.recupererPrixStation(listStation);
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count, activationReadStation);
            }
            return listStation;
        }

        public List<StationAndDistance> getPrixPosition(int distance, float longitude, float latitude)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationParRapportPosition((int distance, float longitude, float latitude) avec distance = " + distance + " & longitude = " + " & latitude = " + latitude, activationReadStation);
            List<StationAndDistance> listStation = daoReadDonneeStation.recupererStationParRapportPosition(distance, longitude, latitude);
            if (listStation != null)
            {
                listStation = delegateRecuperationPrixStation.recupererPrixStationAndDistance(listStation);
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count, activationReadStation);
            }
            return listStation;
        }

        public List<Station> getPrixDepartement(string departement)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationDepartementSansPrix(string departement) avec departement = " + departement, activationReadStation);
            List<Station> listStation = daoReadDonneeStation.recupererStationDepartementSansPrix(departement);
            if (listStation != null)
            {
                listStation = delegateRecuperationPrixStation.recupererPrixStation(listStation);
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count, activationReadStation);
            }
            return listStation;
        }

        public List<Station> getPrixVille(string ville, string departement)
        {
            AffichagePrix.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.recupererStationVilleSansPrix(string ville, string departement) avec ville = " + ville + " & departement = " + departement, activationReadStation);
            List<Station> listStation = daoReadDonneeStation.recupererStationVilleSansPrix(ville, departement);
            if (listStation != null)
            {
                listStation = delegateRecuperationPrixStation.recupererPrixStation(listStation);
                AffichagePrix.logger.ecrireInfoLogger("Le nombre de station récupéré est de : " + listStation.Count, activationReadStation);
            }
            return listStation;
        }
    }
}