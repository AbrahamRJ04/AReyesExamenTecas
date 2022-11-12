using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class TipoTransaccion
{
    public int IdTipoTransaccion { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; } = new List<Transaccion>();
}
