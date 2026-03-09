using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class frmParticipante : Form
    {
        private ClaseConexion varConexion;
        private int idParticipanteSeleccionado = -1;

        public frmParticipante()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaParticipantes();
            limpiaElementos();
        }

        private void cargaParticipantes()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT * FROM Persona.Participante";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvParticipante.DataSource = tabla;

                    dgvParticipante.Columns["IdParticipante"].HeaderText = "ID Participante";
                    dgvParticipante.Columns["NombreParticipante"].HeaderText = "Nombre del Participante";
                    dgvParticipante.Columns["Genero"].HeaderText = "Género";
                    dgvParticipante.Columns["Telefono"].HeaderText = "Teléfono";
                    dgvParticipante.Columns["CorreoElectronico"].HeaderText = "Correo Electrónico";
                    dgvParticipante.Columns["FechaNacimiento"].HeaderText = "Fecha de nacimiento";
                    dgvParticipante.Columns["Edad"].HeaderText = "Edad";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Participantes: " + ex.Message);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string genero = cbGenero.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            DateTime fecha = mcFecha.SelectionStart.Date;

            if (string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(genero) ||
                string.IsNullOrEmpty(telefono) ||
                string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }

            if (txtTelefono.Text.Length < 10)
            {
                MessageBox.Show("El número de teléfono debe tener 10 dígitos");
                return;
            }
            if (txtCorreo.Text.Contains("@") == false || txtCorreo.Text.Contains(".") == false)
            {
                MessageBox.Show("Ingrese un correo electrónico válido");
                return;
            }
            string query = "INSERT INTO Persona.Participante " +
                           "(NombreParticipante, Genero, Telefono, CorreoElectronico, FechaNacimiento) " +
                           "VALUES (@nombre, @genero, @telefono, @correo, @fecha)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@genero", genero);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@fecha", fecha);

                    command.ExecuteNonQuery();

                    //MessageBox.Show("Se agregó con éxito al nuevo Participante");

                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idParticipanteSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Participante para modificar");
                return;
            }
            string nombre = txtNombre.Text;
            string genero = cbGenero.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            DateTime fecha = mcFecha.SelectionStart.Date;
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }
            string query = "UPDATE Persona.Participante " +
                           "SET NombreParticipante = @nombre, " +
                           "Genero = @genero, " +
                           "Telefono = @telefono, " +
                           "CorreoElectronico = @correo, " +
                           "FechaNacimiento = @fecha " +
                           "WHERE IdParticipante = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@genero", genero);
                command.Parameters.AddWithValue("@telefono", telefono);
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@id", idParticipanteSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante modificado correctamente");
                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idParticipanteSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Participante para eliminar");
                return;
            }
            string query = "DELETE FROM Persona.Participante WHERE IdParticipante = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idParticipanteSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante eliminado correctamente");
                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void dgvParticipante_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvParticipante.Rows[e.RowIndex];

                    idParticipanteSeleccionado = Convert.ToInt32(fila.Cells["IdParticipante"].Value);
                    txtNombre.Text = fila.Cells["NombreParticipante"].Value.ToString();
                    cbGenero.Text = fila.Cells["Genero"].Value.ToString();
                    txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                    txtCorreo.Text = fila.Cells["CorreoElectronico"].Value.ToString();
                    mcFecha.SetDate(Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value));
                    actualizaBotones(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Participante: " + ex.Message);
            }

        }

        private void limpiaElementos()
        {
            txtNombre.Clear();
            cbGenero.SelectedIndex = 0;
            txtTelefono.Clear();
            txtCorreo.Clear();
            mcFecha.SetDate(DateTime.Today);
            idParticipanteSeleccionado = -1;
            actualizaBotones(0);
        }

        private void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            if(idParticipanteSeleccionado != -1)
            {
                Form formulario = new CapturaJugador(idParticipanteSeleccionado);
                formulario.ShowDialog();
                limpiaElementos();
            }
            else
            {
                MessageBox.Show("Selecciona un Participante");
                return;
            }
        }

        private void actualizaBotones(int op)
        {
            if (op == 1)
            {
                btnRegistrarArbitro.Enabled = true;
                btnRegistrarArbitro.Visible = true;
                btnRegistrarJugador.Enabled = true;
                btnRegistrarJugador.Visible = true;
            }
            else
            {
                btnRegistrarArbitro.Enabled = false;
                btnRegistrarArbitro.Visible = false;
                btnRegistrarJugador.Enabled = false;
                btnRegistrarJugador.Visible = false;
            }
        }
    }
}