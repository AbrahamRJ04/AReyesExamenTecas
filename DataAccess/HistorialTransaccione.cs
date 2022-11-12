using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class HistorialTransaccione
{
    public int IdTransaccion { get; set; }

    public int IdTipoTransaccion { get; set; }

    public string? Transaccion { get; set; }

    public string? Detalle { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public int IdNumeroCuenta { get; set; }

    public string NombreDeCuenta { get; set; } = null!;

    public decimal Saldo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string? NumeroCliente { get; set; }

    public int? IdRol { get; set; }

    public string? TipoRol { get; set; }

    public decimal MontoTransaccion { get; set; }
}
