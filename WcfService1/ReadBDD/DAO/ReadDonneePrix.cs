using FuelTracker_Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WcfService1.ReadBDD.DAO
{
    public class ReadDonneePrix
    {
        private string myConnectionString;
        private bool activationReadPrix;

        public ReadDonneePrix()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                activationReadPrix = Convert.ToBoolean(ConfigurationManager.AppSettings["activationReadPrix"]);
            }
            catch (FormatException e)
            {
                activationReadPrix = false;
            }
        }

        public List<Prix> readPrixByStation(string id_station)
        {
            List<Prix> listPrix = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            listPrix = new List<Prix>();
            try
            {
                if (AffichagePrix.logger != null)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadPrix);
                }
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select prix_type_id, type_nom, prix_valeur, prix_date From prix Join type on type.type_id = prix.prix_type_id where prix_station_id = @stationId";
                if (AffichagePrix.logger != null)
                {
                    AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre id_station = " + id_station, activationReadPrix);
                }
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@stationId", id_station);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string type_id = dr["prix_type_id"].ToString();
                    string type_nom = dr["type_nom"].ToString();
                    float price = Single.Parse(dr["prix_valeur"].ToString());
                    string dateMiseAjour = dr["prix_date"].ToString();

                    listPrix.Add(new Prix(id_station, type_id, type_nom, price, dateMiseAjour));
                }

            }
            catch (Exception e)
            {
                if (AffichagePrix.logger != null)
                {
                    AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                }
                return null;
            }

            return listPrix;
        }

        
    }
}