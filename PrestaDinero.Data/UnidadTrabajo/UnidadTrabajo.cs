using PrestaDinero.Core;
using PrestaDinero.Core.Interfaces;
using PrestaDinero.Core.Repositorios;
using PrestaDinero.Core.UnidadTrabajo;
using PrestaDinero.Data.Context;
using PrestaDinero.Data.Repositorios;

namespace PrestaDinero.Data.UnidadTrabajo
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly PrestaDineroContext _context;

        public UnidadTrabajo(PrestaDineroContext context)
        {
            _context = context;
        }

        private readonly IRepositorioGenerico<TipoPrestamoEntity> _TipoPrestamo;
        public IRepositorioGenerico<TipoPrestamoEntity> TipoPrestamo => _TipoPrestamo  ?? new RepositorioGenerico<TipoPrestamoEntity>(_context);

        private readonly IRepositorioGenerico<ClienteEntity> _Cliente;
        public IRepositorioGenerico<ClienteEntity> Cliente => _Cliente ?? new RepositorioGenerico<ClienteEntity>(_context);

        private readonly IRepositorioGenerico<DistribuidorEntity> _Distribuidor;
        public IRepositorioGenerico<DistribuidorEntity> Distribuidor => _Distribuidor ?? new RepositorioGenerico<DistribuidorEntity>(_context);

        private readonly ITablaInteresRepositorio _TablaInteres;
        public ITablaInteresRepositorio TablaInteres => _TablaInteres ?? new TablaInteresRepositorio(_context);

        private readonly IValeRepositorio _Vale;
        public IValeRepositorio Vale => _Vale ?? new ValeRepositorio(_context);

        private readonly IReportesRepositorio _Reportes;
        public IReportesRepositorio Reportes => _Reportes ?? new ReportesRepositorio(_context);
    }
}
