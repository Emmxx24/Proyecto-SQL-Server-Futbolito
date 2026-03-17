using ProyectoBD.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ProyectoBD
{
    public partial class CapturaJugador : Form
    {
        private ClaseConexion varConexion;
        private int idJugadorSeleccionado;
        private int idJugadorFK;

        public CapturaJugador(int? idJugador = null)
        {
            //MessageBox.Show(idJugador.ToString());
            InitializeComponent();
            idJugadorFK = -1;
            varConexion = new ClaseConexion();
            cargaJugadores();
            limpiaElementos();
            if (idJugador != null)
            {
                idJugadorFK = (int)idJugador;
                cargaNombreForaneo(idJugadorFK);
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }
        private void cargaNombreForaneo(int id)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT NombreParticipante FROM Persona.Participante WHERE IdParticipante = @idParticipante";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idParticipante", id);
                    object result = comando.ExecuteScalar();
                    //MessageBox.Show("Nombre de participante" + result.ToString());
                    if (result != null)
                    {
                        txtNombre.Text = result.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el nombre del participante: " + ex.Message);
                }
            }
        }

        public void cargaJugadores()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT " +
                        "p.IdParticipante," +
                        "j.IdJugador," +
                        "CONCAT(p.NombreParticipante, ' (', p.Edad, ')') AS NombreConEdad, " +
                        "j.Posicion," +
                        "j.Numero," +
                        "j.TipoSangre," +
                        "j.AcumuladorAmarillas," +
                        "j.Estado " +
                        "FROM Persona.Jugador j " +
                        "INNER JOIN Persona.Participante p " +
                        "ON j.IdParticipante = p.IdParticipante ";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvJugador.DataSource = tabla;

                    dgvJugador.Columns["IdParticipante"].Visible = false;
                    dgvJugador.Columns["IdJugador"].HeaderText = "ID Jugador";
                    dgvJugador.Columns["NombreConEdad"].HeaderText = "Nombre del Jugador (Edad)";
                    dgvJugador.Columns["Posicion"].HeaderText = "Posición";
                    dgvJugador.Columns["Numero"].HeaderText = "Dorsal";
                    dgvJugador.Columns["TipoSangre"].HeaderText = "Tipo de sangre";
                    dgvJugador.Columns["AcumuladorAmarillas"].HeaderText = "Acumulador de amarillas";
                    dgvJugador.Columns["Estado"].HeaderText = "Estado";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Jugadores: " + ex.Message);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //string nombre = txtNombre.Text;
            string posicion = cbPosicion.Text;
            int numero = Convert.ToInt32(numericNumJugador.Value);
            string tipoSangre = cbTipoSangre.Text;


            if (string.IsNullOrEmpty(posicion) ||
                string.IsNullOrEmpty(numero.ToString()) ||
                string.IsNullOrEmpty(tipoSangre))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            else if (idJugadorFK == -1)
            {
                MessageBox.Show("Selecciona a un participante desde el formulario de Participante");
                return;
            }
            string query = "INSERT INTO Persona.Jugador " +
                           "(IdParticipante, Posicion, Numero, TipoSangre, AcumuladorAmarillas, Estado) " +
                           "VALUES (@idParticipante, @posicion, @numero, @tipoSangre, 0, 'Activo')";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idParticipante", idJugadorFK);
                    command.Parameters.AddWithValue("@posicion", posicion);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@tipoSangre", tipoSangre);
                    command.ExecuteNonQuery();

                    //MessageBox.Show("Se agregó con éxito al nuevo Participante");
                    idJugadorFK = -1;
                    cargaJugadores();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void limpiaElementos()
        {
            txtNombre.Clear();
            cbPosicion.SelectedIndex = 0;
            numericNumJugador.Value = 0;
            cbTipoSangre.SelectedIndex = 0;
            idJugadorSeleccionado = -1;
        }

        private void dgvJugador_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvJugador.Rows[e.RowIndex];

                    idJugadorSeleccionado = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                    cargaNombreForaneo(Convert.ToInt32(fila.Cells["IdParticipante"].Value));
                    numericNumJugador.Value = Convert.ToInt32(fila.Cells["Numero"].Value);
                    cbPosicion.Text = fila.Cells["Posicion"].Value.ToString();
                    cbTipoSangre.Text = fila.Cells["TipoSangre"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Participante: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idJugadorSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Jugador para modificar");
                return;
            }
            string posicion = cbPosicion.Text;
            int numero = Convert.ToInt32(numericNumJugador.Value);
            string tipoSangre = cbTipoSangre.Text;
            if (string.IsNullOrEmpty(posicion) ||
                string.IsNullOrEmpty(numero.ToString()) ||
                string.IsNullOrEmpty(tipoSangre))
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }
            string query = "UPDATE Persona.Jugador " +
                           "SET Posicion = @posicion, " +
                           "Numero = @numero, " +
                           "TipoSangre = @tiposangre " +
                           "WHERE IdJugador = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@posicion", posicion);
                command.Parameters.AddWithValue("@numero", numero);
                command.Parameters.AddWithValue("@tiposangre", tipoSangre);
                command.Parameters.AddWithValue("@id", idJugadorSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante modificado correctamente");
                    cargaJugadores();
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
            if (idJugadorSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Jugador para eliminar");
                return;
            }
            string query = "DELETE FROM Persona.Jugador WHERE IdJugador = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idJugadorSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante eliminado correctamente");
                    cargaJugadores();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }
    }
}
