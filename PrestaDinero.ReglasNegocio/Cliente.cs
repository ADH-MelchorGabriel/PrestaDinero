using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public class Cliente : IGeneric<ClienteEntity>
    {
        private string MensajeValidacion;

        ClienteService servicio;
        IUsuario _usuario;


        public Cliente(IConexion conexion, IUsuario usuario)
        {
            servicio = new ClienteService(conexion);
            _usuario = usuario;
        }

        public Task<Response<ClienteEntity>> Borrar(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<ClienteEntity>> Buscar(int id)
        {
            var resultado = await servicio.Buscar(id);
            if (resultado.EsCorrecto)
            {
                return new Response<ClienteEntity>(resultado.Contenido, true);
            }
            return new Response<ClienteEntity>(null, false, resultado.Mensaje);
        }

        public async Task<Response<ClienteEntity>> Guardar(ClienteEntity obj)
        {
            if (Validado(obj))
            {
                var objUsuario = new UsuarioEntity()
                {
                  
                    Correo = obj.Correo,
                    TipoRol = Core.Enumeradores.TipoRolEnum.Cliente,
                    Activo = obj.Activo
                };

                var resultadoUsuario = await _usuario.Guardar(objUsuario);

                if (resultadoUsuario.IsSuccess)
                {
                    var user = await _usuario.BuscarPorCorreo(obj.Correo);
                    
                    var resultado = await servicio.Guardar(obj);
                    if (resultado.EsCorrecto)
                    {
                        return new Response<ClienteEntity>(null, true, "Seguardo correctamente");
                    }
                    return new Response<ClienteEntity>(null, false, resultado.Mensaje);
                }

                return new Response<ClienteEntity>(null, false, resultadoUsuario.Message);
            }
            return new Response<ClienteEntity>(null, false, MensajeValidacion);
        }


        public async Task<Response<ClienteEntity>> Listar()
        {

            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
                return new Response<ClienteEntity>(resultado.Contenido, true);
            }
            return new Response<ClienteEntity>(null, false, "No se encontraron registros");
        }


        private bool Validado(ClienteEntity obj)
        {
            MensajeValidacion = "";
            bool resultado = true;
            if (string.IsNullOrEmpty(obj.Nombre))
            {
                MensajeValidacion += $"El nombre del distribuidor no puede quedar en blanco\n";
                resultado = false;
            }

            if (string.IsNullOrEmpty(obj.ApellidoPaterno))
            {
                MensajeValidacion += $"El apellido paterno no puede quedar en blanco\n";
                resultado = false;
            }

            if (string.IsNullOrEmpty(obj.Correo))
            {
                obj.Correo = $"{obj.ApellidoPaterno}{obj.ApellidoMaterno}{obj.Nombre.Replace(' ','_')}@PrestoDinero.com";
            }




            return resultado;
        }
    }
}
