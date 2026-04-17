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
            CargarDetalleTorneo();
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
                    cmbTorneo.SelectedIndex = -1;
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

                cmbTorneo.SelectedValue = Convert.ToInt64(fila.Cells["IdTorneo"].Value);
                cmbEquipo.SelectedValue = Convert.ToInt64(fila.Cells["IdEquipo"].Value);
                

                //cmbTorneo.SelectedValue = fila.Cells["IdTorneo"].Value;
                //cmbEquipo.SelectedValue = fila.Cells["IdEquipo"].Value;
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

                    //string query = "SELECT * FROM Club.Equipo";
                    string query = "SELECT IdEquipo, NombreEquipo FROM Club.Equipo";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    cmbEquipo.DataSource = dt;
                    cmbEquipo.DisplayMember = "NombreEquipo";
                    cmbEquipo.ValueMember = "IdEquipo";
                    cmbEquipo.SelectedIndex = -1;

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

                    // 🔍 VALIDAR DUPLICADO
                    string checkQuery = @"SELECT COUNT(*) 
                                  FROM Juego.DetalleTorneo 
                                  WHERE IdTorneo = @torneo AND IdEquipo = @equipo";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    /*
                    checkCmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                    checkCmd.Parameters.AddWithValue("@equipo", cmbEquipo.SelectedValue);
                    */
                    checkCmd.Parameters.Add("@torneo", SqlDbType.Int).Value = (long)cmbTorneo.SelectedValue;
                    checkCmd.Parameters.Add("@equipo", SqlDbType.Int).Value = (long)cmbEquipo.SelectedValue;

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Este equipo ya está inscrito en el torneo");
                        return;
                    }

                    // ✅ INSERT
                    string query = @"INSERT INTO Juego.DetalleTorneo(IdTorneo,IdEquipo)
                             VALUES(@torneo,@equipo)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    //cmd.Parameters.AddWithValue("@torneo", cmbTorneo.SelectedValue);
                    //cmd.Parameters.AddWithValue("@equipo", cmbEquipo.SelectedValue);
                    cmd.Parameters.Add("@torneo", SqlDbType.Int).Value = (long)cmbTorneo.SelectedValue;
                    cmd.Parameters.Add("@equipo", SqlDbType.Int).Value = (long)cmbEquipo.SelectedValue;
                    cmd.ExecuteNonQuery();

                    //MessageBox.Show("Equipo inscrito correctamente");

                    cmbTorneo.SelectedIndex = -1;
                    cmbEquipo.SelectedIndex = -1;

                    CargarDetalleTorneo();
                    //dgvDetalleTorneo.Refresh();
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
                        SELECT
                        DT.IdTorneo,
                        DT.IdEquipo,

                        T.NombreTorneo + ' (' +
                        CONVERT(varchar(10), T.FechaInicio, 103) + ' - ' + 
                        CONVERT(varchar(10), T.FechaFin, 103) + ')' AS Torneo,

                        E.NombreEquipo AS [Nombre Equipo],

                        T.NumJornadas AS [Número de Jornadas],
                        T.CantEquipos AS [Cantidad de Equipos]

                        FROM Juego.DetalleTorneo DT
                        INNER JOIN Juego.Torneo T ON DT.IdTorneo = T.IdTorneo
                        INNER JOIN Club.Equipo E ON DT.IdEquipo = E.IdEquipo";

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

                    long idTorneo = Convert.ToInt64(dgvDetalleTorneo.CurrentRow.Cells["IdTorneo"].Value);
                    long idEquipo = Convert.ToInt64(dgvDetalleTorneo.CurrentRow.Cells["IdEquipo"].Value);

                    // 🔍 VALIDAR DUPLICADO
                    string checkQuery = @"SELECT COUNT(*) 
                                 FROM Juego.DetalleTorneo 
                                 WHERE IdTorneo = @torneoNuevo 
                                 AND IdEquipo = @equipoNuevo
                                 AND NOT (IdTorneo = @torneo AND IdEquipo = @equipo)";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    /*checkCmd.Parameters.AddWithValue("@torneoNuevo", cmbTorneo.SelectedValue);
                    checkCmd.Parameters.AddWithValue("@equipoNuevo", cmbEquipo.SelectedValue);
                    checkCmd.Parameters.AddWithValue("@torneo", idTorneo);
                    checkCmd.Parameters.AddWithValue("@equipo", idEquipo);
                    */
                    checkCmd.Parameters.Add("@torneoNuevo", SqlDbType.Int).Value = (long)cmbTorneo.SelectedValue;
                    checkCmd.Parameters.Add("@equipoNuevo", SqlDbType.Int).Value = (long)cmbEquipo.SelectedValue;
                    checkCmd.Parameters.Add("@torneo", SqlDbType.Int).Value = idTorneo;
                    checkCmd.Parameters.Add("@equipo", SqlDbType.Int).Value = idEquipo;

                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe ese equipo en ese torneo");
                        return;
                    }

                    // ✅ UPDATE
                    string query = @"UPDATE Juego.DetalleTorneo
                             SET IdTorneo=@torneoNuevo,
                                 IdEquipo=@equipoNuevo
                             WHERE IdTorneo=@torneo AND IdEquipo=@equipo";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    //cmd.Parameters.AddWithValue("@torneoNuevo", cmbTorneo.SelectedValue);
                    //cmd.Parameters.AddWithValue("@equipoNuevo", cmbEquipo.SelectedValue);
                    //cmd.Parameters.AddWithValue("@torneo", idTorneo);
                    //cmd.Parameters.AddWithValue("@equipo", idEquipo);
                    cmd.Parameters.Add("@torneoNuevo", SqlDbType.BigInt).Value = (long)cmbTorneo.SelectedValue;
                    cmd.Parameters.Add("@equipoNuevo", SqlDbType.BigInt).Value = (long)cmbEquipo.SelectedValue;
                    cmd.Parameters.Add("@torneo", SqlDbType.BigInt).Value = idTorneo;
                    cmd.Parameters.Add("@equipo", SqlDbType.BigInt).Value = idEquipo;
                    cmd.ExecuteNonQuery();

                    //MessageBox.Show("Registro modificado");

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

                    long idTorneo = Convert.ToInt64(dgvDetalleTorneo.CurrentRow.Cells["IdTorneo"].Value);
                    long idEquipo = Convert.ToInt64(dgvDetalleTorneo.CurrentRow.Cells["IdEquipo"].Value);

                    string query = @"DELETE FROM Juego.DetalleTorneo
                         WHERE IdTorneo=@torneo AND IdEquipo=@equipo";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    /*cmd.Parameters.AddWithValue("@torneo", idTorneo);
                    cmd.Parameters.AddWithValue("@equipo", idEquipo);
                    */

                    cmd.Parameters.Add("@torneo", SqlDbType.Int).Value = idTorneo;
                    cmd.Parameters.Add("@equipo", SqlDbType.Int).Value = idEquipo;

                    /*DialogResult res = MessageBox.Show(
                        "¿Seguro que deseas eliminar este registro?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                        );

                    if (res != DialogResult.Yes)
                        return;
                    */

                    cmd.ExecuteNonQuery();

                    //MessageBox.Show("Registro eliminado");

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
