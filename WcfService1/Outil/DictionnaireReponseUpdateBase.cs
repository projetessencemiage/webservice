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
            reponseUpdateBase.Add(8, new ReponseUpdateBase("Les données transmisent au service sont incorrectes", false));
            reponseUpdateBase.Add(9, new ReponseUpdateBase("Utilisateur enregistré", true));
            reponseUpdateBase.Add(10, new ReponseUpdateBase("Impossible de créer l'utilisateur", false));
            reponseUpdateBase.Add(11, new ReponseUpdateBase("Ce pseudo est déjà utilisé", false));
            reponseUpdateBase.Add(12, new ReponseUpdateBase("La station a été validée", true));
            reponseUpdateBase.Add(13, new ReponseUpdateBase("La station n'a pas été validée", false));
            reponseUpdateBase.Add(14, new ReponseUpdateBase("La station a été supprimée", true));
            reponseUpdateBase.Add(15, new ReponseUpdateBase("La station n'a pas été supprimée", false));
            reponseUpdateBase.Add(16, new ReponseUpdateBase("Votre compte a été supprimé", true));
            reponseUpdateBase.Add(17, new ReponseUpdateBase("Identifiant ou Mot de Passe incorrects.", false));
            reponseUpdateBase.Add(18, new ReponseUpdateBase("La station a été validée", true));
            reponseUpdateBase.Add(19, new ReponseUpdateBase("La station n'a pas été validée", false));
            reponseUpdateBase.Add(20, new ReponseUpdateBase("Le compte a été modifié", true));
            reponseUpdateBase.Add(21, new ReponseUpdateBase("Le compte n'a pas été modifié", false));
            reponseUpdateBase.Add(22, new ReponseUpdateBase("Le compte n'a pas été supprimé", false));
            reponseUpdateBase.Add(23, new ReponseUpdateBase("Impossible d'obtenir les coordonnées GPS. Station non modifiée", false));
            reponseUpdateBase.Add(24, new ReponseUpdateBase("Votre mot de passe a été modifié.", true));
            reponseUpdateBase.Add(25, new ReponseUpdateBase("Identifiant incorrect", false));
            reponseUpdateBase.Add(26, new ReponseUpdateBase("Impossible de créer la demande", false));
            reponseUpdateBase.Add(27, new ReponseUpdateBase("Un mail vous a été envoyé sur l'adresse rattachée à votre compte", true));
            reponseUpdateBase.Add(28, new ReponseUpdateBase("Impossible de regénérer le mot de passe", false));
            reponseUpdateBase.Add(29, new ReponseUpdateBase("Mot de Passe incorrects.", false));
        }

        public ReponseUpdateBase getReponseUpdateBase(int id)
        {
            ReponseUpdateBase rep;
            reponseUpdateBase.TryGetValue(id, out rep);
            return rep;
        }
    }
}