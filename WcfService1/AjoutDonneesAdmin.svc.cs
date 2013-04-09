using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ReadBDD.Delegate;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "AjoutDonneesAdmin" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez AjoutDonneesAdmin.svc ou AjoutDonneesAdmin.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class AjoutDonneesAdmin : IAjoutDonneesAdmin
    {
        private DelegateMiseAjourBase delegateMiseAjourBase;

        public AjoutDonneesAdmin()
        {
            delegateMiseAjourBase = new DelegateMiseAjourBase();
        }

        public bool MiseAjourBaseXml(string url)
        {
            return delegateMiseAjourBase.delegateMiseAjourBaseXml(url);
        }
    }
}
