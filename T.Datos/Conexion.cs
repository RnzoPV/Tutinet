using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data.SqlClient;

namespace T.Datos
{
    public class Conexion
    {
        string cadenaConexion = "Persist Security Info=false;Integrated Security = true;" +
            "Initial Catalog = TutinetDB; server = DESKTOP-SALICIE\\SQLRISK";
        protected SqlConnection cn;
       
        public Conexion()
        {
            cn = new SqlConnection(cadenaConexion);
        }

    }
}
