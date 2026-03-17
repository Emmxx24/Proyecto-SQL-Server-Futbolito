namespace ProyectoBD
{
    partial class CapturaLugar
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
            lblNombreLugar = new Label();
            lblUbiLugar = new Label();
            lblCapaciLugar = new Label();
            dgvLugar = new DataGridView();
            txtUbiLugar = new TextBox();
            txtNombreLugar = new TextBox();
            numericCapacidad = new NumericUpDown();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLugar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericCapacidad).BeginInit();
            SuspendLayout();
            // 
            // lblNombreLugar
            // 
            lblNombreLugar.AutoSize = true;
            lblNombreLugar.Location = new Point(38, 25);
            lblNombreLugar.Name = "lblNombreLugar";
            lblNombreLugar.Size = new Size(105, 20);
            lblNombreLugar.TabIndex = 0;
            lblNombreLugar.Text = "Nombre lugar:";
            // 
            // lblUbiLugar
            // 
            lblUbiLugar.AutoSize = true;
            lblUbiLugar.Location = new Point(38, 111);
            lblUbiLugar.Name = "lblUbiLugar";
            lblUbiLugar.Size = new Size(78, 20);
            lblUbiLugar.TabIndex = 1;
            lblUbiLugar.Text = "Ubicación:";
            // 
            // lblCapaciLugar
            // 
            lblCapaciLugar.AutoSize = true;
            lblCapaciLugar.Location = new Point(38, 196);
            lblCapaciLugar.Name = "lblCapaciLugar";
            lblCapaciLugar.Size = new Size(187, 20);
            lblCapaciLugar.TabIndex = 2;
            lblCapaciLugar.Text = "Capacidad (mayor a 1000):";
            // 
            // dgvLugar
            // 
            dgvLugar.AllowUserToAddRows = false;
            dgvLugar.AllowUserToDeleteRows = false;
            dgvLugar.AllowUserToResizeColumns = false;
            dgvLugar.AllowUserToResizeRows = false;
            dgvLugar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLugar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLugar.Location = new Point(14, 296);
            dgvLugar.Margin = new Padding(3, 4, 3, 4);
            dgvLugar.MultiSelect = false;
            dgvLugar.Name = "dgvLugar";
            dgvLugar.ReadOnly = true;
            dgvLugar.RowHeadersWidth = 51;
            dgvLugar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLugar.Size = new Size(703, 200);
            dgvLugar.TabIndex = 11;
            dgvLugar.CellClick += dgvLugar_CellClick;
            // 
            // txtUbiLugar
            // 
            txtUbiLugar.Location = new Point(38, 149);
            txtUbiLugar.Margin = new Padding(3, 4, 3, 4);
            txtUbiLugar.MaxLength = 50;
            txtUbiLugar.Name = "txtUbiLugar";
            txtUbiLugar.Size = new Size(268, 27);
            txtUbiLugar.TabIndex = 6;
            // 
            // txtNombreLugar
            // 
            txtNombreLugar.Location = new Point(38, 63);
            txtNombreLugar.Margin = new Padding(3, 4, 3, 4);
            txtNombreLugar.MaxLength = 50;
            txtNombreLugar.Name = "txtNombreLugar";
            txtNombreLugar.Size = new Size(268, 27);
            txtNombreLugar.TabIndex = 5;
            // 
            // numericCapacidad
            // 
            numericCapacidad.Location = new Point(38, 235);
            numericCapacidad.Margin = new Padding(3, 4, 3, 4);
            numericCapacidad.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericCapacidad.Name = "numericCapacidad";
            numericCapacidad.Size = new Size(137, 27);
            numericCapacidad.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(517, 65);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(86, 31);
            btnAgregar.TabIndex = 8;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(517, 151);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 31);
            btnModificar.TabIndex = 9;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(517, 235);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // CapturaLugar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 513);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(numericCapacidad);
            Controls.Add(txtNombreLugar);
            Controls.Add(txtUbiLugar);
            Controls.Add(dgvLugar);
            Controls.Add(lblCapaciLugar);
            Controls.Add(lblUbiLugar);
            Controls.Add(lblNombreLugar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CapturaLugar";
            Text = "Lugar";
            ((System.ComponentModel.ISupportInitialize)dgvLugar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericCapacidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreLugar;
        private Label lblUbiLugar;
        private Label lblCapaciLugar;
        private DataGridView dgvLugar;
        private TextBox txtUbiLugar;
        private TextBox txtNombreLugar;
        private NumericUpDown numericCapacidad;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}