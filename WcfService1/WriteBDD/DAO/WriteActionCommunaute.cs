using FuelTracker_Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using WcfService1.Outil;

namespace WcfService1.WriteBDD.DAO
{
    public class WriteActionCommunaute
    {
        private string myConnectionString;
        private DictionnaireReponseUpdateBase drub;
        public WriteActionCommunaute()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            drub = new DictionnaireReponseUpdateBase();
        }

        public ReponseUpdateBase writePushPrice(string id_station, int id_price, double price)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;

            int ligneMiseAjour = 0;

            DateTime dt = DateTime.Now;
            String[] format = {"yyyy/MM/dd HH:mm:ss"};
            String date = dt.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
            try
            {
                ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE prix SET prix_valeur=@price, prix_date=@date Where prix_station_id=@id_station AND prix_type_id=@id_price;commit;";
                ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre price = " + price + " & prix_date = " + date + " & id_station = " + id_station + " & id_price = " + id_price);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id_station", id_station);
                cmd.Parameters.AddWithValue("@id_price", id_price);
                ligneMiseAjour = cmd.ExecuteNonQuery();
                ActionCommunaute.logger.ecrireInfoLogger("Nombre de lignes mises à jour : " + ligneMiseAjour);
            }
            catch (Exception e)
            {
                ActionCommunaute.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return drub.getReponseUdateBase(1);
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
                return drub.getReponseUdateBase(0);
            }
            else
            {
                return drub.getReponseUdateBase(2);
            }
        }

        public ReponseUpdateBase writePushStation(string address, string code_postal, string city, string tel, double lattitude, double longitude, int id_enseigne, List<Prix> price_list)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            //ds.Tables.Add("update");

            int ligneMiseAjour = 0;
            bool ajoutPrice = false;
            bool ajoutStation = false;
            string id_station = "";

            DateTime dt = DateTime.Now;
            String[] format = {"yyyy/MM/dd HH:mm:ss"};
            String date = dt.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
            try
            {
                // Renseignement Station
                cmd = connection.CreateCommand();
                ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                cmd.CommandText = "INSERT INTO station(station_adresse,station_cp,station_ville,station_tel,station_lat, station_long, station_id_enseigne)VALUES(@station_adresse,@station_cp,@station_ville,@station_tel,@station_lat,@station_long,@station_id_enseigne); SELECT LAST_INSERT_ID();commit;";
                ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre station_adresse = " + address + " & station_cp = " + code_postal + " & station_ville = " + city + " & station_tel = " + tel + " & station_lat = " + lattitude + " & station_long = " + longitude + " & station_id_enseigne = " + id_enseigne);
                cmd.Parameters.AddWithValue("@station_adresse", address);
                cmd.Parameters.AddWithValue("@station_cp", code_postal);
                cmd.Parameters.AddWithValue("@station_ville", city);
                cmd.Parameters.AddWithValue("@station_tel", tel);
                cmd.Parameters.AddWithValue("@station_lat", lattitude);
                cmd.Parameters.AddWithValue("@station_long", longitude);
                cmd.Parameters.AddWithValue("@station_id_enseigne", id_enseigne);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                //adap.Update(ds, "update");
                
                // Renseignement Prix
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ajoutStation = true;
                    ajoutPrice = true;
                    id_station = dr.ItemArray[0].ToString();
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station);
                    foreach (Prix unPrix in price_list)
                    {
                        ligneMiseAjour = 0;
                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO prix(prix_type_id, prix_station_id, prix_valeur, prix_date)VALUES(@prix_type_id,@prix_station_id,@prix_valeur,@prix_date);commit;";
                        ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre prix_valeur = " + unPrix.price + " & prix_date = " + date + " & id_station = " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type);
                        cmd.Parameters.AddWithValue("@prix_type_id", unPrix.carburant_type.id_type);
                        cmd.Parameters.AddWithValue("@prix_station_id", id_station);
                        cmd.Parameters.AddWithValue("@prix_valeur", unPrix.price);
                        cmd.Parameters.AddWithValue("@prix_date", date);
                        ligneMiseAjour = cmd.ExecuteNonQuery();
                        if (ligneMiseAjour == 0)
                        {
                            ajoutPrice = false;
                            ActionCommunaute.logger.ecrireInfoLogger("ECHEC : Echec de l'execution de la requete avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type);
                            break;
                        }
                        ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type);
                    }
                    
                    
                }
            }
            catch (Exception e)
            {
                ActionCommunaute.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return drub.getReponseUdateBase(1);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            if (ajoutStation && ajoutPrice)
            {
                return drub.getReponseUdateBase(0);
            }
            else
            {
                if (!ajoutStation)
                {
                    ActionCommunaute.logger.ecrireInfoLogger("ECHEC : Echec de la requete réussi avec l'insertion de l'id_station : " + id_station);
                    return drub.getReponseUdateBase(3);
                }
                bool suppressionOk = false;
                suppressionOk = suppressionPrice(id_station);
                if (suppressionOk)
                {
                    suppressionOk = suppressionStation(id_station);
                }
                if (suppressionOk)
                {
                    return drub.getReponseUdateBase(4);
                }
                else
                {
                    return drub.getReponseUdateBase(5);
                }
            }
        }

        private bool suppressionPrice(string id_station)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            try
            {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM prix WHERE prix_station_id = @station_id;";
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre @station_id = " + id_station);
                    cmd.Parameters.AddWithValue("@station_id", id_station);
                    cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
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

        private bool suppressionStation(string id_station)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            try
            {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM station WHERE prix_station_id = @station_id;";
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre @station_id = " + id_station);
                    cmd.Parameters.AddWithValue("@station_id", id_station);
                    cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
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
    }
}