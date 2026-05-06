using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class CapturaArbitro : Form
    {
        private ClaseConexion varConexion;
        private int idParticipanteSeleccionado = -1;

        public CapturaArbitro(int? idParticipante = null)
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaArbitros();

            if (idParticipante.HasValue)
            {
                idParticipanteSeleccionado = idParticipante.Value;
                cargaNombreParticipante(idParticipante.Value);
            }
        }

        private void cargaNombreParticipante(int id)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT NombreParticipante FROM Persona.Participante WHERE IdParticipante = @id";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@id", id);
                    object result = comando.ExecuteScalar();
                    if (result != null)
                        tbParcipante.Text = result.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el participante: " + ex.Message);
                }
            }
        }

        private void cargaArbitros()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT a.IdArbitro, 
                     p.NombreParticipante + ' (' + CAST(p.Edad AS VARCHAR) + ' años)' AS NombreParticipante,
                     a.CedulaArbitro
                     FROM Persona.Arbitro a
                     INNER JOIN Persona.Participante p 
                     ON a.IdParticipante = p.IdParticipante";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvArbitro.DataSource = tabla;
                    dgvArbitro.Columns["IdArbitro"].HeaderText = "ID Árbitro";
                    dgvArbitro.Columns["NombreParticipante"].HeaderText = "Nombre";
                    dgvArbitro.Columns["CedulaArbitro"].HeaderText = "Cédula Profesional";
                    dgvArbitro.Columns["NombreParticipante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvArbitro.Columns["CedulaArbitro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar árbitros: " + ex.Message);
                }
            }
        }

        private void tbCedula_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idParticipanteSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Participante desde el formulario de Participantes");
                return;
            }

            string cedula = tbCedula.Text.Trim();

            if (string.IsNullOrEmpty(cedula) || cedula.Length < 15)
            {
                MessageBox.Show("Ingrese la Cédula Profesional");
                return;
            }
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string queryVerifica = "SELECT COUNT(*) FROM Persona.Jugador WHERE IdParticipante = @id";
                    SqlCommand comandoVerifica = new SqlCommand(queryVerifica, conexion);
                    comandoVerifica.Parameters.AddWithValue("@id", idParticipanteSeleccionado);
                    int esJugador = Convert.ToInt32(comandoVerifica.ExecuteScalar());

                    if (esJugador > 0)
                    {
                        MessageBox.Show("Este participante ya tiene el rol de Jugador, no puede ser Árbitro");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el participante: " + ex.Message);
                    return;
                }
            }
            string query = "INSERT INTO Persona.Arbitro (IdParticipante, CedulaArbitro) " +
                           "VALUES (@idParticipante, @cedula)";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@idParticipante", idParticipanteSeleccionado);
                    comando.Parameters.AddWithValue("@cedula", cedula);
                    comando.ExecuteNonQuery();

                    tbCedula.Clear();
                    tbParcipante.Clear();
                    idParticipanteSeleccionado = -1;
                    cargaArbitros();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (dgvArbitro.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Árbitro para eliminar");
                return;
            }

            int idArbitro = Convert.ToInt32(dgvArbitro.CurrentRow.Cells["IdArbitro"].Value);

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este árbitro?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            string query = "DELETE FROM Persona.Arbitro WHERE IdArbitro = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@id", idArbitro);
                    comando.ExecuteNonQuery();

                    //MessageBox.Show("Árbitro eliminado correctamente");
                    cargaArbitros();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void dgvArbitro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !dgvArbitro.Rows[e.RowIndex].IsNewRow)
                {
                    DataGridViewRow fila = dgvArbitro.Rows[e.RowIndex];
                    tbCedula.Text = fila.Cells["CedulaArbitro"].Value.ToString();
                    tbParcipante.Text = fila.Cells["NombreParticipante"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar árbitro: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArbitro.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Árbitro para modificar");
                return;
            }
            string cedula = tbCedula.Text.Trim();

            if (string.IsNullOrEmpty(cedula) || cedula.Length < 15)
            {
                MessageBox.Show("Ingrese la nueva Cédula Profesional");
                return;
            }
            int idArbitro = Convert.ToInt32(dgvArbitro.CurrentRow.Cells["IdArbitro"].Value);
            string query = "UPDATE Persona.Arbitro SET CedulaArbitro = @cedula WHERE IdArbitro = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@cedula", cedula);
                    comando.Parameters.AddWithValue("@id", idArbitro);
                    comando.ExecuteNonQuery();
                    //MessageBox.Show("Árbitro modificado correctamente");
                    tbCedula.Clear();
                    tbParcipante.Clear();
                    idParticipanteSeleccionado = -1;
                    cargaArbitros();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }
    }
}