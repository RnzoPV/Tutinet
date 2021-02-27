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
        bool operacionNuevo =true;
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
            dgvEmpleados.DataSource = empN.getAllEmpleados();

            if (empN.getAllEmpleados().Rows.Count > 0) {
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
            if (operacionNuevo)
            {
                dgvEmpleados.CellClick += dgvEmpleados_CellClick;
                GenerarEmpleado();
            }
            ActivarControles(false);
            CargarDGVEmpleados();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (operacionNuevo)
            {
                dgvEmpleados.CellClick += dgvEmpleados_CellClick;
            }
            ActivarControles(false);
            CargarDGVEmpleados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            operacionNuevo = false;
            ActivarControles(true);
        }
        private void GenerarEmpleado()
        {
            try{
                string newContraseña = GenPassword.GenerarContraseña(6);
                Empleado emp = new Empleado();
                emp.empleado_nombre = txtNombres.Text;
                emp.empleado_apellido = txtApellidos.Text;
                emp.empleado_fec_nac = dtpFecNac.Value;
                emp.empleado_tipodoc_id = (int)cboTipoDoc.SelectedValue;
                emp.empleado_doc = txtNroDoc.Text;
                emp.empleado_celular = txtCelular.Text;
                emp.empleado_usuario = txtUsuario.Text;
                emp.empleado_contrasena = Encrypt.GetSHA256(newContraseña);
                emp.empleado_estado = cboEstado.SelectedIndex;
                empN.insertarEmpleado(emp);
                MessageBox.Show("Usuario registrado extiosamente "+" su contraseña es :"+ newContraseña);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+e.StackTrace);
            }

        }
    }
}
