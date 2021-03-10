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
        public DataTable filtrarEmpleado(string s1, string s2)
        {
            string query = $"where {s1} like '%{s2}%'";
            //string query = "where " + s1 + "="+"'"+s2+"'";
            return empleadoDatos.filtrarEmpleado(query);
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
                throw e;
            }
            return emp;
        }
        public string actualizarEmpleado(Empleado emp)
        {
            string mensaje;
            try
            {
                if (empleadoDatos.actualizarEmpleado(emp) > 0)
                {
                    mensaje = "Se actualizo con exito, la contraseña es: ";
                }
                else
                {
                    mensaje = "Algo sucedio vuelve a intentarlo mas tarde.";
                }
            }
            catch (Exception e)
            {

               mensaje =  e.Message;
            }
            return mensaje;
        }
        public string actualizarEmpleadoContrasena(Empleado emp)
        {
            string mensaje = "";
            string contraseña = GenPassword.GenerarContraseña(6);
            emp.empleado_contrasena = Encrypt.GetSHA256(contraseña);
            try
            {
                if (empleadoDatos.actualizarEmpleadoContrasena(emp)>0) {
                    mensaje = "La nueva contraseña es: " + contraseña;
                }
                else
                {
                    mensaje = "Algo sucedio vuelve a intentarlo mas tarde.";
                }

            }
            catch (Exception e)
            {
                mensaje = "Ocurrio un problema"+ e.Message;
            }
            return mensaje;
        }
        public string insertarEmpleado(Empleado emp)
        {
            string mensaje = "";
            try
            {
                string newContraseña = GenPassword.GenerarContraseña(6);
                emp.empleado_contrasena = Encrypt.GetSHA256(newContraseña);
                if (empleadoDatos.insertarEmpleado(emp)>0) {
                    mensaje = "Se guardo con exito, la contraseña es: "+ newContraseña;
                }
                else
                {
                    mensaje = "Algo sucedio vuelve a intentarlo mas tarde.";
                }
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            return mensaje;
        }
        public int eliminarEmpleado(int id)
        {
            return empleadoDatos.eliminarEmpleado(id);
        }
    }
}
