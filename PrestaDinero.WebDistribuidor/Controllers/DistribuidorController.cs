using Microsoft.AspNetCore.Mvc;
using PrestaDinero.Core;
using PrestaDinero.Core.UnidadTrabajo;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class DistribuidorController : Controller
    {

        public readonly IUnidadTrabajo _unidadTrabajo;

        public DistribuidorController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IActionResult> Index()
        {
            var (_, datos) = await _unidadTrabajo.Distribuidor.Listar();
            return View(datos);
        }

        public async Task<IActionResult> Create(int id)
        {
            var (response, datos) = await _unidadTrabajo.Distribuidor.Buscar(id);
            var distribuidor = datos;
            if (!response.Estado)
            {
                 distribuidor = new DistribuidorEntity()
                {
                    Activo=true
                };
               
            }

            return View(distribuidor);
        }


        [HttpPost]
        public async Task<IActionResult> Create(DistribuidorEntity obj)
        {

            if (obj.IdDistribuidor == 0)
            {
                var (response, _) = await _unidadTrabajo.Distribuidor.Guardar(obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {

                var (response, _) = await _unidadTrabajo.Distribuidor.Modificar(obj.IdDistribuidor, obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(obj);
        }

        }
    }
