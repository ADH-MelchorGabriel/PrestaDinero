using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrestaDinero.SQLServer
{
    public class Respuesta<T>
    {
        public bool EsCorrecto { get; set; }

        public string Mensaje { get; set; }

        public List<T> Contenido { get; set; }

   

        public Respuesta(bool esCorrecto, string mensaje = "",List<T> data= null)
        {
            EsCorrecto = esCorrecto;
            Mensaje = mensaje;
            Contenido = data;
        }
    }
}
