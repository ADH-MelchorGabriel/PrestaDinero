using Microsoft.EntityFrameworkCore;
using PrestaDinero.Core.Helppers;
using PrestaDinero.Core.Interfaces;
using PrestaDinero.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestaDinero.Data.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly PrestaDineroContext _contexto;
        private readonly DbSet<T> Entidad;
        Respuesta respuesta = new Respuesta();
        public RepositorioGenerico(PrestaDineroContext context)
        {
            _contexto = context;
            Entidad = _contexto.Set<T>();

        }

        public async Task<(Respuesta,List<T>)> Listar()
        {

            try
            {
                var obj = await Entidad.ToListAsync();

                if (obj.Count == 0)
                    throw new Excepcion("No existen registros");

                return (respuesta, obj);

            }
            catch (Excepcion ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
            }
            return (respuesta, null);
        }

        public async Task<(Respuesta,T)> Buscar(int id)
        {
            try
            {
                var obj = await Entidad.FindAsync(id);
                if (obj == null)
                    throw new Exception("Registro no encontrado");

                return (respuesta, obj);
            }
            catch (Excepcion ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;

            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;    
            }


            return (respuesta, null);
        }

        public async Task<(Respuesta, T)> Guardar(T obj)
        {
            try
            {
                Entidad.Add(obj);
                await _contexto.SaveChangesAsync();

                return (respuesta, obj);

            }
            catch (Excepcion ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
            }

            return (respuesta,null);
        }

        public async Task<(Respuesta, T)> Modificar(int id, T obj)
        {
            try
            {                
                Entidad.Attach(obj);
                _contexto.Entry(obj).State = EntityState.Modified;
                 await _contexto.SaveChangesAsync();

                return (respuesta, obj); 
            }
            catch (Excepcion ex)
            {
                        
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;

            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje =ex.Message;
            }

            return (respuesta, null);
        }

        public async Task<(Respuesta, T)> Borrar(int id)
        {

            try
            {
                var borrar = await Entidad.FindAsync(id);
                if (borrar == null)
                    throw new Excepcion("Registro no encontrado");
               
                _contexto.Remove(borrar);
                _contexto.SaveChanges();

                return (respuesta, borrar);
            }
            catch (Excepcion ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
                
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
               
            }

            return (respuesta, null);
        }

      

      
    }
}
