using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.ReadBDD.DAO
{
    public class ReadDonneePrix
    {
        public ReadDonneePrix()
        {
        }

        public List<string> readPrixCommune(int codePostal)
        {
            List<string> list = new List<string>();
            list.Add(codePostal.ToString());
            return list;
        }

    }
}