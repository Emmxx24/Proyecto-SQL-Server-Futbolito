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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            lblNombre.Location = new Point(32, 29);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(31, 122);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(48, 15);
            lblGenero.TabIndex = 1;
            lblGenero.Text = "Género:";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(165, 122);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(56, 15);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(32, 74);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(108, 15);
            lblCorreo.TabIndex = 3;
            lblCorreo.Text = "Correo Electrónico:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(32, 47);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(243, 23);
            txtNombre.TabIndex = 1;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(165, 140);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(110, 23);
            txtTelefono.TabIndex = 4;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(32, 91);
            txtCorreo.Margin = new Padding(3, 2, 3, 2);
            txtCorreo.MaxLength = 50;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(243, 23);
            txtCorreo.TabIndex = 2;
            // 
            // dgvParticipante
            // 
            dgvParticipante.AllowUserToAddRows = false;
            dgvParticipante.AllowUserToDeleteRows = false;
            dgvParticipante.AllowUserToResizeColumns = false;
            dgvParticipante.AllowUserToResizeRows = false;
            dgvParticipante.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParticipante.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvParticipante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvParticipante.DefaultCellStyle = dataGridViewCellStyle1;
            dgvParticipante.Location = new Point(14, 229);
            dgvParticipante.Margin = new Padding(3, 2, 3, 2);
            dgvParticipante.MultiSelect = false;
            dgvParticipante.Name = "dgvParticipante";
            dgvParticipante.ReadOnly = true;
            dgvParticipante.RowHeadersWidth = 51;
            dgvParticipante.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipante.Size = new Size(793, 141);
            dgvParticipante.TabIndex = 9;
            dgvParticipante.CellClick += dgvParticipante_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(648, 67);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(82, 22);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(648, 151);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(648, 109);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(82, 22);
            btnModificar.TabIndex = 7;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(316, 27);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(122, 15);
            lblFecha.TabIndex = 13;
            lblFecha.Text = "Fecha de Nacimiento:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(371, 85);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 14;
            // 
            // mcFecha
            // 
            mcFecha.Location = new Point(316, 47);
            mcFecha.Margin = new Padding(8, 7, 8, 7);
            mcFecha.Name = "mcFecha";
            mcFecha.TabIndex = 5;
            // 
            // cbGenero
            // 
            cbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGenero.FormattingEnabled = true;
            cbGenero.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cbGenero.Location = new Point(31, 140);
            cbGenero.Margin = new Padding(3, 2, 3, 2);
            cbGenero.Name = "cbGenero";
            cbGenero.Size = new Size(110, 23);
            cbGenero.TabIndex = 3;
            // 
            // btnRegistrarJugador
            // 
            btnRegistrarJugador.BackColor = Color.OliveDrab;
            btnRegistrarJugador.Enabled = false;
            btnRegistrarJugador.Location = new Point(14, 382);
            btnRegistrarJugador.Margin = new Padding(3, 2, 3, 2);
            btnRegistrarJugador.Name = "btnRegistrarJugador";
            btnRegistrarJugador.Size = new Size(126, 22);
            btnRegistrarJugador.TabIndex = 17;
            btnRegistrarJugador.Text = "Registrar Jugador";
            btnRegistrarJugador.UseVisualStyleBackColor = false;
            btnRegistrarJugador.Visible = false;
            btnRegistrarJugador.Click += btnRegistrarJugador_Click;
            // 
            // btnRegistrarArbitro
            // 
            btnRegistrarArbitro.BackColor = Color.OliveDrab;
            btnRegistrarArbitro.Enabled = false;
            btnRegistrarArbitro.Location = new Point(145, 382);
            btnRegistrarArbitro.Margin = new Padding(3, 2, 3, 2);
            btnRegistrarArbitro.Name = "btnRegistrarArbitro";
            btnRegistrarArbitro.Size = new Size(113, 22);
            btnRegistrarArbitro.TabIndex = 18;
            btnRegistrarArbitro.Text = "Registrar Arbitro";
            btnRegistrarArbitro.UseVisualStyleBackColor = false;
            btnRegistrarArbitro.Visible = false;
            btnRegistrarArbitro.Click += btnRegistrarArbitro_Click;
            // 
            // frmParticipante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 406);
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
            Margin = new Padding(3, 2, 3, 2);
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
