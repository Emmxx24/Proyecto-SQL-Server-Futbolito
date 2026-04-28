namespace ProyectoBD
{
    partial class CapturaTorneos
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
            lblNombreTorneo = new Label();
            txtNombreTorneo = new TextBox();
            lblEdadMin = new Label();
            dgvTorneo = new DataGridView();
            lblEdadMax = new Label();
            lblGenero = new Label();
            cbGenero = new ComboBox();
            lblFechaIni = new Label();
            numericEdadMin = new NumericUpDown();
            numericEdadMax = new NumericUpDown();
            mcFechaIni = new MonthCalendar();
            lblFechaFin = new Label();
            mcFechaFin = new MonthCalendar();
            btnAgregar = new Button();
            btnModif = new Button();
            btnElim = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTorneo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericEdadMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericEdadMax).BeginInit();
            SuspendLayout();
            // 
            // lblNombreTorneo
            // 
            lblNombreTorneo.AutoSize = true;
            lblNombreTorneo.Location = new Point(21, 12);
            lblNombreTorneo.Name = "lblNombreTorneo";
            lblNombreTorneo.Size = new Size(108, 15);
            lblNombreTorneo.TabIndex = 0;
            lblNombreTorneo.Text = "Nombre de torneo:";
            // 
            // txtNombreTorneo
            // 
            txtNombreTorneo.Location = new Point(21, 29);
            txtNombreTorneo.Margin = new Padding(3, 2, 3, 2);
            txtNombreTorneo.MaxLength = 50;
            txtNombreTorneo.Name = "txtNombreTorneo";
            txtNombreTorneo.Size = new Size(122, 23);
            txtNombreTorneo.TabIndex = 1;
            // 
            // lblEdadMin
            // 
            lblEdadMin.AutoSize = true;
            lblEdadMin.Location = new Point(21, 61);
            lblEdadMin.Name = "lblEdadMin";
            lblEdadMin.Size = new Size(80, 15);
            lblEdadMin.TabIndex = 3;
            lblEdadMin.Text = "Edad mínima:";
            // 
            // dgvTorneo
            // 
            dgvTorneo.AllowUserToAddRows = false;
            dgvTorneo.AllowUserToDeleteRows = false;
            dgvTorneo.AllowUserToResizeColumns = false;
            dgvTorneo.AllowUserToResizeRows = false;
            dgvTorneo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTorneo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTorneo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvTorneo.DefaultCellStyle = dataGridViewCellStyle1;
            dgvTorneo.Location = new Point(13, 220);
            dgvTorneo.Margin = new Padding(3, 2, 3, 2);
            dgvTorneo.MultiSelect = false;
            dgvTorneo.Name = "dgvTorneo";
            dgvTorneo.ReadOnly = true;
            dgvTorneo.RowHeadersWidth = 51;
            dgvTorneo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTorneo.Size = new Size(858, 128);
            dgvTorneo.TabIndex = 10;
            dgvTorneo.CellClick += dgvTorneo_CellClick;
            // 
            // lblEdadMax
            // 
            lblEdadMax.AutoSize = true;
            lblEdadMax.Location = new Point(21, 111);
            lblEdadMax.Name = "lblEdadMax";
            lblEdadMax.Size = new Size(81, 15);
            lblEdadMax.TabIndex = 5;
            lblEdadMax.Text = "Edad máxima:";
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(21, 160);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(48, 15);
            lblGenero.TabIndex = 7;
            lblGenero.Text = "Género:";
            // 
            // cbGenero
            // 
            cbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGenero.FormattingEnabled = true;
            cbGenero.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cbGenero.Location = new Point(21, 177);
            cbGenero.Margin = new Padding(3, 2, 3, 2);
            cbGenero.Name = "cbGenero";
            cbGenero.Size = new Size(133, 23);
            cbGenero.TabIndex = 4;
            // 
            // lblFechaIni
            // 
            lblFechaIni.AutoSize = true;
            lblFechaIni.Location = new Point(220, 12);
            lblFechaIni.Name = "lblFechaIni";
            lblFechaIni.Size = new Size(89, 15);
            lblFechaIni.TabIndex = 9;
            lblFechaIni.Text = "Fecha de inicio:";
            // 
            // numericEdadMin
            // 
            numericEdadMin.Location = new Point(21, 79);
            numericEdadMin.Margin = new Padding(3, 2, 3, 2);
            numericEdadMin.Name = "numericEdadMin";
            numericEdadMin.Size = new Size(131, 23);
            numericEdadMin.TabIndex = 2;
            // 
            // numericEdadMax
            // 
            numericEdadMax.Location = new Point(21, 128);
            numericEdadMax.Margin = new Padding(3, 2, 3, 2);
            numericEdadMax.Name = "numericEdadMax";
            numericEdadMax.Size = new Size(131, 23);
            numericEdadMax.TabIndex = 3;
            // 
            // mcFechaIni
            // 
            mcFechaIni.Location = new Point(220, 34);
            mcFechaIni.Margin = new Padding(8, 7, 8, 7);
            mcFechaIni.Name = "mcFechaIni";
            mcFechaIni.TabIndex = 5;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(498, 12);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(102, 15);
            lblFechaFin.TabIndex = 14;
            lblFechaFin.Text = "Fecha de termino:";
            // 
            // mcFechaFin
            // 
            mcFechaFin.Location = new Point(498, 34);
            mcFechaFin.Margin = new Padding(8, 7, 8, 7);
            mcFechaFin.Name = "mcFechaFin";
            mcFechaFin.TabIndex = 6;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(789, 61);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(82, 22);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModif
            // 
            btnModif.BackColor = SystemColors.ActiveCaption;
            btnModif.Location = new Point(789, 104);
            btnModif.Margin = new Padding(3, 2, 3, 2);
            btnModif.Name = "btnModif";
            btnModif.Size = new Size(82, 22);
            btnModif.TabIndex = 8;
            btnModif.Text = "Modificar";
            btnModif.UseVisualStyleBackColor = false;
            btnModif.Click += btnModif_Click;
            // 
            // btnElim
            // 
            btnElim.BackColor = SystemColors.ActiveCaption;
            btnElim.Location = new Point(789, 153);
            btnElim.Margin = new Padding(3, 2, 3, 2);
            btnElim.Name = "btnElim";
            btnElim.Size = new Size(82, 22);
            btnElim.TabIndex = 9;
            btnElim.Text = "Eliminar";
            btnElim.UseVisualStyleBackColor = false;
            btnElim.Click += btnElim_Click;
            // 
            // CapturaTorneos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 359);
            Controls.Add(btnElim);
            Controls.Add(btnModif);
            Controls.Add(btnAgregar);
            Controls.Add(mcFechaFin);
            Controls.Add(lblFechaFin);
            Controls.Add(mcFechaIni);
            Controls.Add(numericEdadMax);
            Controls.Add(numericEdadMin);
            Controls.Add(lblFechaIni);
            Controls.Add(cbGenero);
            Controls.Add(lblGenero);
            Controls.Add(lblEdadMax);
            Controls.Add(dgvTorneo);
            Controls.Add(lblEdadMin);
            Controls.Add(txtNombreTorneo);
            Controls.Add(lblNombreTorneo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "CapturaTorneos";
            Text = "Torneo";
            ((System.ComponentModel.ISupportInitialize)dgvTorneo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericEdadMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericEdadMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreTorneo;
        private TextBox txtNombreTorneo;
        private Label lblEdadMin;
        private DataGridView dgvTorneo;
        private Label lblEdadMax;
        private Label lblGenero;
        private ComboBox cbGenero;
        private Label lblFechaIni;
        private NumericUpDown numericEdadMin;
        private NumericUpDown numericEdadMax;
        private MonthCalendar mcFechaIni;
        private Label lblFechaFin;
        private MonthCalendar mcFechaFin;
        private Button btnAgregar;
        private Button btnModif;
        private Button btnElim;
    }
}