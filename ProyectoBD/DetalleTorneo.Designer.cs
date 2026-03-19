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
            cmbTorneo.Location = new Point(14, 39);
            cmbTorneo.Margin = new Padding(3, 4, 3, 4);
            cmbTorneo.Name = "cmbTorneo";
            cmbTorneo.Size = new Size(298, 28);
            cmbTorneo.TabIndex = 0;
            cmbTorneo.DropDown += cmbTorneo_DropDown;
            cmbTorneo.SelectedIndexChanged += cmbTorneo_SelectedIndexChanged;
            // 
            // cmbEquipo
            // 
            cmbEquipo.FormattingEnabled = true;
            cmbEquipo.Location = new Point(14, 99);
            cmbEquipo.Margin = new Padding(3, 4, 3, 4);
            cmbEquipo.Name = "cmbEquipo";
            cmbEquipo.Size = new Size(298, 28);
            cmbEquipo.TabIndex = 1;
            cmbEquipo.DropDown += cmbEquipo_DropDown;
            cmbEquipo.SelectedIndexChanged += cmbEquipo_SelectedIndexChanged;
            // 
            // bttnInscribir
            // 
            bttnInscribir.BackColor = SystemColors.ActiveCaption;
            bttnInscribir.Location = new Point(14, 159);
            bttnInscribir.Margin = new Padding(3, 4, 3, 4);
            bttnInscribir.Name = "bttnInscribir";
            bttnInscribir.Size = new Size(90, 37);
            bttnInscribir.TabIndex = 2;
            bttnInscribir.Text = "Agregar";
            bttnInscribir.UseVisualStyleBackColor = false;
            bttnInscribir.Click += bttnInscribir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 15);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 3;
            label1.Text = "Torneo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 75);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 4;
            label2.Text = "Equipo";
            // 
            // dgvDetalleTorneo
            // 
            dgvDetalleTorneo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalleTorneo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleTorneo.Location = new Point(14, 233);
            dgvDetalleTorneo.Margin = new Padding(3, 4, 3, 4);
            dgvDetalleTorneo.MultiSelect = false;
            dgvDetalleTorneo.Name = "dgvDetalleTorneo";
            dgvDetalleTorneo.ReadOnly = true;
            dgvDetalleTorneo.RowHeadersWidth = 51;
            dgvDetalleTorneo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleTorneo.Size = new Size(522, 344);
            dgvDetalleTorneo.TabIndex = 5;
            // 
            // bttnModificar
            // 
            bttnModificar.BackColor = SystemColors.ActiveCaption;
            bttnModificar.Location = new Point(126, 159);
            bttnModificar.Margin = new Padding(3, 4, 3, 4);
            bttnModificar.Name = "bttnModificar";
            bttnModificar.Size = new Size(90, 37);
            bttnModificar.TabIndex = 6;
            bttnModificar.Text = "Modificar";
            bttnModificar.UseVisualStyleBackColor = false;
            bttnModificar.Click += bttnModificar_Click;
            // 
            // bttnEliminar
            // 
            bttnEliminar.BackColor = SystemColors.ActiveCaption;
            bttnEliminar.Location = new Point(241, 159);
            bttnEliminar.Margin = new Padding(3, 4, 3, 4);
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Size = new Size(90, 37);
            bttnEliminar.TabIndex = 7;
            bttnEliminar.Text = "Eliminar";
            bttnEliminar.UseVisualStyleBackColor = false;
            bttnEliminar.Click += bttnEliminar_Click;
            // 
            // DetalleTorneo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 593);
            Controls.Add(bttnEliminar);
            Controls.Add(bttnModificar);
            Controls.Add(dgvDetalleTorneo);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bttnInscribir);
            Controls.Add(cmbEquipo);
            Controls.Add(cmbTorneo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
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