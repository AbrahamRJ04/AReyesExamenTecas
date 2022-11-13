using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Inicio()
        {
            Business.Cliente cliente = new Business.Cliente();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Ingresar(Business.Cliente cliente)
        {
            cliente.NumeroCLiente = Request.Form["NumeroCliente"].ToString();
            cliente.Password = Request.Form["pass"].ToString();

            Business.ControlResult result = Business.Login.GetByUserName(cliente.NumeroCLiente);
            if (result.ProcesoCorrecto)
            {
                Business.Cliente cleintepass = new Business.Cliente();
                cleintepass = (Business.Cliente)result.Objeto;

                if (cliente.Password == cleintepass.Password)
                {
                    if (cleintepass.Rol.IdRol == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("GetByIdCuenta", "UserCliente", new { IdCliente = cleintepass.IdCliente });
                    }

                }
                else
                {
                    ViewBag.Mensaje = "PASSWORD INCORRECTO";
                }

            }
            else
            {
                ViewBag.Mensaje = "EL USUARIO INGRESADO NO EXISTE";
            }
            return View("Modal");
        }
    }
}
