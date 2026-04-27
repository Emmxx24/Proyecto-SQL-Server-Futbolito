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
            dgvJornada = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvJornada).BeginInit();
            SuspendLayout();
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
            dgvJornada.Location = new Point(12, 13);
            dgvJornada.Margin = new Padding(3, 4, 3, 4);
            dgvJornada.Name = "dgvJornada";
            dgvJornada.ReadOnly = true;
            dgvJornada.RowHeadersWidth = 51;
            dgvJornada.Size = new Size(854, 574);
            dgvJornada.TabIndex = 7;
            // 
            // Jornada
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 600);
            Controls.Add(dgvJornada);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Jornada";
            Text = "Jornada";
            ((System.ComponentModel.ISupportInitialize)dgvJornada).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvJornada;
    }
}