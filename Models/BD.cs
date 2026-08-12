namespace TP5.Models;

using Microsoft.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=localhost;Database=BaseRegistro;Integrated Security=True;TrustServerCertificate=true;";

    // Registers a new user. Returns true if insert affected rows.
    public static bool RegistrarUsuario(Usuario usuario)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
        const string sql = @"INSERT INTO Usuario (NombreUsuario, Contraseña, Nombre, Apellido, TipoUsuario)
                             VALUES (@NombreUsuario, @Contrasena, @Nombre, @Apellido, @TipoUsuario)";
        var affected = connection.Execute(sql, new
        {
            usuario.NombreUsuario,
            Contrasena = usuario.Contrasena,
            usuario.Nombre,
            usuario.Apellido,
            usuario.TipoUsuario
        });
        return affected > 0;
    }
    }

    // Returns the user if credentials match, otherwise null.
    public static Usuario? IniciarSesion(string nombreUsuario, string contrasena)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
        const string sql = "SELECT NombreUsuario AS NombreUsuario, Contraseña AS Contrasena, Nombre, Apellido, TipoUsuario FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contrasena";
        return connection.QuerySingleOrDefault<Usuario>(sql, new { NombreUsuario = nombreUsuario, Contrasena = contrasena });
        }
    }

    // Deletes a user by username (if that's intended). Returns true if row deleted.
    public static bool CerrarSesionEliminarUsuario(string nombreUsuario)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        const string sql = "DELETE FROM Usuario WHERE NombreUsuario = @NombreUsuario";
        var affected = connection.Execute(sql, new { NombreUsuario = nombreUsuario });
        return affected > 0;
    }
}
