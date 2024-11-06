using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrestaDinero.Core;
using PrestaDinero.Core.UnidadTrabajo;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class TablaInteresController : Controller
    {

 
        public readonly IUnidadTrabajo _unidadTrabajo;

        public TablaInteresController( IUnidadTrabajo unidadTrabajo)
        {
        
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IActionResult> Index(int IdTipoPrestamo = 1)
        {

            var (_, datos) = await _unidadTrabajo.TablaInteres.Listar();
            datos= datos.Where(x => x.IdTipoPrestamo == IdTipoPrestamo).ToList();
         

            var (_, tipoPrestamo) = await _unidadTrabajo.TipoPrestamo.Listar();      
            ViewBag.TipoPrestamos = new SelectList(tipoPrestamo, "IdTipoPrestamo", "Nombre",IdTipoPrestamo);

            return View(datos);
        }


        public async Task<IActionResult> Create(int id)
        {
            var (response, datos) = await _unidadTrabajo.TablaInteres.Buscar(id);

            var tablaInteres = datos;
            if (!response.Estado)
            {
                tablaInteres = new TablaInteresEntity()
                {
                    Activo = true
                };
              
            }

            return View(tablaInteres);
        }


        [HttpPost]
        public async Task<IActionResult> Create(TablaInteresEntity obj)
        {
            if (obj.IdTablaInteres == 0)
            {
                var (response, _) = await _unidadTrabajo.TablaInteres.Guardar(obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index",new { obj.IdTipoPrestamo});
                }
            }
            else
            {

                var (response, _) = await _unidadTrabajo.TablaInteres.Modificar(obj.IdTablaInteres, obj);

                if (response.Estado)
                {
                    return RedirectToAction("Index", new { obj.IdTipoPrestamo });
                }
            }
            return View(obj);
        }

    }
}
