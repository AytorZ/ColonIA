using System;
using System.Collections.Generic;

namespace ColonIA.Models;

public partial class Usuario 
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public int IdRole { get; set; }

    public string? ResetCode { get; set; }

    public DateTime? ResetCodeExpiration { get; set; }

    public virtual RolUsuario IdRoleNavigation { get; set; } = null!;
}
