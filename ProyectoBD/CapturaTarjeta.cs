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
        int idTorneoFK;
        int idPartidoSeleccionado;
        int idTorneoSeleccionado;

        public CapturaTarjeta(int? idPartido = null)
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaTarjetas();
            limpiaElementos();
            if (idPartido != null)
            {
                idPartidoFK = (int)idPartido;
                idTorneoFK = obtenTorneo(idPartidoFK);
                cargaDatosPartidoForaneo(idPartidoFK);
                cargaJugadores(idPartidoFK, idTorneoFK);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                //MessageBox.Show(idTorneoFK.ToString());
            }
        }

        private int obtenTorneo(int idPartido)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT t.IdTorneo
                    FROM Evento.Partido p
                    INNER JOIN Juego.Jornada j ON p.IdJornada = j.IdJornada
                    INNER JOIN Juego.Torneo t ON j.IdTorneo = t.IdTorneo
                    WHERE p.IdPartido = @idPartido;";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idPartido", idPartido);
                    object result = comando.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el torneo para el partido especificado.");
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el torneo: " + ex.Message);
                    return -1;
                }
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
                    jug.IdJugador, p.IdPartido, 
                    CONCAT(jug.IdJugador, ' ', par.NombreParticipante, ' [', jug.Posicion, ' - ', jug.Numero, ']') AS Jugador,  
                    CONCAT('EL: ', el.NombreEquipo, ' - EV: ', ev.NombreEquipo, ' - ', p.Fecha, ' [', l.Nombre, ']') AS DatosPartido,
                    tar.Minuto, 
                    eqAfectado.IdEquipo AS IdEqAfec,
                    eqAfectado.NombreEquipo AS EquipoAfectado,
                    t.IdTorneo AS IdTorneo
                FROM Evento.Tarjeta tar
                INNER JOIN Persona.Jugador jug 
                    ON tar.IdJugador = jug.IdJugador
                INNER JOIN Persona.Participante par 
                    ON par.IdParticipante = jug.IdParticipante
                INNER JOIN Evento.Partido p 
                    ON tar.IdPartido = p.IdPartido
                INNER JOIN Juego.Lugar l
                    ON p.IdLugar = l.IdLugar
                INNER JOIN Club.Equipo el 
                    ON p.IdLocal = el.IdEquipo
                INNER JOIN Club.Equipo ev 
                    ON p.IdVisitante = ev.IdEquipo
                INNER JOIN Club.DetalleEquipo de 
                    ON de.IdJugador = jug.IdJugador 
                    AND (de.IdEquipo = p.IdLocal OR de.IdEquipo = p.IdVisitante)
                INNER JOIN Club.Equipo eqAfectado
                    ON eqAfectado.IdEquipo = de.IdEquipo 
                INNER JOIN Juego.Jornada j
                    ON j.IdJornada = p.IdJornada
                INNER JOIN Juego.Torneo t
                    ON t.IdTorneo = j.IdTorneo  
                ORDER BY tar.IdTarjeta";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvTarjetas.DataSource = tabla;

                    dgvTarjetas.Columns["IdTarjeta"].HeaderText = "ID Tarjeta";
                    dgvTarjetas.Columns["TipoTarjeta"].HeaderText = "Tipo Tarjeta";
                    dgvTarjetas.Columns["Jugador"].HeaderText = "Jugador";
                    //dgvTarjetas.Columns["EquipoAfectado"].HeaderText = "Equipo del jugador";
                    dgvTarjetas.Columns["DatosPartido"].HeaderText = "Partido";
                    //dgvTarjetas.Columns["Fecha"].HeaderText = "Fecha";
                    dgvTarjetas.Columns["Minuto"].HeaderText = "Minuto";
                    //dgvTarjetas.Columns["NumeroJornada"].HeaderText = "Jornada";

                    dgvTarjetas.Columns["IdJugador"].Visible = false;
                    dgvTarjetas.Columns["IdPartido"].Visible = false;
                    dgvTarjetas.Columns["IdEqAfec"].Visible = false;
                    dgvTarjetas.Columns["EquipoAfectado"].Visible = false;
                    dgvTarjetas.Columns["IdTorneo"].Visible = false;
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
            cbJugadores.SelectedIndex = -1;
            numericMin.Value = 0;
            cbTarjeta.SelectedIndex = -1;
            idTorneoSeleccionado = -1;
        }

        private void cargaDatosPartidoForaneo(int idPartido)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT 
                    CONCAT('EL: ', el.NombreEquipo, ' - EV: ', ev.NombreEquipo, ' - ', p.Fecha, ' [', l.Nombre, ']') AS Partido
                    FROM Evento.Partido p
                    INNER JOIN Club.Equipo el
                    ON p.IdLocal = el.IdEquipo
                    INNER JOIN Club.Equipo ev
                    ON p.IdVisitante = ev.IdEquipo
                    INNER JOIN Juego.Lugar l
                    ON p.IdLugar = l.IdLugar
                    WHERE p.IdPartido = @idPartido;";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idPartido", idPartido);
                    object result = comando.ExecuteScalar();
                    //MessageBox.Show("Nombre de participante" + result.ToString());
                    if (result != null)
                    {
                        txtPartidoDetalle.Text = result.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el nombre del participante: " + ex.Message);
                }
            }
        }

        private void cargaJugadores(int idPartido, int idTorneo)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    string query = @"
                    SELECT
                    j.IdJugador, CONCAT(j.IdJugador, ' ', p.NombreParticipante, ' [', j.Posicion, ' - ', j.Numero, '] - ', e.NombreEquipo) AS Jugador
                    FROM Persona.Jugador j
                    INNER JOIN Persona.Participante p
                    ON p.IdParticipante = j.IdParticipante
                    INNER JOIN Club.DetalleEquipo de
                    ON j.IdJugador = de.IdJugador
                    INNER JOIN Club.Equipo e
                    ON e.IdEquipo = de.IdEquipo
                    INNER JOIN Evento.Partido pa
                    ON (e.IdEquipo = pa.IdLocal OR e.IdEquipo = pa.IdVisitante) 
                    INNER JOIN Juego.Jornada jo
                    ON jo.IdJornada = pa.IdJornada
                    INNER JOIN Juego.Torneo t
                    ON t.IdTorneo = jo.IdTorneo
                    WHERE pa.IdPartido = @idPartido
                    AND t.Genero = p.Genero;";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@idPartido", idPartido);                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tablaJugadores = new DataTable();
                        adaptador.Fill(tablaJugadores);
                        cbJugadores.DisplayMember = "Jugador";
                        cbJugadores.ValueMember = "IdJugador";
                        cbJugadores.DataSource = tablaJugadores;
                        cbJugadores.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los jugadores del partido: " + ex.Message);
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
                    idPartidoSeleccionado = Convert.ToInt32(fila.Cells["IdPartido"].Value);
                    idTorneoSeleccionado = Convert.ToInt32(fila.Cells["IdTorneo"].Value);
                    numericMin.Value = Convert.ToInt32(fila.Cells["Minuto"].Value);
                    cbTarjeta.Text = fila.Cells["TipoTarjeta"].Value.ToString();
                    
                    cbJugadores.SelectedValue = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                    //idJugadorSeleccionado = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                    idTorneoSeleccionado = Convert.ToInt32(fila.Cells["IdTorneo"].Value);
                    cargaDatosPartidoForaneo(Convert.ToInt32(fila.Cells["IdPartido"].Value));
                    cargaJugadores(idPartidoSeleccionado, idTorneoSeleccionado);
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
            if (cbJugadores.SelectedValue == null || numericMin.Value <= 0 || cbTarjeta.SelectedIndex < 0)
            {
                MessageBox.Show("Completa todos los campos/el minuto no puede ser 0");
                return;
            }

            int idJugador = Convert.ToInt32(cbJugadores.SelectedValue);
            int minuto = Convert.ToInt32(numericMin.Value);
            string tarjeta = cbTarjeta.Text;

            int verifMinuto = verificaMinuto(idPartidoFK, minuto);
            switch (verifMinuto)
            {
                case -1:
                    MessageBox.Show("Error al verificar el minuto de la tarjeta.");
                    return;
                case 0: break;
                default:
                    MessageBox.Show("El minuto de la tarjeta no puede ser cero ni mayor a la hora de termino");
                    return;
            }

            int verifDuplicado = verificaMinutoDuplicado(idPartidoFK, minuto, 0);
            switch (verifDuplicado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Ya existe una tarjeta registrada en ese exacto minuto para este partido.");
                    return;
            }

            // AQUI ESTA EL CAMBIO PARA EL INSERT: Pasamos -1 porque no hay tarjeta previa que ignorar
            int verifEstado = verificaEstadoEnMinuto(idPartidoFK, idJugador, minuto, -1);
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
            if (idTarjetaSeleccionada == -1)
            {
                MessageBox.Show("Selecciona una Tarjeta de la tabla para modificar");
                return;
            }
            if (idPartidoSeleccionado == -1 || cbJugadores.SelectedValue == null || numericMin.Value <= 0)
            {
                MessageBox.Show("Complete todos los campos ");
                return;
            }

            int idJugador = Convert.ToInt32(cbJugadores.SelectedValue);
            int minuto = Convert.ToInt32(numericMin.Value);
            string tarjeta = cbTarjeta.Text;

            int idJugadorOriginal = Convert.ToInt32(dgvTarjetas.CurrentRow.Cells["IdJugador"].Value);
            int idPartidoOriginal = Convert.ToInt32(dgvTarjetas.CurrentRow.Cells["IdPartido"].Value);

            int verifTiempo = verificaTarjetasViejas(idJugadorOriginal, idPartidoOriginal);
            switch (verifTiempo)
            {
                case -1: return;
                case 1:
                    MessageBox.Show("No puedes modificar esta tarjeta. El jugador ya participó en partidos posteriores y alterar esto corrompería su estado actual y acumulador.", "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            int verifMinuto = verificaMinuto(idPartidoSeleccionado, minuto);
            switch (verifMinuto)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("El minuto de la tarjeta no puede ser cero ni mayor a la hora de termino");
                    return;
            }

            int verifDuplicado = verificaMinutoDuplicado(idPartidoSeleccionado, minuto, 1);
            switch (verifDuplicado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Ya existe una tarjeta registrada en ese exacto minuto para este partido.");
                    return;
            }

            // AQUI ESTA EL CAMBIO PARA EL UPDATE: Pasamos el idTarjetaSeleccionada para ignorarlo
            int verifEstado = verificaEstadoEnMinuto(idPartidoSeleccionado, idJugador, minuto, idTarjetaSeleccionada);
            switch (verifEstado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Este jugador está Suspendido, no se le puede registrar una tarjeta");
                    return;
            }

            string query = @"UPDATE Evento.Tarjeta 
                            SET IdJugador = @idJugador, Minuto = @minuto, 
                            TipoTarjeta = @tipoTarjeta 
                            WHERE IdTarjeta = @idTarjeta";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@idJugador", idJugador);
                command.Parameters.AddWithValue("@minuto", minuto);
                command.Parameters.AddWithValue("@tipoTarjeta", tarjeta);
                command.Parameters.AddWithValue("@idTarjeta", idTarjetaSeleccionada);

                try
                {
                    conexion.Open();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idTarjetaSeleccionada == -1)
            {
                MessageBox.Show("Seleccione una Tarjeta para eliminar");
                return;
            }

            int idJugadorOriginal = Convert.ToInt32(dgvTarjetas.CurrentRow.Cells["IdJugador"].Value);
            int idPartidoOriginal = Convert.ToInt32(dgvTarjetas.CurrentRow.Cells["IdPartido"].Value);

            int verifTiempo = verificaTarjetasViejas(idJugadorOriginal, idPartidoOriginal);
            switch (verifTiempo)
            {
                case -1: return;
                case 1:
                    MessageBox.Show("No puedes eliminar esta tarjeta. El jugador ya participó en partidos posteriores y borrarla corrompería su historial y estado actual.", "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            string query = "DELETE FROM Evento.Tarjeta WHERE IdTarjeta = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idTarjetaSeleccionada);

                try
                {
                    conexion.Open();
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
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar minuto duplicado: " + ex.Message);
                    return -1;
                }
            }
        }

        // AQUI ESTA EL METODO MODIFICADO PARA IGNORAR LA TARJETA
        private int verificaEstadoEnMinuto(int idPartido, int idJugador, int minuto, int idTarjetaIgnorar = -1)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
            SELECT COUNT(*) 
            FROM Evento.Tarjeta 
            WHERE IdPartido = @idPartido 
            AND IdJugador = @idJugador 
            AND Minuto <= @minuto 
            AND IdTarjeta != @idTarjetaIgnorar
            AND (TipoTarjeta = 'Roja' OR 
                 (SELECT COUNT(*) FROM Evento.Tarjeta t2 
                  WHERE t2.IdPartido = @idPartido AND t2.IdJugador = @idJugador 
                  AND t2.TipoTarjeta = 'Amarilla' AND t2.Minuto <= @minuto
                  AND t2.IdTarjeta != @idTarjetaIgnorar) >= 2)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@idJugador", idJugador);
                command.Parameters.AddWithValue("@minuto", minuto);
                command.Parameters.AddWithValue("@idTarjetaIgnorar", idTarjetaIgnorar);

                try
                {
                    conexion.Open();
                    int estaExpulsado = Convert.ToInt32(command.ExecuteScalar());

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