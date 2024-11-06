using Dapper;
using PrestaDinero.SQLServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConexionDapper.Interfaces
{
    public interface IComandos<T> : IDisposable
    {
        Task<Respuesta<T>> EjecutarConsultaReader(string consulta);

        Task<Respuesta<T>> EjecutarProcedimientoReader(string procedimiento, DynamicParameters parametros = null);

        Task<Respuesta<T>> EjecutarConsultaNonQuery(string consulta);

        Task<Respuesta<T>> EjecutarProcedimientoNonQuery(string procedimiento, DynamicParameters parametros = null);


       
    }
}
