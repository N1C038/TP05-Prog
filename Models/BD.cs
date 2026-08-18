namespace TP5.Models;

using Microsoft.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=localhost;Database=BaseRegistro;Integrated Security=True;TrustServerCertificate=true;";

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

    public static Usuario IniciarSesion(string nombreUsuario, string contrasena)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        {
        const string sql = "SELECT NombreUsuario, Contraseña, Usuario.Nombre, Apellido, TipoUsuario, IDEspecialidad FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña";
        return connection.QuerySingleOrDefault<Usuario>(sql, new { NombreUsuario = nombreUsuario, Contraseña = contrasena });
        }
    }

    public static bool CerrarSesionEliminarUsuario(string nombreUsuario)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        const string sql = "DELETE FROM Usuario WHERE NombreUsuario = @NombreUsuario";
        var affected = connection.Execute(sql, new { NombreUsuario = nombreUsuario });
        return affected > 0;
    }

    public static Especialidad ObtenerEspecialidad(int idEspecialidad)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        const string sql = "SELECT Especialidad.ID,  Especialidad.Nombre, Especialidad.Descripcion  FROM Especialidad WHERE Especialidad.ID = @ID";
        return connection.QuerySingleOrDefault<Especialidad>(sql, new { ID = idEspecialidad });
    }

    public static bool ExisteUsuario(string nombreUsuario)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        const string sql = "SELECT NombreUsuario, Contraseña, Usuario.Nombre, Apellido, TipoUsuario, IDEspecialidad FROM Usuario WHERE NombreUsuario = @NombreUsuario";
        var usuario = connection.QuerySingleOrDefault<Usuario>(sql, new { NombreUsuario = nombreUsuario });
        return usuario != null;
    }

    public static Usuario ObtenerUsuario(string nombreUsuario)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        const string sql = "SELECT NombreUsuario, Contraseña, Usuario.Nombre, Apellido, TipoUsuario, IDEspecialidad FROM Usuario WHERE NombreUsuario = @NombreUsuario";
        return connection.QuerySingleOrDefault<Usuario>(sql, new { NombreUsuario = nombreUsuario });
    }
}
