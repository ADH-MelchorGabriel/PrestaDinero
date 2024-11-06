using PrestaDinero.Core.Helppers;
using PrestaDinero.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestaDinero.Core.Repositorios
{
    public interface IValeRepositorio : IRepositorioGenerico<ValeEntity>
    {
        Task<(Respuesta,List<ValeDetalleEntity>)> Calcular(ValeEntity vale);
    }
}
