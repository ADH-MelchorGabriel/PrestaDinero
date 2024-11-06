using ConexionDapper.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios
{
    public class DistribuidorService : IGenericServices<DistribuidorEntity>, IDisposable
    {
        Comandos<DistribuidorEntity> command;
    
        public DistribuidorService(IConexion conexion)
        {
            command = new Comandos<DistribuidorEntity>(conexion);
        }

        public async Task<Respuesta<DistribuidorEntity>> Guardar(DistribuidorEntity obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdDistribuidor", obj.IdDistribuidor);
            parameters.Add("@Nombre", obj.Nombre);
            parameters.Add("@ApellidoPaterno", obj.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", obj.ApellidoMaterno);
            parameters.Add("@Correo", obj.Correo);
            parameters.Add("@Telefono", obj.Telefono.Replace("-",""));
            parameters.Add("@LimiteCredito", obj.LimiteCredito);
            parameters.Add("@Activo", obj.Activo);

            return await command.EjecutarProcedimientoNonQuery("spGuardarDistribuidor", parameters);
        }

        public async Task<Respuesta<DistribuidorEntity>> Listar()
        {

            var sql = $"select * from Distribuidor;";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<DistribuidorEntity>> Buscar(int id)
        {

            var sql = $"select * from Distribuidor where IdDistribuidor={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<DistribuidorEntity>> Borrar(int id)
        {

            var sql = $"delete from Distribuidor where IdDistribuidor={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public void Dispose()
        {
            command.Close();
        }
    }
}
