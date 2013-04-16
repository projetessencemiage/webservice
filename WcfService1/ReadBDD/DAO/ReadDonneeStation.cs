using FuelTracker_Lib;
using Geolocalisation_Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WcfService1.Outil;

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

        public List<Station> recupererStationCodePostalSansPrix(string codePostal)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            listStation = new List<Station>();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();
                
                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_cp = @codePostal";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre station_cp = " + codePostal);
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
                    float latitude = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    listStation.Add(new Station(id_station, null, address, city, codePostal, longitude, latitude, id_enseigne, enseigne_marque, tel));
                }

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return null;
            }
            return listStation;
        }

        public List<StationAndDistance> recupererStationParRapportPosition(int distance, float longitude, float latitude)
        {
            //Calcul des longitudes Max et Min à partir de la position.
            //0,01 degré décimaux ~= 1,113km.
            float coefMultiplicateur = 0.01F / 1.113F;
            float dist_deg = (distance * coefMultiplicateur);
            float long_max = longitude + dist_deg;
            float long_min = longitude - dist_deg;
            float lat_max = latitude + dist_deg;
            float lat_min = latitude - dist_deg;

            //On récupère les stations comprises dans le carré.


            List<StationAndDistance> listStation = new List<StationAndDistance>();
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_lat BETWEEN @lat_min AND @lat_max AND station_long BETWEEN @long_min AND @long_max";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec les parametres lat_min = " + lat_min + " & lat_max = " + lat_max + " & long_min = " + long_min + " & long_max = " + long_max);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@lat_min", lat_min);
                cmd.Parameters.AddWithValue("@lat_max", lat_max);
                cmd.Parameters.AddWithValue("@long_min", long_min);
                cmd.Parameters.AddWithValue("@long_max", long_max);

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
                    string codePostal = dr["station_cp"].ToString();
                    float long_station = Single.Parse(dr["station_long"].ToString().Replace(".", ","));
                    float lat_station = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    double distanceAvecPosition = CalculDistance.getDistance(lat_station, long_station, latitude, longitude);
                    if (distanceAvecPosition <= Convert.ToDouble(distance))
                    {
                        listStation.Add(new StationAndDistance(
                            new Station(id_station, null, address, city, codePostal, long_station, lat_station, id_enseigne, enseigne_marque, tel),
                            distanceAvecPosition));
                    }
                }
                listStation.Sort();
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return null;
            }
            return listStation;
        }

        public List<Station> recupererStationDepartementSansPrix(string departement)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            listStation = new List<Station>();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_cp LIKE @departement";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre departement = " + departement);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@departement", departement+"%");
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
                    string codePostal = dr["station_cp"].ToString();
                    float longitude = Single.Parse(dr["station_long"].ToString().Replace(".", ","));
                    float latitude = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    listStation.Add(new Station(id_station, null, address, city, codePostal, longitude, latitude, id_enseigne, enseigne_marque, tel));
                }

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return null;
            }

            return listStation;
        }

        public List<Station> recupererStationVilleSansPrix(string ville, string departement)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            listStation = new List<Station>();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_ville LIKE @ville AND station_cp LIKE @departement";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre ville = " + ville + " & departement = " + departement);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@ville", ville);
                cmd.Parameters.AddWithValue("@departement", departement + "%");
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
                    string codePostal = dr["station_cp"].ToString();
                    float longitude = Single.Parse(dr["station_long"].ToString().Replace(".", ","));
                    float latitude = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    listStation.Add(new Station(id_station, null, address, city, codePostal, longitude, latitude, id_enseigne, enseigne_marque, tel));
                }

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return null;
            }

            return listStation;
        }
    }
}