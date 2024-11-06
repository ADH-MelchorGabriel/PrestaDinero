using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios;
using Seguridad.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public class Usuario: IUsuario
    {

        private string MensajeValidacion;

        UsuarioService servicio;
        private readonly IPasswordServices PasswordService;

        public Usuario(IConexion conexion, IPasswordServices passwordService)
        {
            servicio = new UsuarioService(conexion);
            PasswordService = passwordService;
        }

        public async Task<Response<UsuarioEntity>> Guardar(UsuarioEntity obj)
        {
            if (Validado(obj))
            {

                /* var passAleatorio = PasswordService.GenerarPassword(12);
                 obj.Contraseña = PasswordService.Hash(passAleatorio);
                */
                obj.Contraseña = PasswordService.Hash("Pa$$w0rd*123");
                var resultado = await servicio.Guardar(obj);

                if (resultado.EsCorrecto)
                {
                    return new Response<UsuarioEntity>(null, true, "Seguardo correctamente");
                }
                return new Response<UsuarioEntity>(null, false, resultado.Mensaje);
            }
            return new Response<UsuarioEntity>(null, false, MensajeValidacion);
        }

        public async Task< Response<UsuarioEntity>> Listar()
        {

            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
                return new Response<UsuarioEntity>(resultado.Contenido, true);
            }
            return new Response<UsuarioEntity>(null, false, "No se encontraron registros"   );
        }

        private bool Validado(UsuarioEntity obj)
        {
            MensajeValidacion = "";
            bool resultado = true;
          

            return resultado;
        }

        public async Task<Response<Claim>> Login(UserLoginEntity user)
        {

            (bool valido, UsuarioEntity UsuarioValido) validation = await IsValidUserAsync(user);

            if (validation.valido)
            {
                List<Claim> Claims = new List<Claim>()
                        {
                            new Claim("IdUsuario", validation.UsuarioValido.IdUsuario.ToString()),
                            new Claim(ClaimTypes.Role, validation.UsuarioValido.TipoRol.ToString()),
                        };

                return new Response<Claim>(Claims, true);
            }

            return new Response<Claim>(null, false, "No se encontraron registros");
        }

        private async Task<(bool, UsuarioEntity)> IsValidUserAsync(UserLoginEntity user)
        {
            var resultado = await servicio.BuscarByCorreo(user.Email);

            if (resultado.EsCorrecto)
            {
                bool isValid = PasswordService.Check(resultado.Contenido[0].Contraseña, user.Password);
                return (isValid, resultado.Contenido[0]);
            }

            return (false, null);
        }

        public async Task<Response<UsuarioEntity>> Buscar(int id)
        {
            var resultado = await servicio.Buscar(id);
            if (resultado.EsCorrecto)
            {
                return new Response<UsuarioEntity>(resultado.Contenido, true);
            }
            return new Response<UsuarioEntity>(null, false, resultado.Mensaje);
        }


        public async Task<Response<UsuarioEntity>> BuscarPorCorreo(string correo)
        {
            var resultado = await servicio.BuscarByCorreo(correo);
            if (resultado.EsCorrecto)
            {
                return new Response<UsuarioEntity>(resultado.Contenido, true);
            }
            return new Response<UsuarioEntity>(null, false, resultado.Mensaje);
        }

        public Task<Response<UsuarioEntity>> Borrar(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
