using PrestaDinero.Core;
using System;
using System.Collections.Generic;

namespace PrestaDinero.Servicios.Interfaces
{
    public interface IReportesServices
    {
        List<ValeDetalleEntity> ListaQuincenas(DateTime fecha);//int tipoQuincena, int mes, int año);
    }
}
