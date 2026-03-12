namespace ProyectoBD
{
    partial class MenuPrincipal
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
            menuStrip1 = new MenuStrip();
            personaToolStripMenuItem = new ToolStripMenuItem();
            participanteToolStripMenuItem = new ToolStripMenuItem();
            jugadorToolStripMenuItem = new ToolStripMenuItem();
            arbitroToolStripMenuItem = new ToolStripMenuItem();
            juegoToolStripMenuItem = new ToolStripMenuItem();
            torneoToolStripMenuItem = new ToolStripMenuItem();
            jornadaToolStripMenuItem = new ToolStripMenuItem();
            lugarToolStripMenuItem = new ToolStripMenuItem();
            eventoToolStripMenuItem = new ToolStripMenuItem();
            partidoToolStripMenuItem = new ToolStripMenuItem();
            resultadoPartidoToolStripMenuItem = new ToolStripMenuItem();
            golToolStripMenuItem = new ToolStripMenuItem();
            tarjetaToolStripMenuItem = new ToolStripMenuItem();
            clubToolStripMenuItem = new ToolStripMenuItem();
            equipoToolStripMenuItem = new ToolStripMenuItem();
            detalleTorneoToolStripMenuItem = new ToolStripMenuItem();
            detalleEquipoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { personaToolStripMenuItem, juegoToolStripMenuItem, eventoToolStripMenuItem, clubToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // personaToolStripMenuItem
            // 
            personaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { participanteToolStripMenuItem, jugadorToolStripMenuItem, arbitroToolStripMenuItem });
            personaToolStripMenuItem.Name = "personaToolStripMenuItem";
            personaToolStripMenuItem.Size = new Size(74, 24);
            personaToolStripMenuItem.Text = "Persona";
            // 
            // participanteToolStripMenuItem
            // 
            participanteToolStripMenuItem.Name = "participanteToolStripMenuItem";
            participanteToolStripMenuItem.Size = new Size(170, 26);
            participanteToolStripMenuItem.Text = "Participante";
            participanteToolStripMenuItem.Click += participanteToolStripMenuItem_Click;
            // 
            // jugadorToolStripMenuItem
            // 
            jugadorToolStripMenuItem.Name = "jugadorToolStripMenuItem";
            jugadorToolStripMenuItem.Size = new Size(170, 26);
            jugadorToolStripMenuItem.Text = "Jugador";
            jugadorToolStripMenuItem.Click += jugadorToolStripMenuItem_Click;
            // 
            // arbitroToolStripMenuItem
            // 
            arbitroToolStripMenuItem.Name = "arbitroToolStripMenuItem";
            arbitroToolStripMenuItem.Size = new Size(170, 26);
            arbitroToolStripMenuItem.Text = "Arbitro";
            arbitroToolStripMenuItem.Click += arbitroToolStripMenuItem_Click;
            // 
            // juegoToolStripMenuItem
            // 
            juegoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { torneoToolStripMenuItem, jornadaToolStripMenuItem, lugarToolStripMenuItem, detalleTorneoToolStripMenuItem });
            juegoToolStripMenuItem.Name = "juegoToolStripMenuItem";
            juegoToolStripMenuItem.Size = new Size(62, 24);
            juegoToolStripMenuItem.Text = "Juego";
            // 
            // torneoToolStripMenuItem
            // 
            torneoToolStripMenuItem.Name = "torneoToolStripMenuItem";
            torneoToolStripMenuItem.Size = new Size(224, 26);
            torneoToolStripMenuItem.Text = "Torneo";
            torneoToolStripMenuItem.Click += torneoToolStripMenuItem_Click;
            // 
            // jornadaToolStripMenuItem
            // 
            jornadaToolStripMenuItem.Name = "jornadaToolStripMenuItem";
            jornadaToolStripMenuItem.Size = new Size(224, 26);
            jornadaToolStripMenuItem.Text = "Jornada";
            jornadaToolStripMenuItem.Click += jornadaToolStripMenuItem_Click;
            // 
            // lugarToolStripMenuItem
            // 
            lugarToolStripMenuItem.Name = "lugarToolStripMenuItem";
            lugarToolStripMenuItem.Size = new Size(224, 26);
            lugarToolStripMenuItem.Text = "Lugar";
            lugarToolStripMenuItem.Click += lugarToolStripMenuItem_Click;
            // 
            // eventoToolStripMenuItem
            // 
            eventoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { partidoToolStripMenuItem, resultadoPartidoToolStripMenuItem, golToolStripMenuItem, tarjetaToolStripMenuItem });
            eventoToolStripMenuItem.Name = "eventoToolStripMenuItem";
            eventoToolStripMenuItem.Size = new Size(68, 24);
            eventoToolStripMenuItem.Text = "Evento";
            // 
            // partidoToolStripMenuItem
            // 
            partidoToolStripMenuItem.Name = "partidoToolStripMenuItem";
            partidoToolStripMenuItem.Size = new Size(224, 26);
            partidoToolStripMenuItem.Text = "Partido";
            partidoToolStripMenuItem.Click += partidoToolStripMenuItem_Click;
            // 
            // resultadoPartidoToolStripMenuItem
            // 
            resultadoPartidoToolStripMenuItem.Name = "resultadoPartidoToolStripMenuItem";
            resultadoPartidoToolStripMenuItem.Size = new Size(224, 26);
            resultadoPartidoToolStripMenuItem.Text = "ResultadoPartido";
            resultadoPartidoToolStripMenuItem.Click += resultadoPartidoToolStripMenuItem_Click;
            // 
            // golToolStripMenuItem
            // 
            golToolStripMenuItem.Name = "golToolStripMenuItem";
            golToolStripMenuItem.Size = new Size(224, 26);
            golToolStripMenuItem.Text = "Gol";
            golToolStripMenuItem.Click += golToolStripMenuItem_Click;
            // 
            // tarjetaToolStripMenuItem
            // 
            tarjetaToolStripMenuItem.Name = "tarjetaToolStripMenuItem";
            tarjetaToolStripMenuItem.Size = new Size(224, 26);
            tarjetaToolStripMenuItem.Text = "Tarjeta";
            tarjetaToolStripMenuItem.Click += tarjetaToolStripMenuItem_Click;
            // 
            // clubToolStripMenuItem
            // 
            clubToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { equipoToolStripMenuItem, detalleEquipoToolStripMenuItem });
            clubToolStripMenuItem.Name = "clubToolStripMenuItem";
            clubToolStripMenuItem.Size = new Size(53, 24);
            clubToolStripMenuItem.Text = "Club";
            // 
            // equipoToolStripMenuItem
            // 
            equipoToolStripMenuItem.Name = "equipoToolStripMenuItem";
            equipoToolStripMenuItem.Size = new Size(224, 26);
            equipoToolStripMenuItem.Text = "Equipo";
            equipoToolStripMenuItem.Click += equipoToolStripMenuItem_Click;
            // 
            // detalleTorneoToolStripMenuItem
            // 
            detalleTorneoToolStripMenuItem.Name = "detalleTorneoToolStripMenuItem";
            detalleTorneoToolStripMenuItem.Size = new Size(224, 26);
            detalleTorneoToolStripMenuItem.Text = "DetalleTorneo";
            detalleTorneoToolStripMenuItem.Click += detalleTorneoToolStripMenuItem_Click;
            // 
            // detalleEquipoToolStripMenuItem
            // 
            detalleEquipoToolStripMenuItem.Name = "detalleEquipoToolStripMenuItem";
            detalleEquipoToolStripMenuItem.Size = new Size(224, 26);
            detalleEquipoToolStripMenuItem.Text = "DetalleEquipo";
            detalleEquipoToolStripMenuItem.Click += detalleEquipoToolStripMenuItem_Click;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MenuPrincipal";
            Text = "MenuPrincipal";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem personaToolStripMenuItem;
        private ToolStripMenuItem participanteToolStripMenuItem;
        private ToolStripMenuItem jugadorToolStripMenuItem;
        private ToolStripMenuItem juegoToolStripMenuItem;
        private ToolStripMenuItem eventoToolStripMenuItem;
        private ToolStripMenuItem clubToolStripMenuItem;
        private ToolStripMenuItem arbitroToolStripMenuItem;
        private ToolStripMenuItem torneoToolStripMenuItem;
        private ToolStripMenuItem jornadaToolStripMenuItem;
        private ToolStripMenuItem lugarToolStripMenuItem;
        private ToolStripMenuItem partidoToolStripMenuItem;
        private ToolStripMenuItem resultadoPartidoToolStripMenuItem;
        private ToolStripMenuItem golToolStripMenuItem;
        private ToolStripMenuItem tarjetaToolStripMenuItem;
        private ToolStripMenuItem equipoToolStripMenuItem;
        private ToolStripMenuItem detalleTorneoToolStripMenuItem;
        private ToolStripMenuItem detalleEquipoToolStripMenuItem;
    }
}