namespace ProyectoBD
{
    partial class CapturaEquipo
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
            tbNombre = new TextBox();
            lblLogo = new Label();
            dgvEquipo = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            openFileDialog1 = new OpenFileDialog();
            tbLogo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvEquipo).BeginInit();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(69, 48);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // tbNombre
            // 
            tbNombre.Location = new Point(61, 71);
            tbNombre.Name = "tbNombre";
            tbNombre.Size = new Size(198, 27);
            tbNombre.TabIndex = 1;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(69, 114);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(125, 20);
            lblLogo.TabIndex = 2;
            lblLogo.Text = "URL del logotipo:";
            // 
            // dgvEquipo
            // 
            dgvEquipo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipo.Location = new Point(12, 239);
            dgvEquipo.Name = "dgvEquipo";
            dgvEquipo.RowHeadersWidth = 51;
            dgvEquipo.Size = new Size(720, 624);
            dgvEquipo.TabIndex = 3;
            dgvEquipo.CellClick += dgvEquipo_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(397, 177);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(517, 176);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 5;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(634, 177);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // tbLogo
            // 
            tbLogo.Location = new Point(61, 137);
            tbLogo.Name = "tbLogo";
            tbLogo.Size = new Size(198, 27);
            tbLogo.TabIndex = 7;
            // 
            // CapturaEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 875);
            Controls.Add(tbLogo);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvEquipo);
            Controls.Add(lblLogo);
            Controls.Add(tbNombre);
            Controls.Add(lblNombre);
            MaximizeBox = false;
            Name = "CapturaEquipo";
            Text = "Equipo";
            ((System.ComponentModel.ISupportInitialize)dgvEquipo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private TextBox tbNombre;
        private Label lblLogo;
        private DataGridView dgvEquipo;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private OpenFileDialog openFileDialog1;
        private TextBox tbLogo;
    }
}