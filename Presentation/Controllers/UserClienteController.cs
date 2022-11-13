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

    }
}
