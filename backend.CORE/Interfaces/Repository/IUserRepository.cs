using backend.CORE.DTOs.Models;
using backend.CORE.DTOs.Request;

namespace backend.CORE.Interfaces.Repository;

public interface IUserRepository
{
    Task<List<Users>> ObtenerUsuariosAsync();
    Task<bool> CrearUsuarioAsync(UserRequest user);
    Task CambiarEstadoUsuarioAsync(string user);
    Task<bool> ActualizarUsuarioAsync(UserRequest user);
}
