using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class CapturaTorneos : Form
    {
        private ClaseConexion varConexion;
        private int idTorneoSeleccionado = -1;
        public CapturaTorneos()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            cargaTorneos();
            limpiaElementos();
        }

        public void cargaTorneos()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT * FROM Juego.Torneo";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvTorneo.DataSource = tabla;

                    dgvTorneo.Columns["IdTorneo"].HeaderText = "ID Torneo";
                    dgvTorneo.Columns["NombreTorneo"].HeaderText = "Nombre del Torneo";
                    dgvTorneo.Columns["EdadMin"].HeaderText = "Edad mínima";
                    dgvTorneo.Columns["EdadMax"].HeaderText = "Edad máxima";
                    dgvTorneo.Columns["Genero"].HeaderText = "Género del torneo";
                    dgvTorneo.Columns["FechaInicio"].HeaderText = "Fecha de inicio";
                    dgvTorneo.Columns["FechaFin"].HeaderText = "Fecha de fin";
                    dgvTorneo.Columns["CantEquipos"].HeaderText = "Cantidad de equipos";
                    dgvTorneo.Columns["NumJornadas"].HeaderText = "Número de jornadas";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Participante: " + ex.Message);
                }
            }
        }

        private void dgvTorneo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !dgvTorneo.Rows[e.RowIndex].IsNewRow) //agregar validacion de cuando se haga click en una row que no tiene nada xd
                {
                    DataGridViewRow fila = dgvTorneo.Rows[e.RowIndex];

                    idTorneoSeleccionado = Convert.ToInt32(fila.Cells["IdTorneo"].Value);
                    txtNombreTorneo.Text = fila.Cells["NombreTorneo"].Value.ToString();
                    numericEdadMax.Value = Convert.ToInt32(fila.Cells["EdadMax"].Value);
                    numericEdadMin.Value = Convert.ToInt32(fila.Cells["EdadMin"].Value);
                    cbGenero.Text = fila.Cells["Genero"].Value.ToString();
                    mcFechaIni.SetDate(Convert.ToDateTime(fila.Cells["FechaInicio"].Value));
                    mcFechaFin.SetDate(Convert.ToDateTime(fila.Cells["FechaFin"].Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el torneo: " + ex.Message);
                limpiaElementos();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTorneo.Text;
            int edadMin = Convert.ToInt32(numericEdadMin.Value);
            int edadMax = Convert.ToInt32(numericEdadMax.Value);
            string genero = cbGenero.Text;
            DateTime fechaIni = mcFechaIni.SelectionStart.Date;
            DateTime fechaFin = mcFechaFin.SelectionStart.Date;

            if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(genero) ||
                    string.IsNullOrEmpty(edadMin.ToString()) ||
                    string.IsNullOrEmpty(edadMax.ToString()) ||
                    string.IsNullOrEmpty(fechaIni.ToString()) ||
                    string.IsNullOrEmpty(fechaFin.ToString())
                )
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            else
            {
                if (edadMin > edadMax)
                {
                    MessageBox.Show("La edad mínima no puede ser mayor que la edad máxima");
                    return;
                }
                if (fechaIni > fechaFin)
                {
                    MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin");
                    return;
                }
                if (fechaFin == fechaIni)
                {
                    MessageBox.Show("La fecha de inicio no puede ser igual a la fecha de fin");
                    return;
                }
            }
            string query = "INSERT INTO Juego.Torneo" +
                           "(NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin) " +
                           "VALUES (@nombre, @edadMin, @edadMax, @genero, @fechaIni, @fechaFin)";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@edadMin", edadMin);
                    command.Parameters.AddWithValue("@edadMax", edadMax);
                    command.Parameters.AddWithValue("@genero", genero);
                    command.Parameters.AddWithValue("@fechaIni", fechaIni);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Se agregó con éxito al nuevo Torneo");
                    cargaTorneos();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void btnElim_Click(object sender, EventArgs e)
        {
            if (idTorneoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Torneo para eliminar");
                return;
            }
            string query = "DELETE FROM Juego.Torneo WHERE idTorneo = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idTorneoSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Torneo eliminado correctamente");
                    cargaTorneos();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            if (idTorneoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Torneo para modificar");
                return;
            }
            string nombreEquipo = txtNombreTorneo.Text;
            int edadMin = Convert.ToInt32(numericEdadMin.Value);
            int edadMax = Convert.ToInt32(numericEdadMax.Value);
            string genero = cbGenero.Text;
            DateTime fechaIni = mcFechaIni.SelectionStart.Date;
            DateTime fechaFin = mcFechaFin.SelectionStart.Date;
            if (string.IsNullOrEmpty(nombreEquipo) ||
                    string.IsNullOrEmpty(genero) ||
                    string.IsNullOrEmpty(edadMin.ToString()) ||
                    string.IsNullOrEmpty(edadMax.ToString()) ||
                    string.IsNullOrEmpty(fechaIni.ToString()) ||
                    string.IsNullOrEmpty(fechaFin.ToString())
                )
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            else
            {
                if (edadMin > edadMax)
                {
                    MessageBox.Show("La edad mínima no puede ser mayor que la edad máxima");
                    return;
                }
                if (fechaIni > fechaFin)
                {
                    MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin");
                    return;
                }
                if (fechaFin == fechaIni)
                {
                    MessageBox.Show("La fecha de inicio no puede ser igual a la fecha de fin");
                    return;
                }
            }
            string query = "UPDATE Juego.Torneo " +
                           "SET NombreTorneo = @nombreEquipo, " +
                           "EdadMin = @edadMin," +
                           "EdadMax = @edadMax," +
                           "Genero = @genero," +
                           "FechaInicio = @fechaIni," +
                           "FechaFin = @fechaFin " +
                           "WHERE IdTorneo = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@nombreEquipo", nombreEquipo);
                command.Parameters.AddWithValue("@edadMin", edadMin);
                command.Parameters.AddWithValue("@edadMax", edadMax);
                command.Parameters.AddWithValue("@genero", genero);
                command.Parameters.AddWithValue("@fechaIni", fechaIni);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);
                command.Parameters.AddWithValue("@id", idTorneoSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante modificado correctamente");
                    cargaTorneos();
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
            txtNombreTorneo.Clear();
            numericEdadMin.Value = 0;
            numericEdadMax.Value = 0;
            cbGenero.SelectedIndex = 0;
            mcFechaIni.SetDate(DateTime.Today);
            mcFechaFin.SetDate(DateTime.Today);
            idTorneoSeleccionado = -1;
        }
    }
}