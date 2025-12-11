namespace Examen_Parcial.Models;

public class Reserva
{
    public string UsuarioId { get; set; }
    public string LibroId { get; set; }
    public DateTime FechaReserva { get; set; }
    public string Estado { get; set; } // pendiente, notificada, completada, cancelada
    public DateTime? FechaNotificacion { get; set; } // puede ser null
    public DateTime? FechaExpiracion { get; set; } // 48 horas después de notificación
    public int Prioridad { get; set; } // posición en la cola
}