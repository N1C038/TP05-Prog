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
        const string sql = @"INSERT INTO Usuario (NombreUsuario, Contraseña, Nombre, Apellido, TipoUsuario, IDEspecialidad)
                             VALUES (@NombreUsuario, @Contraseña, @Nombre, @Apellido, @TipoUsuario, @IDEspecialidad)";
        var affected = connection.Execute(sql, new
        {
            usuario.NombreUsuario,
            usuario.Contraseña,
            usuario.Nombre,
            usuario.Apellido,
            usuario.TipoUsuario,
            usuario.IDEspecialidad
        });
        return affected > 0;
    }
    }

    // Returns the user if credentials match, otherwise null.
    public static Usuario? IniciarSesion(string nombreUsuario, string contrasena)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
        const string sql = "SELECT NombreUsuario AS NombreUsuario, Contraseña AS Contraseña, Nombre, Apellido, TipoUsuario, IDEspecialidad FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña";
        return connection.QuerySingleOrDefault<Usuario>(sql, new { NombreUsuario = nombreUsuario, Contraseña = contrasena });
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
