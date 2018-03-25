using DBFTOSQLSERVER.codigo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFTOSQLSERVER
{
    public partial class ventanaPrincipal : Form
    {
        private bool menuDeslizado = false;
        public ventanaPrincipal()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            menuDeslizado = !menuDeslizado;
            lblVersion.Visible = false;
            if (menuDeslizado)
            {
                panel2.Size = new Size(130, 338);
                btn1.Size = new Size(119,23);
                btn2.Size = new Size(119, 23);
                btn3.Size = new Size(119, 23);
                btn1.Text = "CONVERTIR DBF";
                btn2.Text = "IMPORTAR A SQL";
                btn3.Text = "SALIR";
                btnMenu.Text = "MENÚ";
                lblVersion.Visible = true;
            }
            else {
                panel2.Size = new Size(51, 338);
                btn1.Size = new Size(40, 23);
                btn2.Size = new Size(40, 23);
                btn3.Size = new Size(40, 23);
                btnMenu.Text = "☰";
                btn1.Text = "DBF";
                btn2.Text = "SQL";
                btn3.Text = "✗";
            }
        }

        private void ventanaPrincipal_Load(object sender, EventArgs e)
        {
            lblVersion.Visible = false;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ventanaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea salir de la aplicación?","Saliendo",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialogo != DialogResult.Yes) {
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = ofdArchivo.ShowDialog();
            try
            {
                if (dialogo == DialogResult.OK)
                {
                    string nombreArchivo = ofdArchivo.FileName;
                    txtRuta.Text = nombreArchivo;
                }
            }
            catch{

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRuta.Text)) {
                MessageBox.Show("Seleccionar archivo DBF","Seleccionar",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                btnRuta.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTabla.Text)) {
                MessageBox.Show("Ingresar nombre de tabla", "Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTabla.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            procesoDbf proceso = new procesoDbf();
            string cadena = proceso.convertirDBFtoText(txtRuta.Text,txtTabla.Text);
            this.Cursor = Cursors.Default;
            //***************** GUARDAR ARCHIVO *********************
            DialogResult resultado = sfdGuardar.ShowDialog();
            if (resultado == DialogResult.OK) {
                this.Cursor = Cursors.WaitCursor;
               string ruta =  sfdGuardar.FileName;
                StreamWriter escribir = new StreamWriter(ruta);
                escribir.WriteLine(cadena);
                this.Cursor = Cursors.Default;
                MessageBox.Show("Conversión terminada", "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRuta.Text = "";
                txtTabla.Text = "";
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (!panelDbf.Visible) {
                panelDbf.Visible = true;
                txtRuta.Text = "";
                txtTabla.Text = "";
            }            
        }
    }
}
