using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoBD
{
    public static class ManejadorErroresBD
    {
        public static void MostrarErrorAmigable(Exception ex)
        {
            // Verificamos si el error viene específicamente de SQL Server
            if (ex is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 2627: // Violación de Primary Key (ID duplicado)
                    case 2601: // Violación de índice único (Ej. Nombre de torneo repetido)
                        MessageBox.Show("Ya existe un registro con esos datos. Por favor, verifica que el nombre o identificador no esté repetido.",
                                        "Dato Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547: // Violación de Foreign Key (Llave foránea)
                        // Esto pasa si intentas borrar un torneo que ya tiene equipos, jugadores o partidos registrados.
                        MessageBox.Show("No se puede eliminar o modificar este registro porque está siendo usado en otra parte del sistema.",
                                        "Registro en Uso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;

                    case 515: // Intento de insertar NULL en un campo obligatorio
                        MessageBox.Show("Faltan datos obligatorios por llenar en la base de datos.",
                                        "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:  // Error de conexión de red
                    case 17:  // El servidor SQL no existe o acceso denegado
                        MessageBox.Show("No se pudo conectar a la base de datos. Verifica tu conexión o que el servidor esté encendido.",
                                        "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        // Si es un error de SQL que no tenemos mapeado, mostramos el mensaje original pero controlado
                        MessageBox.Show("Ocurrió un error en la base de datos: " + sqlEx.Message,
                                        "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                                "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}