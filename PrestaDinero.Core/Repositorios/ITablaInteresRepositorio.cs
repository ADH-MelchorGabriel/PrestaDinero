using PrestaDinero.Core.Helppers;
using PrestaDinero.Core.Interfaces;
using System.Threading.Tasks;

namespace PrestaDinero.Core.Repositorios
{
    public interface ITablaInteresRepositorio :IRepositorioGenerico<TablaInteresEntity>
    {
        Task<Respuesta> Calcular(int IdTipoPRestamo);
    }
}
