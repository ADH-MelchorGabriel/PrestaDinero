using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConexionDapper.Interfaces
{
    public interface IConexion
    {
        SqlConnection Conectar();
    }
}
