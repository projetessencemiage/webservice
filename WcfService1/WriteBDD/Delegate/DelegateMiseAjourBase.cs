using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using WcfService1.WriteBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateMiseAjourBase
    {
        WriteMiseAjourBaseXML daoWriteMiseAjourBaseXML;

        public DelegateMiseAjourBase()
        {
            daoWriteMiseAjourBaseXML = new WriteMiseAjourBaseXML();
        }

        public bool delegateMiseAjourBaseXml(string url)
        {
            XmlNodeList nodeList  = recuperationNoeudStation(url);
            if (nodeList != null)
            {
                List<Station> listStation = constructionStation(nodeList);
                return daoWriteMiseAjourBaseXML.writeDonneesXML(listStation);
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
                    if(s_long.Length > 2 && s_long !=null){
                        longitude = constructionLongitude(s_long);
                    }
                    if (s_lat.Length > 2 && s_lat != null)
                    {
                        lattitude = constructionLattitude(s_lat);
                    }
                    string id_enseigne = null;
                    string enseigne_marque = "";
                    XmlNodeList listNodePrix = nodeList[i].SelectNodes("prix");
                    List<Prix> list_prix = new List<Prix>();
                    foreach (XmlNode nodePrix in listNodePrix)
                    {
                        list_prix.Add(new Prix(null, null, nodePrix.Attributes["nom"].Value, Single.Parse(nodePrix.Attributes["valeur"].Value.Replace(".", ",")), nodePrix.Attributes["maj"].Value));
                    }
                    listStation.Add(new Station(id_station, list_prix, address, city, code_postal, longitude, lattitude, id_enseigne, enseigne_marque, tel, null));
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listStation;
        }

        private float constructionLattitude(string s_lat)
        {
            s_lat = s_lat.Replace(".", "");
            s_lat = s_lat.Replace(",", "");
            string temp = s_lat.Substring(0, 2) + "," + s_lat.Substring(1, s_lat.Length - 2);
            return Single.Parse(temp);
        }

        private float constructionLongitude(string s_long)
        {
            s_long = s_long.Replace(".", "");
            s_long = s_long.Replace(",", "");
            string temp = s_long.Substring(0,1) + "," + s_long.Substring(1,s_long.Length-1);
            return Single.Parse(temp);
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