using System;
using System.Collections;
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
        //Declaracion de variables
        private ClaseConexion varConexion;
        private int idParticipanteSeleccionado = -1;

        public frmParticipante()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaParticipantes();
            limpiaElementos();
        }

        //Funcion para cargar los participantes en el grid view
        private void cargaParticipantes()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    //Declaracion de la sentencia
                    string query = "SELECT * FROM Persona.Participante";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvParticipante.DataSource = tabla;
                    //Se cambia el texto de las columnas del grid view
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

        //Función del botón de Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Verificar que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(cbGenero.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Guardar en variables los valores de los campos
            string nombre = txtNombre.Text;
            string genero = cbGenero.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            DateTime fecha = mcFecha.SelectionStart.Date;

            //Validar longitud de teléfono
            if (txtTelefono.Text.Length < 10)
            {
                MessageBox.Show("Ingrese un número de teléfono válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            //Validar que el correo electrónico sea válido (básico)
            if (txtCorreo.Text.Contains("@") == false || txtCorreo.Text.Contains(".") == false)
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Creacion de la sentencia
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
                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        //Función del botón de Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Verificar que se haya seleccionado un participante
            if (idParticipanteSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un participante para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }

            //Verificar que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(cbGenero.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text;
            string genero = cbGenero.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            DateTime fecha = mcFecha.SelectionStart.Date;
            //Validar longitud de teléfono
            if (txtTelefono.Text.Length < 10)
            {
                MessageBox.Show("Ingrese un número de teléfono válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            //Validar que el correo electrónico sea válido (básico)
            if (txtCorreo.Text.Contains("@") == false || txtCorreo.Text.Contains(".") == false)
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Declaración de la sentencia
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
                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        //Función del botón de Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificar que se haya seleccionado un participante
            if (idParticipanteSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Participante para eliminar");
                return;
            }

            //Declaración de la sentencia
            string query = "DELETE FROM Persona.Participante WHERE IdParticipante = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idParticipanteSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    cargaParticipantes();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        //Función para cargar en el formulario los datos después de seleccionar un registro del grid view
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
                    //Habilita los botones de árbitro y jugador por si se desea
                    //registrar al participante como uno de esos roles
                    actualizaBotones(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Participante: " + ex.Message);
            }

        }

        //Función para limpiar el formulario
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

        //Función del botón para registrar jugador, verifica antes que no esté registrado como árbitro
        private void btnRegistrarJugador_Click(object sender, EventArgs e)
        {
            if (idParticipanteSeleccionado != -1)
            {
                int esJugador = verificaRegistro(idParticipanteSeleccionado, 1);
                int esArbitro = verificaRegistro(idParticipanteSeleccionado, 2);

                if (esArbitro > 0)
                {
                    MessageBox.Show("Este participante ya está registrado como Árbitro.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (esJugador > 0)
                {
                    MessageBox.Show("Este participante ya está registrado como Jugador.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

        //Función para verificar si el participante ya está registrado como jugador o árbitro
        private int verificaRegistro(int idParticipante, int tipoParticipante)
        {   
            string tabla;
            //Dependiendo del valor, se define la tabla para usar la misma variable en la consulta
            if (tipoParticipante == 1)
                tabla = "Persona.Jugador";
            else
                tabla = "Persona.Arbitro";

            string query = $@"SELECT COUNT(*) FROM {tabla} 
                            WHERE IdParticipante = @idParticipante";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@tabla", tabla);
                comando.Parameters.AddWithValue("@idParticipante", idParticipante);
                try
                {
                    conexion.Open();
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                    return -1;
                }
            }
        }

        //Función para habilitar/deshabilitar los botones de registrar como árbitro y jugador
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