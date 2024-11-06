using ConexionDapper.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios
{
    public class UsuarioService : IGenericServices<UsuarioEntity>
    {
        Comandos<UsuarioEntity> command;

        public UsuarioService(IConexion conexion)
        {
            command = new Comandos<UsuarioEntity>(conexion);
        }

        public async Task< Respuesta<UsuarioEntity>> Guardar(UsuarioEntity obj)
        {
           
 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdUsuario", obj.IdUsuario);
            parameters.Add("@Correo", obj.Correo);
            parameters.Add("@Contraseña", obj.Contraseña);
            parameters.Add("@TipoRol", obj.TipoRol);
            parameters.Add("@Activo", obj.Activo);

            return await command.EjecutarProcedimientoNonQuery("spGuardarUsuario", parameters);
        }

        public async Task< Respuesta<UsuarioEntity>> Listar()
        {
            
            var sql = $"select * from Usuario;";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<  Respuesta<UsuarioEntity>> Buscar(int id)
        {
           
            var sql = $"select * from Usuario where IdUsuario={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task <Respuesta<UsuarioEntity>> Borrar(int id)
        {

            var sql = $"delete from Usuario where IdUsuario={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<Respuesta<UsuarioEntity>> BuscarByCorreo(string correo)
        {

            var sql = $"select * from Usuario where correo='{correo}';";
            return await command.EjecutarConsultaReader(sql);
        }

    }
}
