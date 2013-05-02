using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using User_Lib;
using WcfService1.Outil;

namespace WcfService1.ReadBDD.DAO
{
    public class ReadUserService
    {
        private string myConnectionString;
        private bool activationUserService;

        public ReadUserService()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                activationUserService = Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionUser"]);
            }
            catch (FormatException e)
            {
                activationUserService = false;
            }
        }

        internal ReponseConnectionUser identificationUser(string identifiant, string mdp)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            User user = null;
            DataSet ds = new DataSet();

            try
            {
                UserService.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationUserService);
                connection = new MySqlConnection(myConnectionString);
                cmd = connection.CreateCommand();
                cmd.CommandText = "Select pseudo, nom, prenom, email, mdp, adresse, code_postal, ville, url_avatar, id_station_favorite, id_carburant_favorite, user.id_role, nom_role FROM user Join role on role.id_role = user.id_role Where pseudo = @identifiant AND mdp = @mdp;";
                UserService.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText
                    + " avec les parametres identifiant =" + identifiant + " & mdp = " + mdp, activationUserService);

                cmd.Parameters.AddWithValue("@identifiant", identifiant);
                cmd.Parameters.AddWithValue("@mdp", mdp);


                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int id_role = Convert.ToInt32(dr["id_role"].ToString());
                        string nom_role = dr["nom_role"].ToString();
                        string nom = dr["nom"].ToString();
                        string prenom = dr["prenom"].ToString();
                        string pseudo = dr["pseudo"].ToString();
                        string adresse = dr["adresse"].ToString();
                        string code_postal = dr["code_postal"].ToString();
                        string ville = dr["ville"].ToString();
                        string mot_de_passe = "";
                        string avatar = dr["url_avatar"].ToString();
                        string email = dr["email"].ToString();
                        int id_station_favorite = Convert.ToInt32(dr["id_station_favorite"].ToString());
                        int id_carburant_pref = Convert.ToInt32(dr["id_carburant_favorite"].ToString());
                        user = new User(id_role, nom_role, nom, prenom, pseudo, email, mot_de_passe, adresse, code_postal, ville, avatar, id_station_favorite,id_carburant_pref);
                    }
                }
            }
            catch (Exception e)
            {
                UserService.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return new ReponseConnectionUser(2, user);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (user != null)
            {
                return new ReponseConnectionUser(0, user);
            }
            return new ReponseConnectionUser(1, user);
        }
    }
}