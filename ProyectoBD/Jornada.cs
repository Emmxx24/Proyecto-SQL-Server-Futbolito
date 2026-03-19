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
            cmbTorneo.SelectedIndexChanged += cmbTorneo_SelectedIndexChanged;

            cmbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            ClaseConexion con = new ClaseConexion();
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Ingrese el número de jornada");
                return;
            }

            if (ExisteNumJornada(Convert.ToInt32(cmbTorneo.SelectedValue), (int)numericUpDown1.Value))
            {
                MessageBox.Show("Ya existe una jornada con ese número en el torneo seleccionado");
                return;
            }


            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO Juego.Jornada(IdTorneo, NumeroJornada)
                                 VALUES(@torneo,@jornada)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                    cmd.Parameters.AddWithValue("@jornada", (int)numericUpDown1.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Jornada agregada");

                    MostrarJornadas();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            if (idJornadaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una jornada");
                return;
            }

            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Ingrese el número de jornada");
                return;
            }

            if (ExisteNumJornada(Convert.ToInt64(cmbTorneo.SelectedValue), (int)numericUpDown1.Value, idJornadaSeleccionada))
            {
                MessageBox.Show("Ya existe una jornada con ese número en el torneo seleccionado");
                return;
            }


            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE Juego.Jornada
                                 SET IdTorneo=@torneo,
                                     NumeroJornada=@jornada
                                 WHERE IdJornada=@id";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                    cmd.Parameters.AddWithValue("@jornada", (int)numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@id", idJornadaSeleccionada);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Jornada modificada");

                    MostrarJornadas();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
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
                try
                {
                    conn.Open();

                    string query = "DELETE FROM Juego.Jornada WHERE IdJornada=@id";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", idJornadaSeleccionada);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Jornada eliminada");

                    MostrarJornadas();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        void CargarTorneos()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT IdTorneo, NombreTorneo FROM Juego.Torneo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbTorneo.DataSource = dt;
                    cmbTorneo.DisplayMember = "NombreTorneo";
                    cmbTorneo.ValueMember = "IdTorneo";
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }

        void MostrarJornadas()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())

                try
                {
                    conn.Open();

                    string query = @"SELECT J.IdJornada,
                                        T.NombreTorneo,
                                        J.NumeroJornada
                                 FROM Juego.Jornada J
                                 INNER JOIN Juego.Torneo T
                                 ON J.IdTorneo = T.IdTorneo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvJornada.DataSource = dt;
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
        }
        void Limpiar()
        {
            numericUpDown1.Value = 0;
            idJornadaSeleccionada = 0;
        }

        private void dgvJornada_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvJornada.Rows[e.RowIndex];

                idJornadaSeleccionada = Convert.ToInt32(fila.Cells["IdJornada"].Value);
                numericUpDown1.Value = Convert.ToDecimal(fila.Cells["NumeroJornada"].Value);
            }
        }

        private bool ExisteNumJornada(long idTorneo, int numJornada, long idJornadaActual = 0)
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) 
                             FROM Juego.Jornada 
                             WHERE IdTorneo=@torneo 
                               AND NumeroJornada=@jornada";

                    if (idJornadaActual > 0)
                        query += " AND IdJornada<>@idActual";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@torneo", idTorneo);
                    cmd.Parameters.AddWithValue("@jornada", numJornada);
                    if (idJornadaActual > 0)
                        cmd.Parameters.AddWithValue("@idActual", idJornadaActual);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception e)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(e);
                    return false;
                }
            }
        }

        void MostrarJornadasPorTorneo(long idTorneo)
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT J.IdJornada,
                    T.NombreTorneo,
                    J.NumeroJornada
                    FROM Juego.Jornada J
                    INNER JOIN Juego.Torneo T
                    ON J.IdTorneo = T.IdTorneo
                    WHERE J.IdTorneo = @torneo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@torneo", idTorneo);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvJornada.DataSource = dt;
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbTorneo_DropDown(object sender, EventArgs e)
        {
            CargarTorneos();
        }

        private int ObtenerSiguienteNumeroJornada(long idTorneo)
        {
            int siguienteNumero = 1; // Valor por defecto si no hay jornadas aún

            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT ISNULL(MAX(NumeroJornada), 0) FROM Juego.Jornada WHERE IdTorneo = @idTorneo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int maxJornada))
                    {
                        siguienteNumero = maxJornada + 1;
                    }
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }

            return siguienteNumero;
        }
        private void cmbTorneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTorneo.SelectedValue != null && !(cmbTorneo.SelectedValue is DataRowView))
            {
                long idTorneo = Convert.ToInt64(cmbTorneo.SelectedValue);
                MostrarJornadasPorTorneo(idTorneo);
                numericUpDown1.Value = ObtenerSiguienteNumeroJornada(idTorneo);
                idJornadaSeleccionada = 0;
            }
        }
    }
}
