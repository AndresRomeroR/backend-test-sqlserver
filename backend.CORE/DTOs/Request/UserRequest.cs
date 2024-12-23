using System.ComponentModel.DataAnnotations;

namespace backend.CORE.DTOs.Request;

public class UserRequest
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string CorreoElectronico { get; set; }
    public string Contraseña { get; set; }
    public bool Activo { get; set; }
}
