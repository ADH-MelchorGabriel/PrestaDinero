using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestaDinero.ReglasNegocio.Interfaces
{
    public interface IVale:IGeneric<ValeEntity>
    {
        Response<ValeDetalleEntity> CalcularParcialidades(double disposicion, double porcentajeInteres, int quicenas, DateTime fecha,bool porConvenio);
    }
}
