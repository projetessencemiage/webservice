using FuelTracker_Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WcfService1.ReadBDD.DAO
{
    public class ReadDonneePrix
    {
        private string myConnectionString;
        public ReadDonneePrix()
        {
            string server = "192.168.0.1";
            string database = "test";
            string uid = "dev";
            string password = "dev";
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public List<Prix> readPrixByStation(string id_station)
        {
            List<Prix> listPrix = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select prix_type_id, type_nom, prix_valeur, prix_date From prix Join type on type.type_id = prix.prix_type_id where prix_station_id = @stationId";
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
                    listPrix = new List<Prix>();
                    string type_id = dr["prix_type_id"].ToString();
                    string type_nom = dr["type_nom"].ToString();
                    float price = Single.Parse(dr["prix_valeur"].ToString());
                    string dateMiseAjour = dr["prix_date"].ToString();

                    listPrix.Add(new Prix(id_station, type_id, type_nom, price, dateMiseAjour));
                }

            }
            catch (Exception e)
            {
                return null;
            }

            return listPrix;
        }

        
    }
}