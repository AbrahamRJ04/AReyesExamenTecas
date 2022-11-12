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
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
