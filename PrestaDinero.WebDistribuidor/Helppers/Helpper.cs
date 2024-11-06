using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestaDinero.WebDistribuidor.Helppers
{
    public class Helpper
    {

        //public DateTime CalcularFechaQuincenal()
        //{
            
        //    if (fecha.Day > 12 && fecha.Day <= 22)
        //    {
        //        fecha = fecha.AddMonths(1);
        //        fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-15");
        //    }
        //    else
        //    {
        //        fechaQuincena = DateTime.Parse($"{fecha.Year}-{fecha.Month}-{DiasDelMes(fecha)}");
        //    }

        //    return fechaQuincena;
        //}
        //public int DiasDelMes(DateTime fecha)
        //{
        //    int mes = fecha.Month;

        //    if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
        //    {
        //        return 31;
        //    }

        //    if (mes == 2)
        //    {
        //        if (DateTime.IsLeapYear(fecha.Year))
        //        {
        //            return 29;
        //        }

        //        return 28;
        //    }

        //    return 30;
        //}

    }
}
