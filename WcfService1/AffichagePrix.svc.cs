using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ReadBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "AffichagePrix" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez AffichagePrix.svc ou AffichagePrix.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class AffichagePrix : IAffichagePrix
    {
        private DelegateAffichagePrix delegateAffichagePrix;

        public AffichagePrix()
        {
            delegateAffichagePrix = new DelegateAffichagePrix();
        }

        public List<Station> GetPrixCodePostal(int codePostal)
        {
            return delegateAffichagePrix.getPrixCommune(codePostal);
        }

        public List<string> GetPrixDepartement(int departement)
        {
            return new List<string>();
        }
    }
}
