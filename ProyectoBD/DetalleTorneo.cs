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
    public partial class DetalleTorneo : Form
    {
        public DetalleTorneo()
        {
            InitializeComponent();
        }

        private void DetalleTorneo_Load(object sender, EventArgs e)
        {
            CargarTorneos();
            CargarEquipos();
            CargarDetalleTorneo();

            cmbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void CargarTorneos()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                        T.IdTorneo, 
                        T.NombreTorneo + ' (' + 
                        CONVERT(varchar(10), T.FechaInicio, 103) + ' - ' + 
                        CONVERT(varchar(10), T.FechaFin, 103) + ')' AS NombreCompleto
                        FROM Juego.Torneo T";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbTorneo.DataSource = dt;
                    cmbTorneo.DisplayMember = "NombreCompleto";
                    cmbTorneo.ValueMember = "IdTorneo";
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void dgvDetalleTorneo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvDetalleTorneo.Rows[e.RowIndex];
                cmbTorneo.SelectedValue = fila.Cells["IdTorneo"].Value;
                cmbEquipo.SelectedValue = fila.Cells["IdEquipo"].Value;
            }
        }

        void CargarEquipos()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT IdEquipo, NombreEquipo FROM Club.Equipo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbEquipo.DataSource = dt;
                    cmbEquipo.DisplayMember = "NombreEquipo";
                    cmbEquipo.ValueMember = "IdEquipo";

                }
                catch (Exception e)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(e);
                }
            }
        }



        private void bttnInscribir_Click(object sender, EventArgs e)
        {
            ClaseConexion con = new ClaseConexion();

            if (cmbTorneo.SelectedIndex == -1 || cmbEquipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un torneo y un equipo");
                return;
            }

            using (SqlConnection conn = con.conectar())
            {
                try
                {

                    conn.Open();
                    string query = @"INSERT INTO DetalleTorneo(IdTorneo,IdEquipo)
                             VALUES(@torneo,@equipo)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                    cmd.Parameters.AddWithValue("@equipo", cmbEquipo.SelectedValue);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Equipo inscrito correctamente");

                    CargarDetalleTorneo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void cmbTorneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTorneo.SelectedValue == null)
                return;
        }

        private void cmbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipo.SelectedValue == null)
                return;
        }

        void CargarDetalleTorneo()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {

                    conn.Open();
                    string query = @"
                    SELECT DT.IdTorneo,
                    DT.IdEquipo,
                    T.NombreTorneo,
                    E.NombreEquipo,
                    CONCAT(CONVERT(varchar(10), J.FechaInicio, 103), ' - ', CONVERT(varchar(10), J.FechaFin, 103)) AS PeriodoJornada
                    FROM DetalleTorneo DT
                    INNER JOIN Juego.Torneo T ON DT.IdTorneo = T.IdTorneo
                    INNER JOIN Club.Equipo E ON DT.IdEquipo = E.IdEquipo
                    INNER JOIN Juego.Jornada J ON DT.IdTorneo = J.IdTorneo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dgvDetalleTorneo.DataSource = dt;

                }
                catch (Exception ex) { ManejadorErroresBD.MostrarErrorAmigable(ex); }
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            ClaseConexion con = new ClaseConexion();
            if (dgvDetalleTorneo.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro");
                return;
            }

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    int idTorneo = Convert.ToInt32(dgvDetalleTorneo.CurrentRow.Cells["IdTorneo"].Value);
                    int idEquipo = Convert.ToInt32(dgvDetalleTorneo.CurrentRow.Cells["IdEquipo"].Value);

                    string query = @"UPDATE DetalleTorneo
                         SET IdTorneo=@torneoNuevo,
                             IdEquipo=@equipoNuevo
                         WHERE IdTorneo=@torneo AND IdEquipo=@equipo";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@torneoNuevo", cmbTorneo.SelectedValue);
                    cmd.Parameters.AddWithValue("@equipoNuevo", cmbEquipo.SelectedValue);
                    cmd.Parameters.AddWithValue("@torneo", idTorneo);
                    cmd.Parameters.AddWithValue("@equipo", idEquipo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro modificado");

                    CargarDetalleTorneo();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            ClaseConexion con = new ClaseConexion();
            if (dgvDetalleTorneo.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro");
                return;
            }
            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    int idTorneo = Convert.ToInt32(dgvDetalleTorneo.CurrentRow.Cells["IdTorneo"].Value);
                    int idEquipo = Convert.ToInt32(dgvDetalleTorneo.CurrentRow.Cells["IdEquipo"].Value);

                    string query = @"DELETE FROM DetalleTorneo
                         WHERE IdTorneo=@torneo AND IdEquipo=@equipo";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@torneo", idTorneo);
                    cmd.Parameters.AddWithValue("@equipo", idEquipo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro eliminado");

                    CargarDetalleTorneo();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void cmbTorneo_DropDown(object sender, EventArgs e)
        {
            CargarTorneos();
        }

        private void cmbEquipo_DropDown(object sender, EventArgs e)
        {
            CargarEquipos();
        }
    }
}
