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
            reponseUpdateBase.Add(3, new ReponseUpdateBase("Station non crée", false));
            reponseUpdateBase.Add(4, new ReponseUpdateBase("Station crée, mais l'insertion de certains prix ont échoué. L'ensemble des opérations ont été annulées.", false));
            reponseUpdateBase.Add(5, new ReponseUpdateBase("Station crée, mais l'insertion de certains prix ont échoué. L'ensemble des opérations n'ont pas pu être annulées.", false));
            reponseUpdateBase.Add(6, new ReponseUpdateBase("Impossible de convertir les données GPS en adresse", false));
            reponseUpdateBase.Add(7, new ReponseUpdateBase("Impossible de convertir l'adresse en coordonnée GPS", false));
        }

        public ReponseUpdateBase getReponseUdateBase(int id)
        {
            ReponseUpdateBase rep;
            reponseUpdateBase.TryGetValue(id, out rep);
            return rep;
        }
    }
}