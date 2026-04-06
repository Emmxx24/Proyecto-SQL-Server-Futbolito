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
    }
}
