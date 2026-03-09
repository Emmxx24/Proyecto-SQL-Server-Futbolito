namespace ProyectoBD
{
    partial class CapturaJugador
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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblPosicion = new Label();
            cbPosicion = new ComboBox();
            numericNumJugador = new NumericUpDown();
            lblNum = new Label();
            cbTipoSangre = new ComboBox();
            lblSangre = new Label();
            dgvJugador = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numericNumJugador).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvJugador).BeginInit();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 9);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(123, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre jugador:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 43);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 1;
            // 
            // lblPosicion
            // 
            lblPosicion.AutoSize = true;
            lblPosicion.Location = new Point(12, 82);
            lblPosicion.Name = "lblPosicion";
            lblPosicion.Size = new Size(66, 20);
            lblPosicion.TabIndex = 2;
            lblPosicion.Text = "Posición:";
            // 
            // cbPosicion
            // 
            cbPosicion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPosicion.FormattingEnabled = true;
            cbPosicion.Items.AddRange(new object[] { "Portero", "Defensa", "Medio", "Delantero" });
            cbPosicion.Location = new Point(12, 105);
            cbPosicion.Name = "cbPosicion";
            cbPosicion.Size = new Size(250, 28);
            cbPosicion.TabIndex = 3;
            // 
            // numericNumJugador
            // 
            numericNumJugador.Location = new Point(12, 172);
            numericNumJugador.Name = "numericNumJugador";
            numericNumJugador.Size = new Size(150, 27);
            numericNumJugador.TabIndex = 4;
            // 
            // lblNum
            // 
            lblNum.AutoSize = true;
            lblNum.Location = new Point(12, 149);
            lblNum.Name = "lblNum";
            lblNum.Size = new Size(121, 20);
            lblNum.TabIndex = 5;
            lblNum.Text = "Número (dorsal):";
            // 
            // cbTipoSangre
            // 
            cbTipoSangre.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoSangre.FormattingEnabled = true;
            cbTipoSangre.Items.AddRange(new object[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" });
            cbTipoSangre.Location = new Point(12, 239);
            cbTipoSangre.Name = "cbTipoSangre";
            cbTipoSangre.Size = new Size(151, 28);
            cbTipoSangre.TabIndex = 6;
            // 
            // lblSangre
            // 
            lblSangre.AutoSize = true;
            lblSangre.Location = new Point(12, 216);
            lblSangre.Name = "lblSangre";
            lblSangre.Size = new Size(111, 20);
            lblSangre.TabIndex = 7;
            lblSangre.Text = "Tipo de sangre:";
            // 
            // dgvJugador
            // 
            dgvJugador.AllowUserToAddRows = false;
            dgvJugador.AllowUserToDeleteRows = false;
            dgvJugador.AllowUserToResizeColumns = false;
            dgvJugador.AllowUserToResizeRows = false;
            dgvJugador.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJugador.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJugador.Location = new Point(12, 279);
            dgvJugador.MultiSelect = false;
            dgvJugador.Name = "dgvJugador";
            dgvJugador.ReadOnly = true;
            dgvJugador.RowHeadersWidth = 51;
            dgvJugador.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJugador.Size = new Size(837, 188);
            dgvJugador.TabIndex = 8;
            dgvJugador.CellClick += dgvJugador_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(623, 73);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(623, 140);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(623, 207);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // CapturaJugador
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(861, 481);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvJugador);
            Controls.Add(lblSangre);
            Controls.Add(cbTipoSangre);
            Controls.Add(lblNum);
            Controls.Add(numericNumJugador);
            Controls.Add(cbPosicion);
            Controls.Add(lblPosicion);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            MaximizeBox = false;
            Name = "CapturaJugador";
            Text = "Jugador";
            ((System.ComponentModel.ISupportInitialize)numericNumJugador).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvJugador).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblPosicion;
        private ComboBox cbPosicion;
        private NumericUpDown numericNumJugador;
        private Label lblNum;
        private ComboBox cbTipoSangre;
        private Label lblSangre;
        private DataGridView dgvJugador;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}