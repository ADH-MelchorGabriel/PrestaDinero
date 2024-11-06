using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio.Interfaces
{
    public interface IUsuario:IGeneric<UsuarioEntity>
    {
        Task<Response<Claim>> Login(UserLoginEntity user);
        Task<Response<UsuarioEntity>> BuscarPorCorreo(string correo);
    }

}
