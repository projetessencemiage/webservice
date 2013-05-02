using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using User_Lib;

namespace WcfService1.Outil
{
    [DataContract]
    public class ReponseConnectionUser
    {
        [DataMember]
        private int code;
        [DataMember]
        private User user;

        public ReponseConnectionUser(int code, User user)
        {
            this.code = code;
            this.user = user;
        }
    }
}