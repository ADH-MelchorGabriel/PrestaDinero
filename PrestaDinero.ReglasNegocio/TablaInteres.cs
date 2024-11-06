using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public class TablaInteres : ITablaInteres
    {

        private string MensajeValidacion;

        TablaInteresService servicio;


        public TablaInteres(IConexion conexion)
        {
            servicio = new TablaInteresService(conexion);
        }

        public Task<Response<TablaInteresEntity>> Borrar(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<TablaInteresEntity>> Buscar(int id)
        {
            var resultado = await servicio.Buscar(id);
            if (resultado.EsCorrecto)
            {
                return new Response<TablaInteresEntity>(resultado.Contenido, true);
            }
            return new Response<TablaInteresEntity>(null, false, "No se encontraron registros");
        }

        public async Task<double> GetPorcentaje(int quincenas, double disposicion)
        {

            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
              var x=  resultado.Contenido.Where(l => l.Importe == disposicion).FirstOrDefault();
              
                switch (quincenas)
                {
                    case 6:
                        return x.Q6;
                    case 8:
                        return x.Q8;                  
                    case 10:
                        return x.Q10;                    
                    case 12:
                        return x.Q12;
                    case 14:
                        return x.Q14;                    
                    case 16:
                        return x.Q16;                   
                    case 18:
                        return x.Q18;
                    default :
                        return 0;
                }
            }
            return 0;
        }

        public async Task<Response<TablaInteresEntity>> Guardar(TablaInteresEntity obj)
        {
            if (Validado(obj))
            {

                var resultado = await servicio.Guardar(obj);
                if (resultado.EsCorrecto)
                {
                    return new Response<TablaInteresEntity>(null, true, "Seguardo correctamente");
                }
                return new Response<TablaInteresEntity>(null, false, resultado.Mensaje);
            }
            return new Response<TablaInteresEntity>(null, false, MensajeValidacion);
        }

        public async Task<Response<TablaInteresEntity>> Listar()
        {
            var resultado = await servicio.Listar();
            if (resultado.EsCorrecto)
            {
                return new Response<TablaInteresEntity>(resultado.Contenido, true);
            }
            return new Response<TablaInteresEntity>(null, false, "No se encontraron registros");
        }


        private bool Validado(TablaInteresEntity obj)
        {
            MensajeValidacion = "";
            bool resultado = true;


            return resultado;
        }

    }
}
