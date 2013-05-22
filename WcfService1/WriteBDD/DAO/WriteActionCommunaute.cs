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
        private bool activationActionCommunaute;
        public WriteActionCommunaute()
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
                activationActionCommunaute = Convert.ToBoolean(ConfigurationManager.AppSettings["activationActionCommunaute"]);
            }
            catch (FormatException e)
            {
                activationActionCommunaute = false;
            }
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
                ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionCommunaute);
                MySqlCommand cmd1;
                connection.Open();

                cmd1 = connection.CreateCommand();
                string requete = "Select count(*) From prix where prix_station_id = @prix_station_id AND prix_type_id = @prix_type_id";
                ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + requete + " avec le parametre prix_station_id = " + id_station + " & prix_type_id = " + id_price, activationActionCommunaute);
                cmd1.CommandText = requete;
                cmd1.Parameters.AddWithValue("@prix_station_id", id_station);
                cmd1.Parameters.AddWithValue("@prix_type_id", id_price);
                MySqlDataAdapter adap1 = new MySqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adap1.Fill(ds);
                int resultat = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    resultat = Convert.ToInt32(dr.ItemArray.ElementAt(0));
                    break;
                }

                if (resultat > 0)
                {
                    ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionCommunaute);
                    //connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE prix SET prix_valeur=@price, prix_date=@date Where prix_station_id=@id_station AND prix_type_id=@id_price;commit;";
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre price = " + price + " & prix_date = " + date + " & id_station = " + id_station + " & id_price = " + id_price, activationActionCommunaute);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@id_station", id_station);
                    cmd.Parameters.AddWithValue("@id_price", id_price);

                    ligneMiseAjour = cmd.ExecuteNonQuery();
                    ActionCommunaute.logger.ecrireInfoLogger("Nombre de lignes mises à jour : " + ligneMiseAjour, activationActionCommunaute);
                }
                else
                {
                    ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionCommunaute);
                    //connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO prix(prix_type_id, prix_station_id, prix_valeur, prix_date)VALUES(@id_price,@id_station,@price,@date);SELECT LAST_INSERT_ID();commit;";
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre price = " + price + " & prix_date = " + date + " & id_station = " + id_station + " & id_price = " + id_price, activationActionCommunaute);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@id_station", id_station);
                    cmd.Parameters.AddWithValue("@id_price", id_price);

                    ligneMiseAjour = cmd.ExecuteNonQuery();
                    ActionCommunaute.logger.ecrireInfoLogger("Nombre de lignes mises à jour : " + ligneMiseAjour, activationActionCommunaute);
                }
            }
            catch (Exception e)
            {
                ActionCommunaute.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
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
                return drub.getReponseUpdateBase(0);
            }
            else
            {
                return drub.getReponseUpdateBase(2);
            }
        }

        public ReponseUpdateBase writePushStation(string address, string code_postal, string city, string tel, double lattitude, double longitude, int id_enseigne, List<Prix> price_list, bool isAdmin)
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
                if (!isAdmin)
                {
                    ActionCommunaute.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionCommunaute);
                }
                else
                {
                    ActionAdmin.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString, activationActionCommunaute);
                }
                cmd.CommandText = "INSERT INTO station(station_adresse,station_cp,station_ville,station_tel,station_lat, station_long, station_id_enseigne, station_valide_admin, station_date_creation)VALUES(@station_adresse,@station_cp,@station_ville,@station_tel,@station_lat,@station_long,@station_id_enseigne,@station_valide_admin,@station_date_creation); SELECT LAST_INSERT_ID();commit;";
                if (!isAdmin)
                {
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre station_adresse = " + address + " & station_cp = " + code_postal + " & station_ville = " + city + " & station_tel = " + tel + " & station_lat = " + lattitude + " & station_long = " + longitude + " & station_id_enseigne = " + id_enseigne, activationActionCommunaute);
                }
                else
                {
                    ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre station_adresse = " + address + " & station_cp = " + code_postal + " & station_ville = " + city + " & station_tel = " + tel + " & station_lat = " + lattitude + " & station_long = " + longitude + " & station_id_enseigne = " + id_enseigne, activationActionCommunaute);
                }

                cmd.Parameters.AddWithValue("@station_adresse", address);
                cmd.Parameters.AddWithValue("@station_cp", code_postal);
                cmd.Parameters.AddWithValue("@station_ville", city);
                cmd.Parameters.AddWithValue("@station_tel", tel);
                cmd.Parameters.AddWithValue("@station_lat", lattitude);
                cmd.Parameters.AddWithValue("@station_long", longitude);
                cmd.Parameters.AddWithValue("@station_id_enseigne", id_enseigne);
                cmd.Parameters.AddWithValue("@station_valide_admin", isAdmin);
                cmd.Parameters.AddWithValue("@station_date_creation", date.ToString());

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);
                //adap.Update(ds, "update");
                
                // Renseignement Prix
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ajoutStation = true;
                    ajoutPrice = true;
                    id_station = dr.ItemArray[0].ToString();
                    if (!isAdmin)
                    {
                        ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station, activationActionCommunaute);
                    }
                    else
                    {
                        ActionAdmin.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station, activationActionCommunaute);
                    }
                    foreach (Prix unPrix in price_list)
                    {
                        ligneMiseAjour = 0;
                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO prix(prix_type_id, prix_station_id, prix_valeur, prix_date)VALUES(@prix_type_id,@prix_station_id,@prix_valeur,@prix_date);commit;";
                        if (!isAdmin)
                        {
                            ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre prix_valeur = " + unPrix.price + " & prix_date = " + date + " & id_station = " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                        }
                        else
                        {
                            ActionAdmin.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre prix_valeur = " + unPrix.price + " & prix_date = " + date + " & id_station = " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                        }
                        cmd.Parameters.AddWithValue("@prix_type_id", unPrix.carburant_type.id_type);
                        cmd.Parameters.AddWithValue("@prix_station_id", id_station);
                        cmd.Parameters.AddWithValue("@prix_valeur", unPrix.price);
                        cmd.Parameters.AddWithValue("@prix_date", date);
                        ligneMiseAjour = cmd.ExecuteNonQuery();
                        if (ligneMiseAjour == 0)
                        {
                            ajoutPrice = false;
                            if (!isAdmin)
                            {
                                ActionCommunaute.logger.ecrireInfoLogger("ECHEC : Echec de l'execution de la requete avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                            }
                            else
                            {
                                ActionAdmin.logger.ecrireInfoLogger("ECHEC : Echec de l'execution de la requete avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                            }
                                
                            break;
                        }
                        if (!isAdmin)
                        {
                            ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                        }
                        else
                        {
                            ActionAdmin.logger.ecrireInfoLogger("Execution de la requete réussi avec l'insertion de l'id_station : " + id_station + " & prix_type_id = " + unPrix.carburant_type.id_type, activationActionCommunaute);
                        }
                    }
                    
                    
                }
            }
            catch (Exception e)
            {
                if (!isAdmin)
                {
                    ActionCommunaute.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                }
                else
                {
                    ActionAdmin.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
                }
                return drub.getReponseUpdateBase(1);
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
                return drub.getReponseUpdateBase(0);
            }
            else
            {
                if (!ajoutStation)
                {
                    if (!isAdmin)
                    {
                        ActionCommunaute.logger.ecrireInfoLogger("ECHEC : Echec de la requete réussi avec l'insertion de l'id_station : " + id_station, activationActionCommunaute);
                    }
                    else
                    {
                        ActionAdmin.logger.ecrireInfoLogger("ECHEC : Echec de la requete réussi avec l'insertion de l'id_station : " + id_station, activationActionCommunaute);
                    }
                    return drub.getReponseUpdateBase(3);
                }
                bool suppressionOk = false;
                suppressionOk = suppressionPrice(id_station);
                if (suppressionOk)
                {
                    suppressionOk = suppressionStation(id_station);
                }
                if (suppressionOk)
                {
                    return drub.getReponseUpdateBase(4);
                }
                else
                {
                    return drub.getReponseUpdateBase(5);
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
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre @station_id = " + id_station, activationActionCommunaute);
                    cmd.Parameters.AddWithValue("@station_id", id_station);
                    cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
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
                    ActionCommunaute.logger.ecrireInfoLogger("Execution de la requete : " + cmd.CommandText + " avec le parametre @station_id = " + id_station, activationActionCommunaute);
                    cmd.Parameters.AddWithValue("@station_id", id_station);
                    cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace, true);
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