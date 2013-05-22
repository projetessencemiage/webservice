using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using WcfService1.Outil;
using WcfService1.WriteBDD.DAO;

namespace WcfService1.WriteBDD.Delegate
{
    public class DelegateActionCommunaute
    {
        private WriteActionCommunaute daoWriteActionCommunaute;
        private DictionnaireReponseUpdateBase drub;
        private bool activationActionCommunaute;

        public DelegateActionCommunaute()
        {
            daoWriteActionCommunaute = new WriteActionCommunaute();
            drub = new DictionnaireReponseUpdateBase();
            try
            {
                activationActionCommunaute = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionCommunaute"]);
            }
            catch (FormatException e)
            {
                activationActionCommunaute = false;
            }
        }

        public ReponseUpdateBase pushPrice(string id_station, int id_price, double price)
        {
            ActionCommunaute.logger.ecrireInfoLogger("Accès à daoWriteActionCommunaute.writePushPrice(string id_station, int id_price, double price) avec id_station = " + id_station + " & id_price = " + id_price + " & price = " + price, activationActionCommunaute);
            return daoWriteActionCommunaute.writePushPrice(id_station, id_price, price);
        }

        public ReponseUpdateBase pushStationGPS(string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list, bool isAdmin)
        {
            ActionCommunaute.logger.ecrireInfoLogger("Recuperation XML via l'adresse http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false", activationActionCommunaute);
            XmlNodeList nodeList = OutilGeolocalisation.recupererAdresseGeo("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude.ToString().Replace(",", ".") + "," + longitude.ToString().Replace(",", ".") + "&sensor=false");
            string address = "";
            string city = "";
            string code_postal = "";
            if (nodeList != null && nodeList.Count > 0)
            {
                XmlNodeList address_component = nodeList[0].SelectNodes("address_component");
                address = address_component[0].SelectNodes("long_name").Item(0).InnerText
                    + " " + address_component[1].SelectNodes("long_name").Item(0).InnerText;
                city = address_component[2].SelectNodes("long_name").Item(0).InnerText;
                code_postal = address_component[address_component.Count-1].SelectNodes("long_name").Item(0).InnerText;

                ActionCommunaute.logger.ecrireInfoLogger("Accès à daoWriteActionCommunaute.writePushStation(string address, string code_postal, string city, string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & latitude = " + latitude +
                    " & longitude = " + longitude + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionCommunaute);
                return daoWriteActionCommunaute.writePushStation(address, code_postal, city, tel, latitude, longitude, id_enseigne, price_list, isAdmin);
            }
            return drub.getReponseUpdateBase(6);
        }


        public ReponseUpdateBase pushStationAdress(string address, string code_postal, string city, string tel, int id_enseigne, List<Prix> price_list, bool isAdmin)
        {
            ActionCommunaute.logger.ecrireInfoLogger("Recuperation XML via l'adresse http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false", activationActionCommunaute);
            XmlNodeList nodeList = OutilGeolocalisation.recupererAdresseGeo("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address.Replace(" ", "+") + "," + code_postal + "," + city.Replace(" ", "+") + "&sensor=false");
            double latitude = 0;
            double longitude = 0;
            if (nodeList != null)
            {
                XmlNodeList geometry = nodeList[0].SelectNodes("geometry");
                XmlNodeList location = geometry[0].SelectNodes("location");
                latitude = Convert.ToDouble(location[0].SelectNodes("lat").Item(0).InnerText.ToString().Replace(".", ","));
                longitude = Convert.ToDouble(location[0].SelectNodes("lng").Item(0).InnerText.ToString().Replace(".", ","));
                ActionCommunaute.logger.ecrireInfoLogger("Accès à daoWriteActionCommunaute.writePushStation(string address, string code_postal, string city, string tel, double latitude, double longitude, int id_enseigne, List<Prix> price_list) avec address = " + address + " & code_postal = " + code_postal + " & city = " + city + " & tel = " + tel + " & latitude = " + latitude +
                    " & longitude = " + longitude + " & id_enseigne = " + id_enseigne + " & price_list = " + price_list.ToString(), activationActionCommunaute);
                return daoWriteActionCommunaute.writePushStation(address, code_postal, city, tel, latitude, longitude, id_enseigne, price_list, isAdmin);
            }
            return drub.getReponseUpdateBase(7);
        }
    }
}