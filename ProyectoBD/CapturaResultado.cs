//VERIFICAR QUE CONCATENACIONES VA A PEDIR LA MAESTRA XD

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
    public partial class CapturaResultado : Form
    {
        int idPartidoFK; //para saber que partido es el que se est{a pasando como parámetro desde
        //el formulario de partido
        ClaseConexion varConexion;
        int idResultadoSeleccionado;
        int idPartidoSeleccionado; //para saber que id de partido corresponde a la modificacion

        public CapturaResultado(int? idPartido = null)
        {
            InitializeComponent();
            idResultadoSeleccionado = -1;
            varConexion = new ClaseConexion();
            cargaResultados();
            limpiaElementos();
            if (idPartido != null)
            {
                idPartidoFK = (int)idPartido;
                cargaDatosPartidoForaneo(idPartidoFK);
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void cargaResultados()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT 
                    rp.IdResultado, rp.IdPartido, CONCAT(el.NombreEquipo, ' - ', rp.GolesLocal) AS EquipoLocal, 
                    CONCAT(ev.NombreEquipo, ' - ', rp.GolesVisitante) AS EquipoVisitante, 
                    p.Fecha, p.HoraInicio, rp.HoraFin, j.NumeroJornada,
                    rp.GolesLocal, rp.GolesVisitante

                    FROM Evento.ResultadoPartido rp
                    INNER JOIN Evento.Partido p
                    ON rp.IdPartido = p.IdPartido
                    INNER JOIN Club.Equipo el
                    ON p.IdLocal = el.IdEquipo
                    INNER JOIN Club.Equipo ev
                    ON p.IdVisitante = ev.IdEquipo
                    INNER JOIN Juego.Jornada j
                    ON p.IdJornada = j.IdJornada";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvResultados.DataSource = tabla;
                    dgvResultados.Columns["IdResultado"].HeaderText = "ID Resultado";
                    dgvResultados.Columns["EquipoLocal"].HeaderText = "Equipo Local";
                    dgvResultados.Columns["EquipoVisitante"].HeaderText = "Equipo Visitante";
                    dgvResultados.Columns["IdPartido"].HeaderText = "ID Partido";
                    dgvResultados.Columns["Fecha"].HeaderText = "Fecha";
                    dgvResultados.Columns["HoraInicio"].HeaderText = "Hora de inicio";
                    dgvResultados.Columns["HoraFin"].HeaderText = "Hora de término";
                    dgvResultados.Columns["NumeroJornada"].HeaderText = "Jornada";
                    dgvResultados.Columns["IdPartido"].Visible = false;
                    dgvResultados.Columns["GolesLocal"].Visible = false;
                    dgvResultados.Columns["GolesVisitante"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Partidos: " + ex.Message);
                }
            }
        }

        private void cargaDatosPartidoForaneo(int idPartido)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT 
                                CONCAT(el.NombreEquipo, ' vs ', ev.NombreEquipo, ' - ', 
                                j.NumeroJornada, ' - ', p.Fecha) AS DatosPartido
                                FROM Evento.Partido p
                                INNER JOIN Club.Equipo el
                                ON p.IdLocal = el.IdEquipo
                                INNER JOIN Club.Equipo ev
                                ON p.IdVisitante = ev.IdEquipo
                                INNER JOIN Juego.Jornada j
                                ON j.IdJornada = p.IdJornada
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


        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvResultados.Rows[e.RowIndex];
                    idResultadoSeleccionado = Convert.ToInt32(fila.Cells["IdResultado"].Value);
                    cargaDatosPartidoForaneo(Convert.ToInt32(fila.Cells["IdPartido"].Value));
                    numericGolesLocal.Value = Convert.ToInt32(fila.Cells["GolesLocal"].Value);
                    numericGolesVisitante.Value = Convert.ToInt32(fila.Cells["GolesVisitante"].Value);
                    TimeSpan horaSql = (TimeSpan)fila.Cells["HoraFin"].Value;
                    dtpHoraTermino.Value = DateTime.Today.Add(horaSql);
                    idPartidoSeleccionado = Convert.ToInt32(fila.Cells["IdPartido"].Value);
                    actualizaBotones(1);
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
                MessageBox.Show("Selecciona un Partido desde el formulario de Partido");
                return;
            }
            int golesLocal = Convert.ToInt32(numericGolesLocal.Value);
            int golesVisitante = Convert.ToInt32(numericGolesVisitante.Value);
            TimeSpan horaFin = new TimeSpan(dtpHoraTermino.Value.Hour, dtpHoraTermino.Value.Minute, 0);
            string query = @"INSERT INTO Evento.ResultadoPartido
                            (IdPartido, GolesLocal, GolesVisitante, HoraFin)
                            VALUES(@idPartido, @golesLocal, @golesVisitante, @horaFin)";
            int verifHora = verificaHoraFinInicio(idPartidoFK, horaFin);
            switch (verifHora)
            {
                case -1:
                    MessageBox.Show("Error al verificar la hora del partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("La hora de termino no puede ser antes que la de inicio");
                    return;
            }
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idPartido", idPartidoFK);
                    command.Parameters.AddWithValue("@golesLocal", golesLocal);
                    command.Parameters.AddWithValue("@golesVisitante", golesVisitante);
                    command.Parameters.AddWithValue("@horaFin", horaFin);
                    command.ExecuteNonQuery();

                    //MessageBox.Show("Se agregó con éxito al nuevo Participante");
                    cargaResultados();
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
            if (idResultadoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un resultado de la tabla para modificar.");
                return;
            }
            int golesLocal = Convert.ToInt32(numericGolesLocal.Value);
            int golesVisitante = Convert.ToInt32(numericGolesVisitante.Value);
            TimeSpan horaFin = new TimeSpan(dtpHoraTermino.Value.Hour, dtpHoraTermino.Value.Minute, 0);
            int verifHora = verificaHoraFinInicio(idPartidoSeleccionado, horaFin);
            switch (verifHora)
            {
                case -1:
                    MessageBox.Show("Error al verificar la hora del partido.");
                    return;
                case 0:
                    break;
                default:
                    MessageBox.Show("La hora de termino no puede ser antes que la de inicio.");
                    return;
            }

            string query = @"UPDATE Evento.ResultadoPartido 
                     SET GolesLocal = @golesLocal, 
                         GolesVisitante = @golesVisitante, 
                         HoraFin = @horaFin 
                     WHERE IdPartido = @idPartido";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idPartido", idPartidoSeleccionado);
                    command.Parameters.AddWithValue("@golesLocal", golesLocal);
                    command.Parameters.AddWithValue("@golesVisitante", golesVisitante);
                    command.Parameters.AddWithValue("@horaFin", horaFin);

                    command.ExecuteNonQuery();
                    cargaResultados();
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
            if (idResultadoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Resultado para eliminar");
                return;
            }
            string query = "DELETE FROM Evento.ResultadoPartido WHERE IdResultado = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idResultadoSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Partido eliminado correctamente");
                    cargaResultados();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private int verificaHoraFinInicio(int idPartido, TimeSpan horaFin)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"SELECT COUNT(*) 
                         FROM Evento.Partido 
                         WHERE IdPartido = @idPartido 
                         AND HoraInicio >= @horaFin";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@horaFin", horaFin);

                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al consultar la hora del partido: " + ex.Message);
                    return -1;
                }
            }
        }

        private void limpiaElementos()
        {
            idResultadoSeleccionado = -1;
            txtPartidoDetalle.Clear();
            numericGolesLocal.Value = 0;
            numericGolesVisitante.Value = 0;
            idPartidoSeleccionado = -1;
            idPartidoFK = -1;
            actualizaBotones(0);
        }

        private void actualizaBotones(int op)
        {
            if (op == 1)
            {
                btnRegistraGoles.Enabled = true;
                btnRegistraGoles.Visible = true;
                btnRegistraTarjetas.Enabled = true;
                btnRegistraTarjetas.Visible = true;
            }
            else
            {
                btnRegistraGoles.Enabled = false;
                btnRegistraGoles.Visible = false;
                btnRegistraTarjetas.Enabled = false;
                btnRegistraTarjetas.Visible = false;
            }
        }

        private void btnRegistraGoles_Click(object sender, EventArgs e)
        {
            if (idResultadoSeleccionado != -1)
            {
                Form formulario = new CapturaGol(idPartidoSeleccionado);
                formulario.ShowDialog();
                limpiaElementos();
            }
            else
            {
                MessageBox.Show("Selecciona un Participante");
                return;
            }
        }

        private void btnRegistraTarjetas_Click(object sender, EventArgs e)
        {

        }
    }
}
