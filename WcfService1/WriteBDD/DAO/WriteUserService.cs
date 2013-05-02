using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionUser"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
        }

        internal ReponseUpdateBase inscriptionUser(int idRole, string nom, string prenom, string pseudo, string email, string mdp, string adresse, string code_postal, string ville, string url_avatar, int id_station_favorite, int id_carburant_pref)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            string id_user = null;

            try
            {
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection = new MySqlConnection(myConnectionString);
                cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into test.user(pseudo, nom, prenom, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_favorite, id_role)"
                 + "VALUES(@pseudo, @nom, @prenom, @email, @mdp, @adresse, @code_postal, @ville, @url_avatar, @id_station_favorite, @id_carburant_pref, @idRole);SELECT LAST_INSERT_ID();commit;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText 
                    + " avec les parametres idRole =" + idRole + " & nom = " + nom + " & prenom = " + prenom + " & pseudo = " + pseudo + " & email = " + email
                    + " & mdp = " + mdp + " & adresse = " + adresse + " & code_postal = " + code_postal + " & ville = " + ville + " & url_avatar = " + url_avatar + " & id_station_favorite = " + id_station_favorite + " & id_carburant_favorite = " + id_carburant_pref, activationUserService);

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

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                if(ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        id_user = dr.ItemArray[0].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                foreach (int dr in e.Data.Values)
                {
                    if (dr.ToString().Equals("1062"))
                    {
                        return drub.getReponseUdateBase(11);
                    }
                }
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return drub.getReponseUdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (id_user != null)
            {
                return drub.getReponseUdateBase(9);
            }
            else
            {
                return drub.getReponseUdateBase(10);
            }
        }

        internal User identificationUser(string identifiant, string mdp)
        {
            throw new NotImplementedException();
        }
    }
}