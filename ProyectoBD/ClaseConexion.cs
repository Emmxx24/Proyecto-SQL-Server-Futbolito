using System.Data.SqlClient;

namespace ProyectoBD
{
    public  class ClaseConexion
    {
        private string connectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Futbolito;Integrated Security=True";

        public SqlConnection conectar()
        {
            return new SqlConnection(connectionString);
        }
    }
}