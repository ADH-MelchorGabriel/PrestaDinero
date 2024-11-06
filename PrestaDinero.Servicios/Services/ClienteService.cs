using ConexionDapper.Interfaces;
using Dapper;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios
{
    public class ClienteService : IGenericServices<ClienteEntity>, IDisposable
    {

        Comandos<ClienteEntity> _command;

        public ClienteService(IConexion conexion)
        {
            _command = new Comandos<ClienteEntity>(conexion);
        }

        public async Task<Respuesta<ClienteEntity>> Guardar(ClienteEntity obj)
        {
            

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdCliente", obj.IdCliente);
            parameters.Add("@Nombre", obj.Nombre);
            parameters.Add("@ApellidoPaterno", obj.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", obj.ApellidoMaterno);
            parameters.Add("@Correo", obj.Correo);
            parameters.Add("@Telefono", obj.Telefono);
            parameters.Add("@LimiteCredito", obj.LimiteCredito);
            parameters.Add("@Activo", obj.Activo);

            return await _command.EjecutarProcedimientoNonQuery("spGuardarCliente", parameters);
        }

        public async Task<Respuesta<ClienteEntity>> Listar()
        {
 
            var sql = $"select * from cliente;";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<ClienteEntity>> Buscar(int id)
        {

            var sql = $"select * from Cliente where IdCliente={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<ClienteEntity>> Borrar(int id)
        {
           
            var sql = $"delete from Cliente where IdCliente={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public void Dispose()
        {
            _command.Close();
        }
    }
}

