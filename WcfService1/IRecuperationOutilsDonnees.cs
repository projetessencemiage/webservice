using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IRecuperationOutilsDonnees" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRecuperationOutilsDonnees
    {
        [OperationContract]
        SortedList<int, string> getIdAndTypeEssence();

        [OperationContract]
        SortedList<int, string> getIdAndNomEnseigne();
    }
}
