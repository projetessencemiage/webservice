using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WcfService1.Outil;

namespace WcfService1.WriteBDD.DAO
{
    public class WriteDonneeStation
    {
        private string myConnectionString;
        private bool activationActionAdmin;
        private DictionnaireReponseUpdateBase drub;

        public WriteDonneeStation()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            drub = new DictionnaireReponseUpdateBase();

            try
            {
                activationActionAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionAdmin"]);
            }
            catch (FormatException e)
            {
                activationActionAdmin = false;
            }
        }

        internal ReponseUpdateBase ValiderStation(string id_station)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                connection.Open();
                // Renseignement Station
                cmd = connection.CreateCommand();
                ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionAdmin);
                cmd.CommandText = "Update station SET station_valide_admin=@station_valide_admin Where station_id=@id_station;commit;";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre station_id = " + id_station, activationActionAdmin);
                cmd.Parameters.AddWithValue("@station_valide_admin", 1);
                cmd.Parameters.AddWithValue("@id_station", id_station);

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
            if (resultat>0)
            {
                return drub.getReponseUpdateBase(12);
            }
            return drub.getReponseUpdateBase(13);
        }

        private bool suppressionPrice(string id_station)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            try
            {
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM prix WHERE prix_station_id = @station_id;";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre @station_id = " + id_station, activationActionAdmin);
                cmd.Parameters.AddWithValue("@station_id", id_station);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return true;
        }

        internal ReponseUpdateBase SupprimerStation(string id_station)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();

            int resultat = 0;

            try
            {
                bool suppressionPrix = suppressionPrice(id_station);
                if (suppressionPrix)
                {
                    connection.Open();
                    // Renseignement Station
                    cmd = connection.CreateCommand();
                    ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionAdmin);
                    cmd.CommandText = "DELETE FROM station WHERE station_id=@id_station;commit;";
                    ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre station_id = " + id_station, activationActionAdmin);

                    cmd.Parameters.AddWithValue("@id_station", id_station);

                    resultat = cmd.ExecuteNonQuery();
                }
                else
                {
                    return drub.getReponseUpdateBase(1);
                }
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
                return drub.getReponseUpdateBase(14);
            }
            return drub.getReponseUpdateBase(15);
        }

        internal ReponseUpdateBase modififierStation(string id_station, string address, string code_postal, string city, string tel, double latitude, double longitude, int int_id_enseigne)
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
                cmd.CommandText = "Update station SET station_adresse=@address, station_cp=@code_postal, station_ville=@city, station_tel=@tel, station_lat=@latitude, station_long=@longitude, station_id_enseigne=@int_id_enseigne Where station_id=@id_station;commit;";
                ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText, activationActionAdmin);
                cmd.Parameters.AddWithValue("@id_station", id_station);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@code_postal", code_postal);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Parameters.AddWithValue("@latitude", latitude);
                cmd.Parameters.AddWithValue("@longitude", longitude);
                cmd.Parameters.AddWithValue("@int_id_enseigne", int_id_enseigne);

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
                return drub.getReponseUpdateBase(18);
            }
            return drub.getReponseUpdateBase(19);
        }
    }
}