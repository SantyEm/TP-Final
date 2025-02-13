using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TP_Final.Datos
{
    public class ConexionBD
    {
        public string cadenaDeconexion;

        public ConexionBD()
        {
            // Define the connection string directly
            cadenaDeconexion = "Server=127.0.0.1;Port=3306;User=root;Password=12345;;Database=adopta_gatitos;SslMode=None";
        }
    }

}
