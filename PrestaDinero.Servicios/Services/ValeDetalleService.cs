using ConexionDapper.Interfaces;
using Dapper;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Services
{
    public class ValeDetalleService : IValeDetalleService, IDisposable
    {


        Comandos<ValeDetalleEntity> _command;

        public ValeDetalleService(IConexion conexion)
        {
            _command = new Comandos<ValeDetalleEntity>(conexion);
        }


        public async Task<Respuesta<ValeDetalleEntity>> Borrar(int id)
        {
            var sql = $"delete from ValeDetalle where idVale={id}; ";
            return await _command.EjecutarConsultaReader(sql);
        }

        public Task<Respuesta<ValeDetalleEntity>> Buscar(int id,int idDetalle)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<ValeDetalleEntity>> Guardar(ValeDetalleEntity obj)
        {

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@IdValeDetalle", obj.IdvaleDetalle);
            parameters.Add("@IdVale", obj.IdVale);
            parameters.Add("@Fecha", obj.Fecha);
            parameters.Add("@Parcialidad", obj.Parcialidad);
            parameters.Add("@Dispocision", obj.Dispocision);
            parameters.Add("@Interes", obj.Interes);
            parameters.Add("@Estatus", obj.Estatus);
  

            return await _command.EjecutarProcedimientoNonQuery("spGuardarValeDetalle", parameters);
        }

        public async Task<Respuesta<ValeDetalleEntity>> Listar(int id)
        {
            var sql = $"select * from ValeDetalle where idVale={id}; ";
            return await _command.EjecutarConsultaReader(sql);
        }
    }
}
