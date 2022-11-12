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
        public int IdNumeroCuenta { get; set; }
        public string Nombre { get; set; }
        public Decimal Saldo { get; set; }
        public string FechaCreacion { get; set; }
        public int IdCliente { get; set; }
        public List<Object> Cuentas { get; set; }
        public Business.Transaccion Transaccion { get; set; }

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

                            clientecuenta.IdNumeroCuenta = item.IdNumeroCuenta;
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
        public static Business.ControlResult Add(Business.Cliente cliente)
        {
            Business.ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CuentaAdd '{cliente.Cuenta.Nombre}',{cliente.Cuenta.Saldo},{cliente.IdCliente}");
                    if (query > 0)
                    {
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "Ocurrio un error al crear la nueva cuenta de ahorro";
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
        public static Business.ControlResult Delete(int IdNumeroCuneta)
        {
            Business.ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CuentaDelete {IdNumeroCuneta}");
                    if (query > 0)
                    {
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "Ocurrio un error al intentar eliminar la cuenta";
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

        public static Business.ControlResult GetById(int IdNumeroCuenta)
        {
            ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Cuenta.FromSqlRaw($"CuentaGetById {IdNumeroCuenta}").AsEnumerable().FirstOrDefault();
                    result.Objetos = new List<Object>();

                    if (query != null)
                    {

                        Business.Cuenta clientecuenta = new Business.Cuenta();
                        //clientecuenta.Cuenta = new Cuenta();

                        clientecuenta.IdNumeroCuenta = query.IdNumeroCuenta;
                        clientecuenta.Nombre = query.Nombre;
                        clientecuenta.Saldo = query.Saldo;
                        clientecuenta.FechaCreacion = query.FechaCreacion.ToString("dd-MM-yyyy");
                        clientecuenta.IdCliente = (int)query.IdCliente;
                        result.Objeto = clientecuenta;
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

        public static Business.ControlResult Update(Business.Cliente cliente)
        {
            Business.ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CuentaUpdate {cliente.Cuenta.IdNumeroCuenta},'{cliente.Cuenta.Nombre}',{cliente.IdCliente}");
                    if (query > 0)
                    {
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "Ocurrio un error al crear la nueva cuenta de ahorro";
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
