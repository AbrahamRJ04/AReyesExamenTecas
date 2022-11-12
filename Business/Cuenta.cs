using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Cuenta
    {
        public int IdNumeroCuneta { get; set; }
        public string Nombre { get; set; }
        public Decimal Saldo { get; set; }
        public string FechaCreacion { get; set; }
        public int IdCliente { get; set; }
        public List<Object> Cuentas { get; set; }

        public static Business.ControlResult GetAll(int IdCliente)
        {
            ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Cuenta.FromSqlRaw($"CuentaGetByIdCliente {IdCliente}").ToList();
                    result.Objetos = new List<Object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Business.Cuenta clientecuenta = new Business.Cuenta();
                            //clientecuenta.Cuenta = new Cuenta();
                            
                            clientecuenta.IdNumeroCuneta = item.IdNumeroCuenta;
                            clientecuenta.Nombre = item.Nombre;
                            clientecuenta.Saldo = item.Saldo;
                            clientecuenta.FechaCreacion = item.FechaCreacion.ToString("dd-MM-yyyy");
                            clientecuenta.IdCliente = (int)item.IdCliente;

                            result.Objetos.Add(clientecuenta);
                        }
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.MensajeError = ex.Message;
                result.ProcesoCorrecto = false;
            }
            return result;
        }
    }
}
