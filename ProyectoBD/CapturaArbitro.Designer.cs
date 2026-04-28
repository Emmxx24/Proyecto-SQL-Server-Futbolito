namespace ProyectoBD
{
    partial class CapturaArbitro
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
            lblSeleccion = new Label();
            lblCedula = new Label();
            tbCedula = new TextBox();
            dgvArbitro = new DataGridView();
            btnAgregar = new Button();
            Eliminar = new Button();
            btnModificar = new Button();
            tbParcipante = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvArbitro).BeginInit();
            SuspendLayout();
            // 
            // lblSeleccion
            // 
            lblSeleccion.AutoSize = true;
            lblSeleccion.Location = new Point(30, 44);
            lblSeleccion.Name = "lblSeleccion";
            lblSeleccion.Size = new Size(185, 20);
            lblSeleccion.TabIndex = 0;
            lblSeleccion.Text = "Seleccione un Participante:";
            // 
            // lblCedula
            // 
            lblCedula.AutoSize = true;
            lblCedula.Location = new Point(30, 147);
            lblCedula.Name = "lblCedula";
            lblCedula.Size = new Size(136, 20);
            lblCedula.TabIndex = 2;
            lblCedula.Text = "Cédula Profesional:";
            // 
            // tbCedula
            // 
            tbCedula.Location = new Point(30, 170);
            tbCedula.MaxLength = 15;
            tbCedula.Name = "tbCedula";
            tbCedula.Size = new Size(176, 27);
            tbCedula.TabIndex = 3;
            tbCedula.TextChanged += tbCedula_TextChanged;
            // 
            // dgvArbitro
            // 
            dgvArbitro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvArbitro.Location = new Point(30, 290);
            dgvArbitro.Name = "dgvArbitro";
            dgvArbitro.RowHeadersWidth = 51;
            dgvArbitro.Size = new Size(466, 188);
            dgvArbitro.TabIndex = 4;
            dgvArbitro.CellClick += dgvArbitro_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(30, 239);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // Eliminar
            // 
            Eliminar.BackColor = SystemColors.ActiveCaption;
            Eliminar.Location = new Point(317, 239);
            Eliminar.Name = "Eliminar";
            Eliminar.Size = new Size(94, 29);
            Eliminar.TabIndex = 6;
            Eliminar.Text = "Eliminar";
            Eliminar.UseVisualStyleBackColor = false;
            Eliminar.Click += Eliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.ForeColor = Color.Black;
            btnModificar.Location = new Point(179, 239);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 7;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // tbParcipante
            // 
            tbParcipante.Location = new Point(30, 67);
            tbParcipante.MaxLength = 15;
            tbParcipante.Name = "tbParcipante";
            tbParcipante.ReadOnly = true;
            tbParcipante.Size = new Size(176, 27);
            tbParcipante.TabIndex = 8;
            // 
            // CapturaArbitro
            // 
            AccessibleRole = AccessibleRole.None;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 490);
            Controls.Add(tbParcipante);
            Controls.Add(btnModificar);
            Controls.Add(Eliminar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvArbitro);
            Controls.Add(tbCedula);
            Controls.Add(lblCedula);
            Controls.Add(lblSeleccion);
            MaximizeBox = false;
            Name = "CapturaArbitro";
            Text = "Árbitro";
            ((System.ComponentModel.ISupportInitialize)dgvArbitro).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSeleccion;
        private Label lblCedula;
        private TextBox tbCedula;
        private DataGridView dgvArbitro;
        private Button btnAgregar;
        private Button Eliminar;
        private Button btnModificar;
        private TextBox tbParcipante;
    }
}