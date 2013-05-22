using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using WcfService1.Outil;
using WcfService1.WriteBDD.DAO;

namespace WcfService1.WriteBDD.Delegate
{
    public class DelegateActionAdminWrite
    {
        private WriteDonneeStation daoWriteDonneeStation;
        private WriteActionCommunaute daoWriteActionCommunaute;
        private WriteUserService daoUserService;
        private bool activationActionAdmin;
        private DictionnaireReponseUpdateBase drub;

        public DelegateActionAdminWrite()
        {
            daoUserService = new WriteUserService();
            daoWriteDonneeStation = new WriteDonneeStation();
            daoWriteActionCommunaute = new WriteActionCommunaute();
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                activationActionAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionAdmin"]);
            }
            catch (FormatException e)
            {
                activationActionAdmin = false;
            }
        }

        internal ReponseUpdateBase ValiderStation(string id_station)
        {
            ActionAdmin.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.ValiderStation(" + id_station + ")", activationActionAdmin);
            return daoWriteDonneeStation.ValiderStation(id_station);
        }

        internal ReponseUpdateBase SupprimerStation(string id_station)
        {
            ActionAdmin.logger.ecrireInfoLogger("Accès à daoReadDonneeStation.SupprimerStation(" + id_station + ")", activationActionAdmin);
            return daoWriteDonneeStation.SupprimerStation(id_station);
        }

        public ReponseUpdateBase pushStationAdress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list, bool isAdmin)
        {
            ActionAdmin.logger.ecrireInfoLogger("Recuperation XML via l'adresse http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false", activationActionAdmin);
            XmlNodeList nodeList = OutilGeolocalisation.recupererAdresseGeo("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false");
            double latitude = 0;
            double longitude = 0;
            if (nodeList != null)
            {
                XmlNodeList geometry = nodeList[0].SelectNodes("geometry");
                XmlNodeList location = geometry[0].SelectNodes("location");
                latitude = Convert.ToDouble(location[0].SelectNodes("lat").Item(0).InnerText.ToString().Replace(".", ","));
                longitude = Convert.ToDouble(location[0].SelectNodes("lng").Item(0).InnerText.ToString().Replace(".", ","));
                ActionAdmin.logger.ecrireInfoLogger("Accès à daoWriteActionCommunaute.writePushStation(string address, string code_postal, string city, string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & latitude = " + latitude +
                    " & longitude = " + longitude + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionAdmin);
                return daoWriteActionCommunaute.writePushStation(address, code_postal, city, tel, latitude, longitude, id_enseigne, price_list, isAdmin);
            }
            return drub.getReponseUpdateBase(7);
        }

        internal ReponseUpdateBase modififierStation(string id_station, string address, string code_postal, string city, string tel, int int_id_enseigne)
        {
            ActionAdmin.logger.ecrireInfoLogger("Recuperation XML via l'adresse http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false", activationActionAdmin);
            XmlNodeList nodeList = OutilGeolocalisation.recupererAdresseGeo("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false");
            double latitude = 0;
            double longitude = 0;
            if (nodeList != null)
            {
                XmlNodeList geometry = nodeList[0].SelectNodes("geometry");
                XmlNodeList location = geometry[0].SelectNodes("location");
                latitude = Convert.ToDouble(location[0].SelectNodes("lat").Item(0).InnerText.ToString().Replace(".", ","));
                longitude = Convert.ToDouble(location[0].SelectNodes("lng").Item(0).InnerText.ToString().Replace(".", ","));
                return daoWriteDonneeStation.modififierStation(id_station, address, code_postal, city, tel, latitude, longitude, int_id_enseigne);
            }
            return drub.getReponseUpdateBase(23);
        }

        internal ReponseUpdateBase miseAJourProfilUser(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            return daoUserService.miseAJourProfilUserByAdmin(civilite, nom, prenom, pseudo, email, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_pref);
        }

        internal ReponseUpdateBase suppressionCompteByAdmin(string identifiant)
        {
            return daoUserService.suppressionCompteByAdmin(identifiant);
        }
    }
}