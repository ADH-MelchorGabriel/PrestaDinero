using Microsoft.AspNetCore.Mvc;
using PrestaDinero.Core;
using PrestaDinero.Core.UnidadTrabajo;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class TipoPrestamoController : Controller
    {

        public readonly IUnidadTrabajo _unidadTrabajo;

        public TipoPrestamoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IActionResult> Index()
        {
            var (_, datos) = await _unidadTrabajo.TipoPrestamo.Listar();
            return View(datos);
        }

        public async Task<IActionResult> Create(int id)
        {
            var (response, datos) = await _unidadTrabajo.TipoPrestamo.Buscar(id);
           

           var  tipoPrestamo = datos;
            if (response.Estado==false)
            {
               tipoPrestamo= new TipoPrestamoEntity
                {
                    Activo = true
                };   
            }

            return View(tipoPrestamo);
        }


        [HttpPost]
        public async Task<IActionResult> Create(TipoPrestamoEntity obj)
        {           
            if (obj.IdTipoPrestamo == 0)
            {
                var (response, _) = await _unidadTrabajo.TipoPrestamo.Guardar(obj);

                if (response.Estado)
                {
                    await _unidadTrabajo.TablaInteres.Calcular(obj.IdTipoPrestamo);
                    return RedirectToAction("Index");
                }
            }
            else
            {

                var (response, _) = await _unidadTrabajo.TipoPrestamo.Modificar(obj.IdTipoPrestamo, obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

    }
}
