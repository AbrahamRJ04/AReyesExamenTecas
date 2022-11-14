using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserClienteController : Controller
    {

        [HttpGet]
        public IActionResult GetByIdCuenta(int IdCliente)
        {
            Business.ControlResult clientebase = Business.Cliente.GetById(IdCliente);
            if (clientebase.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente = (Business.Cliente)clientebase.Objeto;
                Business.ControlResult result = Business.Cuenta.GetAll(cliente.IdCliente);
                if (result.ProcesoCorrecto)
                {
                    cliente.Cuenta = new Business.Cuenta();
                    cliente.Cuenta.Cuentas = result.Objetos;

                    return View(cliente);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddCuenta(int IdCliente)
        {
            Business.ControlResult result = Business.Cliente.GetById(IdCliente);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente = (Business.Cliente)result.Objeto;
                return View(cliente);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCuenta(Business.Cliente cliente)
        {
            Business.ControlResult result = Business.Cuenta.Add(cliente);
            if (result.ProcesoCorrecto)
            {
                ViewBag.Mensaje = "Cuenta registrada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al registrar la cuenta de ahorro nueva";

            }
            return PartialView("Modal");
        }


        [HttpGet]
        public IActionResult UpdateCuenta(int IdCliente, int IdNumeroCuenta)
        {
            Business.ControlResult result = Business.Cliente.GetById(IdCliente);
            if (result.ProcesoCorrecto)
            {
                Business.ControlResult result1 = Business.Cuenta.GetById(IdNumeroCuenta);
                if (result1.ProcesoCorrecto)
                {
                    Business.Cliente cliente = new Business.Cliente();
                    cliente = (Business.Cliente)result.Objeto;
                    cliente.Cuenta = (Business.Cuenta)result1.Objeto;
                    return View(cliente);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCuenta(Business.Cliente cliente)
        {
            Business.ControlResult result = Business.Cuenta.Update(cliente);
            if (result.ProcesoCorrecto)
            {
                ViewBag.Mensaje = "Cuenta Actualizada  correctamente";

            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al actualizar la cuenta de ahorro";

            }
            return PartialView("Modal");

        }

        [HttpGet]
        public IActionResult Depositar(int IdNumeroCuenta, int IdCliente)
        {
            int Id = 1;
            Business.ControlResult result = Business.Cliente.GetById(IdCliente);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente = (Business.Cliente)result.Objeto;
                Business.ControlResult resultcuenta = Business.Cuenta.GetById(IdNumeroCuenta);
                if (resultcuenta.ProcesoCorrecto)
                {
                    cliente.Cuenta = (Business.Cuenta)resultcuenta.Objeto;
                    Business.ControlResult catalogoTransacciones = Business.TipoTransaccion.GetById(Id);
                    if (catalogoTransacciones.ProcesoCorrecto)
                    {
                        cliente.Cuenta.Transaccion = new Business.Transaccion();
                        cliente.Cuenta.Transaccion.TipoTransaccion = (Business.TipoTransaccion)catalogoTransacciones.Objeto;
                        return View(cliente);
                    }
                }
            }
            return View();

        }

        [HttpPost]
        public IActionResult Depositar(Business.Cliente cliente)
        {
            Business.ControlResult resulttransac = new Business.ControlResult();
            resulttransac.Objetos = new List<object>();

            resulttransac.Objeto = cliente.Cuenta.Transaccion;

            Business.ControlResult cuenta = Business.Cuenta.GetById(cliente.Cuenta.IdNumeroCuenta);
            if (cuenta.ProcesoCorrecto)
            {

                cliente.Cuenta = (Business.Cuenta)cuenta.Objeto;
                cliente.Cuenta.Transaccion = (Business.Transaccion)resulttransac.Objeto;

                decimal SaldoActual = cliente.Cuenta.Saldo;
                cliente.Cuenta.Saldo = SaldoActual + cliente.Cuenta.Transaccion.MontoTransaccion;
                cliente.Cuenta.Transaccion.TipoTransaccion = new Business.TipoTransaccion();
                cliente.Cuenta.Transaccion.TipoTransaccion.IdTipoTransaccion = 1;
                Business.ControlResult result = Business.Transaccion.Depositar(cliente);
                if (result.ProcesoCorrecto)
                {
                    ViewBag.Mensaje = "Deposito realizado con exito";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al realizar el deposito. " + result.exception;
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Retirar(int IdNumeroCuenta, int IdCliente)
        {
            int Id = 2;
            Business.ControlResult result = Business.Cliente.GetById(IdCliente);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente = (Business.Cliente)result.Objeto;
                Business.ControlResult resultcuenta = Business.Cuenta.GetById(IdNumeroCuenta);
                if (resultcuenta.ProcesoCorrecto)
                {
                    cliente.Cuenta = (Business.Cuenta)resultcuenta.Objeto;
                    Business.ControlResult catalogoTransacciones = Business.TipoTransaccion.GetById(Id);
                    if (catalogoTransacciones.ProcesoCorrecto)
                    {
                        cliente.Cuenta.Transaccion = new Business.Transaccion();
                        cliente.Cuenta.Transaccion.TipoTransaccion = (Business.TipoTransaccion)catalogoTransacciones.Objeto;
                        return View(cliente);
                    }
                }
            }
            return View();

        }

        [HttpPost]
        public IActionResult Retirar(Business.Cliente cliente)
        {
            Business.ControlResult resulttransac = new Business.ControlResult();
            resulttransac.Objetos = new List<object>();

            resulttransac.Objeto = cliente.Cuenta.Transaccion;

            Business.ControlResult cuenta = Business.Cuenta.GetById(cliente.Cuenta.IdNumeroCuenta);
            if (cuenta.ProcesoCorrecto)
            {

                cliente.Cuenta = (Business.Cuenta)cuenta.Objeto;
                cliente.Cuenta.Transaccion = (Business.Transaccion)resulttransac.Objeto;

                decimal SaldoActual = cliente.Cuenta.Saldo;
                if (SaldoActual > cliente.Cuenta.Transaccion.MontoTransaccion)
                {
                    cliente.Cuenta.Saldo = SaldoActual - cliente.Cuenta.Transaccion.MontoTransaccion;
                    cliente.Cuenta.Transaccion.TipoTransaccion = new Business.TipoTransaccion();
                    cliente.Cuenta.Transaccion.TipoTransaccion.IdTipoTransaccion = 2;

                    Business.ControlResult result = Business.Transaccion.Depositar(cliente);
                    if (result.ProcesoCorrecto)
                    {
                        ViewBag.Mensaje = "Retiro realizado con exito";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al realizar el retiro. " + result.exception;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "La operacion no se pudo realizar debido a que no cuenta con saldo suficiente." +
                        "\n Vefique e intente de nuevo. Gracias.";
                }

            }
            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult DeleteCuenta(int IdNumeroCuenta)
        {
            Business.ControlResult result = Business.Cuenta.Delete(IdNumeroCuenta);
            if (result.ProcesoCorrecto)
            {
                ViewBag.Mensaje = "Cuenta eliminada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al registrar la cuenta de ahorro nueva";
            }
            return PartialView("Modal");

        }
        [HttpGet]
        public IActionResult HistorialGetByIdCliente(int IdCliente)
        {
            Business.ControlResult result = Business.HistorialTransacciones.GetByIdCliente(IdCliente);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente.Historial = new Business.HistorialTransacciones();
                cliente.Historial.Transacciones = result.Objetos;

                return View(cliente);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al realizar busqueda: " + result.MensajeError;
                return PartialView("Modal");
            }
            return View();
        }

        [HttpGet]
        public IActionResult HistorialGetByIdCuenta(int IdNumeroCuenta)
        {
            Business.ControlResult result = Business.HistorialTransacciones.GetByIdNumeroCuenta(IdNumeroCuenta);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente.Historial = new Business.HistorialTransacciones();
                cliente.Historial.Transacciones = result.Objetos;

                return View(cliente);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al realizar busqueda: " + result.MensajeError;
                return PartialView("Modal");
            }
            return View();
        }
    }
}
