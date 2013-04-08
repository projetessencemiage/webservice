using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateAffichagePrix
    {
        private ReadDonneePrix daoReadDonneePrix;

        public DelegateAffichagePrix()
        {
            daoReadDonneePrix = new ReadDonneePrix();
        }

        public List<string> getPrixCommune(int codePostal)
        {
            return daoReadDonneePrix.readPrixCommune(codePostal);
        }
    }
}