﻿using FuelTracker_Lib;
using Logger_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.Outil;
using WcfService1.ReadBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "AffichagePrix" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez AffichagePrix.svc ou AffichagePrix.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class AffichagePrix : IAffichagePrix
    {
        private DelegateAffichagePrix delegateAffichagePrix;
        public static Logger logger;

        public AffichagePrix()
        {
            delegateAffichagePrix = new DelegateAffichagePrix();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "AffichagePrix");
        }

        public List<Station> GetPrixCodePostal(string codePostal)
        {
            logger.ecrireInfoLogger("Accès à delegateAffichagePrix.getPrixCommune(string codePostal) avec codePostal = " + codePostal);
            return delegateAffichagePrix.getPrixCommune(codePostal);
        }

        public List<StationAndDistance> GetPrixPosition(int distance, float longitude, float latitude)
        {
            logger.ecrireInfoLogger("Accès à delegateAffichagePrix.getPrixPosition(int distance, float longitude, float latitude) avec distance = " + distance + " & longitude = " + longitude + " & latitude = " + latitude);
            return delegateAffichagePrix.getPrixPosition(distance, longitude, latitude);
        }

        public List<Station> GetPrixDepartement(string departement)
        {
            logger.ecrireInfoLogger("Accès à delegateAffichagePrix.getPrixDepartement(string departement) avec departement = " + departement);
            return delegateAffichagePrix.getPrixDepartement(departement);
        }

        public List<Station> GetPrixVille(string ville, string departement)
        {
            logger.ecrireInfoLogger("Accès à delegateAffichagePrix.getPrixVille(string ville, string codePostal) avec ville = " + ville + " & departement = " + departement);
            return delegateAffichagePrix.getPrixVille(ville, departement);
        }
    }
}
