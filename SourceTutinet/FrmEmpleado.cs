using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T.Modelo;
using T.Negocio;

namespace SourceTutinet
{
    public partial class FrmEmpleado : Form
    {
        EmpleadoNegocios empN = new EmpleadoNegocios();
        TDocumentoNegocios tdocn = new TDocumentoNegocios();
        bool operacionNuevo = true;
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            CargarDGVEmpleados();
            CargarTipoDocumento();

        }
        private void CargarDGVEmpleados()
        {
            dgvEmpleados.DataSource = null;
            if (empN.getAllEmpleados().Rows.Count > 0) {
                dgvEmpleados.DataSource = empN.getAllEmpleados();
                lblDNEncontrados.Visible = false;
                dgvEmpleados_CellClick(null, null);
            }
            else
            {
                lblDNEncontrados.Visible = false;
            }
        }
        private void CargarTipoDocumento()
        {
            cboTipoDoc.DataSource = tdocn.getTipoDocumentos();
            cboTipoDoc.DisplayMember = "tipodoc_abreviatura";
            cboTipoDoc.ValueMember = "tipodoc_id";
        }
        private void LimpControles()
        {
            txtId.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            dtpFecNac.Value = DateTime.Now;
            txtNroDoc.Text = "";
            txtCelular.Text = "";
            txtUsuario.Text = "";
            cboEstado.SelectedIndex = 1;
        }
        private void ActivarControles(bool x)
        {
            if (!operacionNuevo)
            {
                lblContraseña.Visible = x;
                btnReiniciarCon.Visible = x;
            }
            txtNombres.Enabled = x;
            txtApellidos.Enabled = x;
            dtpFecNac.Enabled = x;
            cboTipoDoc.Enabled = x;
            txtNroDoc.Enabled = x;
            txtCelular.Enabled = x;
            txtUsuario.Enabled = x;
            cboEstado.Enabled = x;
            btnGuardar.Visible = x;
            btnCancelar.Visible = x;
            btnNuevo.Visible = !x;
            btnEditar.Visible = !x;
        }

        private string ValidarCampos()
        {
            string resultado = "";

            if (txtNombres.Text =="")
            {
                resultado += "Nombre \n";
            }
            if (txtApellidos.Text == "")
            {
                resultado += "Apellido \n";
            }
            if (dtpFecNac.Text =="") {
                resultado += "Fecha \n";
            }
            if (cboTipoDoc.Text=="") {
                resultado += "Tido documento \n";    
            }
            if (txtNroDoc.Text == "") {
                resultado += "nro. Documento \n";
            }
            if (txtCelular.Text == "")
            {
                resultado += "nro. Celular \n";
            }
            if (txtUsuario.Text == "")
            {
                resultado += "Usuario \n";
            }
            if (resultado!="")
            {
                return "Falta rellenar los siguientes campos : \n" + resultado;
            }

            return resultado;
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmpleados.CurrentRow != null)
            {
                txtId.Text = dgvEmpleados.CurrentRow.Cells["ID"].Value.ToString();
                txtNombres.Text = dgvEmpleados.CurrentRow.Cells["Nombres"].Value.ToString();
                txtApellidos.Text = dgvEmpleados.CurrentRow.Cells["Apellidos"].Value.ToString();
                dtpFecNac.Text = dgvEmpleados.CurrentRow.Cells["FEC. NAC."].Value.ToString();
                cboTipoDoc.Text = dgvEmpleados.CurrentRow.Cells["Documento"].Value.ToString();
                txtNroDoc.Text = dgvEmpleados.CurrentRow.Cells["NRO. DOC."].Value.ToString();
                txtCelular.Text = dgvEmpleados.CurrentRow.Cells["Celular"].Value.ToString();
                txtUsuario.Text = dgvEmpleados.CurrentRow.Cells["Usuario"].Value.ToString();
                cboEstado.SelectedIndex = (int)(dgvEmpleados.CurrentRow.Cells["Estado"].Value);/*se obtiene error al seleccionar una celda vacia*/
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            operacionNuevo = true;
            ActivarControles(true);
            LimpControles();
            dgvEmpleados.CellClick -= dgvEmpleados_CellClick;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string resultado = ValidarCampos();
            if (resultado == "")
            {
                if (operacionNuevo)
                {
                    dgvEmpleados.CellClick += dgvEmpleados_CellClick;
                    GenerarEmpleado();
                }
                else
                {
                    ActualizarEmpleado();
                }
                ActivarControles(false);
                CargarDGVEmpleados();
            }
            else
            {
                MessageBox.Show(resultado);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (operacionNuevo)
            {
                dgvEmpleados.CellClick += dgvEmpleados_CellClick;
                CargarDGVEmpleados();   
            }
            ActivarControles(false);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            operacionNuevo = false;
            ActivarControles(true);
        }
        private void GenerarEmpleado()
        {
                Empleado emp = new Empleado();
                emp.empleado_nombre = txtNombres.Text;
                emp.empleado_apellido = txtApellidos.Text;
                emp.empleado_fec_nac = dtpFecNac.Value;
                emp.empleado_tipodoc_id = (int)cboTipoDoc.SelectedValue;
                emp.empleado_doc = txtNroDoc.Text;
                emp.empleado_celular = txtCelular.Text;
                emp.empleado_usuario = txtUsuario.Text;
                emp.empleado_estado = cboEstado.SelectedIndex;
                MessageBox.Show(empN.insertarEmpleado(emp));
        }
        private void ActualizarEmpleado()
        {
            try
            {
                Empleado emp = new Empleado();
                emp.empleado_id = Convert.ToInt32(txtId.Text);
                emp.empleado_nombre = txtNombres.Text;
                emp.empleado_apellido = txtApellidos.Text;
                emp.empleado_fec_nac = dtpFecNac.Value;
                emp.empleado_tipodoc_id = (int)cboTipoDoc.SelectedValue;
                emp.empleado_doc = txtNroDoc.Text;
                emp.empleado_celular = txtCelular.Text;
                emp.empleado_usuario = txtUsuario.Text;
                emp.empleado_estado = cboEstado.SelectedIndex;
                empN.actualizarEmpleado(emp);
                MessageBox.Show("Usuario actualizado extiosamente ");
            }
            catch (Exception e)
            {

                MessageBox.Show("Algo sali mal"+e );
            }

        }
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            string mensaje = "¿Deseas eliminar los usuarios seleccionados?";
            string caption = "Alerta eliminacion de usuarios";
            DialogResult result = MessageBox.Show(mensaje,caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvEmpleados.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Eliminar"].Value)) {
                        int id = Convert.ToInt32(row.Cells["Id"].Value);
                        if (empN.eliminarEmpleado(id)!=1)
                        {
                            MessageBox.Show("El Usuario no pudo ser eliminado","Eliminacion de Usuario",
                                MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                }
                CargarDGVEmpleados();
            }
        }
        private void btnReiniciarCon_Click(object sender, EventArgs e)
        {
            string mensaje = "Los datos se guardaran al generar la contraseña. ¿Esta seguro de realizar esta accion?";
            string caption = "Alerta";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(mensaje,caption, buttons);
            if (result == DialogResult.Yes) {
                string resultado = ValidarCampos();
                if (resultado =="") {
                        //string contraseña = GenPassword.GenerarContraseña(6);
                        Empleado emp = new Empleado();
                        emp.empleado_id = Convert.ToInt32(txtId.Text);
                        emp.empleado_nombre = txtNombres.Text;
                        emp.empleado_apellido = txtApellidos.Text;
                        emp.empleado_fec_nac = dtpFecNac.Value;
                        emp.empleado_tipodoc_id = (int)cboTipoDoc.SelectedValue;
                        emp.empleado_doc = txtNroDoc.Text;
                        emp.empleado_celular = txtCelular.Text;
                        emp.empleado_usuario = txtUsuario.Text;
                       // emp.empleado_contrasena = Encrypt.GetSHA256(contraseña);
                        emp.empleado_estado = cboEstado.SelectedIndex;
                        MessageBox.Show(empN.actualizarEmpleadoContrasena(emp));
                        ActivarControles(false);
                        CargarDGVEmpleados();
                }
                else
                {
                    MessageBox.Show(resultado);
                }
            }
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) {
                if (txtFiltro.Text != "" && cboFiltro.Text != "") {
                    dgvEmpleados.DataSource = null;
                    dgvEmpleados.DataSource = empN.filtrarEmpleado(cboFiltro.Text,txtFiltro.Text);
                }
                else
                {
                    CargarDGVEmpleados();
                }
            }
        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvEmpleados.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = 
                    (DataGridViewCheckBoxCell)dgvEmpleados.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }
    }
}
