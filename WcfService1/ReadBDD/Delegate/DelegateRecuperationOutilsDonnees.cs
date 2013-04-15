using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateRecuperationOutilsDonnees
    {
        private ReadOutilsDonnees daoReadOutilsDonnees;

        public DelegateRecuperationOutilsDonnees()
        {
            daoReadOutilsDonnees = new ReadOutilsDonnees();
        }

        public SortedList<int, string> getIdAndTypeEssence()
        {
            RecuperationOutilsDonnees.logger.ecrireInfoLogger("Accès à daoReadOutilsDonnees.getIdAndTypeEssence()");
            return daoReadOutilsDonnees.getIdAndTypeEssence();
        }
    }
}