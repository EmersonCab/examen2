using Google.Cloud.Firestore;
using Examen_Parcial.Models;

namespace Examen_Parcial.Services;

public class ReservaService
{
    private readonly CollectionReference _reservas;

    public ReservaService(FirestoreDb db)
    {
        _reservas = db.Collection("reservas");
    }

    public async Task<string> CrearReservaAsync(Reserva reserva)
    {
        // Obtener la prioridad (posición en la cola)
        var reservasLibro = await _reservas
            .WhereEqualTo("LibroId", reserva.LibroId)
            .GetSnapshotAsync();

        int prioridad = reservasLibro.Documents.Count + 1;

        // Asignar valores automáticos
        reserva.FechaReserva = DateTime.UtcNow;
        reserva.Estado = "pendiente";
        reserva.FechaNotificacion = null;
        reserva.FechaExpiracion = null;
        reserva.Prioridad = prioridad;

        DocumentReference doc = await _reservas.AddAsync(reserva);
        return doc.Id;
    }
}