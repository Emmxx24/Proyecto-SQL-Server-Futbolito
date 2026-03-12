namespace ProyectoBD
{
    partial class frmParticipante
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblGenero = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            dgvParticipante = new DataGridView();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnModificar = new Button();
            lblFecha = new Label();
            label2 = new Label();
            mcFecha = new MonthCalendar();
            cbGenero = new ComboBox();
            btnRegistrarJugador = new Button();
            btnRegistrarArbitro = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvParticipante).BeginInit();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(37, 39);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(35, 163);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(60, 20);
            lblGenero.TabIndex = 1;
            lblGenero.Text = "Género:";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(189, 163);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(70, 20);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(37, 99);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(135, 20);
            lblCorreo.TabIndex = 3;
            lblCorreo.Text = "Correo Electrónico:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(37, 63);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(277, 27);
            txtNombre.TabIndex = 5;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(189, 187);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(125, 27);
            txtTelefono.TabIndex = 7;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(37, 121);
            txtCorreo.MaxLength = 50;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(277, 27);
            txtCorreo.TabIndex = 8;
            // 
            // dgvParticipante
            // 
            dgvParticipante.AllowUserToAddRows = false;
            dgvParticipante.AllowUserToDeleteRows = false;
            dgvParticipante.AllowUserToResizeColumns = false;
            dgvParticipante.AllowUserToResizeRows = false;
            dgvParticipante.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParticipante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParticipante.Location = new Point(16, 305);
            dgvParticipante.MultiSelect = false;
            dgvParticipante.Name = "dgvParticipante";
            dgvParticipante.ReadOnly = true;
            dgvParticipante.RowHeadersWidth = 51;
            dgvParticipante.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipante.Size = new Size(906, 188);
            dgvParticipante.TabIndex = 9;
            dgvParticipante.CellClick += dgvParticipante_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(741, 89);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(741, 201);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(741, 145);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 12;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(361, 36);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(152, 20);
            lblFecha.TabIndex = 13;
            lblFecha.Text = "Fecha de Nacimiento:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(424, 113);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 14;
            // 
            // mcFecha
            // 
            mcFecha.Location = new Point(361, 63);
            mcFecha.Name = "mcFecha";
            mcFecha.TabIndex = 15;
            // 
            // cbGenero
            // 
            cbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGenero.FormattingEnabled = true;
            cbGenero.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cbGenero.Location = new Point(35, 187);
            cbGenero.Name = "cbGenero";
            cbGenero.Size = new Size(125, 28);
            cbGenero.TabIndex = 16;
            // 
            // btnRegistrarJugador
            // 
            btnRegistrarJugador.BackColor = Color.OliveDrab;
            btnRegistrarJugador.Enabled = false;
            btnRegistrarJugador.Location = new Point(16, 510);
            btnRegistrarJugador.Name = "btnRegistrarJugador";
            btnRegistrarJugador.Size = new Size(144, 29);
            btnRegistrarJugador.TabIndex = 17;
            btnRegistrarJugador.Text = "Registrar Jugador";
            btnRegistrarJugador.UseVisualStyleBackColor = false;
            btnRegistrarJugador.Visible = false;
            btnRegistrarJugador.Click += btnRegistrarJugador_Click;
            // 
            // btnRegistrarArbitro
            // 
            btnRegistrarArbitro.Enabled = false;
            btnRegistrarArbitro.Location = new Point(166, 510);
            btnRegistrarArbitro.Name = "btnRegistrarArbitro";
            btnRegistrarArbitro.Size = new Size(129, 29);
            btnRegistrarArbitro.TabIndex = 18;
            btnRegistrarArbitro.Text = "Registrar Arbitro";
            btnRegistrarArbitro.UseVisualStyleBackColor = true;
            btnRegistrarArbitro.Visible = false;
            // 
            // frmParticipante
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 541);
            Controls.Add(btnRegistrarArbitro);
            Controls.Add(btnRegistrarJugador);
            Controls.Add(cbGenero);
            Controls.Add(mcFecha);
            Controls.Add(label2);
            Controls.Add(lblFecha);
            Controls.Add(btnModificar);
            Controls.Add(btnEliminar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvParticipante);
            Controls.Add(txtCorreo);
            Controls.Add(txtTelefono);
            Controls.Add(txtNombre);
            Controls.Add(lblCorreo);
            Controls.Add(lblTelefono);
            Controls.Add(lblGenero);
            Controls.Add(lblNombre);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmParticipante";
            Text = "Participante";
            ((System.ComponentModel.ISupportInitialize)dgvParticipante).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private Label lblGenero;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private DataGridView dgvParticipante;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnModificar;
        private Label lblFecha;
        private Label label2;
        private MonthCalendar mcFecha;
        private ComboBox cbGenero;
        private Button btnRegistrarJugador;
        private Button btnRegistrarArbitro;
    }
}
