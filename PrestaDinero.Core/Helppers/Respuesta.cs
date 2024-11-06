using System.Collections.Generic;

namespace PrestaDinero.Core.Helppers
{
    public class Respuesta
    {

        public bool Estado { get; set; }
        public string Mensaje { get; set; }

        public Respuesta()
        {
            Estado = true;
            Mensaje = "Sin errores";
        }
        public Respuesta(bool estado, string mensaje)
        {
            Estado = estado;
            Mensaje = mensaje;
        }

    }
}
