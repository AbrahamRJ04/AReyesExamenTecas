using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class Login
	{
        public static Business.ControlResult GetByUserName(string NUmeroCLiente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Clientes.FromSqlRaw($"ClienteByIdNumeroCliente '{NUmeroCLiente}'").AsEnumerable().FirstOrDefault();
                    result.Objetos = new List<object>();
                    if (query != null)
                    {
                        Cliente cliente = new Cliente();
                        cliente.Rol = new Rol();

                        cliente.IdCliente = query.IdCliente;
                        cliente.Nombre = query.Nombre;
                        cliente.ApellidoPaterno = query.ApellidoPaterno;
                        cliente.ApellidoMaterno = query.ApellidoMaterno;
                        cliente.NumeroCLiente = query.NumeroCliente;
                        cliente.Email = query.Email;
                        cliente.Password = query.Password;
                        cliente.FechaNaacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        cliente.Telefono = query.Telefono;
                        cliente.CURP = query.Curp;
                        cliente.Imagen = query.Imagen;
                        cliente.Rol.IdRol = (int)query.IdRol;
                        cliente.Rol.Nombre = query.TipoRol;
                        result.Objeto = cliente;
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "Ocurrio un error al realizar la busqueda";
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
