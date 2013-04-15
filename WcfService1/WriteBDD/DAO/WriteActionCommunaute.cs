using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE prix SET prix_valeur=@price, prix_date=@date Where prix_station_id=@id_station AND prix_type_id=@id_price;commit;";
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id_station", id_station);
                cmd.Parameters.AddWithValue("@id_price", id_price);
                ligneMiseAjour = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                AffichagePrix.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
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
    }
}