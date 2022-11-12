using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Transaccion
    {

        /*PROPIEDADES*/
        public int IdTransaccion { get; set; }
        public string Detalle { get; set; }
        public string FechaTransaccion { get; set; }
        public decimal MontoTransaccion { get; set; }
        public Business.TipoTransaccion TipoTransaccion { get; set; }

        /*METODOS*/
        public static Business.ControlResult Depositar(Cliente cliente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AccionTransaccion  {cliente.Cuenta.Transaccion.TipoTransaccion.IdTipoTransaccion},'{cliente.Cuenta.Transaccion.Detalle}',{cliente.Cuenta.IdNumeroCuenta},{cliente.Cuenta.Transaccion.MontoTransaccion},{cliente.Cuenta.Saldo}");
                    if (query > 0)
                    {
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
                result.ProcesoCorrecto = false;
                result.MensajeError = ex.Message;
            }
            return result;
        }
    }
}
