using PrestaDinero.Core;
using PrestaDinero.ReglasNegocio;

using System;

namespace PrestaDinero.Consola
{
    class Program
    {
        static  void   Main(string[] args)
        {



          //  Listar();
        

        }


        private async void Listar()
        {
           Empresa empresa = new Empresa();

            empresa.IdEmpresa = 9;
            empresa.Nombre = "Melchor  castro";
            empresa.Activo = true;

            var res = await empresa.Listar();


            foreach (var item in res.Data)
            {
                Console.WriteLine(item.Nombre);
            }
        }

    }
}
