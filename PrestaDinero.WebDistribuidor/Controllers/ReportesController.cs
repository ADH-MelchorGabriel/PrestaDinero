using Microsoft.AspNetCore.Mvc;
using PrestaDinero.Core.UnidadTrabajo;
using Rotativa.AspNetCore;
using System;
using System.Linq;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class ReportesController : Controller
    {
        public readonly IUnidadTrabajo _unidadTrabajo;

        public ReportesController(IUnidadTrabajo unidadTrabajo)
        {

            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaQuincenal()
        {
            ViewBag.Fecha = DateTime.Now;
            return PartialView();
        }

        [HttpPost]
        public IActionResult ListaQuincenal(DateTime fecha)
        {
            var datos = _unidadTrabajo.Reportes.ListaQuincenal(fecha);

            return new ViewAsPdf("RepListaQuincenal", datos.OrderBy(x=> x.Vale.Cliente.NombreCompleto).ToList(), viewData: ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 20, 20),
              //  CustomSwitches = "--page-offset 0 --footer-left [page] --footer-font-size 8",
               PageOrientation=Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }

        public IActionResult ImpresionVales()
        {

            return PartialView();
        }

        [HttpPost]
        public IActionResult ImpresionVales(DateTime fecha)
        {
            var datos = _unidadTrabajo.Reportes.ListaQuincenal(fecha);

            return new ViewAsPdf("RepImpresionVales", datos.OrderBy(x => x.Vale.Cliente.NombreCompleto).ToList(), viewData: ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 20, 20),
                CustomSwitches = "--page-offset 0 --footer-left [page] --footer-font-size 8",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }
   
        public IActionResult SaldoClientes()
        {
            return View();
        }
    
    
    }
}
