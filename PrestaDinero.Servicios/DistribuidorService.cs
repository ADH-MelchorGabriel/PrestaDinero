using Dapper;
using Microsoft.Extensions.Configuration;
using PrestaDinero.Core;
using PrestaDinero.SQLServer;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios
{
    public class DistribuidorService
    {
        IConfiguration _config;
        string CadenaConexion;

        public DistribuidorService(IConfiguration configuration)
        {
            _config = configuration;
            CadenaConexion = _config.GetConnectionString("Default");
        }

        public async Task<Respuesta<DistribuidorEntity>> Guardar(DistribuidorEntity obj)
        {
            Comandos<DistribuidorEntity> command = new Comandos<DistribuidorEntity>(CadenaConexion);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdUsuario", obj.IdCliente);
            parameters.Add("@Nombre", obj.Nombre);
            parameters.Add("@ApellidoPaterno", obj.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", obj.ApellidoMaterno);
            parameters.Add("@Correo", obj.Correo);
            parameters.Add("@Telefono", obj.Telefono);
            parameters.Add("@LimiteCredito", obj.LimiteCredito);
            parameters.Add("@Activo", obj.Activo);

            return await command.EjecutarProcedimientoNonQuery("spGuardarDistribuidor", parameters);
        }

        public async Task<Respuesta<DistribuidorEntity>> Listar()
        {
            Comandos<DistribuidorEntity> command = new Comandos<DistribuidorEntity>(CadenaConexion);
            var sql = $"select * from Distribuidor;";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<DistribuidorEntity>> Buscar(int id)
        {
            Comandos<DistribuidorEntity> command = new Comandos<DistribuidorEntity>(CadenaConexion);
            var sql = $"select * from Distribuidor where IdDistribuidor={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<DistribuidorEntity>> Borrar(int id)
        {
            Comandos<DistribuidorEntity> command = new Comandos<DistribuidorEntity>(CadenaConexion);
            var sql = $"delete from Distribuidor where IdDistribuidor={id};";
            return await command.EjecutarConsultaReader(sql);
        }

    }
}
