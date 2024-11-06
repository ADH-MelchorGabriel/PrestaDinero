using Microsoft.AspNetCore.Mvc;
using PrestaDinero.Core;
using PrestaDinero.Core.UnidadTrabajo;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class ClienteController : Controller
    {

        public readonly IUnidadTrabajo _unidadTrabajo;

        public ClienteController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IActionResult> Index()
        {
            var (_, datos) = await _unidadTrabajo.Cliente.Listar();
            return View(datos.OrderBy(x=> x.Nombre).ThenBy(x=> x.ApellidoPaterno).ThenBy(x=> x.ApellidoMaterno).ToList());
        }

        public async Task<IActionResult> Create(int id)
        {
            var (response,datos) = await _unidadTrabajo.Cliente.Buscar(id);

            var cliente = datos;

            if (!response.Estado)
            {
                cliente = new ClienteEntity()
                {
                    Activo = true
                };
               
            }

            return View(cliente);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ClienteEntity obj)
        {
         
           

            if (obj.IdCliente==0)
            {
                var (response, _) = await _unidadTrabajo.Cliente.Guardar(obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {

                var (response, _) = await _unidadTrabajo.Cliente.Modificar(obj.IdCliente,obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index");
                }
            }


            return View(obj);
        }
    }
}
