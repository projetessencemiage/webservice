using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using User_Lib;
using WcfService1.Outil;

namespace WcfService1.WriteBDD.DAO
{
    public class WriteUserService
    {

        private string myConnectionString;
        private DictionnaireReponseUpdateBase drub;
        private bool activationUserService;
        private bool activationActionAdmin;

        public WriteUserService()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            drub = new DictionnaireReponseUpdateBase();

            try
            {
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["activationUserService"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
            try
            {
                activationActionAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionAdmin"]);
            }
            catch (FormatException e)
            {
                activationActionAdmin = false;
            }
        }

        internal ReponseUpdateBase inscriptionUser(int idRole, string civilite, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            int ligneMiseAjour = 0;

            try
            {
                connection.Open();
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);

                cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into test.user(pseudo, nom, prenom, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_favorite, id_role, civilite)VALUES(@pseudo, @nom, @prenom, @email, @mdp, @adresse, @code_postal, @ville, @url_avatar, @id_station_favorite, @id_carburant_pref, @idRole, @civilite);commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText 
                    + " avec les parametres idRole =" + idRole + " & nom = " + nom + " & prenom = " + prenom + " & pseudo = " + pseudo + " & email = " + email
                    + " & mdp = " + mdp + " & adresse = " + adresse + " & code_postal = " + code_postal + " & ville = " + ville + " & url_avatar = " + url_avatar
                    + " & id_station_favorite = " + id_station_favorite + " & id_carburant_favorite = " + id_carburant_pref + " & civilite = " + civilite, activationUserService);

                cmd.Parameters.AddWithValue("@civilite", civilite);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@mdp", mdp);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@code_postal", code_postal);
                cmd.Parameters.AddWithValue("@ville", ville);
                cmd.Parameters.AddWithValue("@url_avatar", url_avatar);

                cmd.Parameters.AddWithValue("@id_station_favorite", id_station_favorite);

                cmd.Parameters.AddWithValue("@id_carburant_pref", id_carburant_pref);

                cmd.Parameters.AddWithValue("@idRole", idRole);

                ligneMiseAjour = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                foreach (int dr in e.Data.Values)
                {
                    if (dr.ToString().Equals("1062"))
                    {
                        return drub.getReponseUpdateBase(11);
                    }
                }
                UserService.logger.ecrireInfoLogger("ERROR : " + e.Message + "\n" + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (ligneMiseAjour > 0)
            {
                return drub.getReponseUpdateBase(9);
            }
            else
            {
                return drub.getReponseUpdateBase(10);
            }
        }

        internal ReponseUpdateBase desinscription(string identifiant, string mdp)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            int resultat = -1;

            try
            {
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "Delete FROM user Where pseudo = @identifiant AND mdp = @mdp;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres identifiant =" + identifiant + " & mdp = " + mdp, activationUserService);

                cmd.Parameters.AddWithValue("@identifiant", identifiant);
                cmd.Parameters.AddWithValue("@mdp", mdp);

                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(16);
            }
            return drub.getReponseUpdateBase(17);
        }

        internal ReponseUpdateBase miseAJourProfilUser(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                cmd.CommandText = "Update user SET nom=@nom, prenom=@prenom, email=@email, adresse=@adresse, code_postal=@code_postal, ville=@ville, url_avatar=@url_avatar, id_station_favorite=@id_station_favorite, id_carburant_favorite=@id_carburant_pref, civilite=@civilite Where pseudo=@pseudo;commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText, activationUserService);

                cmd.Parameters.AddWithValue("@pseudo", pseudo);

                cmd.Parameters.AddWithValue("@civilite", civilite);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@code_postal", code_postal);
                cmd.Parameters.AddWithValue("@ville", ville);
                cmd.Parameters.AddWithValue("@url_avatar", url_avatar);
                cmd.Parameters.AddWithValue("@id_station_favorite", id_station_favorite);
                cmd.Parameters.AddWithValue("@id_carburant_pref", id_carburant_pref);

                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(20);
            }
            return drub.getReponseUpdateBase(17);
        }

        internal ReponseUpdateBase miseAJourProfilUserByAdmin(string civilite, string nom, string prenom, string pseudo, string email, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionAdmin);
                cmd.CommandText = "Update user SET nom=@nom, prenom=@prenom, email=@email, adresse=@adresse, code_postal=@code_postal, ville=@ville, url_avatar=@url_avatar, id_station_favorite=@id_station_favorite, id_carburant_favorite=@id_carburant_pref, civilite=@civilite Where pseudo=@pseudo;commit;";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText, activationActionAdmin);

                cmd.Parameters.AddWithValue("@pseudo", pseudo);

                cmd.Parameters.AddWithValue("@civilite", civilite);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@code_postal", code_postal);
                cmd.Parameters.AddWithValue("@ville", ville);
                cmd.Parameters.AddWithValue("@url_avatar", url_avatar);
                cmd.Parameters.AddWithValue("@id_station_favorite", id_station_favorite);
                cmd.Parameters.AddWithValue("@id_carburant_pref", id_carburant_pref);

                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(20);
            }
            return drub.getReponseUpdateBase(21);
        }

        internal ReponseUpdateBase suppressionCompteByAdmin(string identifiant)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            int resultat = -1;

            try
            {
                ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "Delete FROM user Where pseudo = @identifiant";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres identifiant =" + identifiant, activationUserService);

                cmd.Parameters.AddWithValue("@identifiant", identifiant);

                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(16);
            }
            return drub.getReponseUpdateBase(22);
        }

        internal ReponseUpdateBase changementMDP(string identifiant, string ancienMDP, string nouveauMDP)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                cmd.CommandText = "Update user SET mdp=@nouveauMDP Where pseudo=@pseudo AND mdp=@ancienMDP;commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText, activationUserService);

                cmd.Parameters.AddWithValue("@pseudo", identifiant);
                cmd.Parameters.AddWithValue("@ancienMDP", ancienMDP);
                cmd.Parameters.AddWithValue("@nouveauMDP", nouveauMDP);

                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(20);
            }
            return drub.getReponseUpdateBase(29);
        }

        internal ReponseUpdateBase motDePasseOublie(string identifiant)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            int ligneMiseAjour = 0;

            DateTime dt = DateTime.Now;
            String[] format = { "yyyy/MM/dd HH:mm:ss" };
            string date_demande = dt.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
            string cle_demande = creationCleDemande(identifiant, date_demande);

            try
            {
                connection.Open();
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);

                cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into test.demande_new_mdp(cle_demande, pseudo, date_demande)VALUES(@cle_demande, @pseudo, @date_demande);commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres cle_demande =" + cle_demande + " & pseudo = " + identifiant + " & date_demande = " + date_demande, activationUserService);

                cmd.Parameters.AddWithValue("@cle_demande", cle_demande);
                cmd.Parameters.AddWithValue("@pseudo", identifiant);
                cmd.Parameters.AddWithValue("@date_demande", date_demande);

                ligneMiseAjour = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.Message + "\n" + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (ligneMiseAjour > 0)
            {
                string email = recuperationMail(identifiant);
                if (!email.Equals(""))
                {
                    GestionMail mail = new GestionMail(cle_demande, identifiant, email);
                    return mail.envoyerMail();
                }
                return drub.getReponseUpdateBase(26);
            }
            else
            {
                return drub.getReponseUpdateBase(26);
            }
        }

        private string recuperationMail(string identifiant)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            string email = "";

            try
            {
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection = new MySqlConnection(myConnectionString);
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "Select email FROM user Where pseudo = @identifiant;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres identifiant =" + identifiant, activationUserService);

                cmd.Parameters.AddWithValue("@identifiant", identifiant);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        email = dr["email"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return "";
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return email;
        }

        private string creationCleDemande(string identifiant, string date_demande)
        {
            Random rndNumbers = new Random();
            HashAlgorithm hash = new SHA256Managed();

            string baseCleDemande = identifiant + date_demande + rndNumbers.Next();

            byte[] baseCleDemandeBytes = Encoding.UTF8.GetBytes(baseCleDemande);
            byte[] hashBytes = hash.ComputeHash(baseCleDemandeBytes);

            string hashValueNoReplace = Convert.ToBase64String(hashBytes);
            string hashValue = hashValueNoReplace.Replace("&", rndNumbers.Next(0, 9).ToString()).Replace("+", rndNumbers.Next(0, 9).ToString());

            return hashValue;
        }

        internal ReponseUpdateBase reinitialisationMDP(string identifiant, string cle, string nouveauMDP)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            MySqlCommand cmd2;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                cmd.CommandText = "Update user SET mdp=@nouveauMDP Where pseudo=@pseudo;commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText, activationUserService);

                cmd.Parameters.AddWithValue("@pseudo", identifiant);
                cmd.Parameters.AddWithValue("@nouveauMDP", nouveauMDP);

                resultat = cmd.ExecuteNonQuery();

                if (resultat > 0)
                {
                    cmd2 = connection.CreateCommand();
                    cmd2.CommandText = "Delete FROM demande_new_mdp Where cle_demande = @cle_demande";
                    UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd2.CommandText
                        + " avec les parametres cle_demande =" + cle, activationUserService);

                    cmd2.Parameters.AddWithValue("@cle_demande", cle);

                    cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUpdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (resultat > 0)
            {
                return drub.getReponseUpdateBase(24);
            }
            return drub.getReponseUpdateBase(17);
        }

        internal bool verificationCle(string cle, string pseudo)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            bool cleOK = false;
            String date_demande = null;
            DateTime now = DateTime.Now;

            try
            {
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection = new MySqlConnection(myConnectionString);
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "Select date_demande FROM demande_new_mdp Where pseudo = @pseudo AND cle_demande = @cle_demande;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres cle_demande = " + cle + " & pseudo = " + pseudo, activationUserService);

                cmd.Parameters.AddWithValue("@cle_demande", cle);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        date_demande = dr["date_demande"].ToString();
                    }
                }
                if (date_demande != null)
                {
                    TimeSpan span = now - DateTime.Parse(date_demande);
                    if (span.TotalSeconds < 1800)
                    {
                        cleOK = true;
                    }
                }
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return cleOK;
        }
    }
}