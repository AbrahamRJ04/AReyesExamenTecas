using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ClienteController : Controller
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            Business.ControlResult result = Business.Cliente.GetAll();
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cliente = new Business.Cliente();
                cliente.Clientes = result.Objetos;
                return View(cliente);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Form(int? IdCliente)
        {
            Business.Cliente cliente = new Business.Cliente();
            cliente.Rol = new Business.Rol();


            Business.ControlResult resultRoles = Business.Rol.GetAll();
            if (IdCliente == null)
            {
                cliente.Rol.Roles = resultRoles.Objetos;
                return View(cliente);
            }
            else
            {
                Business.ControlResult result = Business.Cliente.GetById(IdCliente.Value);
                if (result.ProcesoCorrecto)
                {
                    cliente = (Business.Cliente)result.Objeto;
                    cliente.Rol.Roles = resultRoles.Objetos;
                    return View(cliente);
                }

            }
            return View("Modal");
        }
        [HttpPost]
        public IActionResult Form(Business.Cliente cliente)
        {
            IFormFile image = Request.Form.Files["IFImage"];

            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                cliente.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            Business.ControlResult result = new Business.ControlResult();
            if (cliente.IdCliente == 0)
            {
                result = Business.Cliente.Add(cliente);
                if (result.ProcesoCorrecto)
                {
                    ViewBag.Mensaje = "El cliente se registro con exito";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al registrar al cliente";
                }
            }
            else
            {
                result = Business.Cliente.Update(cliente);
                if (result.ProcesoCorrecto)
                {
                    ViewBag.Mensaje = "Cliente actualizaco correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar el registro";
                }
            }
            return PartialView("Modal");
        }


        [HttpGet]
        public IActionResult Delete(int IdCliente)
        {
            Business.ControlResult result = Business.Cliente.Delete(IdCliente);
            if (result.ProcesoCorrecto)
            {
                ViewBag.Mensaje = "El cliente y sus cuentas registradas han sido borradas exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al intentar eliminar el cliente";
            }
            return PartialView("Modal");
        }


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

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
