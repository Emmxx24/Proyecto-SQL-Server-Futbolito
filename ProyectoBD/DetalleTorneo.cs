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
                conn.Open();

                string query = "SELECT IdTorneo, NombreTorneo FROM Torneo";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                cmbTorneo.DataSource = dt;
                cmbTorneo.DisplayMember = "NombreTorneo";
                cmbTorneo.ValueMember = "IdTorneo";
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
                conn.Open();

                string query = "SELECT IdEquipo, NombreEquipo FROM Equipo";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                cmbEquipo.DataSource = dt;
                cmbEquipo.DisplayMember = "NombreEquipo";
                cmbEquipo.ValueMember = "IdEquipo";
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
                conn.Open();

                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    int idTorneo = Convert.ToInt32(cmbTorneo.SelectedValue);
                    int idEquipo = Convert.ToInt32(cmbEquipo.SelectedValue);

                    // 1️⃣ VALIDAR SI EL EQUIPO YA ESTA INSCRITO
                    string validar = @"SELECT COUNT(*)
                               FROM DetalleTorneo
                               WHERE IdTorneo=@torneo AND IdEquipo=@equipo";

                    SqlCommand cmdValidar = new SqlCommand(validar, conn, trans);

                    cmdValidar.Parameters.AddWithValue("@torneo", idTorneo);
                    cmdValidar.Parameters.AddWithValue("@equipo", idEquipo);

                    int existe = (int)cmdValidar.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Ese equipo ya está inscrito en el torneo");
                        trans.Rollback();
                        return;
                    }

                    // 2️⃣ VALIDAR NOMBRE DE EQUIPO DUPLICADO
                    string validarNombre = @"
                    SELECT COUNT(*)
                    FROM DetalleTorneo DT
                    INNER JOIN Equipo E ON DT.IdEquipo = E.IdEquipo
                    WHERE DT.IdTorneo=@torneo AND E.NombreEquipo =
                    (SELECT NombreEquipo FROM Equipo WHERE IdEquipo=@equipo)";

                    SqlCommand cmdNombre = new SqlCommand(validarNombre, conn, trans);

                    cmdNombre.Parameters.AddWithValue("@torneo", idTorneo);
                    cmdNombre.Parameters.AddWithValue("@equipo", idEquipo);

                    int nombreExiste = (int)cmdNombre.ExecuteScalar();

                    if (nombreExiste > 0)
                    {
                        MessageBox.Show("Ya existe un equipo con ese nombre en el torneo");
                        trans.Rollback();
                        return;
                    }

                    // 3️⃣ INSERTAR EQUIPO
                    string insert = @"INSERT INTO DetalleTorneo(IdTorneo,IdEquipo)
                              VALUES(@torneo,@equipo)";

                    SqlCommand cmdInsert = new SqlCommand(insert, conn, trans);

                    cmdInsert.Parameters.AddWithValue("@torneo", idTorneo);
                    cmdInsert.Parameters.AddWithValue("@equipo", idEquipo);

                    cmdInsert.ExecuteNonQuery();

                    // 4️⃣ CONTAR EQUIPOS
                    string count = @"SELECT COUNT(*)
                             FROM DetalleTorneo
                             WHERE IdTorneo=@torneo";

                    SqlCommand cmdCount = new SqlCommand(count, conn, trans);

                    cmdCount.Parameters.AddWithValue("@torneo", idTorneo);

                    int totalEquipos = (int)cmdCount.ExecuteScalar();

                    // 5️⃣ CALCULAR JORNADAS
                    int jornadas = 0;

                    if (totalEquipos > 1)
                    {
                        jornadas = totalEquipos - 1;
                    }
                    // 6️⃣ ACTUALIZAR TORNEO
                    string update = @"UPDATE Torneo
                              SET CantEquipos=@equipos,
                                  NumJornadas=@jornadas
                              WHERE IdTorneo=@torneo";

                    SqlCommand cmdUpdate = new SqlCommand(update, conn, trans);

                    cmdUpdate.Parameters.AddWithValue("@equipos", totalEquipos);
                    cmdUpdate.Parameters.AddWithValue("@jornadas", jornadas);
                    cmdUpdate.Parameters.AddWithValue("@torneo", idTorneo);

                    cmdUpdate.ExecuteNonQuery();

                    trans.Commit();
                    cmbEquipo.SelectedIndex = -1;
                    CargarEquipos();
                    CargarDetalleTorneo();

                    MessageBox.Show("Equipo inscrito correctamente");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
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
                conn.Open();

                string query = @"SELECT DT.IdTorneo,
                 DT.IdEquipo,
                 T.NombreTorneo,
                 E.NombreEquipo
                 FROM DetalleTorneo DT
                 INNER JOIN Torneo T ON DT.IdTorneo = T.IdTorneo
                 INNER JOIN Equipo E ON DT.IdEquipo = E.IdEquipo";


                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvDetalleTorneo.DataSource = dt;
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
        }
    }
}
