using Microsoft.Extensions.Options;
using Seguridad.Interfaces;
using Seguridad.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Seguridad.Services
{
    public class PasswordService : IPasswordServices
    {
        private readonly PasswordOptions _options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            string[] parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            int iterations = Convert.ToInt32(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            using (Rfc2898DeriveBytes algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations
                ))
            {
                byte[] keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {

            using (Rfc2898DeriveBytes algorithm = new Rfc2898DeriveBytes(
                password,
                _options.SaltSize,
                _options.Iterations
                ))
            {
                string key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                string salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";
            }
        }

        public  string GenerarPassword(int longitud)
        {
            string contraseña = string.Empty;
            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",};
            string[] caracteres = { "*", "/", "?", "%", "&", "#" };

            Random EleccionAleatoria = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int LetraAleatoria = EleccionAleatoria.Next(0, 150);
                int NumeroAleatorio = EleccionAleatoria.Next(0, 30);
                int CaracterAleatorio = EleccionAleatoria.Next(0, 5);

                if (LetraAleatoria < letras.Length)
                {
                    contraseña += letras[LetraAleatoria];
                }
                else if (NumeroAleatorio<=9)
                {
                    contraseña += NumeroAleatorio.ToString();
                }
                else
                {
                    contraseña += caracteres[CaracterAleatorio].ToString();
                }
            }
            return contraseña;
        }


    }
}
