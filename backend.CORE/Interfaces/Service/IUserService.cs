using backend.CORE.DTOs.Models;
using backend.CORE.DTOs.Request;

namespace backend.CORE.Interfaces.Service;

public interface IUserService
{
    Task<List<Users>> ObtenerUsuariosAsync();
    Task<string> CrearUsuarioAsync(UserRequest user);
    Task CambiarEstadoUsuarioAsync(string user);
    Task<string> ActualizarUsuarioAsync(UserRequest user);
}
