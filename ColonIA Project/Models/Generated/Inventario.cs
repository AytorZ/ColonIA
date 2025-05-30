using System;
using System.Collections.Generic;

namespace ColonIA.Models;

public partial class Inventario
{
    public int IdActivo { get; set; }

    public int IdCategoria { get; set; }

    public string NombreDelActivo { get; set; } = null!;

    public int CantidadActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual CategoriaInventario IdCategoriaNavigation { get; set; } = null!;
}
