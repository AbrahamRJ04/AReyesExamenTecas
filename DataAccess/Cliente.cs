using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string? NumeroCliente { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string Curp { get; set; } = null!;

    public string? Imagen { get; set; }

    public int? IdRol { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; } = new List<Cuentum>();

    public virtual Rol? IdRolNavigation { get; set; }
    /*PROPIEDADES AGREGADAS POR APARTE*/

    public string TipoRol { get; set; }
}
