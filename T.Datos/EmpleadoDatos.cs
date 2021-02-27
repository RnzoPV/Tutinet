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
        public DataTable getAllEmplados()
        {
            cn.Open();
            string query = "USP_Get_Empleados";
            SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable table = new DataTable();
            adapter.Fill(table);
            cn.Close();
            return table;
        }

        public void insertarEmpleado(Empleado emp)
        {
            try
            {
                cn.Open();
                string query = "USP_Insertar_Empleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empleado_nombre", emp.empleado_nombre);
                cmd.Parameters.AddWithValue("@empleado_apellido", emp.empleado_apellido);
                cmd.Parameters.AddWithValue("@empleado_fecnac", emp.empleado_fec_nac);
                cmd.Parameters.AddWithValue("@empleado_tipodoc_id", emp.empleado_tipodoc_id);
                cmd.Parameters.AddWithValue("@empleado_doc", emp.empleado_doc);
                cmd.Parameters.AddWithValue("@empleado_celular", emp.empleado_celular);
                cmd.Parameters.AddWithValue("@empleado_usuario", emp.empleado_usuario);
                cmd.Parameters.AddWithValue("@empleado_contrasena", emp.empleado_contrasena);
                cmd.Parameters.AddWithValue("@empleado_estado", emp.empleado_estado);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
