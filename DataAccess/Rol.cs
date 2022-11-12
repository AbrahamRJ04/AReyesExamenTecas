using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();
}
