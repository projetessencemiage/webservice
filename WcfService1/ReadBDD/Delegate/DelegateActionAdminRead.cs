using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using User_Lib;
using WcfService1.Outil;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateActionAdminRead
    {
        private ReadDonneeStation daoReadDonneeStation;
        private ReadUserService daoReadUserService;
        private ReadDonneePrix daoRecuperationPrixStation;
        private bool activationActionAdmin;

        public DelegateActionAdminRead()
        {
            daoRecuperationPrixStation = new ReadDonneePrix();
            daoReadDonneeStation = new ReadDonneeStation();
            daoReadUserService = new ReadUserService();
            try
            {
                activationActionAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionAdmin"]);
            }
            catch (FormatException e)
            {
                activationActionAdmin = false;
            }
        }

        internal List<Station> ListStationAValider()
        {
            ActionAdmin.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.ListStationAValider()", activationActionAdmin);
            return daoReadDonneeStation.ListStationAValider();
        }

        internal List<User> listUtilisateur()
        {
            ActionAdmin.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.listUtilisateur()", activationActionAdmin);
            return daoReadUserService.listUtilisateur();
        }

        internal Station GetStationByID(string id_station)
        {
            ActionAdmin.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.ListStationAValider("+id_station+")", activationActionAdmin);
            Station station = daoReadDonneeStation.getStationByID(id_station);
            if (station != null)
            {
                station.setPrice(daoRecuperationPrixStation.readPrixByStation(station.id_station));
            }
            return station;
        }
    }
}