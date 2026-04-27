using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class Jornada : Form
    {
        public Jornada()
        {
            InitializeComponent();
            MostrarJornadas();
        }

        private void Jornada_Load(object sender, EventArgs e)
        {
            
        }

        public void RefrescarJornadas()  
        {
            MostrarJornadas();
        }

        void MostrarJornadas()
        {
            ClaseConexion con = new ClaseConexion();

            using (SqlConnection conn = con.conectar())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            J.IdJornada,
                            T.NombreTorneo + ' (' + 
                            CONVERT(varchar(10), T.FechaInicio, 103) + ' - ' + 
                            CONVERT(varchar(10), T.FechaFin, 103) + ')' AS Torneo,
                            J.NumeroJornada AS [Número de Jornada]
                        FROM Juego.Jornada J
                        INNER JOIN Juego.Torneo T ON J.IdTorneo = T.IdTorneo
                        ORDER BY T.NombreTorneo, J.NumeroJornada";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvJornada.DataSource = dt;
                    dgvJornada.ReadOnly = true;
                    dgvJornada.AllowUserToAddRows = false;
                    dgvJornada.AllowUserToDeleteRows = false;
                    dgvJornada.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Ocultar columna técnica
                    if (dgvJornada.Columns.Contains("IdJornada"))
                        dgvJornada.Columns["IdJornada"].Visible = false;
                }
                catch (Exception ex)
                {
                    ManejadorErroresBD.MostrarErrorAmigable(ex);
                }
            }
        }
    }
}