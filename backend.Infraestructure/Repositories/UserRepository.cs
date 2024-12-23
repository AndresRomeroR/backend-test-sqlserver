using Dapper;
using backend.CORE.DTOs.Models;
using Microsoft.Data.SqlClient;
using backend.CORE.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using backend.CORE.DTOs.Request;

namespace backend.Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<Users>> ObtenerUsuariosAsync()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var usuarios = await connection.QueryAsync<Users>("SELECT * FROM Users");
                return usuarios.ToList();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}"); 
            return new List<Users>();
        }
    }

    public async Task<bool> CrearUsuarioAsync(UserRequest user)
    {
        try
        {
            // Validar que el correo no exista
            string correoDB = "";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"SELECT CorreoElectronico FROM Users WHERE CorreoElectronico = @Correo;";
                correoDB = await connection.QueryFirstOrDefaultAsync<string>(query, new { Correo = user.CorreoElectronico });
                await connection.CloseAsync();
            }

            if (!string.Equals(correoDB, user.CorreoElectronico, StringComparison.OrdinalIgnoreCase))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"INSERT INTO Users (
                                Nombre, 
                                Apellido, 
                                CorreoElectronico, 
                                Contraseña, 
                                FechaRegistro, 
                                Activo)
                          VALUES (@Nombre, @Apellido, @CorreoElectronico, @Contraseña, GETDATE(), @Activo)";

                    var parameters = new
                    {
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        CorreoElectronico = user.CorreoElectronico,
                        Contraseña = user.Contraseña,
                        Activo = user.Activo
                    };

                    await connection.ExecuteAsync(query, parameters);

                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task CambiarEstadoUsuarioAsync(string user)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"UPDATE Users 
                SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END 
                WHERE CorreoElectronico = @Correo";
                await connection.ExecuteAsync(query, new { Correo = user });
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task<bool> ActualizarUsuarioAsync(UserRequest user)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                UPDATE Users
                SET 
                    Nombre = @Nombre,
                    Apellido = @Apellido,
                    Contraseña = @Contraseña,
                    Activo = @Activo
                WHERE CorreoElectronico = @CorreoElectronico";

                var parameters = new
                {
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    CorreoElectronico = user.CorreoElectronico,
                    Contraseña = user.Contraseña,
                    Activo = user.Activo
                };

                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}
