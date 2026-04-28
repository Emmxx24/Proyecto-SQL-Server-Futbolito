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
        private int idTorneoSeleccionado = -1; // se llena desde el combo de equipos
        private int idEquipoSeleccionado = -1;
        private int idJugadorSeleccionado = -1;
        private bool cargandoDesdeGrid = false;

        private int idEquipoAnterior = -1;
        private int idJugadorAnterior = -1;

        public DetalleEquipo()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaEquipos();
            cargaJugadores();
            cargaDetalleEquipo();
        }

        // El combo muestra "NombreEquipo (NombreTorneo)" y trae IdTorneo
        // para usarlo internamente en las validaciones sin necesitar un combo extra.
        private void cargaEquipos()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"
                        SELECT e.IdEquipo,
                               e.NombreEquipo + ' (' + t.NombreTorneo + ')' AS NombreEquipo,
                               dt.IdTorneo
                        FROM Club.Equipo e
                        INNER JOIN Juego.DetalleTorneo dt ON e.IdEquipo = dt.IdEquipo
                        INNER JOIN Juego.Torneo t         ON t.IdTorneo = dt.IdTorneo
                        ORDER BY e.NombreEquipo, t.NombreTorneo";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);

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

        private void cargaJugadores()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"
                        SELECT j.IdJugador,
                               CAST(j.IdJugador AS VARCHAR) + ' - ' + p.NombreParticipante +
                               ' [' + j.Posicion + ' - ' + CAST(j.Numero AS VARCHAR) + ']' AS NombreJugador
                        FROM Persona.Jugador j
                        INNER JOIN Persona.Participante p ON j.IdParticipante = p.IdParticipante
                        ORDER BY p.NombreParticipante";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);

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
                    string query = @"
                    SELECT
                        de.IdEquipo,
                        e.NombreEquipo,
                        de.IdJugador,
                        CONCAT(j.IdJugador, ' ', p.NombreParticipante, ' [', j.Posicion, ' - ', j.Numero, ']') AS DetalleJugador
                    FROM Club.DetalleEquipo de
                    INNER JOIN Club.Equipo e          ON de.IdEquipo      = e.IdEquipo
                    INNER JOIN Persona.Jugador j      ON de.IdJugador     = j.IdJugador
                    INNER JOIN Persona.Participante p ON j.IdParticipante = p.IdParticipante
                    ORDER BY e.NombreEquipo, j.Numero";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);

                    dgvDetalleEquipo.DataSource = tabla;

                    dgvDetalleEquipo.Columns["IdEquipo"].HeaderText = "ID Equipo";
                    dgvDetalleEquipo.Columns["IdEquipo"].Visible = false;
                    dgvDetalleEquipo.Columns["NombreEquipo"].HeaderText = "Equipo";
                    dgvDetalleEquipo.Columns["IdJugador"].HeaderText = "ID Jugador";
                    dgvDetalleEquipo.Columns["IdJugador"].Visible = false;
                    dgvDetalleEquipo.Columns["DetalleJugador"].HeaderText = "Jugador";


                    //dgvDetalleEquipo.Columns["NombreEquipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar detalle equipo: " + ex.Message);
                }
            }
        }

        private bool validarInscripcion(int idTorneo, int idEquipo, int idJugador,
                                        int excluirEquipoAnterior = -1,
                                        int excluirJugadorAnterior = -1)
        {
            bool esModificacion = (excluirEquipoAnterior != -1 && excluirJugadorAnterior != -1);

            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();

                    // 1. ¿El jugador ya está inscrito en este equipo?
                    string qDup = esModificacion
                        ? @"SELECT COUNT(*) FROM Club.DetalleEquipo
                            WHERE IdEquipo=@e AND IdJugador=@j
                            AND NOT (IdEquipo=@exE AND IdJugador=@exJ)"
                        : @"SELECT COUNT(*) FROM Club.DetalleEquipo
                            WHERE IdEquipo=@e AND IdJugador=@j";

                    using (SqlCommand cmd = new SqlCommand(qDup, conexion))
                    {
                        cmd.Parameters.AddWithValue("@e", idEquipo);
                        cmd.Parameters.AddWithValue("@j", idJugador);
                        if (esModificacion) { cmd.Parameters.AddWithValue("@exE", excluirEquipoAnterior); cmd.Parameters.AddWithValue("@exJ", excluirJugadorAnterior); }
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Este jugador ya está inscrito en el equipo seleccionado.",
                                            "Inscripción duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // 2. ¿Dorsal duplicado en el equipo?
                    string qDorsal = esModificacion
                        ? @"SELECT COUNT(*) FROM Club.DetalleEquipo de
                            INNER JOIN Persona.Jugador j ON de.IdJugador=j.IdJugador
                            WHERE de.IdEquipo=@e
                            AND j.Numero=(SELECT Numero FROM Persona.Jugador WHERE IdJugador=@j)
                            AND de.IdJugador<>@j
                            AND NOT (de.IdEquipo=@exE AND de.IdJugador=@exJ)"
                        : @"SELECT COUNT(*) FROM Club.DetalleEquipo de
                            INNER JOIN Persona.Jugador j ON de.IdJugador=j.IdJugador
                            WHERE de.IdEquipo=@e
                            AND j.Numero=(SELECT Numero FROM Persona.Jugador WHERE IdJugador=@j)
                            AND de.IdJugador<>@j";

                    using (SqlCommand cmd = new SqlCommand(qDorsal, conexion))
                    {
                        cmd.Parameters.AddWithValue("@e", idEquipo);
                        cmd.Parameters.AddWithValue("@j", idJugador);
                        if (esModificacion) { cmd.Parameters.AddWithValue("@exE", excluirEquipoAnterior); cmd.Parameters.AddWithValue("@exJ", excluirJugadorAnterior); }
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Ya existe un jugador con el mismo número de dorsal en este equipo.",
                                            "Dorsal duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // 3. ¿El jugador ya está en otro equipo del mismo torneo?
                    string qTorneo = esModificacion
                        ? @"SELECT TOP 1 eOtro.NombreEquipo
                            FROM Club.DetalleEquipo deOtro
                            INNER JOIN Juego.DetalleTorneo dt ON dt.IdEquipo  = deOtro.IdEquipo
                            INNER JOIN Club.Equipo eOtro      ON eOtro.IdEquipo = deOtro.IdEquipo
                            WHERE deOtro.IdJugador=@j AND dt.IdTorneo=@t AND deOtro.IdEquipo<>@e
                            AND NOT (deOtro.IdEquipo=@exE AND deOtro.IdJugador=@exJ)"
                        : @"SELECT TOP 1 eOtro.NombreEquipo
                            FROM Club.DetalleEquipo deOtro
                            INNER JOIN Juego.DetalleTorneo dt ON dt.IdEquipo  = deOtro.IdEquipo
                            INNER JOIN Club.Equipo eOtro      ON eOtro.IdEquipo = deOtro.IdEquipo
                            WHERE deOtro.IdJugador=@j AND dt.IdTorneo=@t AND deOtro.IdEquipo<>@e";

                    using (SqlCommand cmd = new SqlCommand(qTorneo, conexion))
                    {
                        cmd.Parameters.AddWithValue("@j", idJugador);
                        cmd.Parameters.AddWithValue("@t", idTorneo);
                        cmd.Parameters.AddWithValue("@e", idEquipo);
                        if (esModificacion) { cmd.Parameters.AddWithValue("@exE", excluirEquipoAnterior); cmd.Parameters.AddWithValue("@exJ", excluirJugadorAnterior); }
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                MessageBox.Show(
                                    $"El jugador ya está inscrito en '{r["NombreEquipo"]}', " +
                                    $"que también participa en este torneo.\n\n" +
                                    $"Un jugador no puede representar a dos equipos en el mismo torneo.",
                                    "Conflicto en torneo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }

                    // 4. ¿El género del jugador coincide con el torneo?
                    string qGenero = @"
                        SELECT TOP 1 t.NombreTorneo, t.Genero, p.Genero AS GeneroJugador
                        FROM Juego.Torneo t
                        INNER JOIN Persona.Jugador j      ON j.IdJugador      = @j
                        INNER JOIN Persona.Participante p ON p.IdParticipante = j.IdParticipante
                        WHERE t.IdTorneo = @t AND p.Genero <> t.Genero";

                    using (SqlCommand cmd = new SqlCommand(qGenero, conexion))
                    {
                        cmd.Parameters.AddWithValue("@j", idJugador);
                        cmd.Parameters.AddWithValue("@t", idTorneo);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                MessageBox.Show(
                                    $"El torneo '{r["NombreTorneo"]}' es de género '{r["Genero"]}', " +
                                    $"pero el jugador tiene género '{r["GeneroJugador"]}'.",
                                    "Género no compatible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }

                    // 5. ¿La edad del jugador está dentro del rango del torneo?
                    string qEdad = @"
                        SELECT TOP 1 t.NombreTorneo, t.EdadMin, t.EdadMax, p.Edad
                        FROM Juego.Torneo t
                        INNER JOIN Persona.Jugador j      ON j.IdJugador      = @j
                        INNER JOIN Persona.Participante p ON p.IdParticipante = j.IdParticipante
                        WHERE t.IdTorneo = @t AND (p.Edad < t.EdadMin OR p.Edad > t.EdadMax)";

                    using (SqlCommand cmd = new SqlCommand(qEdad, conexion))
                    {
                        cmd.Parameters.AddWithValue("@j", idJugador);
                        cmd.Parameters.AddWithValue("@t", idTorneo);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                MessageBox.Show(
                                    $"El torneo '{r["NombreTorneo"]}' acepta jugadores de " +
                                    $"{r["EdadMin"]} a {r["EdadMax"]} años, " +
                                    $"pero el jugador tiene {r["Edad"]} años.",
                                    "Edad fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al validar inscripción: " + ex.Message);
                    return false;
                }
            }
        }

        // ─── Eventos de combos ────────────────────────────────────────────────

        private void cbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDesdeGrid) return;
            if (cbEquipo.SelectedItem != null && cbEquipo.SelectedIndex != -1)
            {
                DataRowView fila = (DataRowView)cbEquipo.SelectedItem;
                idEquipoSeleccionado = Convert.ToInt32(fila["IdEquipo"]);
                // El IdTorneo ya viene en la fila del combo — no necesitamos combo extra
                idTorneoSeleccionado = Convert.ToInt32(fila["IdTorneo"]);
            }
            else
            {
                idEquipoSeleccionado = -1;
                idTorneoSeleccionado = -1;
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

        // ─── CRUD ─────────────────────────────────────────────────────────────

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idEquipoSeleccionado == -1) { MessageBox.Show("Seleccione un Equipo."); return; }
            if (idJugadorSeleccionado == -1) { MessageBox.Show("Seleccione un Jugador."); return; }

            if (!validarInscripcion(idTorneoSeleccionado, idEquipoSeleccionado, idJugadorSeleccionado))
                return;

            string query = "INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador) VALUES (@e, @j)";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    cmd.Parameters.AddWithValue("@e", idEquipoSeleccionado);
                    cmd.Parameters.AddWithValue("@j", idJugadorSeleccionado);
                    cmd.ExecuteNonQuery();
                    limpiaElementos();
                    cargaDetalleEquipo();
                }
                catch (Exception ex) { ManejadorErroresBD.MostrarErrorAmigable(ex); }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleEquipo.CurrentRow == null || dgvDetalleEquipo.CurrentRow.IsNewRow)
            { MessageBox.Show("Seleccione un registro para modificar."); return; }
            if (idEquipoSeleccionado == -1 || idJugadorSeleccionado == -1)
            { MessageBox.Show("Seleccione un Equipo y un Jugador."); return; }

            if (!validarInscripcion(idTorneoSeleccionado, idEquipoSeleccionado, idJugadorSeleccionado,
                                    idEquipoAnterior, idJugadorAnterior))
                return;

            string query = @"UPDATE Club.DetalleEquipo 
                             SET IdEquipo=@e, IdJugador=@j
                             WHERE IdEquipo=@exE AND IdJugador=@exJ";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    cmd.Parameters.AddWithValue("@e", idEquipoSeleccionado);
                    cmd.Parameters.AddWithValue("@j", idJugadorSeleccionado);
                    cmd.Parameters.AddWithValue("@exE", idEquipoAnterior);
                    cmd.Parameters.AddWithValue("@exJ", idJugadorAnterior);
                    cmd.ExecuteNonQuery();
                    limpiaElementos();
                    cargaDetalleEquipo();
                }
                catch (Exception ex) { ManejadorErroresBD.MostrarErrorAmigable(ex); }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleEquipo.CurrentRow == null || dgvDetalleEquipo.CurrentRow.IsNewRow)
            { MessageBox.Show("Seleccione un registro para eliminar."); return; }

            int idEquipo = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdEquipo"].Value);
            int idJugador = Convert.ToInt32(dgvDetalleEquipo.CurrentRow.Cells["IdJugador"].Value);

            if (MessageBox.Show("¿Está seguro de eliminar este jugador del equipo?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string query = "DELETE FROM Club.DetalleEquipo WHERE IdEquipo=@e AND IdJugador=@j";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    cmd.Parameters.AddWithValue("@e", idEquipo);
                    cmd.Parameters.AddWithValue("@j", idJugador);
                    cmd.ExecuteNonQuery();
                    limpiaElementos();
                    cargaDetalleEquipo();
                }
                catch (Exception ex) { ManejadorErroresBD.MostrarErrorAmigable(ex); }
            }
        }

        private void dgvDetalleEquipo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dgvDetalleEquipo.Rows[e.RowIndex].IsNewRow) return;

                cargandoDesdeGrid = true;
                DataGridViewRow fila = dgvDetalleEquipo.Rows[e.RowIndex];

                int idEquipo = Convert.ToInt32(fila.Cells["IdEquipo"].Value);
                int idJugador = Convert.ToInt32(fila.Cells["IdJugador"].Value);

                idEquipoSeleccionado = idEquipo;
                idJugadorSeleccionado = idJugador;
                idEquipoAnterior = idEquipo;
                idJugadorAnterior = idJugador;

                // Obtenemos el IdTorneo del equipo para las validaciones
                using (SqlConnection conexion = varConexion.conectar())
                {
                    conexion.Open();
                    string q = @"SELECT TOP 1 IdTorneo FROM Juego.DetalleTorneo WHERE IdEquipo = @e";
                    using (SqlCommand cmd = new SqlCommand(q, conexion))
                    {
                        cmd.Parameters.AddWithValue("@e", idEquipo);
                        object result = cmd.ExecuteScalar();
                        idTorneoSeleccionado = result != null ? Convert.ToInt32(result) : -1;
                    }
                }

                // Seleccionar equipo en el combo (buscamos por IdEquipo e IdTorneo
                // para elegir la fila correcta si el equipo está en varios torneos)
                foreach (DataRowView item in cbEquipo.Items)
                {
                    if (Convert.ToInt32(item["IdEquipo"]) == idEquipo &&
                        Convert.ToInt32(item["IdTorneo"]) == idTorneoSeleccionado)
                    { cbEquipo.SelectedItem = item; break; }
                }

                // Seleccionar jugador en el combo
                foreach (DataRowView item in cbJugador.Items)
                {
                    if (Convert.ToInt32(item["IdJugador"]) == idJugador)
                    { cbJugador.SelectedItem = item; break; }
                }

                cargandoDesdeGrid = false;
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
            idTorneoSeleccionado = -1;
            idEquipoSeleccionado = -1;
            idJugadorSeleccionado = -1;
            idEquipoAnterior = -1;
            idJugadorAnterior = -1;
        }
    }
}