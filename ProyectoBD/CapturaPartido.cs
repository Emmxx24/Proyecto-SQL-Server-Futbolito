//FALTARA ACTUALIZAR LAS CONCATENACIONES DE JORNADA Y EQUIPO CUANDO LA MAESTRA LAS AUTORICE
//Y VERIFICAR QUE LAS CONCATENACIONES QUE PUSE ESTÁN CORRECTAS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ProyectoBD
{
    public partial class CapturaPartido : Form
    {
        private ClaseConexion varConexion;
        private int idPartidoSeleccionado = -1;
        private int idJornadaSeleccuionada = -1;
        public CapturaPartido()
        {
            varConexion = new ClaseConexion();
            InitializeComponent();
            cargaPartidos();
            cargaJornadas();
            cargaArbitros();
            cargaLugares();
            limpiaElementos();

        }
        private void cargaLugares()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = "SELECT IdLugar, CONCAT(Nombre, ' [', Capacidad, ']') AS Nombre FROM Juego.Lugar";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tablaLugares = new DataTable();
                adaptador.Fill(tablaLugares);

                cbLugar.DataSource = tablaLugares;
                cbLugar.DisplayMember = "Nombre";
                cbLugar.ValueMember = "IdLugar";
            }
        }
        private void cargaPartidos()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT p.IdPartido, 
                    t.IdTorneo, p.IdJornada, p.IdArbitro, p.IdLugar, p.IdLocal, p.IdVisitante,
                    el.NombreEquipo AS eqloc, ev.NombreEquipo AS eqvisi, 
                    CONCAT(p.IdArbitro, ' - ',pa.NombreParticipante) AS Arbitro, 
                    /*CONCAT(t.NombreTorneo, ' (', t.FechaInicio, ' - ', t.FechaFin, ')') AS DatosTorneo,*/  
                    CONCAT(t.NombreTorneo, ' (', t.FechaInicio, ' - ', t.FechaFin, ' Jornada ', j.NumeroJornada, ')') AS Jornada, 
                    CONCAT(l.Nombre, ' [', Capacidad, ']') AS NombreLugar, p.Fecha, p.HoraInicio, p.Estado 
                    FROM Evento.Partido p INNER JOIN Juego.Jornada j 
                    ON p.IdJornada = j.IdJornada INNER JOIN Club.Equipo el 
                    ON p.IdLocal = el.IdEquipo INNER JOIN Club.Equipo ev 
                    ON p.IdVisitante = ev.IdEquipo INNER JOIN Persona.Arbitro a 
                    ON p.IdArbitro = a.IdArbitro INNER JOIN Persona.Participante pa 
                    ON pa.IdParticipante = a.IdParticipante INNER JOIN Juego.Lugar l 
                    ON p.IdLugar = l.IdLugar 
                    INNER JOIN Juego.Torneo t ON j.IdTorneo = t.IdTorneo";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvPartidos.DataSource = tabla;

                    dgvPartidos.Columns["IdPartido"].HeaderText = "ID Partido";
                    dgvPartidos.Columns["eqloc"].HeaderText = "Equipo Local";
                    dgvPartidos.Columns["eqvisi"].HeaderText = "Equipo Visitante";
                    dgvPartidos.Columns["Arbitro"].HeaderText = "Id Árbitro - Árbitro";
                    //dgvPartidos.Columns["DatosTorneo"].HeaderText = "Torneo (fecha inicio - fecha fin)";
                    dgvPartidos.Columns["Jornada"].HeaderText = "Jornada";
                    dgvPartidos.Columns["NombreLugar"].HeaderText = "Lugar [capacidad]";
                    dgvPartidos.Columns["Fecha"].HeaderText = "Fecha";
                    dgvPartidos.Columns["HoraInicio"].HeaderText = "Hora de inicio";
                    dgvPartidos.Columns["Estado"].HeaderText = "Estado";
                    //no visibles pero importantes para manejar los combobox
                    dgvPartidos.Columns["IdTorneo"].Visible = false;
                    dgvPartidos.Columns["IdJornada"].Visible = false;
                    dgvPartidos.Columns["IdArbitro"].Visible = false;
                    dgvPartidos.Columns["IdLugar"].Visible = false;
                    dgvPartidos.Columns["IdLocal"].Visible = false;
                    dgvPartidos.Columns["IdVisitante"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Partidos: " + ex.Message);
                }
            }
        }

        private void cargaArbitros()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    string query = @"SELECT a.IdArbitro,
                                    CONCAT(a.IdArbitro, ' - ',p.NombreParticipante) AS Arbitro
                                    FROM Persona.Arbitro a
                                    INNER JOIN Persona.Participante p
                                    ON a.IdParticipante = p.IdParticipante";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tablaArbitros = new DataTable();
                    adaptador.Fill(tablaArbitros);

                    cbArbitro.ValueMember = "IdArbitro";
                    cbArbitro.DisplayMember = "Arbitro";
                    cbArbitro.DataSource = tablaArbitros;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Árbitros: " + ex.Message);
                }
            }
        }
        private void limpiaElementos()
        {
            cbJornada.SelectedIndex = -1;
            cbArbitro.SelectedIndex = -1;
            cbLugar.SelectedIndex = -1;
            cbLocal.SelectedIndex = -1;
            cbVisitante.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;
            idPartidoSeleccionado = -1;
            actualizaBotones(0);
        }

        private void dgvPartidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvPartidos.Rows[e.RowIndex];
                    idPartidoSeleccionado = Convert.ToInt32(fila.Cells["IdPartido"].Value);
                    //obtener id de torneo, con algun inner join de la jornada

                    cbJornada.SelectedValue = Convert.ToInt32(fila.Cells["IdJornada"].Value);
                    cbLocal.SelectedValue = Convert.ToInt32(fila.Cells["IdLocal"].Value);
                    cbVisitante.SelectedValue = Convert.ToInt32(fila.Cells["IdVisitante"].Value);
                    cbArbitro.SelectedValue = Convert.ToInt32(fila.Cells["IdArbitro"].Value);
                    cbLugar.SelectedValue = Convert.ToInt32(fila.Cells["IdLugar"].Value);

                    dtpFecha.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
                    TimeSpan horaSql = (TimeSpan)fila.Cells["HoraInicio"].Value;
                    dtpHora.Value = DateTime.Today.Add(horaSql);
                    actualizaBotones(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Partido: " + ex.Message);
            }
        }

        //hacer lo mismo pero para jornada (cargar los equipos nada más)
        private void cbJornada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJornada.SelectedValue != null && int.TryParse(cbJornada.SelectedValue.ToString(), out int idJornada))
            {
                //MessageBox.Show(idJornada.ToString());
                cargaEquipos(idJornada);
                
            }

                
        }
        //private void cbTorneo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbTorneo.SelectedValue != null && int.TryParse(cbTorneo.SelectedValue.ToString(), out int idTorneo))
        //    {
        //        cargaEquipos(idTorneo);
        //cargaJornadas(idTorneo);
        //    }
        //}

        private void cargaEquipos(int idJornada)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"SELECT e.IdEquipo, e.NombreEquipo 
                               FROM Club.Equipo e
                               INNER JOIN Juego.DetalleTorneo dt
                               ON e.IdEquipo = dt.IdEquipo
                               INNER JOIN Juego.Torneo t
                               ON t.IdTorneo = dt.IdTorneo
                               INNER JOIN Juego.Jornada j
                               ON j.IdTorneo = dt.IdTorneo
                               WHERE j.IdJornada = @idJornada";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idJornada", idJornada);

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tablaEquipos = new DataTable();
                adaptador.Fill(tablaEquipos);

                cbLocal.ValueMember = "IdEquipo";
                cbLocal.DisplayMember = "NombreEquipo";
                cbLocal.DataSource = tablaEquipos;

                DataTable tablaVisitantes = tablaEquipos.Copy();
                cbVisitante.ValueMember = "IdEquipo";
                cbVisitante.DisplayMember = "NombreEquipo";
                cbVisitante.DataSource = tablaVisitantes;
            }
        }
        private void cargaJornadas()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"SELECT j.IdJornada, 
                                CONCAT(t.NombreTorneo, ' (', t.FechaInicio, ' - ', 
                                t.FechaFin, ' Jornada ', j.NumeroJornada, ')') 
                                AS Jornada 
                                FROM Juego.Jornada j
                                INNER JOIN Juego.Torneo t
                                ON j.IdTorneo = t.IdTorneo";
                SqlCommand comando = new SqlCommand(query, conexion);
                //comando.Parameters.AddWithValue("@IdTorneo", idTorneo);

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tablaJornadas = new DataTable();
                adaptador.Fill(tablaJornadas);

                cbJornada.DisplayMember = "Jornada";
                cbJornada.ValueMember = "j.IdJornada";
                cbJornada.DataSource = tablaJornadas;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbArbitro.SelectedIndex == -1 ||
                cbJornada.SelectedIndex == -1 || cbLugar.SelectedIndex == -1 ||
                cbLocal.SelectedIndex == -1 || cbVisitante.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            int idArbitro = Convert.ToInt32(cbArbitro.SelectedValue);
            int idJornada = Convert.ToInt32(cbJornada.SelectedValue);
            int idLugar = Convert.ToInt32(cbLugar.SelectedValue);
            int equiLocal = Convert.ToInt32(cbLocal.SelectedValue);
            int equiVisitante = Convert.ToInt32(cbVisitante.SelectedValue);
            DateTime fecha = dtpFecha.Value.Date;
            TimeSpan horaSql = new TimeSpan(dtpHora.Value.Hour, dtpHora.Value.Minute, 0);
            if (equiLocal == equiVisitante)
            {
                MessageBox.Show("El equipo local y el equipo visitante no pueden ser el mismo.",
                                "Error de lógica", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int verificaArbitro = verificarArbitro(idArbitro, fecha, horaSql, 0);
            switch (verificaArbitro)
            {
                case -1:
                    MessageBox.Show("Error al verificar el árbitro.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("El árbitro seleccionado ya está asignado a otro partido a la misma hora");
                    return;
            }

            int verificaLug = verificaLugar(idLugar, fecha, horaSql, 0);
            switch (verificaLug)
            {
                case -1:
                    MessageBox.Show("Error al verificar el lugar.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("El lugar seleccionado ya está asignado a otro partido a la misma hora");
                    return;
            }

            int verifPartInv = verificaPartidoInverso(equiLocal, equiVisitante, idJornada, 0);
            switch (verifPartInv)
            {
                case -1:
                    MessageBox.Show("Error al verificar el partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("Ya hay un partido de estos equipos en la misma jornada");
                    return;
            }

            int verifEquDisp = verificarEquiposDisponibles(equiLocal, equiVisitante, fecha, horaSql, 0);
            switch (verifEquDisp)
            {
                case -1:
                    MessageBox.Show("Error al verificar el partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("Ya hay un partido de estos equipos en la misma fecha");
                    return;
            }

            string query = "INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) " +
                           "VALUES (@idArbitro, @idJornada, @idLugar, @idLocal, @idVisitante, @fecha, @horaInicio)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idArbitro", idArbitro);
                    command.Parameters.AddWithValue("@idJornada", idJornada);
                    command.Parameters.AddWithValue("@idLugar", idLugar);
                    command.Parameters.AddWithValue("@idLocal", equiLocal);
                    command.Parameters.AddWithValue("@idVisitante", equiVisitante);
                    command.Parameters.AddWithValue("@fecha", fecha);
                    command.Parameters.AddWithValue("@horaInicio", horaSql);
                    command.ExecuteNonQuery();
                    cargaPartidos();
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
            if (idPartidoSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un Partido de la tabla para modificar");
                return;
            }

            if (cbArbitro.SelectedIndex == -1 || cbJornada.SelectedIndex == -1 ||
                cbLugar.SelectedIndex == -1 || cbLocal.SelectedIndex == -1 ||
                cbVisitante.SelectedIndex == -1)
            {
                MessageBox.Show("Complete todos los campos ");
                return;
            }

            int idArbitro = Convert.ToInt32(cbArbitro.SelectedValue);
            int idJornada = Convert.ToInt32(cbJornada.SelectedValue);
            int idLugar = Convert.ToInt32(cbLugar.SelectedValue);
            int equiLocal = Convert.ToInt32(cbLocal.SelectedValue);
            int equiVisitante = Convert.ToInt32(cbVisitante.SelectedValue);
            DateTime fecha = dtpFecha.Value.Date;
            TimeSpan horaSql = new TimeSpan(dtpHora.Value.Hour, dtpHora.Value.Minute, 0);
            if (equiLocal == equiVisitante)
            {
                MessageBox.Show("El equipo local y el equipo visitante no pueden ser el mismo");
                return;
            }

            int verificaArbitro = verificarArbitro(idArbitro, fecha, horaSql, 1);
            switch (verificaArbitro)
            {
                case -1:
                    MessageBox.Show("Error al verificar el árbitro.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("El árbitro seleccionado ya está asignado a otro partido a la misma hora");
                    return;
            }

            int verificaLug = verificaLugar(idLugar, fecha, horaSql, 1);
            switch (verificaLug)
            {
                case -1:
                    MessageBox.Show("Error al verificar el lugar.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("El lugar seleccionado ya está asignado a otro partido a la misma hora");
                    return;
            }

            int verifPartInv = verificaPartidoInverso(equiLocal, equiVisitante, idJornada, 1);
            switch (verifPartInv)
            {
                case -1:
                    MessageBox.Show("Error al verificar el partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("Ya hay un partido de estos equipos en la misma jornada");
                    return;
            }

            int verifEquDisp = verificarEquiposDisponibles(equiLocal, equiVisitante, fecha, horaSql, 1);
            switch (verifEquDisp)
            {
                case -1:
                    MessageBox.Show("Error al verificar el partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("Ya hay un partido de estos equipos en la misma fecha");
                    return;
            }

            string query = "UPDATE Evento.Partido " +
                           "SET IdArbitro = @idArbitro, " +
                           "IdJornada = @idJornada, " +
                           "IdLugar = @idLugar, " +
                           "IdLocal = @idLocal, " +
                           "IdVisitante = @idVisitante, " +
                           "Fecha = @fecha, " +
                           "HoraInicio = @horaInicio " +
                           "WHERE IdPartido = @idPartido";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@idArbitro", idArbitro);
                command.Parameters.AddWithValue("@idJornada", idJornada);
                command.Parameters.AddWithValue("@idLugar", idLugar);
                command.Parameters.AddWithValue("@idLocal", equiLocal);
                command.Parameters.AddWithValue("@idVisitante", equiVisitante);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@horaInicio", horaSql);
                command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    cargaPartidos();
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
            if (idPartidoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Partido para eliminar");
                return;
            }
            string query = "DELETE FROM Evento.Partido WHERE IdPartido = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idPartidoSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Partido eliminado correctamente");
                    cargaPartidos();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }


        //Todas las funciones de verificación siguen la misma lógica:
        //Se utiliza sql para primero verificar si es modificacion, si sí, ya se omite el resto del bloque
        //si no, toma en cuenta el id del partido que se está modificando para no contarlo en la busqueda

        //Verifica que el árbito no tenga otro partido asignado en la misma fecha y hora
        private int verificarArbitro(int idArbitro, DateTime fecha, TimeSpan horaInicio, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = "SELECT COUNT(*) FROM Evento.Partido " +
                "WHERE IdArbitro = @idArbitro " +
                "AND Fecha = @fecha " +
                "AND HoraInicio = @horaInicio " +
                "AND (@esModificacion = 0 OR IdPartido != @idPartido)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idArbitro", idArbitro);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@horaInicio", horaInicio);
                command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);
                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el árbitro: " + ex.Message);
                    return -1;
                }
            }
        }

        //Verifica que no haya un partido en la misma fecha en la misma hora en el mismo lugar
        private int verificaLugar(int idLugar, DateTime fecha, TimeSpan horaInicio, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = "SELECT COUNT(*) FROM Evento.Partido " +
                "WHERE IdLugar = @idLugar " +
                "AND Fecha = @fecha " +
                "AND HoraInicio = @horaInicio " +
                "AND (@esModificacion = 0 OR IdPartido != @idPartido)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idLugar", idLugar);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@horaInicio", horaInicio);
                command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);
                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la disponibilidad del lugar: " + ex.Message);
                    return -1;
                }
            }
        }

        //verificar partido inverso (mismos equipos pero en diferente orden) en la misma jornada
        private int verificaPartidoInverso(int idLocal, int idVisitante, int idJornada, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = "SELECT COUNT(*) FROM Evento.Partido " +
                "WHERE ((IdLocal = @idLocal AND IdVisitante = @idVisitante) " +
                "OR (IdLocal = @idVisitante AND IdVisitante = @idLocal)) " +
                "AND IdJornada = @idJornada " +
                "AND (@esModificacion = 0 OR IdPartido != @idPartido)";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idLocal", idLocal);
                command.Parameters.AddWithValue("@idVisitante", idVisitante);
                command.Parameters.AddWithValue("@idJornada", idJornada);
                command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);
                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el partido inverso: " + ex.Message);
                    return -1;
                }
            }
        }

        // Verifica que ninguno de los dos equipos esté ocupado exactamente a la misma hora
        private int verificarEquiposDisponibles(int idLocal, int idVisitante, DateTime fecha, TimeSpan horaInicio, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"SELECT COUNT(*) 
                         FROM Evento.Partido 
                         WHERE Fecha = @fecha 
                         AND HoraInicio = @horaInicio
                         AND (IdLocal IN (@idLocal, @idVisitante) OR IdVisitante IN (@idLocal, @idVisitante))
                         AND (@esModificacion = 0 OR IdPartido != @idPartido)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idLocal", idLocal);
                command.Parameters.AddWithValue("@idVisitante", idVisitante);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@horaInicio", horaInicio);
                command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);

                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar los equipos: " + ex.Message);
                    return -1;
                }
            }
        }

        private void actualizaBotones(int op)
        {
            if (op == 1)
            {
                btnRegisResult.Enabled = true;
                btnRegisResult.Visible = true;
            }
            else
            {
                btnRegisResult.Enabled = false;
                btnRegisResult.Visible = false;
            }
        }

        private void btnRegisResult_Click(object sender, EventArgs e)
        {
            if (idPartidoSeleccionado != -1)
            {
                Form formulario = new CapturaResultado(idPartidoSeleccionado);
                formulario.ShowDialog();
                limpiaElementos();
            }
            else
            {
                MessageBox.Show("Selecciona un Participante");
                return;
            }
        }


    }
}