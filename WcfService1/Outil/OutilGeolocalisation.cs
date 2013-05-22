using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace WcfService1.Outil
{
    public class OutilGeolocalisation
    {
        public static XmlNodeList recupererAdresseGeo(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(new StreamReader(stream));
                        XmlNode node = document.DocumentElement;
                        return node.SelectNodes("result");
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}