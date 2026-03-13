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
    public partial class Jornada : Form
    {
        int idJornadaSeleccionada = 0;
        public Jornada()
        {
            InitializeComponent();
        }

        private void Jornada_Load(object sender, EventArgs e)
        {
            CargarTorneos();
            MostrarJornadas();

            cmbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            ClaseConexion con = new ClaseConexion();

            if (txtNumJornada.Text == "")
            {
                MessageBox.Show("Ingrese el número de jornada");
                return;
            }

            using (SqlConnection conn = con.conectar())
            {
                conn.Open();

                string query = @"INSERT INTO Jornada(IdTorneo, NumJornada)
                                 VALUES(@torneo,@jornada)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                cmd.Parameters.AddWithValue("@jornada", txtNumJornada.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Jornada agregada");

                MostrarJornadas();
                Limpiar();
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            if (idJornadaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una jornada");
                return;
            }

            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                conn.Open();

                string query = @"UPDATE Jornada
                                 SET IdTorneo=@torneo,
                                     NumJornada=@jornada
                                 WHERE IdJornada=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                cmd.Parameters.AddWithValue("@jornada", txtNumJornada.Text);
                cmd.Parameters.AddWithValue("@id", idJornadaSeleccionada);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Jornada modificada");

                MostrarJornadas();
                Limpiar();
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (idJornadaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una jornada");
                return;
            }

            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                conn.Open();

                string query = "DELETE FROM Jornada WHERE IdJornada=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", idJornadaSeleccionada);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Jornada eliminada");

                MostrarJornadas();
                Limpiar();
            }
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

        void MostrarJornadas()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                conn.Open();

                string query = @"SELECT J.IdJornada,
                                        T.NombreTorneo,
                                        J.NumJornada
                                 FROM Jornada J
                                 INNER JOIN Torneo T
                                 ON J.IdTorneo = T.IdTorneo";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvJornada.DataSource = dt;
            }
        }
        void Limpiar()
        {
            txtNumJornada.Clear();
            idJornadaSeleccionada = 0;
        }

        private void dgvJornada_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvJornada.Rows[e.RowIndex];

                idJornadaSeleccionada = Convert.ToInt32(fila.Cells["IdJornada"].Value);

                txtNumJornada.Text = fila.Cells["NumJornada"].Value.ToString();
            }
        }
    }
}
