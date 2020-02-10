using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenances
{
    public class DbConexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection cn = new SqlConnection("Data Source=FRANKLIN-PC\\MSSQLSERVERR;Initial Catalog=DBMovie;Integrated Security=True");

            return cn;
        }
    }
}
