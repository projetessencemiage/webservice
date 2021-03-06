﻿using FuelTracker_Lib;
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
        private bool activationReadStation;
        private bool activationActionAdmin;

        public ReadDonneeStation()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                activationReadStation = Convert.ToBoolean(ConfigurationManager.AppSettings["activationReadStation"]);
            }
            catch (FormatException e)
            {
                activationReadStation = false;
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

        public List<Station> recupererStationCodePostalSansPrix(string codePostal)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();
                
                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_cp = @codePostal AND station_valide_admin = @station_valider";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre station_cp = " + codePostal, activationReadStation);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@codePostal", codePostal);
                cmd.Parameters.AddWithValue("@station_valider", 1);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                
                adap.Fill(ds);

                listStation = traitementDonneesSqlPourStation(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
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


            List<StationAndDistance> listStation = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_lat BETWEEN @lat_min AND @lat_max AND station_long BETWEEN @long_min AND @long_max AND station_valide_admin = @station_valider";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec les parametres lat_min = " + lat_min + " & lat_max = " + lat_max + " & long_min = " + long_min + " & long_max = " + long_max, activationReadStation);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@lat_min", lat_min);
                cmd.Parameters.AddWithValue("@lat_max", lat_max);
                cmd.Parameters.AddWithValue("@long_min", long_min);
                cmd.Parameters.AddWithValue("@long_max", long_max);
                cmd.Parameters.AddWithValue("@station_valider", 1);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);

                listStation = traitementDonneesSqlPourStationAndDistance(ds, distance, longitude, latitude);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return null;
            }
            return listStation;
        }

        public List<Station> recupererStationDepartementSansPrix(string departement)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_cp LIKE @departement AND station_valide_admin = @station_valider";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre departement = " + departement, activationReadStation);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@departement", departement+"%");
                cmd.Parameters.AddWithValue("@station_valider", 1);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds);

                listStation = traitementDonneesSqlPourStation(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return null;
            }

            return listStation;
        }

        public List<Station> recupererStationVilleSansPrix(string ville, string departement)
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                AffichagePrix.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_ville LIKE @ville AND station_cp LIKE @departement AND station_valide_admin = @station_valider";
                AffichagePrix.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre ville = " + ville + " & departement = " + departement, activationReadStation);
                cmd.CommandText = requete;
                cmd.Parameters.AddWithValue("@ville", ville);
                cmd.Parameters.AddWithValue("@station_valider", 1);
                cmd.Parameters.AddWithValue("@departement", departement + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds);

                listStation = traitementDonneesSqlPourStation(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return null;
            }

            return listStation;
        }

        internal List<Station> ListStationAValider()
        {
            List<Station> listStation = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_valide_admin = @station_a_valider";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre station_valide_admin = " + 0, activationReadStation);
                cmd.CommandText = requete;

                cmd.Parameters.AddWithValue("@station_a_valider", 0);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);

                listStation = traitementDonneesSqlPourStation(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return null;
            }
            return listStation;
        }

        private List<Station> traitementDonneesSqlPourStation(DataSet ds)
        {
            List<Station> list_station = new List<Station>();
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
                string dateCreation = dr["station_date_creation"].ToString();
                list_station.Add(new Station(id_station, null, address, city, codePostal, longitude, latitude, id_enseigne, enseigne_marque, tel, dateCreation));
            }
            return list_station;
        }

        private List<StationAndDistance> traitementDonneesSqlPourStationAndDistance(DataSet ds, int distance, float longitude, float latitude)
        {
            List<StationAndDistance> listStation = new List<StationAndDistance>();
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
                string dateCreation = dr["station_date_creation"].ToString();
                double distanceAvecPosition = CalculDistance.getDistance(lat_station, long_station, latitude, longitude);
                if (distanceAvecPosition <= Convert.ToDouble(distance))
                {
                    listStation.Add(new StationAndDistance(
                        new Station(id_station, null, address, city, codePostal, long_station, lat_station, id_enseigne, enseigne_marque, tel, dateCreation),
                        distanceAvecPosition));
                }
            }
            listStation.Sort();
            return listStation;
        }


        internal Station getStationByID(string id_station)
        {
            Station station = null;
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationReadStation);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select station_id, station_adresse, station_cp, station_ville, station_tel, station_lat, station_long, station_id_enseigne, enseigne_marque, station_date_creation From station Join enseigne on enseigne.enseigne_id = station.station_id_enseigne where station_id = @station_id";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre station_id = " + id_station, activationReadStation);
                cmd.CommandText = requete;

                cmd.Parameters.AddWithValue("@station_id", id_station);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string address = dr["station_adresse"].ToString();
                    string city = dr["station_ville"].ToString();
                    string tel = dr["station_tel"].ToString();
                    string codePostal = dr["station_cp"].ToString();
                    float longitude = Single.Parse(dr["station_long"].ToString().Replace(".", ","));
                    float latitude = Single.Parse(dr["station_lat"].ToString().Replace(".", ","));
                    string id_enseigne = dr["station_id_enseigne"].ToString();
                    string enseigne_marque = dr["enseigne_marque"].ToString();
                    string dateCreation = dr["station_date_creation"].ToString();
                    station = new Station(id_station, null, address, city, codePostal, longitude, latitude, id_enseigne, enseigne_marque, tel, dateCreation);
                }
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return null;
            }
            return station;
        }
    }
}