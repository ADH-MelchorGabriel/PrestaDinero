using PrestaDinero.SQLServer;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Interfaces
{
    public interface IGenericServices<T>
    {
      
        Task< Respuesta<T>> Guardar(T obj);

        Task< Respuesta<T>> Listar();

        Task<Respuesta<T>> Buscar(int id);

        Task<Respuesta<T>> Borrar(int id);

       //Task<Respuesta<T>> Modificar(T obj);
    
    }
}
