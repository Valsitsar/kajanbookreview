using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class DataAccessBase
    {
        protected const string connString = "Server=mssqlstud.fhict.local;Database=dbi515059;User Id=dbi515059;Password=Vgo86*bbvq;";

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connString);
                return connection;
            }
            catch
            {
                throw new IOException("Database connection failed to establish.");
            }
        }
    }
}
