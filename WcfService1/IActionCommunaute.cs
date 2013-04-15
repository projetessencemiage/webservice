﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.Outil;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IActionCommunaute" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IActionCommunaute
    {
        [OperationContract]
        ReponseUpdateBase PushPrice(string id_station, int id_price, double price);
    }
}
