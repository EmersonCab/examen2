using Google.Cloud.Firestore;
using Examen_Parcial.Models;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Examen_Parcial.Services;

public class AuthService
{
    private readonly CollectionReference _usuarios;
    private readonly IConfiguration _config;

    public AuthService(FirestoreDb db, IConfiguration config)
    {
        _usuarios = db.Collection("usuarios");
        _config = config;
    }

    // ✅ REGISTRO
    public async Task<string> RegistrarAsync(Users usuario)
    {
        // Validar correo único
        var correoQuery = await _usuarios.WhereEqualTo("Correo", usuario.Correo).GetSnapshotAsync();
        if (correoQuery.Documents.Count > 0)
            throw new Exception("El correo ya está registrado.");

        // Asignar valores automáticos
        usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
        usuario.Rol = "usuario";
        usuario.Multas = 0;
        usuario.Activo = true;
        usuario.FechaRegistro = DateTime.UtcNow;

        DocumentReference doc = await _usuarios.AddAsync(usuario);
        return doc.Id;
    }

    // ✅ LOGIN
    public async Task<string> LoginAsync(string correo, string contrasena)
    {
        var query = await _usuarios.WhereEqualTo("Correo", correo).GetSnapshotAsync();
        if (query.Documents.Count == 0)
            throw new Exception("Credenciales inválidas.");

        var userDoc = query.Documents[0];
        var usuario = userDoc.ConvertTo<Users>();

        if (!usuario.Activo)
            throw new Exception("La cuenta está inactiva.");

        if (!BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
            throw new Exception("Credenciales inválidas.");

        // ✅ Generar JWT
        return GenerarToken(userDoc.Id, usuario);
    }

    private string GenerarToken(string userId, Users usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("userId", userId),
            new Claim("correo", usuario.Correo),
            new Claim("rol", usuario.Rol)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
