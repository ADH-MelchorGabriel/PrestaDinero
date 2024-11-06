using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrestaDinero.WebDistribuidor.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       // private IUsuario _usuario;

        public HomeController(ILogger<HomeController> logger)//,IUsuario usuario)
        {
            _logger = logger;
          //  _usuario = usuario;
        }


      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(UserLoginEntity user, string returnUrl = "")
        //{
        //         Response<Claim> resultado= await _usuario.Login(user);
                      
        //    if (resultado.IsSuccess)
        //    {
        //        ClaimsIdentity PrestoDineroIdentity = new ClaimsIdentity(resultado.Data, "Presto Identity");
        //        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { PrestoDineroIdentity });

        //        await HttpContext.SignInAsync(userPrincipal);

        //        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return View(user);
        //}


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
