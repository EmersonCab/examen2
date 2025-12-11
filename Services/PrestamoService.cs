using Google.Cloud.Firestore;
using Examen_Parcial.Models;

namespace Examen_Parcial.Services;

public class PrestamoService
{
    private readonly CollectionReference _prestamos;

    public PrestamoService(FirestoreDb db)
    {
        _prestamos = db.Collection("prestamos");
    }

    public async Task<string> CrearPrestamoAsync(Prestamo prestamo)
    {
        // Asignar valores automáticos
        prestamo.FechaPrestamo = DateTime.UtcNow;
        prestamo.FechaDevolucionReal = null;
        prestamo.DiasRetraso = 0;
        prestamo.MultaGenerada = 0;
        prestamo.Estado = "activo";
        prestamo.Renovaciones = 0;

        DocumentReference doc = await _prestamos.AddAsync(prestamo);
        return doc.Id;
    }
}