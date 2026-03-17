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
    public partial class CapturaLugar : Form
    {
        private ClaseConexion varConexion;
        private int idLugarSeleccionado = -1;

        public CapturaLugar()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            limpiaElementos();
            cargaLugares();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreLugar.Text;
            string ubicacion = txtUbiLugar.Text;
            int capacidad = Convert.ToInt32(numericCapacidad.Value);

            if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(ubicacion) ||
                    string.IsNullOrEmpty(capacidad.ToString())
                )
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            else
            {
                if (capacidad == 0)
                {
                    MessageBox.Show("Ingrese una capacidad válida");
                    return;
                }
                else if (capacidad <= 1000)
                {
                    MessageBox.Show("La capacidad debe ser mayor a 1000");
                    return;
                }
            }
            string query = "INSERT INTO Juego.Lugar" +
                           "(Ubicacion, Nombre, Capacidad) " +
                           "VALUES (@ubicacion, @nombre, @capacidad)";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@ubicacion", ubicacion);
                    command.Parameters.AddWithValue("@capacidad", capacidad);
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Se agregó con éxito al nuevo Torneo");
                    cargaLugares();
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
            if (idLugarSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Lugar para modificar");
                return;
            }
            string nombre = txtNombreLugar.Text;
            string ubicacion = txtUbiLugar.Text;
            int capacidad = Convert.ToInt32(numericCapacidad.Value);

            if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(ubicacion) ||
                    string.IsNullOrEmpty(capacidad.ToString())
                )
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            else
            {
                if (capacidad == 0)
                {
                    MessageBox.Show("Ingrese una capacidad válida");
                    return;
                }
                else if (capacidad <= 1000)
                {
                    MessageBox.Show("La capacidad debe ser mayor a 1000");
                    return;
                }
            }
            string query = "UPDATE Juego.Lugar " +
                           "SET Nombre = @nombre, " +
                           "Ubicacion = @ubicacion," +
                           "Capacidad = @capacidad " +
                           "WHERE IdLugar = @idLugar"
                           ;

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@ubicacion", ubicacion);
                command.Parameters.AddWithValue("@capacidad", capacidad);
                command.Parameters.AddWithValue("@idLugar", idLugarSeleccionado);
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante modificado correctamente");
                    cargaLugares();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void cargaLugares()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT * FROM Juego.Lugar";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvLugar.DataSource = tabla;
                    dgvLugar.Columns["IdLugar"].HeaderText = "ID Lugar";
                    dgvLugar.Columns["Nombre"].HeaderText = "Nombre del Lugar";
                    dgvLugar.Columns["Ubicacion"].HeaderText = "Ubicación";
                    dgvLugar.Columns["Capacidad"].HeaderText = "Capacidad";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar Participante: " + ex.Message);
                }
            }
        }

        private void limpiaElementos()
        {
            txtNombreLugar.Text = "";
            txtUbiLugar.Text = "";
            numericCapacidad.Value = 0;
            idLugarSeleccionado = -1;
        }

        private void dgvLugar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !dgvLugar.Rows[e.RowIndex].IsNewRow)
                {
                    DataGridViewRow fila = dgvLugar.Rows[e.RowIndex];
                    idLugarSeleccionado = Convert.ToInt32(fila.Cells["IdLugar"].Value);
                    txtNombreLugar.Text = fila.Cells["Nombre"].Value.ToString();
                    txtUbiLugar.Text = fila.Cells["Ubicacion"].Value.ToString();
                    numericCapacidad.Value = Convert.ToInt32(fila.Cells["Capacidad"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el torneo: " + ex.Message);
                limpiaElementos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idLugarSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Lugar para eliminar");
                return;
            }
            string query = "DELETE FROM Juego.Lugar WHERE IdLugar = @id";
            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", idLugarSeleccionado);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Participante eliminado correctamente");
                    cargaLugares();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }
    }
}
