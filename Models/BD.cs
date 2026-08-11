namespace TP5.Models;
public static class BD
{
private static string _connectionString = @"Server = localhost; DataBase = BaseRegistro; Integrated Security = True; TrustServerCertificate = true;";

public void RegistrarUsuarios()
    {
Usuario us = new Usuario();
 using(SqlConnection connect = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Usuario SET NombreUsuario, Contraseña, Nombre, Apellido, TipoUsuario = @NombreUsuario, @Contraseña, @Nombre, @Apellido, @TipoUsuario";
        //us = connect.Query<Usuario>(query);
        }
    }

public void IniciarSesión()
{
Usuario usu = new Usuario();
 using(SqlConnection connect = new SqlConnection(_connectionString))
 {
    string query = "SELECT NombreUsuario, Contraseña FROM Usuario WHERE NombreUsuario = @NombrUsuario AND Contraseña = @Contraseña";
 }
}


public void CerrarSesión()
{

Session.Remove(@"Usuario");
}
}