using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using T.Modelo;

namespace T.Datos
{
    public class EmpleadoDatos : Conexion
    {
        public EmpleadoDatos() : base() {
        
        } 
        public Empleado getValidacion(string usuario,string contrasena)
        {
            cn.Open();
            Empleado emp = null ;
            string query = "USP_Validacion_Login";
            SqlCommand cm = new SqlCommand(query, cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@emp_usuario",usuario);
            cm.Parameters.AddWithValue("@emp_contrasena",contrasena);
            SqlDataReader dr = cm.ExecuteReader();
            try {
                while (dr.Read()) {
                    emp = new Empleado();
                    emp.empleado_id = dr.GetInt32(0);
                    emp.empleado_nombre = dr.GetString(1);
                    emp.empleado_apellido = dr.GetString(2);
                    emp.empleado_estado = dr.GetInt32(10);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Empleado Datos"+e.Message);
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return emp;
        }
    }
}
