using System.ComponentModel.DataAnnotations;

namespace backend.CORE.DTOs.Models;

public  class Users
{
    [Key]
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string CorreoElectronico { get; set; }
    public string Contraseña { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }
}
