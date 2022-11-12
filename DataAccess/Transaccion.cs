﻿using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public int IdTipoTransaccion { get; set; }

    public string? Detalle { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public int? IdCuenta { get; set; }

    public decimal MontoTransaccion { get; set; }

    public virtual Cuentum? IdCuentaNavigation { get; set; }

    public virtual TipoTransaccion IdTipoTransaccionNavigation { get; set; } = null!;
}
