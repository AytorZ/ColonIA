using System;
using System.Collections.Generic;

namespace ColonIA.Models;

public partial class CategoriaInventario
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
