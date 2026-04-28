namespace ProyectoBD
{
    partial class DetalleEquipo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

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
            lblEquipo.Location = new Point(33, 15);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(59, 20);
            lblEquipo.TabIndex = 1;
            lblEquipo.Text = "Equipo:";
            // 
            // cbEquipo
            // 
            cbEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEquipo.FormattingEnabled = true;
            cbEquipo.Location = new Point(33, 38);
            cbEquipo.Name = "cbEquipo";
            cbEquipo.Size = new Size(327, 28);
            cbEquipo.TabIndex = 0;
            cbEquipo.SelectedIndexChanged += cbEquipo_SelectedIndexChanged;
            // 
            // lblJugador
            // 
            lblJugador.AutoSize = true;
            lblJugador.Location = new Point(33, 86);
            lblJugador.Name = "lblJugador";
            lblJugador.Size = new Size(65, 20);
            lblJugador.TabIndex = 3;
            lblJugador.Text = "Jugador:";
            // 
            // cbJugador
            // 
            cbJugador.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJugador.FormattingEnabled = true;
            cbJugador.Location = new Point(33, 109);
            cbJugador.Name = "cbJugador";
            cbJugador.Size = new Size(327, 28);
            cbJugador.TabIndex = 2;
            cbJugador.SelectedIndexChanged += cbJugador_SelectedIndexChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = SystemColors.ActiveCaption;
            btnAgregar.Location = new Point(33, 170);
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
            btnModificar.Location = new Point(153, 170);
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
            btnEliminar.Location = new Point(286, 170);
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
            dgvDetalleEquipo.Location = new Point(33, 225);
            dgvDetalleEquipo.Name = "dgvDetalleEquipo";
            dgvDetalleEquipo.RowHeadersWidth = 51;
            dgvDetalleEquipo.Size = new Size(720, 188);
            dgvDetalleEquipo.TabIndex = 7;
            dgvDetalleEquipo.CellClick += dgvDetalleEquipo_CellClick;
            // 
            // DetalleEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 430);
            Controls.Add(dgvDetalleEquipo);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(cbJugador);
            Controls.Add(lblJugador);
            Controls.Add(cbEquipo);
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