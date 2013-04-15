﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WcfService1.ReadBDD.DAO
{
    public class ReadOutilsDonnees
    {
        private string myConnectionString;
        public ReadOutilsDonnees()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string uid = ConfigurationManager.AppSettings["uid"];
            string password = ConfigurationManager.AppSettings["password"];
            myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public SortedList<int, string> getIdAndTypeEssence()
        {
            SortedList<int, string> listIdAndTypeEssence = new SortedList<int, string>();
            DataSet ds = new DataSet();
            MySqlConnection connection;
            try
            {
                RecuperationOutilsDonnees.logger.ecrireInfoLogger("Connection à la base : " + myConnectionString);
                connection = new MySqlConnection(myConnectionString);
                MySqlCommand cmd;
                connection.Open();

                cmd = connection.CreateCommand();
                string requete = "Select type_id, type_nom From type;";
                RecuperationOutilsDonnees.logger.ecrireInfoLogger("Execution de la requete : " + requete);
                cmd.CommandText = requete;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                adap.Fill(ds);

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int type_id = Convert.ToInt32(dr["type_id"].ToString());
                    string type_nom = dr["type_nom"].ToString();

                    listIdAndTypeEssence.Add(type_id, type_nom);
                }

            }
            catch (Exception e)
            {
                RecuperationOutilsDonnees.logger.ecrireInfoLogger("ERROR : " + e.StackTrace);
                return null;
            }
            RecuperationOutilsDonnees.logger.ecrireInfoLogger("Retour de " + listIdAndTypeEssence.Count + " valeur avec ID pour l'essence.");
            return listIdAndTypeEssence;
        }
    }
}