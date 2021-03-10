using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Security.Cryptography;

namespace T.Negocio
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            //instancia shad256
            SHA256 sha256 = SHA256Managed.Create();
            //codificador ascii
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            //cadena mutable
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) {
                //llevar cada caracter hash a hexadecimal
                sb.AppendFormat("{0:x2}",stream[i]);
            }
            return sb.ToString();
        }
    }
}
