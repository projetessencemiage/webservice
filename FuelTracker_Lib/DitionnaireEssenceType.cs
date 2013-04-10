using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelTracker_Lib
{
    public class DitionnaireEssenceType
    {
        Dictionary<int, string> essenceTypeValue;
        Dictionary<string, int> essenceTypeKey;

        public DitionnaireEssenceType()
        {
            essenceTypeValue = new Dictionary<int, string>();
            essenceTypeValue.Add(0, "diesel excellium");
            essenceTypeValue.Add(1, "diesel");
            essenceTypeValue.Add(2, "SP95-E10");
            essenceTypeValue.Add(3, "SP98");
            essenceTypeValue.Add(4, "GPL");

            essenceTypeKey = new Dictionary<string, int>();
            essenceTypeKey.Add("diesel excellium",0);
            essenceTypeKey.Add("diesel",1);
            essenceTypeKey.Add("SP95-E10",2);
            essenceTypeKey.Add("SP98",3);
            essenceTypeKey.Add("GPL",4);
        }

        public int getKey(string s)
        {
            int retour;
            bool b = essenceTypeKey.TryGetValue(s, out retour);
            if (b)
            {
                return retour;
            }
            return -1;
        }
    }
}
