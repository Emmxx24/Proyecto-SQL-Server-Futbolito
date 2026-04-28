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
            // cbJugadores
            // 
            cbJugadores.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJugadores.FormattingEnabled = true;
            cbJugadores.Location = new Point(14, 33);
            cbJugadores.Margin = new Padding(3, 4, 3, 4);
            cbJugadores.Name = "cbJugadores";
            cbJugadores.Size = new Size(602, 28);
            cbJugadores.TabIndex = 2;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(14, 9);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(65, 20);
            lblJugador.TabIndex = 3;
            lblJugador.Text = "Jugador:";
            // 
            // lblMinuto
            // 
            lblMinuto.AutoSize = true;
            lblMinuto.Location = new Point(14, 84);
            lblMinuto.Name = "lblMinuto";
            lblMinuto.Size = new Size(59, 20);
            lblMinuto.TabIndex = 4;
            lblMinuto.Text = "Minuto:";
            // 
            // numericMin
            // 
            numericMin.Location = new Point(14, 108);
            numericMin.Margin = new Padding(3, 4, 3, 4);
            numericMin.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericMin.Name = "numericMin";
            numericMin.Size = new Size(137, 27);
            numericMin.TabIndex = 5;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(798, 33);
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
            btnModificar.Location = new Point(798, 106);
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
            btnEliminar.Location = new Point(798, 178);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
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
            dgvGoles.Location = new Point(14, 243);
            dgvGoles.Margin = new Padding(3, 4, 3, 4);
            dgvGoles.MultiSelect = false;
            dgvGoles.Name = "dgvGoles";
            dgvGoles.ReadOnly = true;
            dgvGoles.RowHeadersWidth = 51;
            dgvGoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoles.Size = new Size(1070, 423);
            dgvGoles.TabIndex = 9;
            dgvGoles.CellClick += dgvGoles_CellClick;
            // 
            // CapturaGol
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 679);
            Controls.Add(dgvGoles);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(numericMin);
            Controls.Add(lblMinuto);
            Controls.Add(lblJugador);
            Controls.Add(cbJugadores);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "CapturaGol";
            Text = "CapturaGol";
            ((System.ComponentModel.ISupportInitialize)numericMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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