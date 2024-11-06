using ConexionDapper.Interfaces;
using Dapper;
using PrestaDinero.Core;
using PrestaDinero.Servicios.Interfaces;
using PrestaDinero.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestaDinero.Servicios.Services
{
    public class ReportesService: IReportesServices
    {

        Comandos<ValeDetalleEntity> _detale;

        public ReportesService(IConexion conexion)
        {
            _detale = new Comandos<ValeDetalleEntity>(conexion);
        }

        private int DiasDelMes(DateTime fecha)
        {
            int mes = fecha.Month;

            if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
            {
                return 31;
            }

            if (mes == 2)
            {
                if (DateTime.IsLeapYear(fecha.Year))
                {
                    return 29;
                }

                return 28;
            }

            return 30;
        }

        public List<ValeDetalleEntity> ListaQuincenas( DateTime fecha ) //int tipoQuincena,int mes , int año)
        {

            string sql =$@"select vd.*,v.*,c.* from ValeDetalle vd 
                           inner join Vale v on v.IdVale=vd.IdVale
                           inner join Cliente c on v.IdCliente= c.IdCliente 
                           where  vd.Fecha='{fecha.Year}-{fecha.Month}-{fecha.Day}'";

            //if (tipoQuincena==15)
            //{
            //    sql += $" where vd.Fecha='{año}-{mes}-{tipoQuincena}'";
            //}
            //else
            //{
            //    sql += $" where vd.Fecha='{año}-{mes}-{DiasDelMes(DateTime.Parse($"{año}-{mes}-01"))}'";
            //}


            var result = _detale.bd.Query<ValeDetalleEntity, ValeEntity, ClienteEntity, ValeDetalleEntity>(sql,
                                                       (detalle, vale, cliente) =>
                                                       {
                                                           vale.Cliente = cliente;
                                                           detalle.Vale = vale;
                                                           return detalle;
                                                       },splitOn: "IdVale,IdCliente").ToList();


            return   result.AsList<ValeDetalleEntity>();


            
        }


    }
}
