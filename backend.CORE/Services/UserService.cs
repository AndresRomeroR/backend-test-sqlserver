using backend.CORE.DTOs.Models;
using backend.CORE.DTOs.Request;
using backend.CORE.Interfaces.Repository;
using backend.CORE.Interfaces.Service;

namespace backend.CORE.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Users>> ObtenerUsuariosAsync()
    {
        return await _userRepository.ObtenerUsuariosAsync();
    }

    public async Task<string> CrearUsuarioAsync(UserRequest user)
    {
        bool userCreated = await _userRepository.CrearUsuarioAsync(user);
        if (userCreated)
        {
            return "Usuario Creado con éxito";
        }
        else
        {
            return "Usuario existente";
        }
    }

    public async Task CambiarEstadoUsuarioAsync(string user)
    {
        await _userRepository.CambiarEstadoUsuarioAsync(user);
    }

    public async Task<string> ActualizarUsuarioAsync(UserRequest user)
    {
        bool userUpdated = await _userRepository.ActualizarUsuarioAsync(user);
        if (userUpdated)
        {
            return "Usuario Actualizado con éxito";
        }
        else
        {
            return "Operación errada";
        }
    }
}
