using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T.Datos;
using T.Modelo;

namespace T.Negocio
{
    public class EmpleadoNegocios
    {
        EmpleadoDatos empleadoDatos;
        public EmpleadoNegocios()
        {
            empleadoDatos = new EmpleadoDatos();
        }
        public Empleado getValidacion(string usuario, string contrasena)
        {
            Empleado emp = null;
            try
            {

                emp = empleadoDatos.getValidacion(usuario, contrasena);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error T.Negocio Empleado: " + e.Message);
            }
            return emp;
        }
    }
}
