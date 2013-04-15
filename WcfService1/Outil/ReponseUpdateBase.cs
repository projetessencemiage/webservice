using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.Outil
{
    [DataContract]
    public class ReponseUpdateBase
    {
        [DataMember]
        public string message;
        [DataMember]
        public bool reponse;

        public ReponseUpdateBase(string message, bool reponse)
        {
            this.message = message;
            this.reponse = reponse;
        }
    }
}