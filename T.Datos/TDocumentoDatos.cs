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
    public class TDocumentoDatos : Conexion
    {
        public TDocumentoDatos() : base()
        {
        }
        public DataTable getTipoDocumentos()
        {
            DataTable table = new DataTable();
            try
            {
                cn.Open();
                string query = "SELECT * FROM TipoDoc";
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.Fill(table);
                cn.Close();
                return table;

            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("Error al obtener Tipo Documento"+e.Message);
            }
            finally
            {
                cn.Close();

            }
            return table;
        }
    }
}
