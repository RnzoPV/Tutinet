using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace T.Datos
{
    public class FDBHelper
    {
     static string cadenaConexion = "Persist Security Info=false;Integrated Security = true;" +
    "Initial Catalog = TutinetDB; server = DESKTOP-SALICIE\\SQLRISK";
        public FDBHelper()
        {
        }

        public static SqlParameter ConvertSQLParameter(string paraName, SqlDbType dbType,int size,object objValue)
        {
            SqlParameter param;
            try
            {

                if (size > 0)
                    param = new SqlParameter(paraName, dbType, size);
                else
                    param = new SqlParameter(paraName, dbType);
                param.Value = objValue;
            }
            catch (Exception e)
            {

                throw e;
            }
            return param;
        }
        public static DataTable getQuery(string spName, SqlParameter[] Params)
        {
            DataTable tb = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                cn.Open();
                tb = new DataTable();
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                if (Params !=null)
                {
                    foreach (SqlParameter SQLPrm in Params) {
                        da.SelectCommand.Parameters.Add(SQLPrm);
                    }
                }
                da.Fill(tb);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (cn != null) cn.Close();
            }   
            return tb;
        }
        public static DataTable getQuery(string spName)
        {
            DataTable tb = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                cn.Open();
                tb = new DataTable();
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (cn != null) cn.Close();
            }
            return tb;
        }
        public static int ExcuteNonQuery(string spName, SqlParameter [] Params)
        {
            int rs = 0;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand(spName,cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Params != null)
            {
                foreach (SqlParameter p in Params)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                cn.Open();
                rs =  cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return rs;
        }
        public static SqlDataReader ExcuteDataReader(string spName, SqlParameter[] Params)
        {
            SqlDataReader dr = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand(spName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Params != null)
            {
                foreach (SqlParameter p in Params)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }
            return dr;
        }
    }

}
