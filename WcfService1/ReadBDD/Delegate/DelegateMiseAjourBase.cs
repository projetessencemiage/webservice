using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateMiseAjourBase
    {
        public bool delegateMiseAjourBaseXml(string url)
        {
            XmlNodeList nodeList  = recuperationNoeudStation(url);
            if (nodeList != null)
            {
                List<Station> listStation = constructionStation(nodeList);
                // Faire APPEL DAO
            }
            return false;
        }

        private List<Station> constructionStation(XmlNodeList nodeList)
        {
            List<Station> listStation = new List<Station>();
            try
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    string id_station = null;
                    string address = nodeList[i].SelectNodes("adresse").Item(0).InnerText; 
                    string city = nodeList[i].SelectNodes("ville").Item(0).InnerText;
                    string tel = "";
                    string code_postal = nodeList[i].Attributes["cp"].Value;
                    string s_long = nodeList[i].Attributes["longitude"].Value;
                    string s_lat = nodeList[i].Attributes["latitude"].Value;
                    float longitude = 0;
                    float lattitude = 0;
                    if(s_long != "" && s_long !=null){
                        longitude = Single.Parse(s_long.Replace(".", ","));
                    }
                    if(s_lat != "" && s_lat !=null){
                        lattitude = Single.Parse(s_lat.Replace(".", ","));
                    }
                    string id_enseigne = null;
                    string enseigne_marque = "";
                    XmlNodeList listNodePrix = nodeList[i].SelectNodes("prix");
                    List<Prix> list_prix = new List<Prix>();
                    foreach (XmlNode nodePrix in listNodePrix)
                    {
                        list_prix.Add(new Prix(null, null, nodePrix.Attributes["nom"].Value, Single.Parse(nodePrix.Attributes["valeur"].Value.Replace(".", ",")), nodePrix.Attributes["maj"].Value));
                    }
                    listStation.Add(new Station(id_station, list_prix, address, city, code_postal, longitude, lattitude, id_enseigne, enseigne_marque, tel));
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listStation;
        }

        private XmlNodeList recuperationNoeudStation(string url)
        {
            XmlDocument document = new XmlDocument();
            XmlNodeList nodeList;
            try
            {
                document.Load(url);
                XmlNode node = document.DocumentElement;
                nodeList = node.SelectNodes("pdv");
            }
            catch (Exception)
            {
                return null;
            }
            return nodeList;
        }
    }
}