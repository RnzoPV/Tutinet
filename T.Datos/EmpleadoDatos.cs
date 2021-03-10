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
    public class EmpleadoDatos :Conexion
    {
        public EmpleadoDatos() :base()
        {
        }
        public Empleado getValidacion(string usuario, string contrasena)
        {
            cn.Open();
            Empleado emp = null;
            string query = "USP_Validacion_Login";
            SqlCommand cm = new SqlCommand(query, cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@emp_usuario", usuario);
            cm.Parameters.AddWithValue("@emp_contrasena", contrasena);
            SqlDataReader dr = cm.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    emp = new Empleado();
                    emp.empleado_id = dr.GetInt32(0);
                    emp.empleado_nombre = dr.GetString(1);
                    emp.empleado_apellido = dr.GetString(2);
                    emp.empleado_estado = dr.GetInt32(10);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (cn != null)
                    cn.Close();
            }
            return emp;
        }
        public DataTable getAllEmplados()
        {
            SqlParameter[] Params = new SqlParameter[] { };
            return FDBHelper.getQuery("USP_LISTAR_CLIENTE", Params);
        }
        public DataTable filtrarEmpleado(string query)
        {
            DataTable table = null;
            try
            {

                string querybase = "SELECT * FROM V_Empleados " + query;
                table = FDBHelper.getQuery(querybase);

            }
            catch (Exception e)
            {
                throw e;
            }
            return table;
        }

        public int insertarEmpleado(Empleado emp)
        {
            int rs = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[]
                {
                    FDBHelper.ConvertSQLParameter("@empleado_nombre",SqlDbType.VarChar,0,emp.empleado_nombre),
                    FDBHelper.ConvertSQLParameter("@empleado_apellido",SqlDbType.VarChar,0,emp.empleado_apellido),
                    FDBHelper.ConvertSQLParameter("@empleado_fecnac",SqlDbType.DateTime,0,emp.empleado_fec_nac),
                    FDBHelper.ConvertSQLParameter("@empleado_tipodoc_id",SqlDbType.Int,0,emp.empleado_tipodoc_id),
                    FDBHelper.ConvertSQLParameter("@empleado_doc",SqlDbType.VarChar,0,emp.empleado_doc),
                    FDBHelper.ConvertSQLParameter("@empleado_celular",SqlDbType.VarChar,0,emp.empleado_celular),
                    FDBHelper.ConvertSQLParameter("@empleado_usuario",SqlDbType.VarChar,0,emp.empleado_usuario),
                    FDBHelper.ConvertSQLParameter("@empleado_contrasena",SqlDbType.VarChar,0,emp.empleado_contrasena),
                    FDBHelper.ConvertSQLParameter("@empleado_estado",SqlDbType.Int,0,emp.empleado_estado)
                };
                rs = FDBHelper.ExcuteNonQuery("USP_Insertar_Empleado", Params);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error en la insercion del Empleado" + e);
            }
            return rs;
        }

        public int actualizarEmpleado(Empleado emp)
        {
            int rs = 0;

            try
            {
                SqlParameter[] Params = new SqlParameter[]{
                    FDBHelper.ConvertSQLParameter("@empleado_id",SqlDbType.Int,0,emp.empleado_id),
                    FDBHelper.ConvertSQLParameter("@empleado_nombre",SqlDbType.VarChar,0,emp.empleado_nombre),
                    FDBHelper.ConvertSQLParameter("@empleado_apellido",SqlDbType.VarChar,0,emp.empleado_apellido),
                    FDBHelper.ConvertSQLParameter("@empleado_fecnac",SqlDbType.DateTime,0,emp.empleado_fec_nac),
                    FDBHelper.ConvertSQLParameter("@empleado_tipodoc_id",SqlDbType.Int,0,emp.empleado_tipodoc_id),
                    FDBHelper.ConvertSQLParameter("@empleado_doc",SqlDbType.VarChar,0,emp.empleado_doc),
                    FDBHelper.ConvertSQLParameter("@empleado_celular",SqlDbType.VarChar,0,emp.empleado_celular),
                    FDBHelper.ConvertSQLParameter("@empleado_usuario",SqlDbType.VarChar,0,emp.empleado_usuario),
                    FDBHelper.ConvertSQLParameter("@empleado_estado",SqlDbType.Int,0,emp.empleado_estado)
                };
                rs = FDBHelper.ExcuteNonQuery("SP_ACTUALIZAR_EMPLEADO_CONTRASENA", Params);
            }
            catch (SqlException e)
            {
                throw e;
            }
            return rs;
        }

        public int actualizarEmpleadoContrasena(Empleado emp)
        {
            int rs = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[]{
                    FDBHelper.ConvertSQLParameter("@empleado_id",SqlDbType.Int,0,emp.empleado_id),
                    FDBHelper.ConvertSQLParameter("@empleado_nombre",SqlDbType.VarChar,0,emp.empleado_nombre),
                    FDBHelper.ConvertSQLParameter("@empleado_apellido",SqlDbType.VarChar,0,emp.empleado_apellido),
                    FDBHelper.ConvertSQLParameter("@empleado_fecnac",SqlDbType.DateTime,0,emp.empleado_fec_nac),
                    FDBHelper.ConvertSQLParameter("@empleado_tipodoc_id",SqlDbType.Int,0,emp.empleado_tipodoc_id),
                    FDBHelper.ConvertSQLParameter("@empleado_doc",SqlDbType.VarChar,0,emp.empleado_doc),
                    FDBHelper.ConvertSQLParameter("@empleado_celular",SqlDbType.VarChar,0,emp.empleado_celular),
                    FDBHelper.ConvertSQLParameter("@empleado_usuario",SqlDbType.VarChar,0,emp.empleado_usuario),
                    FDBHelper.ConvertSQLParameter("@empleado_contrasena",SqlDbType.VarChar,0,emp.empleado_contrasena),
                    FDBHelper.ConvertSQLParameter("@empleado_estado",SqlDbType.Int,0,emp.empleado_estado)
                };
                rs = FDBHelper.ExcuteNonQuery("SP_ACTUALIZAR_EMPLEADO_CONTRASENA", Params);
            }
            catch (SqlException e)
            {
                throw e;
            }
            return rs;
        }

        public int eliminarEmpleado(int id)
        {
            int rs = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[]{
                    FDBHelper.ConvertSQLParameter("@empleado_id",SqlDbType.Int,0,id),
                };
                rs = FDBHelper.ExcuteNonQuery("USP_ELIMINAR_EMPLEADO", Params);
            }
            catch (SqlException e)
            {
                throw e;
            }
            return rs;
        }

    }
}
