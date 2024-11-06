using ConexionDapper.Interfaces;
using PrestaDinero.Core;
using PrestaDinero.SQLServer;
using Seguridad.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestaDinero.Servicios.Services
{
   public  class UserLoginService
    {
        Comandos<UserLoginEntity> _command;

        public UserLoginService(IConexion conexion)
        {
            _command = new Comandos<UserLoginEntity>(conexion);
        }




       
    }
}
