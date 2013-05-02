using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Lib
{
    public class Role
    {
        public string nomRole;
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
