using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T.Modelo
{
    public class Empleado
    {
        /*Propiedades como descriptores de acceso get set
        private int _empleado_id = 0;
        public int Empleado_Id{
            get{
                return _empleado_id;
                }
            set{
                _empleado_id = value;
                }
            }
         */
        public int empleado_id { get; set; }
        public string empleado_nombre { get; set; }
        public string empleado_apellido { get; set; }
        public DateTime empleado_fec_nac { get; set; }
        public int empleado_tipodoc_id { get; set; }
        public string tipodoc_abreviatura { get; set; }
        public string empleado_doc { get; set; }
        public DateTime empleado_fecre { get; set; }
        public string empleado_celular { get; set; }
        public string empleado_usuario { get; set; }
        public string empleado_contrasena { get; set; }
        public int empleado_estado { get; set; }

    }
   /* public override string ToString()
    {
        return "Empleado = " + Empleado_Id ;
    }*/
}
