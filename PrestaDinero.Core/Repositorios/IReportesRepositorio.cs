using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestaDinero.Core.Repositorios
{
    public interface IReportesRepositorio
    {
        List<ValeDetalleEntity> ListaQuincenal(DateTime fecha);
    }
}
