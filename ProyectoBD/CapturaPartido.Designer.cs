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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            lblTorneo.Location = new Point(10, 7);
            lblTorneo.Name = "lblTorneo";
            lblTorneo.Size = new Size(176, 15);
            lblTorneo.TabIndex = 0;
            lblTorneo.Text = "Torneo (fecha inicio - fecha fin):";
            // 
            // lblJornada
            // 
            lblJornada.AutoSize = true;
            lblJornada.Location = new Point(267, 7);
            lblJornada.Name = "lblJornada";
            lblJornada.Size = new Size(51, 15);
            lblJornada.TabIndex = 1;
            lblJornada.Text = "Jornada:";
            // 
            // lblArbitro
            // 
            lblArbitro.AutoSize = true;
            lblArbitro.Location = new Point(10, 53);
            lblArbitro.Name = "lblArbitro";
            lblArbitro.Size = new Size(112, 15);
            lblArbitro.TabIndex = 2;
            lblArbitro.Text = "Id Árbritro - Árbitro:";
            // 
            // lblLugar
            // 
            lblLugar.AutoSize = true;
            lblLugar.Location = new Point(268, 53);
            lblLugar.Name = "lblLugar";
            lblLugar.Size = new Size(105, 15);
            lblLugar.TabIndex = 3;
            lblLugar.Text = "Lugar [capacidad]:";
            // 
            // cbTorneo
            // 
            cbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTorneo.FormattingEnabled = true;
            cbTorneo.Location = new Point(10, 24);
            cbTorneo.Margin = new Padding(3, 2, 3, 2);
            cbTorneo.Name = "cbTorneo";
            cbTorneo.Size = new Size(222, 23);
            cbTorneo.TabIndex = 1;
            cbTorneo.SelectedIndexChanged += cbTorneo_SelectedIndexChanged;
            // 
            // cbJornada
            // 
            cbJornada.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJornada.FormattingEnabled = true;
            cbJornada.Location = new Point(268, 24);
            cbJornada.Margin = new Padding(3, 2, 3, 2);
            cbJornada.Name = "cbJornada";
            cbJornada.Size = new Size(68, 23);
            cbJornada.TabIndex = 2;
            // 
            // cbArbitro
            // 
            cbArbitro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbArbitro.FormattingEnabled = true;
            cbArbitro.Location = new Point(10, 70);
            cbArbitro.Margin = new Padding(3, 2, 3, 2);
            cbArbitro.Name = "cbArbitro";
            cbArbitro.Size = new Size(222, 23);
            cbArbitro.TabIndex = 3;
            // 
            // cbLugar
            // 
            cbLugar.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLugar.FormattingEnabled = true;
            cbLugar.Location = new Point(267, 70);
            cbLugar.Margin = new Padding(3, 2, 3, 2);
            cbLugar.Name = "cbLugar";
            cbLugar.Size = new Size(185, 23);
            cbLugar.TabIndex = 4;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(268, 173);
            dtpHora.Margin = new Padding(3, 2, 3, 2);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(120, 23);
            dtpHora.TabIndex = 8;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(10, 156);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 9;
            lblFecha.Text = "Fecha:";
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(268, 156);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(84, 15);
            lblHora.TabIndex = 10;
            lblHora.Text = "Hora de inicio:";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(11, 173);
            dtpFecha.Margin = new Padding(3, 2, 3, 2);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(240, 23);
            dtpFecha.TabIndex = 7;
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(10, 104);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(75, 15);
            lblLocal.TabIndex = 12;
            lblLocal.Text = "Equipo local:";
            // 
            // lblVisitante
            // 
            lblVisitante.AutoSize = true;
            lblVisitante.Location = new Point(268, 104);
            lblVisitante.Name = "lblVisitante";
            lblVisitante.Size = new Size(94, 15);
            lblVisitante.TabIndex = 13;
            lblVisitante.Text = "Equipo visitante:";
            // 
            // cbLocal
            // 
            cbLocal.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLocal.FormattingEnabled = true;
            cbLocal.Location = new Point(10, 124);
            cbLocal.Margin = new Padding(3, 2, 3, 2);
            cbLocal.Name = "cbLocal";
            cbLocal.Size = new Size(241, 23);
            cbLocal.TabIndex = 5;
            // 
            // cbVisitante
            // 
            cbVisitante.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVisitante.FormattingEnabled = true;
            cbVisitante.Location = new Point(269, 124);
            cbVisitante.Margin = new Padding(3, 2, 3, 2);
            cbVisitante.Name = "cbVisitante";
            cbVisitante.Size = new Size(241, 23);
            cbVisitante.TabIndex = 6;
            // 
            // dgvPartidos
            // 
            dgvPartidos.AllowUserToAddRows = false;
            dgvPartidos.AllowUserToDeleteRows = false;
            dgvPartidos.AllowUserToResizeColumns = false;
            dgvPartidos.AllowUserToResizeRows = false;
            dgvPartidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPartidos.DefaultCellStyle = dataGridViewCellStyle1;
            dgvPartidos.Location = new Point(10, 212);
            dgvPartidos.Margin = new Padding(3, 2, 3, 2);
            dgvPartidos.MultiSelect = false;
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidos.Size = new Size(865, 412);
            dgvPartidos.TabIndex = 12;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            // 
            // btnRegisResult
            // 
            btnRegisResult.Location = new Point(10, 628);
            btnRegisResult.Margin = new Padding(3, 2, 3, 2);
            btnRegisResult.Name = "btnRegisResult";
            btnRegisResult.Size = new Size(129, 22);
            btnRegisResult.TabIndex = 13;
            btnRegisResult.Text = "Registrar resultado";
            btnRegisResult.UseVisualStyleBackColor = true;
            btnRegisResult.Click += btnRegisResult_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(592, 70);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(82, 22);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(592, 124);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(82, 22);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(592, 173);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // CapturaPartido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 661);
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
            Margin = new Padding(3, 2, 3, 2);
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