using ConexionDapper.Interfaces;
using System.Data.SqlClient;

namespace PrestaDinero.SQLServer
{
    public class Conexion: IConexion
    {
     
        private SqlConnection con=null;

        string _cadenaConexion;

        public Conexion( string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;      
        }

       

        public SqlConnection Conectar()
        {
            if (con == null)
            {
                con = new SqlConnection(_cadenaConexion);
            }
            return con;
        }

        //public SqlConnection GetCon()
        //{
        //    return con;
        //}

        //public void CerrarConexion()
        //{
        //    objConexion = null;
        //}

    }
}