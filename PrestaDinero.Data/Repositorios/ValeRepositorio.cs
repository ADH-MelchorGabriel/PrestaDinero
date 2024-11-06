

using Microsoft.EntityFrameworkCore;
using PrestaDinero.Core;
using PrestaDinero.Core.Enumeradores;
using PrestaDinero.Core.Helppers;
using PrestaDinero.Core.Repositorios;
using PrestaDinero.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.Data.Repositorios
{
    public class ValeRepositorio : IValeRepositorio
    {


        private readonly PrestaDineroContext _contexto;
        Respuesta respuesta = new Respuesta();

        public ValeRepositorio(PrestaDineroContext context)
        {
            _contexto = context;

        }

        public async Task<(Respuesta, List<ValeEntity>)> Listar()
        {
            try
            {
                var obj = await  _contexto.Vale.Include(x=> x.Cliente)
                                          .Include(x=> x.Distribuidor)
                                          .Include(x=> x.TipoPrestamo)
                                          .Include(x=> x.ValeDetalle)
                                          .ToListAsync();

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

        public async Task<(Respuesta, ValeEntity)> Buscar(int id)
        {
            try
            {
                var obj = await _contexto.Vale
                                          .Include(x => x.Cliente)
                                          .Include(x => x.Distribuidor)
                                          .Include(x => x.TipoPrestamo)
                                          .Include(x => x.ValeDetalle)
                                          .Where(x => x.IdVale == id)
                                          .FirstOrDefaultAsync();

                if (obj == null)
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
            return (respuesta,null);
        }
   
        public async Task<(Respuesta, ValeEntity)> Guardar(ValeEntity obj)
        {

            var transaction = _contexto.Database.BeginTransaction();

                try
                {

                    var (_, detalles) = await Calcular(obj);
                    obj.ValeDetalle = detalles;

                obj.Interes = detalles.Sum(x => x.Interes);

                    _contexto.Vale.Add(obj);
                    await _contexto.SaveChangesAsync();

                       
               
                transaction.Commit();

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
                transaction.Rollback();
            

            return (respuesta, null);

        }
  
        public async Task<(Respuesta, ValeEntity)> Modificar(int id, ValeEntity obj)
        {
            try
            {
                _contexto.Vale.Attach(obj);
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

        public async  Task<(Respuesta, ValeEntity)> Borrar(int id)
        {
            var transaction = _contexto.Database.BeginTransaction();
            try
            {
               

                var borrar = await _contexto.Vale.FindAsync(id);
                if (borrar == null)              
                    throw new Excepcion("Registro no encontrado");


                var borrardetalle = await _contexto.ValeDetalle.Where(x => x.IdVale == id).ToListAsync();

                _contexto.RemoveRange(borrardetalle);
                _contexto.SaveChanges();

                _contexto.Remove(borrar);
                _contexto.SaveChanges();

                transaction.Commit();

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
            transaction.Rollback();
            return (respuesta, null);
        }

        public async Task<(Respuesta,List<ValeDetalleEntity>)> Calcular(ValeEntity vale)
        {
            var lista = new List<ValeDetalleEntity>();
            var Intereses = await GetIntereses((int)vale.Quincenas, vale.Dispocision,vale.IdTipoPrestamo);
            double abono = vale.Dispocision / (int)vale.Quincenas;

            DateTime fecha = vale.FechaPorConvenio==true ?   vale.FechaPrimerPago : vale.Fecha;

            if (!vale.FechaPorConvenio)
            {
                fecha = CalcularFechaQuincenal(fecha);
            }

            for (int i = 0; i < (int)vale.Quincenas; i++)
            {
                lista.Add(new ValeDetalleEntity
                {
                    IdVale = 0,
                    IdvaleDetalle = 0,
                    Fecha = fecha,
                    Parcialidad = i + 1,
                    Dispocision = abono,
                    Interes = Intereses,
                    SaldoAnterior=(vale.Dispocision+(Intereses * (int)vale.Quincenas))-((abono + Intereses) * i),
                    Estatus = EstatusValeEnum.Pendiente
                });

                if (fecha.Day == 15)
                {
                    if (fecha.Month==2)
                    {
                        fecha = DateTime.Parse($"{fecha.Year}-{fecha.Month}-28");
                    }
                    else
                    {
                        fecha = DateTime.Parse($"{fecha.Year}-{fecha.Month}-30");
                    }
                }
                else
                {
                    fecha = fecha.AddDays(15);
                    fecha = DateTime.Parse($"{fecha.Year}-{fecha.Month}-15");
                }
            }

            return (respuesta, lista) ;
        }

       

        private async Task< double> GetIntereses (int quincenas,double disposicion ,int idTipoPrestamo)
        {
            var x = await _contexto.TablaInteres
                                       .Where(x => x.Importe == disposicion && x.IdTipoPrestamo== idTipoPrestamo)
                                       .FirstOrDefaultAsync();

            switch (quincenas)
            {
                case 6:
                    return x.Q6/6;
                case 8:
                    return x.Q8/8;
                case 10:
                    return x.Q10/10;
                case 12:
                    return x.Q12/12;
                case 14:
                    return x.Q14/14;
                case 16:
                    return x.Q16/16;
                case 18:
                    return x.Q18/18;
                default:
                    return 0;
            }

        }
        private static DateTime CalcularFechaQuincenal(DateTime fecha)
        {
            DateTime fechaQuincena;
            if (fecha.Day > 7 && fecha.Day <= 21)
            {
                if (fecha.Month == 2)
                {
                    fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-28");
                }
                else
                {
                    fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-30");
                }
            }
            else
            {
                fecha = fecha.AddMonths(1);
                fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-15");
            }
            return fechaQuincena;
        }
        private static int DiasDelMes(DateTime fecha)
        {
            int mes = fecha.Month;

            if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
            {
                return 30;
            }

            if (mes == 2)
            {
                if (DateTime.IsLeapYear(fecha.Year))
                {
                    return 28;
                }

                return 28;
            }

            return 30;
        }

    }
}
