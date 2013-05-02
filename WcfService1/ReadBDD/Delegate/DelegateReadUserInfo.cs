using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using User_Lib;
using WcfService1.Outil;
using WcfService1.ReadBDD.DAO;

namespace WcfService1.ReadBDD.Delegate
{
    public class DelegateReadUserInfo
    {
        ReadUserService daoReadUserService;
        public DelegateReadUserInfo()
        {
            daoReadUserService = new ReadUserService();
        }

        internal ReponseConnectionUser identificationUser(string identifiant, string mdp)
        {
            return daoReadUserService.identificationUser(identifiant, mdp);
        }
    }
}