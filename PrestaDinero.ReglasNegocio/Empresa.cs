using ConexionDapper.Interfaces;
using Microsoft.Extensions.Configuration;
using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio.Comunes;
using PrestaDinero.ReglasNegocio.Interfaces;
using PrestaDinero.Servicios;
using PrestaDinero.Servicios.Interfaces;
using System.Threading.Tasks;

namespace PrestaDinero.ReglasNegocio
{
    public class Empresa: IGeneric<EmpresaEntity>
    {

        private string MensajeValidacion;

        EmpresaService servicio;


        public Empresa(IConexion conexion)
        {
            servicio = new EmpresaService(conexion) ;
        }

        public Task<Response<EmpresaEntity>> Borrar(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<EmpresaEntity>> Buscar(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<EmpresaEntity>> Guardar(EmpresaEntity obj)
        {
            if (Validado(obj))
            {
             
                var resultado= await servicio.Guardar(obj);
                if (resultado.EsCorrecto)
                {
                    return new Response<EmpresaEntity>(null,true,"Seguardo correctamente");
                }
                return new Response<EmpresaEntity>(null, false, resultado.Mensaje);
            }
            return new Response<EmpresaEntity>(null, false, MensajeValidacion) ;
        }

        
        public async Task< Response<EmpresaEntity>> Listar()
        {
          
            var resultado = await servicio.Listar();
            if(resultado.EsCorrecto)
            {
                return new Response<EmpresaEntity>(resultado.Contenido, true);
            }
            return new Response<EmpresaEntity>(null, false,"No se encontraron registros");
        }


        private bool Validado(EmpresaEntity obj)
        {
            MensajeValidacion = "";
            bool resultado = true;
            if (string.IsNullOrEmpty(obj.Nombre))
            {
                MensajeValidacion += $"El nombre de la empresa no puede quedar en blanco\n";
                resultado = false;
            }

            return resultado;
        }


    }
}
