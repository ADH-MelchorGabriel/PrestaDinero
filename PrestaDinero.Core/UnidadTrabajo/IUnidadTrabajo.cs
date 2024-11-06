using PrestaDinero.Core.Interfaces;
using PrestaDinero.Core.Repositorios;

namespace PrestaDinero.Core.UnidadTrabajo
{
    public interface IUnidadTrabajo
    {
        IRepositorioGenerico<TipoPrestamoEntity> TipoPrestamo { get; }
        IRepositorioGenerico<ClienteEntity> Cliente { get; }
        IRepositorioGenerico<DistribuidorEntity> Distribuidor { get; }
        ITablaInteresRepositorio TablaInteres { get; }
        IValeRepositorio Vale { get; }
        IReportesRepositorio Reportes { get; }
    }
}
