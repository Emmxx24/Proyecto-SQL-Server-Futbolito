using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class CapturaEquipo : Form
    {
        private ClaseConexion varConexion;
        private int idEquipoSeleccionado = -1;

        public CapturaEquipo()
        {
            InitializeComponent();
            varConexion = new ClaseConexion();
            inicializaGrid();
            cargaEquipos();
            limpiaElementos();
        }

        private void inicializaGrid()
        {
            dgvEquipo.RowTemplate.Height = 60;
            dgvEquipo.AutoGenerateColumns = false;
            dgvEquipo.Columns.Clear();

            dgvEquipo.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "IdEquipo",
                HeaderText = "ID Equipo",
                DataPropertyName = "IdEquipo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvEquipo.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NombreEquipo",
                HeaderText = "Nombre del Equipo",
                DataPropertyName = "NombreEquipo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "ImagenLogo";
            imgCol.HeaderText = "Logo";
            imgCol.Width = 80;
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvEquipo.Columns.Add(imgCol);

            dgvEquipo.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CantJugadores",
                HeaderText = "Cant. Jugadores",
                DataPropertyName = "CantJugadores",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvEquipo.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Logo",
                DataPropertyName = "Logo",
                Visible = false
            });
        }

        private void cargaEquipos()
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT IdEquipo, NombreEquipo, Logo, CantJugadores FROM Club.Equipo";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dgvEquipo.DataSource = tabla;

                    // Aqui se captura la imagen
                    foreach (DataGridViewRow fila in dgvEquipo.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        string url = fila.Cells["Logo"].Value?.ToString();
                        if (!string.IsNullOrEmpty(url))
                        {
                            try
                            {
                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers.Add("User-Agent", "Mozilla/5.0");
                                    byte[] data = wc.DownloadData(url); // Aqui descarga la imagen de internet
                                    using (MemoryStream ms = new MemoryStream(data))
                                    {
                                        fila.Cells["ImagenLogo"].Value = Image.FromStream(ms); // Aqui la imagen se muestra en el grid
                                    }
                                }
                            }
                            catch
                            {
                                fila.Cells["ImagenLogo"].Value = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar equipos: " + ex.Message);
                }
            }
        }

        private bool existeNombreEquipo(string nombre, int idExcluir = -1)
        {
            using (SqlConnection conexion = varConexion.conectar())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT COUNT(*) FROM Club.Equipo WHERE NombreEquipo = @nombre AND IdEquipo != @id";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@id", idExcluir);
                    return Convert.ToInt32(comando.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar nombre: " + ex.Message);
                    return false;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text.Trim();
            string logo = tbLogo.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(logo))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }

            // Validar que no exista un equipo con el mismo nombre
            if (existeNombreEquipo(nombre))
            {
                MessageBox.Show("Ya existe un equipo con ese nombre");
                return;
            }

            string query = "INSERT INTO Club.Equipo (NombreEquipo, Logo, CantJugadores) " +
                           "VALUES (@nombre, @logo, 0)";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@logo", logo);
                    comando.ExecuteNonQuery();

                    cargaEquipos();
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
            if (idEquipoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Equipo para modificar");
                return;
            }
            string nombre = tbNombre.Text.Trim();
            string logo = tbLogo.Text.Trim();
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(logo))
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }
            if (existeNombreEquipo(nombre, idEquipoSeleccionado))
            {
                MessageBox.Show("Ya existe un equipo con ese nombre");
                return;
            }

            string query = "UPDATE Club.Equipo SET NombreEquipo = @nombre, Logo = @logo " +
                           "WHERE IdEquipo = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@logo", logo);
                    comando.Parameters.AddWithValue("@id", idEquipoSeleccionado);
                    comando.ExecuteNonQuery();

                    cargaEquipos();
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
            if (idEquipoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un Equipo para eliminar");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este equipo?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            string query = "DELETE FROM Club.Equipo WHERE IdEquipo = @id";

            using (SqlConnection conexion = varConexion.conectar())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                try
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@id", idEquipoSeleccionado);
                    comando.ExecuteNonQuery();
                    cargaEquipos();
                    limpiaElementos();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void dgvEquipo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !dgvEquipo.Rows[e.RowIndex].IsNewRow)
                {
                    DataGridViewRow fila = dgvEquipo.Rows[e.RowIndex];
                    idEquipoSeleccionado = Convert.ToInt32(fila.Cells["IdEquipo"].Value);
                    tbNombre.Text = fila.Cells["NombreEquipo"].Value.ToString();
                    tbLogo.Text = fila.Cells["Logo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar equipo: " + ex.Message);
            }
        }

        private void limpiaElementos()
        {
            tbNombre.Clear();
            tbLogo.Clear();
            idEquipoSeleccionado = -1;
        }

        private void tbLogo_TextChanged(object sender, EventArgs e)
        {
        }
    }
}