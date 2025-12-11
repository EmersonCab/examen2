using Google.Cloud.Firestore;
using BCrypt.Net;
using Examen_Parcial.Models;

namespace Examen_Parcial.Services;

public class UsuarioService
{
    private readonly CollectionReference _users;

    public UsuarioService(FirestoreDb db)
    {
        _users = db.Collection("usuarios");
    }

    public async Task<string> CrearUsuarioAsync(Users usuario)
    {
        var correoQuery = await _users.WhereEqualTo("Correo", usuario.Correo).GetSnapshotAsync();
        if (correoQuery.Documents.Count > 0)
            throw new Exception("El correo ya está registrado.");

        var identidadQuery = await _users.WhereEqualTo("NumeroIdentidad", usuario.NumeroIdentidad).GetSnapshotAsync();
        if (identidadQuery.Documents.Count > 0)
            throw new Exception("El número de identidad ya está registrado.");

        usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
        usuario.FechaRegistro = DateTime.UtcNow;
        usuario.Activo = true;
        usuario.Multas = 0;

        DocumentReference doc = await _users.AddAsync(usuario);
        return doc.Id;
    }
}