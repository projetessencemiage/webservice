using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateRecuperationOutilsDonnees
    {
        private ReadOutilsDonnees daoReadOutilsDonnees;
        private bool activationRecuperationOutils;

        public DelegateRecuperationOutilsDonnees()
        {
            daoReadOutilsDonnees = new ReadOutilsDonnees();
            try
            {
                activationRecuperationOutils = Convert.ToBoolean(ConfigurationManager.AppSettings["activationRecuperationOutils"]);
            }
            catch (FormatException e)
            {
                activationRecuperationOutils = false;
            }
        }

        public SortedList<int, string> getIdAndTypeEssence()
        {
            RecuperationOutilsDonnees.logger.ecrireInfoLogger("Accès à daoReadOutilsDonnees.getIdAndTypeEssence()", activationRecuperationOutils);
            return daoReadOutilsDonnees.getIdAndTypeEssence();
        }

        public SortedList<int, string> getIdAndNomEnseigne()
        {
            RecuperationOutilsDonnees.logger.ecrireInfoLogger("Accès à daoReadOutilsDonnees.getIdAndNomEnseigne()", activationRecuperationOutils);
            return daoReadOutilsDonnees.getIdAndNomEnseigne();
        }
    }
}