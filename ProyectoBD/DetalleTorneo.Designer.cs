namespace ProyectoBD
{
    partial class DetalleTorneo
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
            cmbTorneo = new ComboBox();
            cmbEquipo = new ComboBox();
            bttnInscribir = new Button();
            label1 = new Label();
            label2 = new Label();
            dgvDetalleTorneo = new DataGridView();
            bttnModificar = new Button();
            bttnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleTorneo).BeginInit();
            SuspendLayout();
            // 
            // cmbTorneo
            // 
            cmbTorneo.FormattingEnabled = true;
            cmbTorneo.Location = new Point(12, 29);
            cmbTorneo.Name = "cmbTorneo";
            cmbTorneo.Size = new Size(261, 23);
            cmbTorneo.TabIndex = 0;
            cmbTorneo.SelectedIndexChanged += cmbTorneo_SelectedIndexChanged;
            // 
            // cmbEquipo
            // 
            cmbEquipo.FormattingEnabled = true;
            cmbEquipo.Location = new Point(12, 74);
            cmbEquipo.Name = "cmbEquipo";
            cmbEquipo.Size = new Size(261, 23);
            cmbEquipo.TabIndex = 1;
            cmbEquipo.SelectedIndexChanged += cmbEquipo_SelectedIndexChanged;
            // 
            // bttnInscribir
            // 
            bttnInscribir.BackColor = SystemColors.ActiveCaption;
            bttnInscribir.Location = new Point(12, 119);
            bttnInscribir.Name = "bttnInscribir";
            bttnInscribir.Size = new Size(79, 28);
            bttnInscribir.TabIndex = 2;
            bttnInscribir.Text = "Agregar";
            bttnInscribir.UseVisualStyleBackColor = false;
            bttnInscribir.Click += bttnInscribir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 3;
            label1.Text = "Torneo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 56);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 4;
            label2.Text = "Equipo";
            // 
            // dgvDetalleTorneo
            // 
            dgvDetalleTorneo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalleTorneo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleTorneo.Location = new Point(12, 175);
            dgvDetalleTorneo.MultiSelect = false;
            dgvDetalleTorneo.Name = "dgvDetalleTorneo";
            dgvDetalleTorneo.ReadOnly = true;
            dgvDetalleTorneo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleTorneo.Size = new Size(457, 258);
            dgvDetalleTorneo.TabIndex = 5;
            // 
            // bttnModificar
            // 
            bttnModificar.BackColor = SystemColors.ActiveCaption;
            bttnModificar.Location = new Point(110, 119);
            bttnModificar.Name = "bttnModificar";
            bttnModificar.Size = new Size(79, 28);
            bttnModificar.TabIndex = 6;
            bttnModificar.Text = "Modificar";
            bttnModificar.UseVisualStyleBackColor = false;
            bttnModificar.Click += bttnModificar_Click;
            // 
            // bttnEliminar
            // 
            bttnEliminar.BackColor = SystemColors.ActiveCaption;
            bttnEliminar.Location = new Point(211, 119);
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Size = new Size(79, 28);
            bttnEliminar.TabIndex = 7;
            bttnEliminar.Text = "Eliminar";
            bttnEliminar.UseVisualStyleBackColor = false;
            bttnEliminar.Click += bttnEliminar_Click;
            // 
            // DetalleTorneo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 445);
            Controls.Add(bttnEliminar);
            Controls.Add(bttnModificar);
            Controls.Add(dgvDetalleTorneo);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bttnInscribir);
            Controls.Add(cmbEquipo);
            Controls.Add(cmbTorneo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DetalleTorneo";
            Text = "DetalleTorneo";
            ((System.ComponentModel.ISupportInitialize)dgvDetalleTorneo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbTorneo;
        private ComboBox cmbEquipo;
        private Button bttnInscribir;
        private Label label1;
        private Label label2;
        private DataGridView dgvDetalleTorneo;
        private Button bttnModificar;
        private Button bttnEliminar;
    }
}