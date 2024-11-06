using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public class Distribuidor : IGeneric<DistribuidorEntity>
    {
        private string MensajeValidacion;

        DistribuidorService servicio;
        IUsuario  _usuario;


        public Distribuidor(IConexion conexion, IUsuario usuario)
        {
            servicio = new DistribuidorService(conexion);
            _usuario = usuario;
        }

        public Task<Response<DistribuidorEntity>> Borrar(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<DistribuidorEntity>> Buscar(int id)
        {
            var resultado = await servicio.Buscar(id);
            if (resultado.EsCorrecto)
            {
                return new Response<DistribuidorEntity>(resultado.Contenido, true);
            }
            return new Response<DistribuidorEntity>(null, false, resultado.Mensaje);
        }

        public async Task<Response<DistribuidorEntity>> Guardar(DistribuidorEntity obj)
        {
            if (Validado(obj))
            {
                var objUsuario = new UsuarioEntity()
                {
                    Correo=obj.Correo,
                    TipoRol=Core.Enumeradores.TipoRolEnum.Distribuidor,
                    Activo=obj.Activo
                };

                var resultadoUsuario = await _usuario.Guardar(objUsuario);

                if (resultadoUsuario.IsSuccess)
                {
                   var user = await _usuario.BuscarPorCorreo(obj.Correo);
                    var resultado = await servicio.Guardar(obj);
                    if (resultado.EsCorrecto)
                    {
                        return new Response<DistribuidorEntity>(null, true, "Seguardo correctamente");
                    }
                    return new Response<DistribuidorEntity>(null, false, resultado.Mensaje);
                }
                                
                return new Response<DistribuidorEntity>(null, false, resultadoUsuario.Message);
            }
            return new Response<DistribuidorEntity>(null, false, MensajeValidacion);
        }


        public async Task<Response<DistribuidorEntity>> Listar()
        {

            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
                return new Response<DistribuidorEntity>(resultado.Contenido, true);
            }
            return new Response<DistribuidorEntity>(null, false, "No se encontraron registros");
        }


        private bool Validado(DistribuidorEntity obj)
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
                MensajeValidacion += $"El correo no puede quedar en blanco\n";
                resultado = false;
            }

            if (string.IsNullOrEmpty(obj.Telefono))
            {
                MensajeValidacion += $"El telefono no puede quedar en blanco\n";
                resultado = false;
            }


         

            return resultado;
        }
    }
}
