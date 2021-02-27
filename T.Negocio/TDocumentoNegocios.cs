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
    public class TDocumentoNegocios
    {
        TDocumentoDatos tdocd;
        public TDocumentoNegocios()
        {
            tdocd = new TDocumentoDatos();
        }
        public DataTable getTipoDocumentos()
        {
            return tdocd.getTipoDocumentos();
        }
    }
}
