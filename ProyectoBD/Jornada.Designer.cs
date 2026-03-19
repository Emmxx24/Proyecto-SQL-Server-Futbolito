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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            cmbTorneo = new ComboBox();
            bttnEliminar = new Button();
            bttnModificar = new Button();
            bttnAgregar = new Button();
            dgvJornada = new DataGridView();
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dgvJornada).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 16);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 0;
            label1.Text = "Torneo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(87, 85);
            label2.Name = "label2";
            label2.Size = new Size(143, 20);
            label2.TabIndex = 1;
            label2.Text = "Número de Jornada:";
            // 
            // cmbTorneo
            // 
            cmbTorneo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTorneo.FormattingEnabled = true;
            cmbTorneo.Location = new Point(160, 16);
            cmbTorneo.Margin = new Padding(3, 4, 3, 4);
            cmbTorneo.Name = "cmbTorneo";
            cmbTorneo.Size = new Size(278, 28);
            cmbTorneo.TabIndex = 2;
            cmbTorneo.DropDown += cmbTorneo_DropDown;
            cmbTorneo.SelectedIndexChanged += cmbTorneo_SelectedIndexChanged;
            // 
            // bttnEliminar
            // 
            bttnEliminar.BackColor = SystemColors.ActiveCaption;
            bttnEliminar.Location = new Point(352, 157);
            bttnEliminar.Margin = new Padding(3, 4, 3, 4);
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Size = new Size(86, 40);
            bttnEliminar.TabIndex = 4;
            bttnEliminar.Text = "Eliminar";
            bttnEliminar.UseVisualStyleBackColor = false;
            bttnEliminar.Click += bttnEliminar_Click;
            // 
            // bttnModificar
            // 
            bttnModificar.BackColor = SystemColors.ActiveCaption;
            bttnModificar.Location = new Point(220, 157);
            bttnModificar.Margin = new Padding(3, 4, 3, 4);
            bttnModificar.Name = "bttnModificar";
            bttnModificar.Size = new Size(86, 40);
            bttnModificar.TabIndex = 5;
            bttnModificar.Text = "Modificar";
            bttnModificar.UseVisualStyleBackColor = false;
            bttnModificar.Click += bttnModificar_Click;
            // 
            // bttnAgregar
            // 
            bttnAgregar.BackColor = SystemColors.ActiveCaption;
            bttnAgregar.Location = new Point(87, 157);
            bttnAgregar.Margin = new Padding(3, 4, 3, 4);
            bttnAgregar.Name = "bttnAgregar";
            bttnAgregar.Size = new Size(86, 40);
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
            dgvJornada.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJornada.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvJornada.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvJornada.DefaultCellStyle = dataGridViewCellStyle1;
            dgvJornada.Location = new Point(14, 225);
            dgvJornada.Margin = new Padding(3, 4, 3, 4);
            dgvJornada.Name = "dgvJornada";
            dgvJornada.ReadOnly = true;
            dgvJornada.RowHeadersWidth = 51;
            dgvJornada.Size = new Size(680, 359);
            dgvJornada.TabIndex = 7;
            dgvJornada.CellClick += dgvJornada_CellClick;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(236, 83);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(52, 27);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // Jornada
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 600);
            Controls.Add(numericUpDown1);
            Controls.Add(dgvJornada);
            Controls.Add(bttnAgregar);
            Controls.Add(bttnModificar);
            Controls.Add(bttnEliminar);
            Controls.Add(cmbTorneo);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Jornada";
            Text = "Jornada";
            ((System.ComponentModel.ISupportInitialize)dgvJornada).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbTorneo;
        private Button bttnEliminar;
        private Button bttnModificar;
        private Button bttnAgregar;
        private DataGridView dgvJornada;
        private NumericUpDown numericUpDown1;
    }
}