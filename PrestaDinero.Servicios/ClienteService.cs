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
    public class ClienteService : IGenericServices<ClienteEntity>
    {

        //IConfiguration _config;
        //string CadenaConexion;

        //public ClienteService(IConfiguration configuration)
        //{
        //    _config = configuration;
        //    CadenaConexion = _config.GetConnectionString("Default");
        //}

        //public async Task<Respuesta<ClienteEntity>> Guardar(ClienteEntity obj)
        //{
        //    Comandos<ClienteEntity> command = new Comandos<ClienteEntity>(CadenaConexion);

        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@IdUsuario", obj.IdCliente);
        //    parameters.Add("@Nombre", obj.Nombre);
        //    parameters.Add("@ApellidoPaterno", obj.ApellidoPaterno);
        //    parameters.Add("@ApellidoMaterno", obj.ApellidoMaterno);
        //    parameters.Add("@Correo", obj.Correo);
        //    parameters.Add("@Telefono", obj.Telefono);
        //    parameters.Add("@LimiteCredito", obj.LimiteCredito);
        //    parameters.Add("@Activo", obj.Activo);

        //    return await command.EjecutarProcedimientoNonQuery("spGuardarCliente", parameters);
        //}

        //public async Task<Respuesta<ClienteEntity>> Listar()
        //{
        //    Comandos<ClienteEntity> command = new Comandos<ClienteEntity>(CadenaConexion);
        //    var sql = $"select * from Usuario;";
        //    return await command.EjecutarConsultaReader(sql);
        //}

        //public async Task<Respuesta<ClienteEntity>> Buscar(int id)
        //{
        //    Comandos<ClienteEntity> command = new Comandos<ClienteEntity>(CadenaConexion);
        //    var sql = $"select * from Usuario where IdUsuario={id};";
        //    return await command.EjecutarConsultaReader(sql);
        //}

        //public async Task<Respuesta<ClienteEntity>> Borrar(int id)
        //{
        //    Comandos<ClienteEntity> command = new Comandos<ClienteEntity>(CadenaConexion);
        //    var sql = $"delete from Usuario where IdUsuario={id};";
        //    return await command.EjecutarConsultaReader(sql);
        //}

    }
}
