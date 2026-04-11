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
    public partial class CapturaTarjeta : Form
    {
        ClaseConexion varConexion;
        int idTarjetaSeleccionada;
        int idPartidoFK;
        int idPartidoSeleccionado;
        int idJugadorSeleccionado;

        public CapturaTarjeta(int? idPartido = null)
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaTarjetas();
            limpiaElementos();
            if (idPartido != null)
            {
                idPartidoFK = (int)idPartido;
                cargaEquipos(idPartidoFK);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                MessageBox.Show(cbEquipos.SelectedIndex.ToString());
            }
        }

        private void cargaTarjetas()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"
                SELECT 
                    tar.IdTarjeta, tar.TipoTarjeta,
                    jug.IdJugador, 
                    CONCAT(par.NombreParticipante, ' - ', jug.Numero) AS Jugador, 
                    tar.Minuto, 
                    CONCAT(el.NombreEquipo, ' vs ', ev.NombreEquipo) AS DatosPartido, 
                    p.IdPartido, 
                    p.Fecha, 
                    j.NumeroJornada, 
                    eqAfectado.IdEquipo AS IdEqAfec,
                    eqAfectado.NombreEquipo AS EquipoAfectado
                FROM Evento.Tarjeta tar
                INNER JOIN Persona.Jugador jug 
                    ON tar.IdJugador = jug.IdJugador
                INNER JOIN Persona.Participante par 
                    ON par.IdParticipante = jug.IdParticipante
                INNER JOIN Evento.Partido p 
                    ON tar.IdPartido = p.IdPartido
                INNER JOIN Club.Equipo el 
                    ON p.IdLocal = el.IdEquipo
                INNER JOIN Club.Equipo ev 
                    ON p.IdVisitante = ev.IdEquipo
                INNER JOIN Juego.Jornada j 
                    ON j.IdJornada = p.IdJornada
                INNER JOIN Club.DetalleEquipo de 
                    ON de.IdJugador = jug.IdJugador 
                    AND (de.IdEquipo = p.IdLocal OR de.IdEquipo = p.IdVisitante)
                INNER JOIN Club.Equipo eqAfectado
                    ON eqAfectado.IdEquipo = de.IdEquipo 
                ORDER BY tar.IdTarjeta";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvTarjetas.DataSource = tabla;

                    // Nombres de las columnas que sí ve el usuario
                    dgvTarjetas.Columns["IdTarjeta"].HeaderText = "ID Tarjeta";
                    dgvTarjetas.Columns["TipoTarjeta"].HeaderText = "Tipo Tarjeta";
                    dgvTarjetas.Columns["Jugador"].HeaderText = "Jugador (Dorsal)";
                    dgvTarjetas.Columns["EquipoAfectado"].HeaderText = "Equipo del jugador"; // ¡Columna nueva!
                    dgvTarjetas.Columns["DatosPartido"].HeaderText = "Partido";
                    dgvTarjetas.Columns["Fecha"].HeaderText = "Fecha";
                    dgvTarjetas.Columns["Minuto"].HeaderText = "Minuto";
                    dgvTarjetas.Columns["NumeroJornada"].HeaderText = "Jornada";

                    // Ocultar IDs internos que el usuario no necesita ver
                    dgvTarjetas.Columns["IdJugador"].Visible = false;
                    dgvTarjetas.Columns["IdPartido"].Visible = false;
                    dgvTarjetas.Columns["IdEqAfec"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Tarjetas: " + ex.Message);
                }
            }
        }

        private void limpiaElementos()
        {
            idTarjetaSeleccionada = -1;
            idPartidoFK = -1;
            idPartidoSeleccionado = -1;
            cbEquipos.SelectedIndex = -1;
            cbJugadores.SelectedIndex = -1;
        }

        private void cargaEquipos(int idPartido)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    string query = @"
                    SELECT e.IdEquipo, e.NombreEquipo
                    FROM Club.Equipo e
                    INNER JOIN Evento.Partido p 
                        ON e.IdEquipo = p.IdLocal OR e.IdEquipo = p.IdVisitante
                    WHERE p.IdPartido = @IdPartido";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPartido", idPartido);
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tablaEquipos = new DataTable();
                        adaptador.Fill(tablaEquipos);
                        cbEquipos.DisplayMember = "NombreEquipo"; // Lo que el usuario lee 
                        cbEquipos.ValueMember = "IdEquipo";       // El ID que guarda
                        cbEquipos.DataSource = tablaEquipos;
                        cbEquipos.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los equipos del partido: " + ex.Message);
                }
            }
        }

        private void cbEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEquipos.SelectedValue != null && int.TryParse(cbEquipos.SelectedValue.ToString(), out int idEquipo))
                cargaJugadores(idEquipo);
        }

        private void cargaJugadores(int idEquipo)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    string query = @"
                    SELECT
                    j.IdJugador, CONCAT(p.NombreParticipante, ' [', j.Posicion, ' - ', j.Numero, ']') AS Jugador
                    FROM Persona.Jugador j
                    INNER JOIN Persona.Participante p
                    ON p.IdParticipante = j.IdParticipante
                    INNER JOIN Club.DetalleEquipo de
                    ON j.IdJugador = de.IdJugador
                    INNER JOIN Club.Equipo e
                    ON e.IdEquipo = de.IdEquipo
                    WHERE e.IdEquipo = @idEquipo;";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@idEquipo", idEquipo);
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tablaJugadores = new DataTable();
                        adaptador.Fill(tablaJugadores);
                        cbJugadores.DisplayMember = "Jugador"; // Lo que el usuario lee 
                        cbJugadores.ValueMember = "IdJugador";       // El ID que guarda
                        cbJugadores.DataSource = tablaJugadores;
                        cbJugadores.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los equipos del partido: " + ex.Message);
                }
            }
        }

        private void dgvTarjetas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvTarjetas.Rows[e.RowIndex];
                    idTarjetaSeleccionada = Convert.ToInt32(fila.Cells["IdTarjeta"].Value);
                    cargaEquipos(Convert.ToInt32(fila.Cells["IdPartido"].Value));
                    cbEquipos.SelectedValue = Convert.ToInt32(fila.Cells["IdEqAfec"].Value);
                    numericMin.Value = Convert.ToInt32(fila.Cells["Minuto"].Value);
                    cbTarjeta.Text = fila.Cells["TipoTarjeta"].Value.ToString();
                    idPartidoSeleccionado = Convert.ToInt32(fila.Cells["IdPartido"].Value);
                    cbJugadores.SelectedValue = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                    idJugadorSeleccionado = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Resultado: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idPartidoFK == -1)
            {
                MessageBox.Show("Selecciona un Resultado desde el formulario de CapturaResultado");
                return;
            }
            if (cbEquipos.SelectedValue == null || cbJugadores.SelectedValue == null ||
                numericMin.Value <= 0 || cbTarjeta.SelectedValue == null)
            {
                MessageBox.Show("Completa todos los campos/el minuto no puede ser 0");
                return;
            }

            int idEquipo = Convert.ToInt32(cbEquipos.SelectedValue);
            int idJugador = Convert.ToInt32(cbJugadores.SelectedValue);
            int minuto = Convert.ToInt32(numericMin.Value);
            string tarjeta = cbTarjeta.Text;

            int verifMinuto = verificaMinuto(idPartidoFK, minuto);
            switch (verifMinuto)
            {
                case -1:
                    MessageBox.Show("Error al verificar el minuto de la tarjeta.");
                    return;
                case 0: break; // 0 = Todo bien
                default:
                    MessageBox.Show("El minuto de la tarjeta no puede ser cero ni mayor a la hora de termino");
                    return;
            }

            int verifDuplicado = verificaMinutoDuplicado(idPartidoFK, minuto, 0);
            switch (verifDuplicado)
            {
                case -1: return;
                case 0: break; // 0 = Todo bien
                default:
                    MessageBox.Show("Ya existe una tarjeta registrada en ese exacto minuto para este partido.");
                    return;
            }

            int verifEstado = verificaEstadoEnMinuto(idPartidoFK, idJugador, minuto);
            switch (verifEstado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Este jugador está Suspendido, no se le puede registrar una tarjeta");
                    return;

            }

            string query = @"INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, Minuto, TipoTarjeta) 
                     VALUES(@idJugador, @idPartido, @minuto, @tipoTarjeta)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idJugador", idJugador);
                    command.Parameters.AddWithValue("@idPartido", idPartidoFK);
                    command.Parameters.AddWithValue("@minuto", minuto);
                    command.Parameters.AddWithValue("@tipoTarjeta", tarjeta);
                    command.ExecuteNonQuery();
                    cargaTarjetas();
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

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private int verificaMinuto(int idPartido, int minuto)
        {
            if (minuto <= 0) return 1;

            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
            SELECT DATEDIFF(MINUTE, p.HoraInicio, r.HoraFin)
            FROM Evento.Partido p
            INNER JOIN Evento.ResultadoPartido r ON p.IdPartido = r.IdPartido
            WHERE p.IdPartido = @idPartido";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);

                try
                {
                    conexion.Open();
                    int duracionTotal = Convert.ToInt32(command.ExecuteScalar());

                    if (minuto <= duracionTotal) return 0;
                    else return 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la duración del partido: " + ex.Message);
                    return -1;
                }
            }
        }

        private int verificaMinutoDuplicado(int idPartido, int minuto, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Evento.Tarjeta 
            WHERE IdPartido = @idPartido AND Minuto = @minuto 
            AND (@esModificacion = 0 OR IdTarjeta != @idTarjetaSeleccionada)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@minuto", minuto);
                command.Parameters.AddWithValue("@idTarjetaSeleccionada", idTarjetaSeleccionada);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);

                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count; // Regresa 0 si no hay duplicados, o > 0 si hay choque
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar minuto duplicado: " + ex.Message);
                    return -1;
                }
            }
        }

        private int verificaEstadoEnMinuto(int idPartido, int idJugador, int minuto)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Evento.Tarjeta 
            WHERE IdPartido = @idPartido 
            AND IdJugador = @idJugador 
            AND Minuto <= @minuto 
            AND (TipoTarjeta = 'Roja' OR 
                 (SELECT COUNT(*) FROM Evento.Tarjeta t2 
                  WHERE t2.IdPartido = @idPartido AND t2.IdJugador = @idJugador 
                  AND t2.TipoTarjeta = 'Amarilla' AND t2.Minuto <= @minuto) >= 2)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@idJugador", idJugador);
                command.Parameters.AddWithValue("@minuto", minuto);

                try
                {
                    conexion.Open();
                    int estaExpulsado = Convert.ToInt32(command.ExecuteScalar());

                    // Siguiendo tu estándar: 0 es todo bien, 1 es que ya la cagó
                    if (estaExpulsado > 0) return 1;
                    else return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la línea de tiempo del jugador: " + ex.Message);
                    return -1;
                }
            }
        }

        private int verificaTarjetasViejas(int idJugador, int idPartidoDeLaTarjeta)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
                SELECT COUNT(*) 
                FROM Evento.Partido p
                INNER JOIN Club.DetalleEquipo de ON (p.IdLocal = de.IdEquipo OR p.IdVisitante = de.IdEquipo)
                WHERE de.IdJugador = @idJugador 
                AND p.Estado = 'Jugado'
                AND p.IdPartido != @idPartido
                AND (
                    p.Fecha > (SELECT Fecha FROM Evento.Partido WHERE IdPartido = @idPartido)
                    OR (
                        p.Fecha = (SELECT Fecha FROM Evento.Partido WHERE IdPartido = @idPartido) 
                        AND p.HoraInicio > (SELECT HoraInicio FROM Evento.Partido WHERE IdPartido = @idPartido)
                       )
                )";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idJugador", idJugador);
                command.Parameters.AddWithValue("@idPartido", idPartidoDeLaTarjeta);

                try
                {
                    conexion.Open();
                    int hayPartidosFuturos = Convert.ToInt32(command.ExecuteScalar());

                    // Si hayPartidosFuturos es > 0, significa que el tiempo ya avanzó y no se puede tocar
                    if (hayPartidosFuturos > 0) return 1;
                    else return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la línea temporal: " + ex.Message);
                    return -1;
                }
            }
        }
    }
}
