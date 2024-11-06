using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.Core.Enumeradores;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public  class Vale: IVale
    {

        private string MensajeValidacion;

        ValeService servicio;
        ValeDetalleService detalle;

        public Vale(IConexion conexion)
        {
            servicio = new ValeService(conexion);
            detalle = new ValeDetalleService(conexion);
        }

        public async Task<Response<ValeEntity>> Buscar(int id)
        {
            var resultado = await servicio.Buscar(id);
            if (resultado.EsCorrecto)
            {
               if (resultado.Contenido.Count>0)
                {
                    return new Response<ValeEntity>(resultado.Contenido, true);
                }
                return new Response<ValeEntity>(null, false, "No exites regitros con el codigo proporcionado");
            }
            return new Response<ValeEntity>(null, false, resultado.Mensaje);
        }

        public async Task<Response<ValeEntity>> Guardar(ValeEntity obj)
        {
            if (Validado(obj))
            {                
                    var resultado = await servicio.Guardar(obj);
                    if (resultado.EsCorrecto)
                    {
                        
                       foreach(var item in obj.ValeDetalle)
                    {
                        item.IdVale = resultado.Contenido[0].IdVale;
                        await  detalle.Guardar(item);
                    }

                        return new Response<ValeEntity>(resultado.Contenido, true, "Seguardo correctamente");
                    }
            }
            return new Response<ValeEntity>(null, false, MensajeValidacion);
        }

        public async Task<Response<ValeEntity>> Listar()
        {
            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
                return new Response<ValeEntity>(resultado.Contenido, true);
            }
            return new Response<ValeEntity>(null, false, "No se encontraron registros");
        }

        private bool Validado(ValeEntity obj)
        {
            MensajeValidacion = "";
            bool resultado = true;
            //if (string.IsNullOrEmpty(obj.Nombre))
            //{
            //    MensajeValidacion += $"El nombre del distribuidor no puede quedar en blanco\n";
            //    resultado = false;
            //}
            return resultado;
        }

        public  Response<ValeDetalleEntity> CalcularParcialidades(double disposicion,double porcentajeInteres, int quicenas,DateTime fecha,bool porConvenio)
        {

            var lista = new List<ValeDetalleEntity>();

            double adbono = disposicion / quicenas;
            double interes = porcentajeInteres/quicenas;

            if (!porConvenio)
            {
                fecha = CalcularFechaQuincenal(fecha);
            }
           
            for (int i=0; i<quicenas;i++ )
            {
                lista.Add(new ValeDetalleEntity { 
                IdVale=0,
                IdvaleDetalle=0,
                Fecha=fecha,
                Parcialidad=i+1,
                Dispocision=adbono,
                Interes=interes,
                Estatus=EstatusValeEnum.Pendiente
                });

                if (fecha.Day==15)
                {
                    fecha = DateTime.Parse($"{fecha.Year}-{fecha.Month}-{DiasDelMes(fecha)}");    
                }
                else
                {
                    fecha = fecha.AddDays(15);
                }
            } 

            return new Response<ValeDetalleEntity>(lista, true);
        }
     
        private DateTime CalcularFechaQuincenal(DateTime fecha)
        {                         
            DateTime fechaQuincena;
           if (fecha.Day > 7 && fecha.Day <= 21)
            {
                fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-{DiasDelMes(fecha)}");
            }
            else
            {
                fecha = fecha.AddMonths(1);
                fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-15");    
            }
            return fechaQuincena;
        }
    
        private int DiasDelMes(DateTime fecha)
        {
            int mes = fecha.Month;
            
            if (mes==1 ||  mes==3 || mes==5 || mes==7|| mes==8|| mes==10 || mes==12 )
            {
                return 31;
            }

            if (mes==2)
            {
                if(DateTime.IsLeapYear(fecha.Year))
                {
                    return 29;
                }

                return 28;
            }

            return 30;
        }

        public async Task<Response<ValeEntity>> Borrar(int id)
        {
            var resultado = await servicio.Borrar(id);
            if (resultado.EsCorrecto)
            {
                if (resultado.Contenido.Count > 0)
                {
                    return new Response<ValeEntity>(resultado.Contenido, true);
                }
                return new Response<ValeEntity>(null, false, "No exites regitros con el codigo proporcionado");
            }
            return new Response<ValeEntity>(null, false, resultado.Mensaje);
        }

       
    }
}
