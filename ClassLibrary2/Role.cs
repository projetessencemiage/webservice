using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace User_Lib
{
    [DataContract]
    public class Role
    {
        [DataMember]
        public string nomRole;
        [DataMember]
        public int idRole;

        public Role(int idRole, string nomRole)
        {
            this.idRole = idRole;
            this.nomRole = nomRole;
        }

        public int getIdRole()
        {
            return idRole;
        }
    }
}
