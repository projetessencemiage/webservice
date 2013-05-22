using FuelTracker_Lib;
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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IActionAdmin" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IActionAdmin
    {
        [OperationContract]
        List<Station> ListStationAValider();

        [OperationContract]
        ReponseUpdateBase ValiderStation(string id_station);

        [OperationContract]
        ReponseUpdateBase SupprimerStation(string id_station);

        [OperationContract]
        ReponseUpdateBase AjouterStationAdmin(string address, string code_postal, string city, string tel, string id_enseigne, List<Prix> price_list);

        [OperationContract]
        ReponseUpdateBase ModififierStation(string id_station, string address, string code_postal, string city, string tel, string id_enseigne);

        [OperationContract]
        ReponseUpdateBase MiseAJourProfilUserByAdmin(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, string string_id_station_favorite, string string_id_carburant_pref);

        [OperationContract]
        ReponseUpdateBase SuppressionCompteByAdmin(string identifiant);

        [OperationContract]
        List<User> ListUtilisateur();

        [OperationContract]
        Station GetStationByID(string id_station);
    }
}
