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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            lblEquipo = new Label();
            lblJugador = new Label();
            lblMinuto = new Label();
            lblTipoTarjeta = new Label();
            cbEquipos = new ComboBox();
            cbJugadores = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            comboBox1 = new ComboBox();
            dgvTarjetas = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTarjetas).BeginInit();
            SuspendLayout();
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = true;
            lblEquipo.Location = new Point(12, 9);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(47, 15);
            lblEquipo.TabIndex = 0;
            lblEquipo.Text = "Equipo:";
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(12, 63);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(52, 15);
            lblJugador.TabIndex = 1;
            lblJugador.Text = "Jugador:";
            // 
            // lblMinuto
            // 
            lblMinuto.AutoSize = true;
            lblMinuto.Location = new Point(12, 119);
            lblMinuto.Name = "lblMinuto";
            lblMinuto.Size = new Size(49, 15);
            lblMinuto.TabIndex = 2;
            lblMinuto.Text = "Minuto:";
            // 
            // lblTipoTarjeta
            // 
            lblTipoTarjeta.AutoSize = true;
            lblTipoTarjeta.Location = new Point(161, 119);
            lblTipoTarjeta.Name = "lblTipoTarjeta";
            lblTipoTarjeta.Size = new Size(86, 15);
            lblTipoTarjeta.TabIndex = 3;
            lblTipoTarjeta.Text = "Tipo de tarjeta:";
            // 
            // cbEquipos
            // 
            cbEquipos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEquipos.FormattingEnabled = true;
            cbEquipos.Location = new Point(12, 27);
            cbEquipos.Name = "cbEquipos";
            cbEquipos.Size = new Size(269, 23);
            cbEquipos.TabIndex = 1;
            cbEquipos.SelectedIndexChanged += cbEquipos_SelectedIndexChanged;
            // 
            // cbJugadores
            // 
            cbJugadores.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJugadores.FormattingEnabled = true;
            cbJugadores.Location = new Point(12, 81);
            cbJugadores.Name = "cbJugadores";
            cbJugadores.Size = new Size(269, 23);
            cbJugadores.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(12, 137);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 3;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Amarilla", "Roja" });
            comboBox1.Location = new Point(161, 137);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(120, 23);
            comboBox1.TabIndex = 4;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvTarjetas.DefaultCellStyle = dataGridViewCellStyle1;
            dgvTarjetas.Location = new Point(12, 231);
            dgvTarjetas.MultiSelect = false;
            dgvTarjetas.Name = "dgvTarjetas";
            dgvTarjetas.ReadOnly = true;
            dgvTarjetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTarjetas.Size = new Size(776, 207);
            dgvTarjetas.TabIndex = 5;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(547, 27);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(547, 81);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 7;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(547, 137);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // CapturaTarjeta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvTarjetas);
            Controls.Add(comboBox1);
            Controls.Add(numericUpDown1);
            Controls.Add(cbJugadores);
            Controls.Add(cbEquipos);
            Controls.Add(lblTipoTarjeta);
            Controls.Add(lblMinuto);
            Controls.Add(lblJugador);
            Controls.Add(lblEquipo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapturaTarjeta";
            Text = "CapturaTarjeta";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
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
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox1;
        private DataGridView dgvTarjetas;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}