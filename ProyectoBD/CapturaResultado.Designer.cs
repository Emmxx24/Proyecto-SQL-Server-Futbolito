namespace ProyectoBD
{
    partial class CapturaResultado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblPartidoDetalle = new Label();
            txtPartidoDetalle = new TextBox();
            lblGolesLocal = new Label();
            numericGolesLocal = new NumericUpDown();
            lblGolesVisitante = new Label();
            numericGolesVisitante = new NumericUpDown();
            lblHoraFin = new Label();
            dtpHoraTermino = new DateTimePicker();
            dgvResultados = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnRegistraGoles = new Button();
            btnRegistraTarjetas = new Button();
            ((System.ComponentModel.ISupportInitialize)numericGolesLocal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericGolesVisitante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // lblPartidoDetalle
            // 
            lblPartidoDetalle.AutoSize = true;
            lblPartidoDetalle.Location = new Point(12, 9);
            lblPartidoDetalle.Name = "lblPartidoDetalle";
            lblPartidoDetalle.Size = new Size(236, 15);
            lblPartidoDetalle.TabIndex = 0;
            lblPartidoDetalle.Text = "Partido (local vs visitante - jornada - fecha):";
            // 
            // txtPartidoDetalle
            // 
            txtPartidoDetalle.Location = new Point(12, 27);
            txtPartidoDetalle.Name = "txtPartidoDetalle";
            txtPartidoDetalle.ReadOnly = true;
            txtPartidoDetalle.Size = new Size(444, 23);
            txtPartidoDetalle.TabIndex = 1;
            // 
            // lblGolesLocal
            // 
            lblGolesLocal.AutoSize = true;
            lblGolesLocal.Location = new Point(12, 65);
            lblGolesLocal.Name = "lblGolesLocal";
            lblGolesLocal.Size = new Size(107, 15);
            lblGolesLocal.TabIndex = 2;
            lblGolesLocal.Text = "Goles equipo local:";
            // 
            // numericGolesLocal
            // 
            numericGolesLocal.Location = new Point(12, 83);
            numericGolesLocal.Name = "numericGolesLocal";
            numericGolesLocal.Size = new Size(120, 23);
            numericGolesLocal.TabIndex = 3;
            // 
            // lblGolesVisitante
            // 
            lblGolesVisitante.AutoSize = true;
            lblGolesVisitante.Location = new Point(152, 65);
            lblGolesVisitante.Name = "lblGolesVisitante";
            lblGolesVisitante.Size = new Size(126, 15);
            lblGolesVisitante.TabIndex = 4;
            lblGolesVisitante.Text = "Goles equipo visitante:";
            // 
            // numericGolesVisitante
            // 
            numericGolesVisitante.Location = new Point(152, 83);
            numericGolesVisitante.Name = "numericGolesVisitante";
            numericGolesVisitante.Size = new Size(120, 23);
            numericGolesVisitante.TabIndex = 5;
            // 
            // lblHoraFin
            // 
            lblHoraFin.AutoSize = true;
            lblHoraFin.Location = new Point(12, 118);
            lblHoraFin.Name = "lblHoraFin";
            lblHoraFin.Size = new Size(97, 15);
            lblHoraFin.TabIndex = 6;
            lblHoraFin.Text = "Hora de termino:";
            // 
            // dtpHoraTermino
            // 
            dtpHoraTermino.Format = DateTimePickerFormat.Time;
            dtpHoraTermino.Location = new Point(12, 136);
            dtpHoraTermino.Name = "dtpHoraTermino";
            dtpHoraTermino.ShowUpDown = true;
            dtpHoraTermino.Size = new Size(120, 23);
            dtpHoraTermino.TabIndex = 7;
            // 
            // dgvResultados
            // 
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.AllowUserToDeleteRows = false;
            dgvResultados.AllowUserToResizeColumns = false;
            dgvResultados.AllowUserToResizeRows = false;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvResultados.DefaultCellStyle = dataGridViewCellStyle2;
            dgvResultados.Location = new Point(12, 196);
            dgvResultados.MultiSelect = false;
            dgvResultados.Name = "dgvResultados";
            dgvResultados.ReadOnly = true;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.Size = new Size(776, 150);
            dgvResultados.TabIndex = 8;
            dgvResultados.CellClick += dgvResultados_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(655, 27);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(655, 94);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(655, 155);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnRegistraGoles
            // 
            btnRegistraGoles.BackColor = Color.SeaGreen;
            btnRegistraGoles.Enabled = false;
            btnRegistraGoles.ForeColor = SystemColors.ButtonHighlight;
            btnRegistraGoles.Location = new Point(12, 352);
            btnRegistraGoles.Name = "btnRegistraGoles";
            btnRegistraGoles.Size = new Size(134, 23);
            btnRegistraGoles.TabIndex = 12;
            btnRegistraGoles.Text = "Registrar goles";
            btnRegistraGoles.UseVisualStyleBackColor = false;
            btnRegistraGoles.Visible = false;
            btnRegistraGoles.Click += btnRegistraGoles_Click;
            // 
            // btnRegistraTarjetas
            // 
            btnRegistraTarjetas.BackColor = Color.FromArgb(255, 128, 128);
            btnRegistraTarjetas.Enabled = false;
            btnRegistraTarjetas.Location = new Point(152, 352);
            btnRegistraTarjetas.Name = "btnRegistraTarjetas";
            btnRegistraTarjetas.Size = new Size(155, 23);
            btnRegistraTarjetas.TabIndex = 13;
            btnRegistraTarjetas.Text = "Registrar tarjetas";
            btnRegistraTarjetas.UseVisualStyleBackColor = false;
            btnRegistraTarjetas.Visible = false;
            btnRegistraTarjetas.Click += btnRegistraTarjetas_Click;
            // 
            // CapturaResultado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 387);
            Controls.Add(btnRegistraTarjetas);
            Controls.Add(btnRegistraGoles);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvResultados);
            Controls.Add(dtpHoraTermino);
            Controls.Add(lblHoraFin);
            Controls.Add(numericGolesVisitante);
            Controls.Add(lblGolesVisitante);
            Controls.Add(numericGolesLocal);
            Controls.Add(lblGolesLocal);
            Controls.Add(txtPartidoDetalle);
            Controls.Add(lblPartidoDetalle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapturaResultado";
            Text = "CapturaResultado";
            ((System.ComponentModel.ISupportInitialize)numericGolesLocal).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericGolesVisitante).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPartidoDetalle;
        private TextBox txtPartidoDetalle;
        private Label lblGolesLocal;
        private NumericUpDown numericGolesLocal;
        private Label lblGolesVisitante;
        private NumericUpDown numericGolesVisitante;
        private Label lblHoraFin;
        private DateTimePicker dtpHoraTermino;
        private DataGridView dgvResultados;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnRegistraGoles;
        private Button btnRegistraTarjetas;
    }
}