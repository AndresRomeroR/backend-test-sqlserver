using backend.CORE.DTOs.Models;
using backend.CORE.DTOs.Request;
using backend.CORE.DTOs.Responses;
using backend.CORE.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace backend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> ConsultarUsuarios()
    {
        try
        {
            List<Users> usuariuos = await _userService.ObtenerUsuariosAsync();

            return Ok(new BaseResponse
            {
                Message = "Consulta Realizada con exito",
                Exception = "0",
                Data = usuariuos
            });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new BaseResponse
            {
                Message = "Consulta con errores",
                Exception = ex.Message,
                Data = 0
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] UserRequest user)
    {
        if (string.IsNullOrEmpty(user.Nombre) || !Regex.IsMatch(user.Nombre, @"^[a-zA-Z\s]+$"))
            return StatusCode(400, new BaseResponse
            {
                Message = "Dato nombre con errores",
                Exception = "",
                Data = 0
            });

        try
        {
            string msgResponse = await _userService.CrearUsuarioAsync(user);
            return StatusCode(200, new BaseResponse
            {
                Message = msgResponse,
                Exception = "",
                Data = 0
            });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new BaseResponse
            {
                Message = "Consulta con errores",
                Exception = ex.Message,
                Data = 0
            });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> CambiarEstado(string data)
    {
        try
        {
            await _userService.CambiarEstadoUsuarioAsync(data);

            return StatusCode(200, new BaseResponse
            {
                Message = "Consulta realizada con exito",
                Exception = "",
                Data = 0
            });
        }
        catch(Exception ex)
        {
            return StatusCode(400, new BaseResponse
            {
                Message = "Consulta con errores",
                Exception = ex.Message,
                Data = 0
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarUsuario([FromBody] UserRequest user)
    {
        if (string.IsNullOrEmpty(user.Nombre) || !Regex.IsMatch(user.Nombre, @"^[a-zA-Z\s]+$"))
            return StatusCode(400, new BaseResponse
            {
                Message = "Dato nombre con errores",
                Exception = "",
                Data = 0
            });

        try
        {
            string msgResponse = await _userService.ActualizarUsuarioAsync(user);
            return StatusCode(200, new BaseResponse
            {
                Message = msgResponse,
                Exception = "",
                Data = 0
            });
        }
        catch (Exception ex)
        {
            return StatusCode(400, new BaseResponse
            {
                Message = "Consulta con errores",
                Exception = ex.Message,
                Data = 0
            });
        }
    }
}
