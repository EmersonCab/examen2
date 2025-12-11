using Google.Cloud.Firestore;
using Examen_Parcial.Models;

namespace Examen_Parcial.Services;

public class LibrosService
{
    private readonly CollectionReference _libros;

    public LibrosService(FirestoreDb db)
    {
        _libros = db.Collection("libros");
    }

    public async Task<string> CrearLibroAsync(Libros libro)
    {
        // Validar ISBN único
        var isbnQuery = await _libros.WhereEqualTo("Isbn", libro.Isbn).GetSnapshotAsync();
        if (isbnQuery.Documents.Count > 0)
            throw new Exception("El ISBN ya está registrado.");

        // Campos automáticos
        libro.FechaIngreso = DateTime.UtcNow;

        // Guardar en Firestore
        DocumentReference doc = await _libros.AddAsync(libro);
        return doc.Id;
    }
}