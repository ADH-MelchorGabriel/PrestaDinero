using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrestaDinero.Core;
using PrestaDinero.Core.DTOS;
using PrestaDinero.Core.UnidadTrabajo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    [Route("{Controller}/{action}")]
    public class ValeController : Controller
    {

        public readonly IUnidadTrabajo _unidadTrabajo;

        public ValeController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public async Task<IActionResult> Index()
        {
           var (_,datos) = await _unidadTrabajo.Vale.Listar();
            return View(datos.Where(x => x.EstaPagado == false).OrderBy(x=> x.Fecha).ToList());
        }



        public async Task<IActionResult> Create(int id =0)
        {
            var (_, vale) = await _unidadTrabajo.Vale.Buscar(id);

            if (vale==null)
            { 
                 vale = new ValeEntity  
                {
                    Dispocision = 1000,
                    Fecha = DateTime.Now,
                    FechaPrimerPago = DateTime.Now,
                    IdDistribuidor = 1,
                };
            }
            var (_,clientes) = await _unidadTrabajo.Cliente.Listar();
            var (_, tipoPrestamos) = await _unidadTrabajo.TipoPrestamo.Listar();

            ViewBag.Clientes = new SelectList(clientes.Where(x => x.Activo).OrderBy(x => x.NombreCompleto).ToList(), "IdCliente", "NombreCompleto");
            ViewBag.TipoPrestamos = new SelectList(tipoPrestamos.Where(x => x.Activo).ToList(), "IdTipoPrestamo", "Nombre");
            return View(vale);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ValeEntity vale)
        {


            await _unidadTrabajo.Vale.Guardar(vale);
            return RedirectToAction("index");
        }


        [HttpPost]
        public async Task<IActionResult> Calcular(ValeEntity obj)
        {

            var ( _, detalles) = await _unidadTrabajo.Vale.Calcular(obj);
            obj.ValeDetalle = detalles;

            var (_, clientes) = await _unidadTrabajo.Cliente.Listar();
            var (_, tipoPrestamos) = await _unidadTrabajo.TipoPrestamo.Listar();

            ViewBag.Clientes = new SelectList(clientes.Where(x => x.Activo).OrderBy(x => x.NombreCompleto).ToList(), "IdCliente", "NombreCompleto");
            ViewBag.TipoPrestamos = new SelectList(tipoPrestamos.Where(x => x.Activo).ToList(), "IdTipoPrestamo", "Nombre");
            return View("Create", obj);
        }






        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var v = await _unidadTrabajo.Vale.Buscar(id);

            var updateVale = new UpdateVale
            {
                IdVale = v.datos.IdVale,
                IdCliente = v.datos.IdCliente,
                FechaDisposicion = v.datos.Fecha
            };

            var (_, clientes) = await _unidadTrabajo.Cliente.Listar();

            ViewBag.Clientes = new SelectList(clientes.Where(x => x.Activo).OrderBy(x => x.NombreCompleto).ToList(), "IdCliente", "NombreCompleto");

            return View(updateVale);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateVale obj)
        {

            var vale = await _unidadTrabajo.Vale.Buscar(obj.IdVale);

            vale.datos.Fecha = obj.FechaDisposicion;
            vale.datos.IdCliente = obj.IdCliente;

            await _unidadTrabajo.Vale.Modificar(obj.IdVale, vale.datos);

            return RedirectToAction("Index");
        }



        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var (_, vale) = await _unidadTrabajo.Vale.Buscar(id);
            return PartialView(vale);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> Delete(ValeEntity obj)
        {
            await _unidadTrabajo.Vale.Borrar(obj.IdVale);
            return RedirectToAction("Index");
        }






    }
}
