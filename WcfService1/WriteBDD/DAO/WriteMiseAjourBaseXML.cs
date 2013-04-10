using FuelTracker_Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.WriteBDD.DAO
{
    public class WriteMiseAjourBaseXML
    {
        private string myConnectionString;
        public WriteMiseAjourBaseXML()
        {
            string server = "192.168.0.1";
            string database = "test";
            string uid = "dev";
            string password = "dev";
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        internal bool writeDonneesXML(List<Station> listStation)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand cmd;
            try
            {
                connection.Open();
                
                int id = 3;
                
                foreach(Station uneStation in listStation)
                {
                    // Renseignement Station
                    cmd = connection.CreateCommand(); 
                    cmd.CommandText = "INSERT INTO station(station_id,station_adresse,station_cp,station_ville,station_tel,station_lat, station_long, station_id_enseigne)VALUES(@station_id,@station_adresse,@station_cp,@station_ville,@station_tel,@station_lat,@station_long,@station_id_enseigne);commit;";
                    cmd.Parameters.AddWithValue("@station_id", id);
                    cmd.Parameters.AddWithValue("@station_adresse", uneStation.address);
                    cmd.Parameters.AddWithValue("@station_cp", uneStation.code_postal);
                    cmd.Parameters.AddWithValue("@station_ville", uneStation.city);
                    cmd.Parameters.AddWithValue("@station_tel", DBNull.Value);
                    cmd.Parameters.AddWithValue("@station_lat", uneStation.lattitude);
                    cmd.Parameters.AddWithValue("@station_long", uneStation.longitude);
                    cmd.Parameters.AddWithValue("@station_id_enseigne", getIdEnseigne());
                    cmd.ExecuteNonQuery();

                    // Renseignement Prix
                    foreach (Prix unPrix in uneStation.price_list)
                    {
                        string id_prix = getIdTypeEssence(unPrix);
                        if(Int32.Parse(id_prix) !=-1)
                        {
                            cmd = connection.CreateCommand(); 
                            cmd.CommandText = "INSERT INTO prix(prix_type_id, prix_station_id, prix_valeur, prix_date)VALUES(@prix_type_id,@prix_station_id,@prix_valeur,@prix_date);commit;";
                            cmd.Parameters.AddWithValue("@prix_type_id", id_prix);
                            cmd.Parameters.AddWithValue("@prix_station_id", id);
                            cmd.Parameters.AddWithValue("@prix_valeur", unPrix.price);
                            cmd.Parameters.AddWithValue("@prix_date", unPrix.dateMiseAjour);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    id++;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return true;
        }

        private string getIdEnseigne()
        {
            int min = 1;
            int max = 10;
            Random rnd = new Random();
            int choix = rnd.Next(min, max);
            return choix.ToString();
        }

        private string getIdTypeEssence(Prix unPrix)
        {
            switch (unPrix.carburant_type.type_nom)
            {
                case "Gazole":
                    int min = 0;
                    int max = 1;
                    Random rnd = new Random();
                    int choix = rnd.Next(min, max);
                    return choix.ToString();
                case "SP98":
                    return "3";
                case "SP95-E10":
                    return "2";
                case "GPL":
                    return "4";
                default:
                    return "-1";
            }
        }
    }
}