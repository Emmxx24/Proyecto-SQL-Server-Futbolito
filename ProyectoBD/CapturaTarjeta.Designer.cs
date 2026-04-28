namespace ProyectoBD
{
    partial class CapturaTarjeta
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblEquipo = new Label();
            lblJugador = new Label();
            lblMinuto = new Label();
            lblTipoTarjeta = new Label();
            cbEquipos = new ComboBox();
            cbJugadores = new ComboBox();
            numericMin = new NumericUpDown();
            cbTarjeta = new ComboBox();
            dgvTarjetas = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numericMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTarjetas).BeginInit();
            SuspendLayout();
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = true;
            lblEquipo.Location = new Point(14, 12);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(59, 20);
            lblEquipo.TabIndex = 0;
            lblEquipo.Text = "Equipo:";
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(14, 84);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(65, 20);
            lblJugador.TabIndex = 1;
            lblJugador.Text = "Jugador:";
            // 
            // lblMinuto
            // 
            lblMinuto.AutoSize = true;
            lblMinuto.Location = new Point(14, 159);
            lblMinuto.Name = "lblMinuto";
            lblMinuto.Size = new Size(59, 20);
            lblMinuto.TabIndex = 2;
            lblMinuto.Text = "Minuto:";
            // 
            // lblTipoTarjeta
            // 
            lblTipoTarjeta.AutoSize = true;
            lblTipoTarjeta.Location = new Point(184, 159);
            lblTipoTarjeta.Name = "lblTipoTarjeta";
            lblTipoTarjeta.Size = new Size(110, 20);
            lblTipoTarjeta.TabIndex = 3;
            lblTipoTarjeta.Text = "Tipo de tarjeta:";
            // 
            // cbEquipos
            // 
            cbEquipos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEquipos.FormattingEnabled = true;
            cbEquipos.Location = new Point(14, 36);
            cbEquipos.Margin = new Padding(3, 4, 3, 4);
            cbEquipos.Name = "cbEquipos";
            cbEquipos.Size = new Size(307, 28);
            cbEquipos.TabIndex = 1;
            cbEquipos.SelectedIndexChanged += cbEquipos_SelectedIndexChanged;
            // 
            // cbJugadores
            // 
            cbJugadores.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJugadores.FormattingEnabled = true;
            cbJugadores.Location = new Point(14, 108);
            cbJugadores.Margin = new Padding(3, 4, 3, 4);
            cbJugadores.Name = "cbJugadores";
            cbJugadores.Size = new Size(307, 28);
            cbJugadores.TabIndex = 2;
            // 
            // numericMin
            // 
            numericMin.Location = new Point(14, 183);
            numericMin.Margin = new Padding(3, 4, 3, 4);
            numericMin.Name = "numericMin";
            numericMin.Size = new Size(137, 27);
            numericMin.TabIndex = 3;
            // 
            // cbTarjeta
            // 
            cbTarjeta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTarjeta.FormattingEnabled = true;
            cbTarjeta.Items.AddRange(new object[] { "Amarilla", "Roja" });
            cbTarjeta.Location = new Point(184, 183);
            cbTarjeta.Margin = new Padding(3, 4, 3, 4);
            cbTarjeta.Name = "cbTarjeta";
            cbTarjeta.Size = new Size(137, 28);
            cbTarjeta.TabIndex = 4;
            // 
            // dgvTarjetas
            // 
            dgvTarjetas.AllowUserToAddRows = false;
            dgvTarjetas.AllowUserToDeleteRows = false;
            dgvTarjetas.AllowUserToOrderColumns = true;
            dgvTarjetas.AllowUserToResizeColumns = false;
            dgvTarjetas.AllowUserToResizeRows = false;
            dgvTarjetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTarjetas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTarjetas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvTarjetas.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTarjetas.Location = new Point(14, 308);
            dgvTarjetas.Margin = new Padding(3, 4, 3, 4);
            dgvTarjetas.MultiSelect = false;
            dgvTarjetas.Name = "dgvTarjetas";
            dgvTarjetas.ReadOnly = true;
            dgvTarjetas.RowHeadersWidth = 51;
            dgvTarjetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTarjetas.Size = new Size(887, 276);
            dgvTarjetas.TabIndex = 5;
            dgvTarjetas.CellClick += dgvTarjetas_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(625, 36);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(86, 31);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(625, 108);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 31);
            btnModificar.TabIndex = 7;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(625, 183);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // CapturaTarjeta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvTarjetas);
            Controls.Add(cbTarjeta);
            Controls.Add(numericMin);
            Controls.Add(cbJugadores);
            Controls.Add(cbEquipos);
            Controls.Add(lblTipoTarjeta);
            Controls.Add(lblMinuto);
            Controls.Add(lblJugador);
            Controls.Add(lblEquipo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CapturaTarjeta";
            Text = "CapturaTarjeta";
            ((System.ComponentModel.ISupportInitialize)numericMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTarjetas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEquipo;
        private Label lblJugador;
        private Label lblMinuto;
        private Label lblTipoTarjeta;
        private ComboBox cbEquipos;
        private ComboBox cbJugadores;
        private NumericUpDown numericMin;
        private ComboBox cbTarjeta;
        private DataGridView dgvTarjetas;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}