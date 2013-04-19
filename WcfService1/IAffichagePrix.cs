using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.Outil;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IAffichagePrix" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IAffichagePrix
    {
        [OperationContract]
        List<Station> GetPrixCodePostal(string codePostal);

        [OperationContract]
        List<Station> GetPrixDepartement(string departement);

        [OperationContract]
        List<Station> GetPrixVille(string ville, string departement);

        [OperationContract]
        List<StationAndDistance> GetPrixPosition(string distance, string longitude, string latitude);
    }
}
