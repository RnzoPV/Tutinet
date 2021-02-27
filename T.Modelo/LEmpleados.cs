using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T.Modelo
{
    class LEmpleados
    {
        private int empleado_id { get; set; }
        private string empleado_nombre { get; set; }
        private string empleado_apellido { get; set; }
        private DateTime empleado_fec_nac { get; set; }
        private int empleado_tipodoc_id { get; set; }
        private string tipodoc_abreviatura{ get; set; }
        private string empleado_doc { get; set; }
        private DateTime empleado_fecre { get; set; }
        private string empleado_celular { get; set; }
        private string empleado_usuario { get; set; }
        private string empleado_contrasena { get; set; }
        private string empleado_estado { get; set; }
    }
}
