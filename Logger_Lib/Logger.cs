using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger_Lib
{
    public class Logger
    {
        private List<string> files;
        private string dir;
        private string classe;
        public Logger(string url_enregistrement, string classe)
        {
            this.classe = classe;
            files = new List<string>();
            DirectoryInfo fileListing = new DirectoryInfo(url_enregistrement);
            foreach (FileInfo file in fileListing.GetFiles("*"+classe+".txt"))
            {
              files.Add(fileListing.ToString() + file.Name);
            }
            if (files!=null && files.Count > 0)
            {
                files.Sort();
            }
            else
            {
                string file_s = fileListing.ToString() + "logger_" + DateTime.Now.ToString().Replace(" ", "_").Replace("/", "").Replace(":", "") + "_" + classe + ".txt";
                files.Add(file_s);
                System.IO.File.AppendAllText(files.Last(), "");
            }
            dir = fileListing.ToString();
        }

        public void ecrireInfoLogger(string info)
        {
            string infoFormate = DateTime.Now.ToString() + " : " + info + "\n";
            FileInfo f = new FileInfo(files.Last());
            long taille = f.Length;
            if (taille < 1048576)
            {
                System.IO.File.AppendAllText(files.Last(), infoFormate);
            }
            else
            {
                string file_s = dir + "logger_" + DateTime.Now.ToString().Replace(" ", "_").Replace("/", "") + "_" + classe + ".txt";
                files.Add(file_s);
                System.IO.File.AppendAllText(files.Last(), "");
                System.IO.File.AppendAllText(files.Last(), infoFormate);
            }
        }
    }
}
