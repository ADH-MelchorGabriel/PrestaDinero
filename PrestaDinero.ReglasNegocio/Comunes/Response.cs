using System;
using System.Collections.Generic;
using System.Text;

namespace PrestaDinero.ReglasNegocio.Comunes
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<T> Data { get; set; }

        //public Response(T data,bool esCorrecto=true, string mensaje = "")
        //{
        //    IsSuccess = esCorrecto;
        //    Message = mensaje;
        //    Data.Add( data);

        //}

        public Response(List<T> data, bool esCorrecto = true, string mensaje = "")
        {
            IsSuccess = esCorrecto;
            Message = mensaje;
            Data=data;

        }

    }
}
