using ConexionDapper.Interfaces;
using Dapper;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Services
{
    public class ValeService : IGenericServices<ValeEntity>, IDisposable
    {
        readonly Comandos<ValeEntity> _command;
        readonly IConexion con;

        public ValeService(IConexion conexion)
        {
            con = conexion;
            _command = new Comandos<ValeEntity>(conexion);
        }


        public async Task<Respuesta<ValeEntity>> Borrar(int id)
        {
      

            ValeDetalleService d = new ValeDetalleService(con);
            await d.Borrar(id);

            var sql = $"delete from vale where IdVale={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<ValeEntity>> Buscar(int id)
        {
            ValeDetalleService d = new ValeDetalleService(con);

             var sql = $"select * from vale where IdVale={id};";
            
            
            var result =  _command.EjecutarConsultaReader(sql);


            result.Result.Contenido[0].ValeDetalle = d.Listar(id).Result.Contenido;

            return await result;
        }


        public void Dispose() => throw new NotImplementedException();

        public async Task<Respuesta<ValeEntity>> Guardar(ValeEntity obj)
        {

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@IdVale", obj.IdVale);
            parameters.Add("@Fecha", obj.Fecha);
            parameters.Add("@IdCliente", obj.IdCliente);
            parameters.Add("@IdDistribuidor", obj.IdDistribuidor);
            parameters.Add("@Quincenas", obj.Quincenas);

            parameters.Add("@Dispocision", obj.Dispocision);
            parameters.Add("@Interes", obj.Interes);
            parameters.Add("@FechaPorConvenio", obj.FechaPorConvenio);
            parameters.Add("@FechaPrimerPago", obj.FechaPrimerPago);

            return await _command.EjecutarProcedimientoNonQuery("spGuardarVale", parameters);
        }

        public async Task<Respuesta<ValeEntity>> Listar()
        {

            var sql = $"select * from Vale v inner join Cliente c on v.IdCliente = c.IdCliente; ";

            var result = _command.bd.Query<ValeEntity, ClienteEntity, ValeEntity>( sql,
                                                        (vale, cliente) =>
                                                         {
                                                            vale.Cliente = cliente;
                                                            return vale;
                                                         },splitOn: "IdCliente").ToList();


            return  new  Respuesta<ValeEntity>(true, data: result.AsList<ValeEntity>());
        }
    }
}
