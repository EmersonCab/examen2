namespace Examen_Parcial.Models;

public class Prestamo
{
    public string UsuarioId { get; set; }
    public string LibroId { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; } // puede ser null
    public int DiasRetraso { get; set; }
    public double MultaGenerada { get; set; }
    public string Estado { get; set; } // activo, devuelto, vencido
    public int Renovaciones { get; set; }
}