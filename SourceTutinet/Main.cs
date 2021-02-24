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

namespace SourceTutinet
{
    public partial class Main : Form
    {
        Empleado emp;
        public Main()
        {
            InitializeComponent();
        }
        public Main(Empleado emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        private void Main_Load(object sender, EventArgs e)
        {
          
        }
    }
}
