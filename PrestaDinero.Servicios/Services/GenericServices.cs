using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Services
{
    public class GenericServices<T> : IGenericServices<T> where T : class
    {
        public Task<Respuesta<T>> Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<T>> Buscar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<T>> Guardar(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<T>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<T>> Modificar(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
