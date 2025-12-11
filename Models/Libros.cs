namespace Examen_Parcial.Models;

public class Libros
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Isbn { get; set; } // único
    public string Categoria { get; set; }
    public string Editorial { get; set; }
    public int AñoPublicacion { get; set; }
    public int CopiasDisponibles { get; set; }
    public int CopiasTotal { get; set; }
    public string Ubicacion { get; set; }
    public string Estado { get; set; } // disponible, agotado, en mantenimiento
    public string Descripcion { get; set; }
    public DateTime FechaIngreso { get; set; }
}