using Microsoft.EntityFrameworkCore;
using PrestaDinero.Core;
using PrestaDinero.Core.Repositorios;
using PrestaDinero.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestaDinero.Data.Repositorios
{
    public class ReportesRepositorio : IReportesRepositorio
    {

        private readonly PrestaDineroContext _contexto;
        

        public ReportesRepositorio(PrestaDineroContext context)
        {
            _contexto = context;

        }

        public List<ValeDetalleEntity> ListaQuincenal(DateTime fecha)
        {

            try
            {
                var listaQuincenal =  _contexto.ValeDetalle
                                               .Include(x => x.Vale)
                                               .ThenInclude(x => x.Cliente)
                                               .Where(x => x.Fecha.Date == fecha.Date)
                                               .ToList();

                return listaQuincenal;
            }
            catch(Exception ex)
            {
                var er = ex.Message;
            }

            return null;
        }
    }
}
