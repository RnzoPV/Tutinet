using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T.Negocio
{
    public static class GenPassword
    {
        public static string GenerarContraseña(int longitud, bool lcase = true, bool ucase = true, bool num = true)
        {
            string[] letrasMin = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

            string[] letrasMay = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string contraseña = string.Empty;
            List<string> lstopciones = new List<string>();
            Random rdm = new Random();
            if (lcase)
            {   
                lstopciones.Add("lcase");
            }
            if (ucase)
            {
                lstopciones.Add("ucase");
            }
            if (num)
            {
                lstopciones.Add("num");
            }

            for (int i=0; i < longitud; i++)
            {
               string cur = lstopciones[rdm.Next(0, lstopciones.Count())];
                if (cur == "lcase") {
                    int nLMin = rdm.Next(0, letrasMin.Length);
                    contraseña += letrasMin[nLMin];
                }
                else if (cur== "ucase") {
                    int nLMay = rdm.Next(0, letrasMay.Length);
                    contraseña += letrasMay[nLMay];
                }
                else
                {
                    int numAleatorio = rdm.Next(0, 9);
                    contraseña += numAleatorio.ToString();
                }
            }
            return contraseña;
        }
    }
}
