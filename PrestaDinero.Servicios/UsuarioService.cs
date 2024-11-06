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
        IConfiguration _config;
        string CadenaConexion;

        public UsuarioService(IConfiguration configuration)
        {
            _config = configuration;
            CadenaConexion = _config.GetConnectionString("Default");
        }
        public async Task< Respuesta<UsuarioEntity>> Guardar(UsuarioEntity obj)
        {
            Comandos<UsuarioEntity> command = new Comandos<UsuarioEntity>(CadenaConexion);
 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdUsuario", obj.IdUsaurio);
            parameters.Add("@Nombre", obj.Nombre);
            parameters.Add("@Correo", obj.Correo);
            parameters.Add("@Contraseña", obj.Contraseña);
            parameters.Add("@Telefono", obj.Telefono);
            parameters.Add("@TipoRol", obj.TipoRol);
            parameters.Add("@Activo", obj.Activo);

            return await command.EjecutarProcedimientoNonQuery("spGuardarUsuario", parameters);
        }

        public async Task< Respuesta<UsuarioEntity>> Listar()
        {
            Comandos<UsuarioEntity> command = new Comandos<UsuarioEntity>(CadenaConexion);
            var sql = $"select * from Usuario;";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task<  Respuesta<UsuarioEntity>> Buscar(int id)
        {
            Comandos<UsuarioEntity> command = new Comandos<UsuarioEntity>(CadenaConexion);
            var sql = $"select * from Usuario where IdUsuario={id};";
            return await command.EjecutarConsultaReader(sql);
        }

        public async Task <Respuesta<UsuarioEntity>> Borrar(int id)
        {
            Comandos<UsuarioEntity> command = new Comandos<UsuarioEntity>(CadenaConexion);
            var sql = $"delete from Usuario where IdUsuario={id};";
            return await command.EjecutarConsultaReader(sql);
        }

       
    }
}
