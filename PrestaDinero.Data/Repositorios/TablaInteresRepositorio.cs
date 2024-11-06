using Microsoft.EntityFrameworkCore;
using PrestaDinero.Core;
using PrestaDinero.Core.Helppers;
using PrestaDinero.Core.Repositorios;
using PrestaDinero.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.Data.Repositorios
{
    public class TablaInteresRepositorio : ITablaInteresRepositorio
    {

        private readonly PrestaDineroContext _contexto;
        Respuesta respuesta = new Respuesta();

        public TablaInteresRepositorio(PrestaDineroContext context)
        {
            _contexto = context;

        }

        public async Task<(Respuesta,List<TablaInteresEntity>)> Listar()
        {
            try
            {
                var obj = _contexto.TablaInteres.ToList() ;

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

        public async Task<(Respuesta, TablaInteresEntity)> Buscar(int id)
        {
            try
            {
                var obj = await _contexto.TablaInteres.FindAsync(id);
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

        public async Task<(Respuesta, TablaInteresEntity)> Guardar(TablaInteresEntity obj)
        {
            try
            {
                _contexto.TablaInteres.Add(obj);
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

            return (respuesta, null);
        }

        public async Task<(Respuesta, TablaInteresEntity)> Modificar(int id, TablaInteresEntity obj)
        {
           try {
                _contexto.TablaInteres.Attach(obj);
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
                respuesta.Mensaje = ex.Message;
            }

            return (respuesta, null);
        }

        public async Task<(Respuesta, TablaInteresEntity)> Borrar(int id)
        {
            try
            {
                var borrar = await _contexto.TablaInteres.FindAsync(id);
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

        public async Task<Respuesta> Calcular(int IdTipoPRestamo)
        {
            try
            {

                int importe = 1000;

                for (int x = 0; x < 19; x++)
                {
                    TablaInteresEntity item = new TablaInteresEntity
                    {
                        IdTipoPrestamo = IdTipoPRestamo,
                        Importe = importe,
                        Q6 = 0,
                        Q8 = 0,
                        Q10 = 0,
                        Q12 = 0,
                        Q14 = 0,
                        Q16 = 0,
                        Q18 = 0,
                        Activo = true
                    };


                    _contexto.TablaInteres.Add(item);
                    await _contexto.SaveChangesAsync();
                    importe += 500;
                }

               
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

            return respuesta;
        }       
    }
}
