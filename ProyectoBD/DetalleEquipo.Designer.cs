namespace ProyectoBD
{
    partial class DetalleEquipo
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
            lblEquipo = new Label();
            lblJugador = new Label();
            cbEquipo = new ComboBox();
            cbJugador = new ComboBox();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvDetalleEquipo = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleEquipo).BeginInit();
            SuspendLayout();
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = true;
            lblEquipo.Location = new Point(33, 43);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(59, 20);
            lblEquipo.TabIndex = 0;
            lblEquipo.Text = "Equipo:";
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(33, 129);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(65, 20);
            lblJugador.TabIndex = 1;
            lblJugador.Text = "Jugador:";
            // 
            // cbEquipo
            // 
            cbEquipo.FormattingEnabled = true;
            cbEquipo.Location = new Point(33, 66);
            cbEquipo.Name = "cbEquipo";
            cbEquipo.Size = new Size(214, 28);
            cbEquipo.TabIndex = 2;
            cbEquipo.SelectedIndexChanged += cbEquipo_SelectedIndexChanged;
            // 
            // cbJugador
            // 
            cbJugador.FormattingEnabled = true;
            cbJugador.Location = new Point(33, 152);
            cbJugador.Name = "cbJugador";
            cbJugador.Size = new Size(214, 28);
            cbJugador.TabIndex = 3;
            cbJugador.SelectedIndexChanged += cbJugador_SelectedIndexChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(33, 226);
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
            btnModificar.Location = new Point(153, 226);
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
            btnEliminar.Location = new Point(286, 226);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvDetalleEquipo
            // 
            dgvDetalleEquipo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleEquipo.Location = new Point(37, 281);
            dgvDetalleEquipo.Name = "dgvDetalleEquipo";
            dgvDetalleEquipo.RowHeadersWidth = 51;
            dgvDetalleEquipo.Size = new Size(720, 188);
            dgvDetalleEquipo.TabIndex = 7;
            // 
            // DetalleEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 476);
            Controls.Add(dgvDetalleEquipo);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(cbJugador);
            Controls.Add(cbEquipo);
            Controls.Add(lblJugador);
            Controls.Add(lblEquipo);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetalleEquipo";
            Text = "DetalleEquipo";
            ((System.ComponentModel.ISupportInitialize)dgvDetalleEquipo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEquipo;
        private Label lblJugador;
        private ComboBox cbEquipo;
        private ComboBox cbJugador;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvDetalleEquipo;
    }
}