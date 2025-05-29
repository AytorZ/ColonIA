using System;
using System.Collections.Generic;

namespace ColonAI.Models;

public partial class RolUsuario
{
    public int IdRole { get; set; }

    public string NombreRole { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
