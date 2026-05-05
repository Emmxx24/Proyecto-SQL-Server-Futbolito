namespace ProyectoBD
{
    partial class CapturaResultado
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
            lblPartidoDetalle = new Label();
            txtPartidoDetalle = new TextBox();
            lblGolesLocal = new Label();
            numericGolesLocal = new NumericUpDown();
            lblGolesVisitante = new Label();
            numericGolesVisitante = new NumericUpDown();
            lblHoraFin = new Label();
            dtpHoraTermino = new DateTimePicker();
            dgvResultados = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnRegistraGoles = new Button();
            btnRegistraTarjetas = new Button();
            ((System.ComponentModel.ISupportInitialize)numericGolesLocal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericGolesVisitante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // lblPartidoDetalle
            // 
            lblPartidoDetalle.AutoSize = true;
            lblPartidoDetalle.Location = new Point(14, 12);
            lblPartidoDetalle.Name = "lblPartidoDetalle";
            lblPartidoDetalle.Size = new Size(59, 20);
            lblPartidoDetalle.TabIndex = 0;
            lblPartidoDetalle.Text = "Partido:";
            // 
            // txtPartidoDetalle
            // 
            txtPartidoDetalle.Location = new Point(14, 36);
            txtPartidoDetalle.Margin = new Padding(3, 4, 3, 4);
            txtPartidoDetalle.Name = "txtPartidoDetalle";
            txtPartidoDetalle.ReadOnly = true;
            txtPartidoDetalle.Size = new Size(507, 27);
            txtPartidoDetalle.TabIndex = 1;
            // 
            // lblGolesLocal
            // 
            lblGolesLocal.AutoSize = true;
            lblGolesLocal.Location = new Point(14, 87);
            lblGolesLocal.Name = "lblGolesLocal";
            lblGolesLocal.Size = new Size(136, 20);
            lblGolesLocal.TabIndex = 2;
            lblGolesLocal.Text = "Goles equipo local:";
            // 
            // numericGolesLocal
            // 
            numericGolesLocal.Location = new Point(14, 111);
            numericGolesLocal.Margin = new Padding(3, 4, 3, 4);
            numericGolesLocal.Name = "numericGolesLocal";
            numericGolesLocal.Size = new Size(137, 27);
            numericGolesLocal.TabIndex = 3;
            // 
            // lblGolesVisitante
            // 
            lblGolesVisitante.AutoSize = true;
            lblGolesVisitante.Location = new Point(174, 87);
            lblGolesVisitante.Name = "lblGolesVisitante";
            lblGolesVisitante.Size = new Size(159, 20);
            lblGolesVisitante.TabIndex = 4;
            lblGolesVisitante.Text = "Goles equipo visitante:";
            // 
            // numericGolesVisitante
            // 
            numericGolesVisitante.Location = new Point(174, 111);
            numericGolesVisitante.Margin = new Padding(3, 4, 3, 4);
            numericGolesVisitante.Name = "numericGolesVisitante";
            numericGolesVisitante.Size = new Size(137, 27);
            numericGolesVisitante.TabIndex = 5;
            // 
            // lblHoraFin
            // 
            lblHoraFin.AutoSize = true;
            lblHoraFin.Location = new Point(14, 157);
            lblHoraFin.Name = "lblHoraFin";
            lblHoraFin.Size = new Size(122, 20);
            lblHoraFin.TabIndex = 6;
            lblHoraFin.Text = "Hora de término:";
            // 
            // dtpHoraTermino
            // 
            dtpHoraTermino.Format = DateTimePickerFormat.Time;
            dtpHoraTermino.Location = new Point(14, 181);
            dtpHoraTermino.Margin = new Padding(3, 4, 3, 4);
            dtpHoraTermino.Name = "dtpHoraTermino";
            dtpHoraTermino.ShowUpDown = true;
            dtpHoraTermino.Size = new Size(137, 27);
            dtpHoraTermino.TabIndex = 7;
            // 
            // dgvResultados
            // 
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.AllowUserToDeleteRows = false;
            dgvResultados.AllowUserToResizeColumns = false;
            dgvResultados.AllowUserToResizeRows = false;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvResultados.DefaultCellStyle = dataGridViewCellStyle1;
            dgvResultados.Location = new Point(14, 261);
            dgvResultados.Margin = new Padding(3, 4, 3, 4);
            dgvResultados.MultiSelect = false;
            dgvResultados.Name = "dgvResultados";
            dgvResultados.ReadOnly = true;
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.Size = new Size(1144, 375);
            dgvResultados.TabIndex = 8;
            dgvResultados.CellClick += dgvResultados_CellClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(749, 36);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(86, 31);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = SystemColors.ActiveCaption;
            btnModificar.Location = new Point(749, 125);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 31);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Location = new Point(749, 207);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnRegistraGoles
            // 
            btnRegistraGoles.BackColor = Color.SeaGreen;
            btnRegistraGoles.Enabled = false;
            btnRegistraGoles.ForeColor = SystemColors.ButtonHighlight;
            btnRegistraGoles.Location = new Point(14, 644);
            btnRegistraGoles.Margin = new Padding(3, 4, 3, 4);
            btnRegistraGoles.Name = "btnRegistraGoles";
            btnRegistraGoles.Size = new Size(153, 31);
            btnRegistraGoles.TabIndex = 12;
            btnRegistraGoles.Text = "Registrar goles";
            btnRegistraGoles.UseVisualStyleBackColor = false;
            btnRegistraGoles.Visible = false;
            btnRegistraGoles.Click += btnRegistraGoles_Click;
            // 
            // btnRegistraTarjetas
            // 
            btnRegistraTarjetas.BackColor = Color.FromArgb(255, 128, 128);
            btnRegistraTarjetas.Enabled = false;
            btnRegistraTarjetas.Location = new Point(174, 644);
            btnRegistraTarjetas.Margin = new Padding(3, 4, 3, 4);
            btnRegistraTarjetas.Name = "btnRegistraTarjetas";
            btnRegistraTarjetas.Size = new Size(177, 31);
            btnRegistraTarjetas.TabIndex = 13;
            btnRegistraTarjetas.Text = "Registrar tarjetas";
            btnRegistraTarjetas.UseVisualStyleBackColor = false;
            btnRegistraTarjetas.Visible = false;
            btnRegistraTarjetas.Click += btnRegistraTarjetas_Click;
            // 
            // CapturaResultado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1170, 688);
            Controls.Add(btnRegistraTarjetas);
            Controls.Add(btnRegistraGoles);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvResultados);
            Controls.Add(dtpHoraTermino);
            Controls.Add(lblHoraFin);
            Controls.Add(numericGolesVisitante);
            Controls.Add(lblGolesVisitante);
            Controls.Add(numericGolesLocal);
            Controls.Add(lblGolesLocal);
            Controls.Add(txtPartidoDetalle);
            Controls.Add(lblPartidoDetalle);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CapturaResultado";
            Text = "CapturaResultado";
            ((System.ComponentModel.ISupportInitialize)numericGolesLocal).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericGolesVisitante).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPartidoDetalle;
        private TextBox txtPartidoDetalle;
        private Label lblGolesLocal;
        private NumericUpDown numericGolesLocal;
        private Label lblGolesVisitante;
        private NumericUpDown numericGolesVisitante;
        private Label lblHoraFin;
        private DateTimePicker dtpHoraTermino;
        private DataGridView dgvResultados;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnRegistraGoles;
        private Button btnRegistraTarjetas;
    }
}