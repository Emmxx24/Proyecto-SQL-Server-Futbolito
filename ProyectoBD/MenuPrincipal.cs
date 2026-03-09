using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ProyectoBD
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            this.Text = "Participante";
            AbrirFormularioHijo(new frmParticipante());
        }


        // Esta función revisa si hay una ventana abierta y pregunta si la quieres cerrar
        private bool CerrarVentanaActual()
        {
            //// Revisa si hay alguna ventana hija activa en este momento
            //if (this.ActiveMdiChild != null)
            //{
            //    // Preguntamos al usuario
            //    DialogResult respuesta = MessageBox.Show(
            //        "¿Estás seguro de que quieres salir de la pantalla actual? Los cambios no guardados se perderán.",
            //        "Advertencia",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Warning);

            //    if (respuesta == DialogResult.Yes)
            //    {
            //        // Si dice que sí, la destruimos
            //        this.ActiveMdiChild.Close();
            //        return true; // Se cerró con éxito, podemos abrir la nueva
            //    }
            //    else
            //    {
            //        // Si dice que no, cancelamos el proceso
            //        return false;
            //    }
            //}
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
            // Si no había ninguna ventana abierta, regresamos true para que abra la nueva directo
            return true;
        }

        // Esta es la función genérica. Recibe "cualquier" formulario hijo que le mandes.
        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if (!CerrarVentanaActual()) return;
            this.ClientSize = new Size(formularioHijo.Width, formularioHijo.Height + menuStrip1.Height);
            formularioHijo.MdiParent = this;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            formularioHijo.Show();
        }

        private void participanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Participante";
            AbrirFormularioHijo(new frmParticipante());
        }

        private void jugadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Jugador";
            AbrirFormularioHijo(new CapturaJugador());
        }

        private void arbitroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void torneoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Torneo";
            AbrirFormularioHijo(new CapturaTorneos());
        }

        private void jornadaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lugarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Lugar";
            AbrirFormularioHijo(new CapturaLugar());
        }

        private void partidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resultadoPartidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void golToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tarjetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void equipoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void detalleTorneoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void detalleEquipoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
