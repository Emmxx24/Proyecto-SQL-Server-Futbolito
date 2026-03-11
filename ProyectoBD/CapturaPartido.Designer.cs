namespace ProyectoBD
{
    partial class CapturaPartido
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
            lblTorneo = new Label();
            lblJornada = new Label();
            lblArbitro = new Label();
            lblLugar = new Label();
            cbTorneo = new ComboBox();
            cbJornada = new ComboBox();
            cbArbitro = new ComboBox();
            cbLugar = new ComboBox();
            dtpHora = new DateTimePicker();
            lblFecha = new Label();
            lblHora = new Label();
            dtpFecha = new DateTimePicker();
            lblLocal = new Label();
            lblVisitante = new Label();
            cbLocal = new ComboBox();
            cbVisitante = new ComboBox();
            dgvPartidos = new DataGridView();
            btnRegisResult = new Button();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            SuspendLayout();
            // 
            // lblTorneo
            // 
            lblTorneo.AutoSize = true;
            lblTorneo.Location = new Point(12, 9);
            lblTorneo.Name = "lblTorneo";
            lblTorneo.Size = new Size(58, 20);
            lblTorneo.TabIndex = 0;
            lblTorneo.Text = "Torneo:";
            // 
            // lblJornada
            // 
            lblJornada.AutoSize = true;
            lblJornada.Location = new Point(305, 9);
            lblJornada.Name = "lblJornada";
            lblJornada.Size = new Size(64, 20);
            lblJornada.TabIndex = 1;
            lblJornada.Text = "Jornada:";
            // 
            // lblArbitro
            // 
            lblArbitro.AutoSize = true;
            lblArbitro.Location = new Point(11, 71);
            lblArbitro.Name = "lblArbitro";
            lblArbitro.Size = new Size(59, 20);
            lblArbitro.TabIndex = 2;
            lblArbitro.Text = "Árbitro:";
            // 
            // lblLugar
            // 
            lblLugar.AutoSize = true;
            lblLugar.Location = new Point(306, 71);
            lblLugar.Name = "lblLugar";
            lblLugar.Size = new Size(49, 20);
            lblLugar.TabIndex = 3;
            lblLugar.Text = "Lugar:";
            // 
            // cbTorneo
            // 
            cbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTorneo.FormattingEnabled = true;
            cbTorneo.Location = new Point(12, 32);
            cbTorneo.Name = "cbTorneo";
            cbTorneo.Size = new Size(253, 28);
            cbTorneo.TabIndex = 4;
            cbTorneo.SelectedIndexChanged += cbTorneo_SelectedIndexChanged;
            // 
            // cbJornada
            // 
            cbJornada.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJornada.FormattingEnabled = true;
            cbJornada.Location = new Point(306, 32);
            cbJornada.Name = "cbJornada";
            cbJornada.Size = new Size(77, 28);
            cbJornada.TabIndex = 5;
            // 
            // cbArbitro
            // 
            cbArbitro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbArbitro.FormattingEnabled = true;
            cbArbitro.Location = new Point(12, 94);
            cbArbitro.Name = "cbArbitro";
            cbArbitro.Size = new Size(253, 28);
            cbArbitro.TabIndex = 6;
            // 
            // cbLugar
            // 
            cbLugar.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLugar.FormattingEnabled = true;
            cbLugar.Location = new Point(305, 94);
            cbLugar.Name = "cbLugar";
            cbLugar.Size = new Size(211, 28);
            cbLugar.TabIndex = 7;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(306, 231);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(137, 27);
            dtpHora.TabIndex = 8;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(12, 208);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(50, 20);
            lblFecha.TabIndex = 9;
            lblFecha.Text = "Fecha:";
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(306, 208);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(106, 20);
            lblHora.TabIndex = 10;
            lblHora.Text = "Hora de inicio:";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(13, 231);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(274, 27);
            dtpFecha.TabIndex = 11;
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(12, 139);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(95, 20);
            lblLocal.TabIndex = 12;
            lblLocal.Text = "Equipo local:";
            // 
            // lblVisitante
            // 
            lblVisitante.AutoSize = true;
            lblVisitante.Location = new Point(306, 139);
            lblVisitante.Name = "lblVisitante";
            lblVisitante.Size = new Size(118, 20);
            lblVisitante.TabIndex = 13;
            lblVisitante.Text = "Equipo visitante:";
            // 
            // cbLocal
            // 
            cbLocal.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLocal.FormattingEnabled = true;
            cbLocal.Location = new Point(12, 166);
            cbLocal.Name = "cbLocal";
            cbLocal.Size = new Size(275, 28);
            cbLocal.TabIndex = 14;
            // 
            // cbVisitante
            // 
            cbVisitante.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVisitante.FormattingEnabled = true;
            cbVisitante.Location = new Point(307, 166);
            cbVisitante.Name = "cbVisitante";
            cbVisitante.Size = new Size(275, 28);
            cbVisitante.TabIndex = 15;
            // 
            // dgvPartidos
            // 
            dgvPartidos.AllowUserToAddRows = false;
            dgvPartidos.AllowUserToDeleteRows = false;
            dgvPartidos.AllowUserToResizeColumns = false;
            dgvPartidos.AllowUserToResizeRows = false;
            dgvPartidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Location = new Point(11, 282);
            dgvPartidos.MultiSelect = false;
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidos.Size = new Size(989, 170);
            dgvPartidos.TabIndex = 16;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            // 
            // btnRegisResult
            // 
            btnRegisResult.Location = new Point(13, 468);
            btnRegisResult.Name = "btnRegisResult";
            btnRegisResult.Size = new Size(147, 29);
            btnRegisResult.TabIndex = 17;
            btnRegisResult.Text = "Registrar resultado";
            btnRegisResult.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(677, 94);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 18;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(677, 165);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 19;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(677, 231);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 20;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // CapturaPartido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 509);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(btnRegisResult);
            Controls.Add(dgvPartidos);
            Controls.Add(cbVisitante);
            Controls.Add(cbLocal);
            Controls.Add(lblVisitante);
            Controls.Add(lblLocal);
            Controls.Add(dtpFecha);
            Controls.Add(lblHora);
            Controls.Add(lblFecha);
            Controls.Add(dtpHora);
            Controls.Add(cbLugar);
            Controls.Add(cbArbitro);
            Controls.Add(cbJornada);
            Controls.Add(cbTorneo);
            Controls.Add(lblLugar);
            Controls.Add(lblArbitro);
            Controls.Add(lblJornada);
            Controls.Add(lblTorneo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapturaPartido";
            Text = "CapturaPartido";
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTorneo;
        private Label lblJornada;
        private Label lblArbitro;
        private Label lblLugar;
        private ComboBox cbTorneo;
        private ComboBox cbJornada;
        private ComboBox cbArbitro;
        private ComboBox cbLugar;
        private DateTimePicker dtpHora;
        private Label lblFecha;
        private Label lblHora;
        private DateTimePicker dtpFecha;
        private Label lblLocal;
        private Label lblVisitante;
        private ComboBox cbLocal;
        private ComboBox cbVisitante;
        private DataGridView dgvPartidos;
        private Button btnRegisResult;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}