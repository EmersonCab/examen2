using Google.Cloud.Firestore;

namespace Examen_Parcial.Models;

[FirestoreData]
public class Users
{
    [FirestoreProperty]
    public string Nombre { get; set; }

    [FirestoreProperty]
    public string Apellido { get; set; }

    [FirestoreProperty]
    public string Correo { get; set; }

    [FirestoreProperty]
    public string Contrasena { get; set; }

    [FirestoreProperty]
    public int Edad { get; set; }

    [FirestoreProperty]
    public string NumeroIdentidad { get; set; }

    [FirestoreProperty]
    public string Telefono { get; set; }

    [FirestoreProperty]
    public string Rol { get; set; }

    [FirestoreProperty]
    public bool Activo { get; set; }

    [FirestoreProperty]
    public DateTime FechaRegistro { get; set; }

    [FirestoreProperty]
    public double Multas { get; set; }

    public Users() {}
}