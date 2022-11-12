using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Cuentum
{
    public int IdNumeroCuenta { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Saldo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; } = new List<Transaccion>();
}
