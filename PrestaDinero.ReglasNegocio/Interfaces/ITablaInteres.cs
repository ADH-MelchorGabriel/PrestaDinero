using PrestaDinero.Core;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio.Interfaces
{
    public interface ITablaInteres: IGeneric<TablaInteresEntity>
    {
        Task<double> GetPorcentaje(int quincenas, double disposicion);
    }
}
