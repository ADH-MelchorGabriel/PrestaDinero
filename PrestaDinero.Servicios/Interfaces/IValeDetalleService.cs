using PrestaDinero.Core;
using PrestaDinero.SQLServer;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Interfaces
{
    public interface IValeDetalleService
    {

        Task<Respuesta<ValeDetalleEntity>> Guardar(ValeDetalleEntity obj);

        Task<Respuesta<ValeDetalleEntity>> Listar(int id);

        Task<Respuesta<ValeDetalleEntity>> Buscar(int id, int idDetalle);

        Task<Respuesta<ValeDetalleEntity>> Borrar(int id);

    }
}
