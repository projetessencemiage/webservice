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

    public class ReadDonneeStation
    {
        private string myConnectionString;
        public ReadDonneeStation()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }


        public List<Station> recupererStationCodePostalSansPrix(int codePostal)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            listStation = new List<Station>();
            MySqlConnection connection;
            try
            {
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();
                
                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_cp = @codePostal";
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@codePostal", codePostal);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                
                adap.Fill(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string id_station = dr["station_id"].ToString();
                    string address = dr["station_adresse"].ToString();
                    string city = dr["station_ville"].ToString();
                    string tel = dr["station_tel"].ToString();
                    float longitude = Single.Parse(dr["station_long"].ToString().Replace(".",","));
                    float lattitude = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    listStation.Add(new Station(id_station, null, address, city, codePostal.ToString(), longitude, lattitude, id_enseigne, enseigne_marque, tel));
                }

            }
            catch (Exception e)
            {
                return null;
            }

            return listStation;
        }
    }
}