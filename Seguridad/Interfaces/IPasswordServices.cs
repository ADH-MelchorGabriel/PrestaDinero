using System;
using System.Collections.Generic;
using System.Text;

namespace Seguridad.Interfaces
{
    public interface IPasswordServices
    {
        string Hash(string password);
        bool Check(string hash, string password);
        string GenerarPassword(int longitud);
    }
}
