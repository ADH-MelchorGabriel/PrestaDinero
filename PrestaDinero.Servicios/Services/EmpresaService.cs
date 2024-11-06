using ConexionDapper.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios
{
    public class EmpresaService : IGenericServices<EmpresaEntity>
    {  
         Comandos<EmpresaEntity> _command;
      
        public EmpresaService(IConexion conexion)
        {
            _command = new Comandos<EmpresaEntity>(conexion);
        }

        public async Task< Respuesta<EmpresaEntity> >Guardar(EmpresaEntity obj)
        {
   
            DynamicParameters parameters = new DynamicParameters();
          
            parameters.Add("@IdEmpresa", obj.IdEmpresa);
            parameters.Add("@Nombre", obj.Nombre);
            parameters.Add("@Activo", obj.Activo );

            return await _command.EjecutarProcedimientoNonQuery("spGuardarEmpresa", parameters);
        }

        public async Task< Respuesta<EmpresaEntity>> Listar()
        {
          
            var sql = $"select * from Empresa;";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task< Respuesta<EmpresaEntity>> Buscar(int id)
        {
           
            var sql = $"select * from Empresa where idEmpresa={id};";
            return await _command.EjecutarConsultaReader(sql);
        }

        public async Task <Respuesta<EmpresaEntity>> Borrar(int id)
        {
          
            var sql = $"delete from Empresa where idEmpresa={id};";
            return await _command.EjecutarConsultaReader(sql);
        }       
    }
}
