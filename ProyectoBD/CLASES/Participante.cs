using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBD.CLASES
{

    //Cambiar el tipo a public (internal)
    public class Participante
    {
        long Id { get; set; }
        string NombreParticipante { get; set; }
        string Genero {get; set; }
        string Telefono {get; set; }
        string Correo { get; set; }
        DateTime FechaNacimiento {  get; set; }
        int Edad {  get; set; }
    }
}
