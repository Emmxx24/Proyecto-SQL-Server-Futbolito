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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            cbJugadores = new ComboBox();
            lblJugador = new Label();
            lblMinuto = new Label();
            numericMin = new NumericUpDown();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvGoles = new DataGridView();
            label1 = new Label();
            txtPartidoDetalle = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numericMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGoles).BeginInit();
            SuspendLayout();
            // 
            // cbJugadores
            // 
            cbJugadores.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJugadores.FormattingEnabled = true;
            cbJugadores.Location = new Point(14, 88);
            cbJugadores.Margin = new Padding(3, 4, 3, 4);
            cbJugadores.Name = "cbJugadores";
            cbJugadores.Size = new Size(602, 28);
            cbJugadores.TabIndex = 2;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(14, 64);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(65, 20);
            lblJugador.TabIndex = 3;
            lblJugador.Text = "Jugador:";
            // 
            // lblMinuto
            // 
            lblMinuto.AutoSize = true;
            lblMinuto.Location = new Point(14, 139);
            lblMinuto.Name = "lblMinuto";
            lblMinuto.Size = new Size(59, 20);
            lblMinuto.TabIndex = 4;
            lblMinuto.Text = "Minuto:";
            // 
            // numericMin
            // 
            numericMin.Location = new Point(14, 163);
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvGoles.DefaultCellStyle = dataGridViewCellStyle2;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 9);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 10;
            label1.Text = "Partido:";
            // 
            // txtPartidoDetalle
            // 
            txtPartidoDetalle.Location = new Point(14, 37);
            txtPartidoDetalle.Name = "txtPartidoDetalle";
            txtPartidoDetalle.ReadOnly = true;
            txtPartidoDetalle.Size = new Size(602, 27);
            txtPartidoDetalle.TabIndex = 11;
            // 
            // CapturaGol
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 679);
            Controls.Add(txtPartidoDetalle);
            Controls.Add(label1);
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
        private Label label1;
        private TextBox txtPartidoDetalle;
    }
}