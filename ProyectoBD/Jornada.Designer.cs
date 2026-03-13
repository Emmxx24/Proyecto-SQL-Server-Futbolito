namespace ProyectoBD
{
    partial class Jornada
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
            label1 = new Label();
            label2 = new Label();
            cmbTorneo = new ComboBox();
            txtNumJornada = new TextBox();
            bttnEliminar = new Button();
            bttnModificar = new Button();
            bttnAgregar = new Button();
            dgvJornada = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvJornada).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 12);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 0;
            label1.Text = "Torneo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 1;
            label2.Text = "Número de Jornada:";
            // 
            // cmbTorneo
            // 
            cmbTorneo.FormattingEnabled = true;
            cmbTorneo.Location = new Point(143, 12);
            cmbTorneo.Name = "cmbTorneo";
            cmbTorneo.Size = new Size(121, 23);
            cmbTorneo.TabIndex = 2;
            // 
            // txtNumJornada
            // 
            txtNumJornada.Location = new Point(143, 65);
            txtNumJornada.Name = "txtNumJornada";
            txtNumJornada.Size = new Size(121, 23);
            txtNumJornada.TabIndex = 3;
            // 
            // bttnEliminar
            // 
            bttnEliminar.BackColor = SystemColors.ActiveCaption;
            bttnEliminar.Location = new Point(191, 118);
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Size = new Size(75, 30);
            bttnEliminar.TabIndex = 4;
            bttnEliminar.Text = "Eliminar";
            bttnEliminar.UseVisualStyleBackColor = false;
            bttnEliminar.Click += bttnEliminar_Click;
            // 
            // bttnModificar
            // 
            bttnModificar.BackColor = SystemColors.ActiveCaption;
            bttnModificar.Location = new Point(110, 118);
            bttnModificar.Name = "bttnModificar";
            bttnModificar.Size = new Size(75, 30);
            bttnModificar.TabIndex = 5;
            bttnModificar.Text = "Modificar";
            bttnModificar.UseVisualStyleBackColor = false;
            bttnModificar.Click += bttnModificar_Click;
            // 
            // bttnAgregar
            // 
            bttnAgregar.BackColor = SystemColors.ActiveCaption;
            bttnAgregar.Location = new Point(29, 118);
            bttnAgregar.Name = "bttnAgregar";
            bttnAgregar.Size = new Size(75, 30);
            bttnAgregar.TabIndex = 6;
            bttnAgregar.Text = "Agregar";
            bttnAgregar.UseVisualStyleBackColor = false;
            bttnAgregar.Click += bttnAgregar_Click;
            // 
            // dgvJornada
            // 
            dgvJornada.AllowUserToAddRows = false;
            dgvJornada.AllowUserToDeleteRows = false;
            dgvJornada.AllowUserToResizeColumns = false;
            dgvJornada.AllowUserToResizeRows = false;
            dgvJornada.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJornada.Location = new Point(12, 202);
            dgvJornada.Name = "dgvJornada";
            dgvJornada.ReadOnly = true;
            dgvJornada.Size = new Size(250, 236);
            dgvJornada.TabIndex = 7;
            dgvJornada.CellClick += dgvJornada_CellClick;
            // 
            // Jornada
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 450);
            Controls.Add(dgvJornada);
            Controls.Add(bttnAgregar);
            Controls.Add(bttnModificar);
            Controls.Add(bttnEliminar);
            Controls.Add(txtNumJornada);
            Controls.Add(cmbTorneo);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Jornada";
            Text = "Jornada";
            ((System.ComponentModel.ISupportInitialize)dgvJornada).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbTorneo;
        private TextBox txtNumJornada;
        private Button bttnEliminar;
        private Button bttnModificar;
        private Button bttnAgregar;
        private DataGridView dgvJornada;
    }
}