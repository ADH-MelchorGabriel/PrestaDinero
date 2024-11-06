using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrestaDinero.Core;
using PrestaDinero.Core.UnidadTrabajo;
using PrestaDinero.Data.Context;
using PrestaDinero.Data.UnidadTrabajo;
using Rotativa.AspNetCore;

namespace PrestaDinero.WebDistribuidor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
       
        }

        public IConfiguration Configuration { get; }
  
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSession();


            //services.AddSingleton<IConexion>(s => {
            //  Conexion  conexion = new Conexion(Configuration.GetConnectionString("Default"));
            //    return conexion;
            //});


            services.AddDbContext<PrestaDineroContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));


           // services.Configure<PasswordOptions>(options => Configuration.GetSection("PasswordOptions").Bind(options));

            services.AddTransient<IUnidadTrabajo, UnidadTrabajo>();

            //services.AddTransient<IGeneric<EmpresaEntity>, Empresa>();
            //services.AddTransient<IUsuario, Usuario>();
            //services.AddTransient<IGeneric<DistribuidorEntity>, Distribuidor>();
            //services.AddTransient<IGeneric<ClienteEntity>, Cliente>();
            //services.AddTransient<ITablaInteres, TablaInteres>();
            //services.AddTransient<IVale, Vale>();

            //services.AddTransient<IReportesServices, ReportesService>();
            //services.AddSingleton<IPasswordServices, PasswordService>();


            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "PrestoDinero.Cookie";
                config.LoginPath = "/Home/Login";
            });


            services.AddControllersWithViews();
        }

    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
              
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            RotativaConfiguration.Setup(env.ContentRootPath, "Rotativa");
        }
    }
}
