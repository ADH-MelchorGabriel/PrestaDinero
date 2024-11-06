using PrestaDinero.ReglasNegocio.Comunes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio.Interfaces
{
    public interface IGeneric<T>
    {
        Task<Response<T>> Guardar(T obj);
        Task<Response<T>> Listar();
        Task<Response<T>> Buscar(int id);
        Task<Response<T>> Borrar(int id);
    }
}
