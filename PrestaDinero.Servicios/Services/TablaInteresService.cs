using ConexionDapper.Interfaces;
using Dapper;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Services
{
    public class TablaInteresService : IGenericServices<TablaInteresEntity>,IDisposable
    {

        Comandos<TablaInteresEntity> _command;

        public TablaInteresService(IConexion conexion)
        {
            _command = new Comandos<TablaInteresEntity>(conexion);
        }

        public async Task<Respuesta<TablaInteresEntity>> Borrar(int id)
        {
            var sql = $"delete from TablaInteres where IdTablaInteres={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<TablaInteresEntity>> Buscar(int id)
        {
            var sql = $"select * from TablaInteres where IdTablaInteres={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public void Dispose()
        {
            _command.Close();
        }

        public async Task<Respuesta<TablaInteresEntity>> Guardar(TablaInteresEntity obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdTablaInteres", obj.IdTablaInteres);
            parameters.Add("@Importe", obj.Importe);
            parameters.Add("@Q6", obj.Q6);
            parameters.Add("@Q8", obj.Q8);
            parameters.Add("@Q10", obj. Q10);
            parameters.Add("@Q12", obj.Q12);
            parameters.Add("@Q14", obj.Q14);
            parameters.Add("@Q16", obj.Q16);
            parameters.Add("@Q18", obj.Q18);
            parameters.Add("@Activo", obj.Activo);

            return await _command.EjecutarProcedimientoNonQuery("spGuardarTablaInteres", parameters);
        }

        public async Task<Respuesta<TablaInteresEntity>> Listar()
        {
            var sql = $"select * from TablaInteres;";
            return await _command.EjecutarConsultaReader(sql);
        }
    }
}
