namespace ProyectoBD
{
    partial class CapturaGol
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
            cbEquipos = new ComboBox();
            cbJugadores = new ComboBox();
            lblJugador = new Label();
            lblMinuto = new Label();
            numericMin = new NumericUpDown();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvGoles = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numericMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoles).BeginInit();
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
            cbJugadores.Location = new Point(12, 82);
            cbJugadores.Name = "cbJugadores";
            cbJugadores.Size = new Size(269, 23);
            cbJugadores.TabIndex = 2;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(12, 64);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(52, 15);
            lblJugador.TabIndex = 3;
            lblJugador.Text = "Jugador:";
            // 
            // lblMinuto
            // 
            lblMinuto.AutoSize = true;
            lblMinuto.Location = new Point(12, 120);
            lblMinuto.Name = "lblMinuto";
            lblMinuto.Size = new Size(49, 15);
            lblMinuto.TabIndex = 4;
            lblMinuto.Text = "Minuto:";
            // 
            // numericMin
            // 
            numericMin.Location = new Point(12, 138);
            numericMin.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericMin.Name = "numericMin";
            numericMin.Size = new Size(120, 23);
            numericMin.TabIndex = 5;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(544, 27);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(544, 82);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 7;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(544, 136);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvGoles
            // 
            dgvGoles.AllowUserToAddRows = false;
            dgvGoles.AllowUserToDeleteRows = false;
            dgvGoles.AllowUserToResizeColumns = false;
            dgvGoles.AllowUserToResizeRows = false;
            dgvGoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGoles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvGoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGoles.DefaultCellStyle = dataGridViewCellStyle1;
            dgvGoles.Location = new Point(12, 227);
            dgvGoles.MultiSelect = false;
            dgvGoles.Name = "dgvGoles";
            dgvGoles.ReadOnly = true;
            dgvGoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoles.Size = new Size(776, 211);
            dgvGoles.TabIndex = 9;
            dgvGoles.CellClick += dgvGoles_CellClick;
            // 
            // CapturaGol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvGoles);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(numericMin);
            Controls.Add(lblMinuto);
            Controls.Add(lblJugador);
            Controls.Add(cbJugadores);
            Controls.Add(cbEquipos);
            Controls.Add(lblEquipo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapturaGol";
            Text = "CapturaGol";
            ((System.ComponentModel.ISupportInitialize)numericMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEquipo;
        private ComboBox cbEquipos;
        private ComboBox cbJugadores;
        private Label lblJugador;
        private Label lblMinuto;
        private NumericUpDown numericMin;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvGoles;
    }
}