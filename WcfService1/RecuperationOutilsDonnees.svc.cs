using Logger_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ReadBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "RecuperationOutilsDonnees" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez RecuperationOutilsDonnees.svc ou RecuperationOutilsDonnees.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class RecuperationOutilsDonnees : IRecuperationOutilsDonnees
    {
        private DelegateRecuperationOutilsDonnees delegateRecuperationOutilsDonnees;
        public static Logger logger;

        public RecuperationOutilsDonnees()
        {
            delegateRecuperationOutilsDonnees = new DelegateRecuperationOutilsDonnees();
            logger = new Logger(ConfigurationManager.AppSettings["url_logger"], "RecuperationOutilsDonnees");
        }

        public SortedList<int, string> getIdAndTypeEssence()
        {
            logger.ecrireInfoLogger("Accès à delegateRecuperationOutilsDonnees.getIdAndTypeEssence()");
            return delegateRecuperationOutilsDonnees.getIdAndTypeEssence();
        }

        public SortedList<int, string> getIdAndNomEnseigne()
        {
            logger.ecrireInfoLogger("Accès à delegateRecuperationOutilsDonnees.getIdAndNomEnseigne()");
            return delegateRecuperationOutilsDonnees.getIdAndNomEnseigne();
        }
    }
}
