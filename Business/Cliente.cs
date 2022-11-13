using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Cliente
    {
        //ATRIBUTOS DE CLIENTE
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroCLiente { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FechaNaacimiento { get; set; }
        public string Telefono { get; set; }
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public Business.Rol Rol { get; set; }
        public List<Object> Clientes { get; set; }
        public Business.Cuenta Cuenta { get; set; }
        public Business.HistorialTransacciones Historial { get; set; }

        //METODOS CLIENTE

        public static Business.ControlResult GetAll()
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Clientes.FromSqlRaw("ClienteGetAll").ToList();
                    result.Objetos = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Cliente cliente = new Cliente();
                            cliente.Rol = new Rol();

                            cliente.IdCliente = item.IdCliente;
                            cliente.Nombre = item.Nombre;
                            cliente.ApellidoPaterno = item.ApellidoPaterno;
                            cliente.ApellidoMaterno = item.ApellidoMaterno;
                            cliente.NumeroCLiente = item.NumeroCliente;
                            cliente.Email = item.Email;
                            cliente.Password = item.Password;
                            cliente.FechaNaacimiento = item.FechaNacimiento.ToString("dd-MM-yyyy");
                            cliente.Telefono = item.Telefono;
                            cliente.CURP = item.Curp;
                            cliente.Imagen = item.Imagen;
                            cliente.Rol.IdRol = (int)item.IdRol;
                            cliente.Rol.Nombre = item.TipoRol;
                            result.Objetos.Add(cliente);
                        }
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "Ocurrio un error al realizar la busqueda de clientes";
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
        public static Business.ControlResult GetById(int IdCliente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Clientes.FromSqlRaw($"ClienteById {IdCliente}").AsEnumerable().FirstOrDefault();
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
        public static Business.ControlResult Delete(int IdCliente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ClienteDelete {IdCliente}");
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
        public static Business.ControlResult Add(Cliente cliente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ClienteAdd '{cliente.Nombre}','{cliente.ApellidoPaterno}','{cliente.ApellidoMaterno}','{cliente.Email}','{cliente.Password}','{cliente.FechaNaacimiento}','{cliente.Telefono}','{cliente.CURP}','{cliente.Imagen}',{cliente.Rol.IdRol}");
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
        public static Business.ControlResult Update(Cliente cliente)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ClienteUpdate {cliente.IdCliente},'{cliente.Nombre}','{cliente.ApellidoPaterno}','{cliente.ApellidoMaterno}','{cliente.Email}','{cliente.Password}','{cliente.FechaNaacimiento}','{cliente.Telefono}','{cliente.CURP}','{cliente.Imagen}',{cliente.Rol.IdRol}");
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
