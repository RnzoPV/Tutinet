using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public DataTable getAllEmpleados()
        {
            return empleadoDatos.getAllEmplados();

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
                throw new Exception(e.Message);
            }
            return emp;
        }
        public void insertarEmpleado(Empleado emp)
        {
            try
            {
                empleadoDatos.insertarEmpleado(emp);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
