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
    public partial class DetalleEquipo : Form
    {
        private ClaseConexion varConexion;
        private int idEquipoSeleccionado = -1;
        private int idJugadorSeleccionado = -1;
        private bool cargandoDesdeGrid = false;

        public DetalleEquipo()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaEquipos();
            cargaJugadores();
            cargaDetalleEquipo();
        }

        private void cargaEquipos()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT IdEquipo, NombreEquipo FROM Club.Equipo";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    cbEquipo.DataSource = tabla;
                    cbEquipo.DisplayMember = "NombreEquipo";
                    cbEquipo.ValueMember = "IdEquipo";
                    cbEquipo.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar equipos: " + ex.Message);
                }
            }
        }

        //Cuando aparezca como llave foránea concatenar IdJugador con NombreJugador y entre corchetes poner la Posición, un guión y su Número Dorsal. eso o que la maestra pida modificacion

        private void cargaJugadores(int idJugadorIncluir = -1)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT j.IdJugador, 
                         CAST(j.IdJugador AS VARCHAR) + ' - ' + p.NombreParticipante + 
                         ' [' + j.Posicion + ' - ' + CAST(j.Numero AS VARCHAR) + ']' AS NombreJugador
                         FROM Persona.Jugador j
                         INNER JOIN Persona.Participante p 
                         ON j.IdParticipante = p.IdParticipante
                         WHERE j.IdJugador NOT IN 
                            (SELECT IdJugador FROM Club.DetalleEquipo)
                         OR j.IdJugador = @idIncluir";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idIncluir", idJugadorIncluir);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    cbJugador.DataSource = tabla;
                    cbJugador.DisplayMember = "NombreJugador";
                    cbJugador.ValueMember = "IdJugador";
                    cbJugador.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar jugadores: " + ex.Message);
                }
            }
        }

        private void cargaDetalleEquipo()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT 
                                     de.IdEquipo,
                                     e.NombreEquipo,
                                     de.IdJugador,
                                     p.NombreParticipante + ' (' + CAST(p.Edad AS VARCHAR) + ' años)' AS NombreParticipante,
                                     j.Posicion
                                     FROM Club.DetalleEquipo de
                                     INNER JOIN Club.Equipo e ON de.IdEquipo = e.IdEquipo
                                     INNER JOIN Persona.Jugador j ON de.IdJugador = j.IdJugador
                                     INNER JOIN Persona.Participante p ON j.IdParticipante = p.IdParticipante";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvDetalleEquipo.DataSource = tabla;

                    dgvDetalleEquipo.Columns["IdEquipo"].HeaderText = "ID Equipo";
                    dgvDetalleEquipo.Columns["NombreEquipo"].HeaderText = "Equipo";
                    dgvDetalleEquipo.Columns["IdJugador"].HeaderText = "ID Jugador";
                    dgvDetalleEquipo.Columns["NombreParticipante"].HeaderText = "Nombre";
                    dgvDetalleEquipo.Columns["Posicion"].HeaderText = "Posición";

                    dgvDetalleEquipo.Columns["NombreEquipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvDetalleEquipo.Columns["NombreParticipante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvDetalleEquipo.Columns["Posicion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar detalle equipo: " + ex.Message);
                }
            }
        }

        private void cbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDesdeGrid) return;
            if (cbEquipo.SelectedItem != null && cbEquipo.SelectedIndex != -1)
            {
                DataRowView fila = (DataRowView)cbEquipo.SelectedItem;
                idEquipoSeleccionado = Convert.ToInt32(fila["IdEquipo"]);
            }
        }

        private void cbJugador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDesdeGrid) return;
            if (cbJugador.SelectedItem != null && cbJugador.SelectedIndex != -1)
            {
                DataRowView fila = (DataRowView)cbJugador.SelectedItem;
                idJugadorSeleccionado = Convert.ToInt32(fila["IdJugador"]);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idEquipoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Equipo");
                return;
            }
            if (idJugadorSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Jugador");
                return;
            }

            string query = "INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador) " +
                           "VALUES (@idEquipo, @idJugador)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@idEquipo", idEquipoSeleccionado);
                    comando.Parameters.AddWithValue("@idJugador", idJugadorSeleccionado);
                    comando.ExecuteNonQuery();

                    limpiaElementos();
                    cargaJugadores();
                    cargaDetalleEquipo();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleEquipo.CurrentRow == null || dgvDetalleEquipo.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione un registro para modificar");
                return;
            }
            if (idEquipoSeleccionado == -1 || idJugadorSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Equipo y un Jugador");
                return;
            }

            int idEquipoAnterior = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdEquipo"].Value);
            int idJugadorAnterior = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdJugador"].Value);

            string query = @"UPDATE Club.DetalleEquipo 
                             SET IdEquipo = @idEquipo, IdJugador = @idJugador
                             WHERE IdEquipo = @idEquipoAnterior AND IdJugador = @idJugadorAnterior";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@idEquipo", idEquipoSeleccionado);
                    comando.Parameters.AddWithValue("@idJugador", idJugadorSeleccionado);
                    comando.Parameters.AddWithValue("@idEquipoAnterior", idEquipoAnterior);
                    comando.Parameters.AddWithValue("@idJugadorAnterior", idJugadorAnterior);
                    comando.ExecuteNonQuery();

                    limpiaElementos();
                    cargaJugadores();
                    cargaDetalleEquipo();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleEquipo.CurrentRow == null || dgvDetalleEquipo.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione un registro para eliminar");
                return;
            }

            int idEquipo = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdEquipo"].Value);
            int idJugador = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdJugador"].Value);

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este jugador del equipo?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            string query = "DELETE FROM Club.DetalleEquipo WHERE IdEquipo = @idEquipo AND IdJugador = @idJugador";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@idEquipo", idEquipo);
                    comando.Parameters.AddWithValue("@idJugador", idJugador);
                    comando.ExecuteNonQuery();

                    limpiaElementos();
                    cargaJugadores();
                    cargaDetalleEquipo();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void dgvDetalleEquipo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !dgvDetalleEquipo.Rows[e.RowIndex].IsNewRow)
                {
                    cargandoDesdeGrid = true;
                    DataGridViewRow fila = dgvDetalleEquipo.Rows[e.RowIndex];
                    int idEquipo = Convert.ToInt32(fila.Cells["IdEquipo"].Value);
                    int idJugador = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                    idEquipoSeleccionado = idEquipo;
                    idJugadorSeleccionado = idJugador;
                    cargaJugadores(idJugador);

                    foreach (DataRowView item in cbEquipo.Items)
                    {
                        if (Convert.ToInt32(item["IdEquipo"]) == idEquipo)
                        {
                            cbEquipo.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (DataRowView item in cbJugador.Items)
                    {
                        if (Convert.ToInt32(item["IdJugador"]) == idJugador)
                        {
                            cbJugador.SelectedItem = item;
                            break;
                        }
                    }
                    cargandoDesdeGrid = false;
                }
            }
            catch (Exception ex)
            {
                cargandoDesdeGrid = false;
                MessageBox.Show("Error al seleccionar registro: " + ex.Message);
            }
        }

        private void limpiaElementos()
        {
            cbEquipo.SelectedIndex = -1;
            cbJugador.SelectedIndex = -1;
            idEquipoSeleccionado = -1;
            idJugadorSeleccionado = -1;
        }
    }
}