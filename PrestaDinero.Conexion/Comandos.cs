
using ConexionDapper.Interfaces;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PrestaDinero.SQLServer
{
    public  class Comandos<T>: IComandos<T>,IDisposable
    {
        private IConexion conn;

      
       public SqlConnection bd;
        public Comandos(IConexion conexion)
        {
            conn = conexion;
            bd = conexion.Conectar();
            if (bd.State!= ConnectionState.Open)
            {
                bd.Open();
            }
            
        }
        

        public void Close()
        {
            bd.Close();
        }

        public void Dispose()
        {
            Close();


        }

        public async Task< Respuesta<T>> EjecutarConsultaReader(string consulta)
        {
            try
            {     
               var lst = await bd.QueryAsync<T>(consulta);
               return new Respuesta<T>(true, data: lst.AsList<T>());
            }
            catch (Exception ex)
            {
                return new Respuesta<T>(false, ex.Message);
            }
        }
    
        public async Task< Respuesta<T>> EjecutarProcedimientoReader(string procedimiento, DynamicParameters parametros = null)
        {
            try
            {           
                var result = await bd.QueryAsync<T>(procedimiento, parametros, commandType: CommandType.StoredProcedure);
                return new Respuesta<T>(true, data:result.AsList<T>() );
            }
            catch (Exception ex)
            {
                return new Respuesta<T>(false, ex.Message);
            }
        }
       
        public async Task<Respuesta<T>> EjecutarConsultaNonQuery(string consulta)
        {
            try
            {
                var result = await bd.ExecuteAsync(consulta);
                return new  Respuesta<T>(true,data:null);
            }
            catch (Exception ex)
            {
                return new Respuesta<T>(false, ex.Message);
            }
        }

        public async Task<Respuesta<T>> EjecutarProcedimientoNonQuery(string procedimiento, DynamicParameters parametros = null)
        {
            try
            {
                var result = await bd.QueryAsync<T>(procedimiento, parametros, commandType: CommandType.StoredProcedure);
                return new Respuesta<T>(true,data:result.AsList<T>());
            }
            catch (Exception ex)
            {

                return new Respuesta<T>(false, ex.Message);
            }
        }
    }
}
