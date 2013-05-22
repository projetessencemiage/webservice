using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using User_Lib;
using WcfService1.Outil;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IUserService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        ReponseUpdateBase InscriptionUser(string civilite, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref);

        [OperationContract]
        ReponseConnectionUser Identification(string identifiant, string mdp);

        [OperationContract]
        ReponseUpdateBase ChangementMDP(string identifiant, string ancienMDP, string nouveauMDP);

        [OperationContract]
        ReponseUpdateBase ReinitialisationMDP(string identifiant, string cle, string nouveauMDP);

        [OperationContract]
        ReponseUpdateBase MotDePasseOublie(string identifiant);

        [OperationContract]
        ReponseUpdateBase Desinscription(string identifiant, string mdp);

        [OperationContract]
        ReponseUpdateBase MiseAJourProfilUser(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref);
    }
}
