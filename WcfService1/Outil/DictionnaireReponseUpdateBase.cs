using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.Outil
{
    [DataContract]
    public class DictionnaireReponseUpdateBase
    {
        [DataMember]
        public Dictionary<int, ReponseUpdateBase> reponseUpdateBase;

        public DictionnaireReponseUpdateBase()
        {
            reponseUpdateBase = new Dictionary<int, ReponseUpdateBase>();
            reponseUpdateBase.Add(0, new ReponseUpdateBase("Mise à jour effectuée", true));
            reponseUpdateBase.Add(1, new ReponseUpdateBase("Fonctionnalité non disponible", false));
            reponseUpdateBase.Add(2, new ReponseUpdateBase("Aucune mise à jour n'a été effectué", true));
        }

        public ReponseUpdateBase getReponseUdateBase(int id)
        {
            ReponseUpdateBase rep;
            reponseUpdateBase.TryGetValue(id, out rep);
            return rep;
        }
    }
}