using PrestaDinero.Core.Helppers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestaDinero.Core.Interfaces
{
    public interface IRepositorioGenerico<T> 
    {
        Task<(Respuesta respuesta,T datos)> Guardar(T obj);
        Task<(Respuesta respuesta, List<T> datos)> Listar();
        Task<(Respuesta respuesta, T datos)> Modificar(int id, T obj);
        Task<(Respuesta respuesta, T datos)> Buscar(int id);
        Task<(Respuesta respuesta, T datos)> Borrar(int id);
    }
}
