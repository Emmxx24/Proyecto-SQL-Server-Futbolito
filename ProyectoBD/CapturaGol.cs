using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoBD
{

    public partial class CapturaGol : Form
    {
        ClaseConexion varConexion;
        int idGolSeleccionado;
        int idPartidoFK;
        int idPartidoSeleccionado;

        public CapturaGol(int? idPartido = null)
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaGoles();
            limpiaElementos();
            if (idPartido != null)
            {
                idPartidoFK = (int)idPartido;
                cargaJugadores(idPartidoFK);
                //cargaEquipos(idPartidoFK);
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void cargaGoles()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open(); // Aseguramos que la conexión esté abierta

                    // La consulta SQL corregida
                    string query = @"
                SELECT 
                    g.IdGol, 
                    jug.IdJugador, p.IdPartido,
                    CONCAT(jug.IdJugador, ' ', par.NombreParticipante, ' [', jug.Posicion, ' - ', jug.Numero, ']') AS Jugador, 
                    CONCAT('EL: ', el.NombreEquipo, ' - EV: ', ev.NombreEquipo, ' - ', p.Fecha, ' [', l.Nombre, ']') AS Partido,
                    g.Minuto,  
                    eqAnotador.IdEquipo AS IdEqGol,
                    eqAnotador.NombreEquipo AS EquipoAnotador
                FROM Evento.Gol g
                INNER JOIN Persona.Jugador jug 
                    ON g.IdJugador = jug.IdJugador
                INNER JOIN Persona.Participante par 
                    ON par.IdParticipante = jug.IdParticipante
                INNER JOIN Evento.Partido p 
                    ON g.IdPartido = p.IdPartido
                INNER JOIN Club.Equipo el 
                    ON p.IdLocal = el.IdEquipo
                INNER JOIN Club.Equipo ev 
                    ON p.IdVisitante = ev.IdEquipo
                INNER JOIN Club.DetalleEquipo de 
                    ON de.IdJugador = jug.IdJugador 
                    AND (de.IdEquipo = p.IdLocal OR de.IdEquipo = p.IdVisitante)
                INNER JOIN Club.Equipo eqAnotador 
                    ON eqAnotador.IdEquipo = de.IdEquipo 
                INNER JOIN Juego.Lugar l 
                    ON p.IdLugar = l.IdLugar
                ORDER BY g.IdGol";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvGoles.DataSource = tabla;

                    // Nombres de las columnas que sí ve el usuario
                    dgvGoles.Columns["IdGol"].HeaderText = "ID Gol";
                    dgvGoles.Columns["Jugador"].HeaderText = "Jugador";
                    dgvGoles.Columns["EquipoAnotador"].HeaderText = "Equipo Anotador"; // ¡Columna nueva!
                    dgvGoles.Columns["Partido"].HeaderText = "Partido";
                    dgvGoles.Columns["Minuto"].HeaderText = "Minuto";

                    // Ocultar IDs internos que el usuario no necesita ver
                    dgvGoles.Columns["IdJugador"].Visible = false;
                    dgvGoles.Columns["IdPartido"].Visible = false;
                    dgvGoles.Columns["IdEqGol"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Goles: " + ex.Message);
                }
            }
        }

        /*
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
        }*/

        /*
        private void cbEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEquipos.SelectedValue != null && int.TryParse(cbEquipos.SelectedValue.ToString(), out int idEquipo))
                cargaJugadores(idPartido);
        }*/

        private void dgvGoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvGoles.Rows[e.RowIndex];
                    idGolSeleccionado = Convert.ToInt32(fila.Cells["IdGol"].Value);
                    cargaJugadores(Convert.ToInt32(fila.Cells["IdPartido"].Value));
                    //DEBE DE VENIR cargaJugadores EN VEZ DE cargaEquipos
                    //cargaEquipos(Convert.ToInt32(fila.Cells["IdPartido"].Value));

                    //cbEquipos.SelectedValue = Convert.ToInt32(fila.Cells["IdEqGol"].Value);
                    numericMin.Value = Convert.ToInt32(fila.Cells["Minuto"].Value);
                    idPartidoSeleccionado = Convert.ToInt32(fila.Cells["IdPartido"].Value);
                    cbJugadores.SelectedValue = Convert.ToInt32(fila.Cells["IdJugador"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el Resultado: " + ex.Message);
            }
        }

        private void cargaJugadores(int idPartido)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    string query = @"
                    SELECT
                    j.IdJugador, CONCAT(p.NombreParticipante, ' [', j.Posicion, ' - ', j.Numero, '] - ', e.NombreEquipo) AS Jugador
                    FROM Persona.Jugador j
                    INNER JOIN Persona.Participante p
                    ON p.IdParticipante = j.IdParticipante
                    INNER JOIN Club.DetalleEquipo de
                    ON j.IdJugador = de.IdJugador
                    INNER JOIN Club.Equipo e
                    ON e.IdEquipo = de.IdEquipo
                    INNER JOIN Evento.Partido pa
                    ON (e.IdEquipo = pa.IdLocal OR e.IdEquipo = pa.IdVisitante) 
                    WHERE pa.IdPartido = @idPartido;";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@idPartido", idPartido);
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

        private int buscaEquipo(int idJugador)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                string query = @"
                    SELECT e.IdEquipo
                    FROM Club.Equipo e
                    INNER JOIN Club.DetalleEquipo de ON e.IdEquipo = de.IdEquipo
                    WHERE de.IdJugador = @idJugador";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@idJugador", idJugador);
                    try
                    {
                        conexion.Open();
                        object result = comando.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idEquipo))
                            return idEquipo;
                        else
                            return -1; // No se encontró el equipo
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar el equipo del jugador: " + ex.Message);
                        return -1;
                    }
                }
            }
        }

        private void limpiaElementos()
        {
            idGolSeleccionado = -1;
            //idPartidoFK = -1;
            idPartidoSeleccionado = -1;
            //cbEquipos.SelectedIndex = -1;
            cbJugadores.SelectedIndex = -1;
            numericMin.Value = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idPartidoFK == -1)
            {
                MessageBox.Show("Selecciona un Resultado desde el formulario de CapturaResultado");
                return;
            }
            if (cbJugadores.SelectedValue == null || numericMin.Value <= 0)
            {
                MessageBox.Show("Completa todos los campos/el minuto no puede ser 0");
                return;
            }
            //Verificar esto:
            //int idEquipo = Convert.ToInt32(cbEquipos.SelectedValue);
            int idJugador = Convert.ToInt32(cbJugadores.SelectedValue);
            int minuto = Convert.ToInt32(numericMin.Value);
            int idEquipo = buscaEquipo(idJugador);

            int verifMinuto = verificaMinuto(idPartidoFK, minuto);
            switch (verifMinuto)
            {
                case -1:
                    MessageBox.Show("Error al verificar el minuto del gol.");
                    return;
                case 0: break; // 0 = Todo bien
                default:
                    MessageBox.Show("El minuto del gol no puede ser cero ni mayor a la hora de termino");
                    return;
            }

            //Verificar esto
            int verifGoles = verificaCantidadGoles(idPartidoFK, idEquipo, 0);
            switch (verifGoles)
            {
                case -1: return;
                case 0: break; // 0 = Todo bien
                default:
                    MessageBox.Show("¡Límite alcanzado! Este equipo ya no puede registrar más goles según el resultado final del partido.");
                    return;
            }

            int verifDuplicado = verificaMinutoDuplicado(idPartidoFK, minuto, 0);
            switch (verifDuplicado)
            {
                case -1: return;
                case 0: break; // 0 = Todo bien
                default:
                    MessageBox.Show("Ya existe un gol registrado en ese exacto minuto para este partido.");
                    return;
            }

            int verifEstado = verificaEstadoEnMinuto(idPartidoFK, idJugador, minuto);
            switch (verifEstado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Este jugador está Suspendido, no se le puede registrar un Gol");
                    return;

            }

            string query = @"INSERT INTO Evento.Gol (IdJugador, IdPartido, Minuto) 
                     VALUES(@idJugador, @idPartido, @minuto)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.AddWithValue("@idJugador", idJugador);
                    command.Parameters.AddWithValue("@idPartido", idPartidoFK);
                    command.Parameters.AddWithValue("@minuto", minuto);
                    command.ExecuteNonQuery();
                    cargaGoles();
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
            if (idGolSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un Gol de la tabla para modificar");
                return;
            }
            if (idPartidoSeleccionado == -1 || cbJugadores.SelectedValue == null || numericMin.Value <= 0)
            {
                MessageBox.Show("Complete todos los campos ");
                return;
            }

            //Verificar esto
            //int idEquipo = Convert.ToInt32(cbEquipos.SelectedValue);
            int idJugador = Convert.ToInt32(cbJugadores.SelectedValue);
            int minuto = Convert.ToInt32(numericMin.Value);
            int idEquipo = buscaEquipo(idJugador);

            int verifMinuto = verificaMinuto(idPartidoSeleccionado, minuto);
            switch (verifMinuto)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("El minuto del gol no puede ser cero ni mayor a la hora de termino");
                    return;
            }
            //Verificar esto:
            int verifGoles = verificaCantidadGoles(idPartidoSeleccionado, idEquipo, 1);
            switch (verifGoles)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("¡Límite alcanzado! Este equipo ya no puede registrar más goles según el resultado final del partido.");
                    return;
            }

            int verifDuplicado = verificaMinutoDuplicado(idPartidoSeleccionado, minuto, 1);
            switch (verifDuplicado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Ya existe un gol registrado en ese exacto minuto para este partido.");
                    return;
            }

            int verifEstado = verificaEstadoEnMinuto(idPartidoSeleccionado, idJugador, minuto);
            switch (verifEstado)
            {
                case -1: return;
                case 0: break;
                default:
                    MessageBox.Show("Este jugador está Suspendido, no se le puede registrar un Gol");
                    return;

            }

            string query = @"UPDATE Evento.Gol 
                     SET IdJugador = @idJugador, Minuto = @minuto 
                     WHERE IdGol = @idGol";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@idJugador", idJugador);
                command.Parameters.AddWithValue("@minuto", minuto);
                command.Parameters.AddWithValue("@idGol", idGolSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    cargaGoles();
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
            if (idGolSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Gol para eliminar");
                return;
            }
            string query = "DELETE FROM Evento.Gol WHERE IdGol = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idGolSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Partido eliminado correctamente");
                    cargaGoles();
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

        private int verificaCantidadGoles(int idPartido, int idEquipo, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                // Aplicamos tu truco del OR IdGol != @idGol en la subconsulta de goles registrados
                string query = @"
            SELECT 
                (SELECT CASE WHEN p.IdLocal = @idEquipo THEN r.GolesLocal ELSE r.GolesVisitante END
                 FROM Evento.Partido p
                 INNER JOIN Evento.ResultadoPartido r ON p.IdPartido = r.IdPartido
                 WHERE p.IdPartido = @idPartido) 
                - 
                (SELECT COUNT(g.IdGol)
                 FROM Evento.Gol g
                 INNER JOIN Club.DetalleEquipo de ON g.IdJugador = de.IdJugador
                 WHERE g.IdPartido = @idPartido AND de.IdEquipo = @idEquipo
                 AND (@esModificacion = 0 OR g.IdGol != @idGolSeleccionado))";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@idEquipo", idEquipo);
                command.Parameters.AddWithValue("@idGolSeleccionado", idGolSeleccionado);
                command.Parameters.AddWithValue("@esModificacion", esModificacion);

                try
                {
                    conexion.Open();
                    int golesDisponibles = Convert.ToInt32(command.ExecuteScalar());

                    // Adaptado a tu estilo: 0 = Todo bien, 1 = Error (límite alcanzado)
                    if (golesDisponibles > 0) return 0;
                    else return 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la cantidad de goles: " + ex.Message);
                    return -1;
                }
            }
        }

        private int verificaMinutoDuplicado(int idPartido, int minuto, int esModificacion)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                // Aplicamos tu truco del OR IdGol != @idGol 
                string query = @"
            SELECT COUNT(*) 
            FROM Evento.Gol 
            WHERE IdPartido = @idPartido AND Minuto = @minuto 
            AND (@esModificacion = 0 OR IdGol != @idGolSeleccionado)";

                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@idPartido", idPartido);
                command.Parameters.AddWithValue("@minuto", minuto);
                command.Parameters.AddWithValue("@idGolSeleccionado", idGolSeleccionado);
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

    }
}
