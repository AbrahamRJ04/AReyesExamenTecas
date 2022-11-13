using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HistorialTransacciones
    {
        public int IdTransaccion { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string Transaccion { get; set; }
        public string Detalle { get; set; }
        public string FechaTransaccion { get; set; }
        public int IdNumeroCuenta { get; set; }
        public string NombreDeCuenta { get; set; }
        public decimal Saldo { get; set; }
        public string FechaCreacion { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroCliente { get; set; }
        public int IdRol { get; set; }
        public string TipoRol { get; set; }
        public decimal MontoTransaccion { get; set; }
        public List<Object> Transacciones { get; set; }


        public static Business.ControlResult GetByIdCliente(int IdCliente)
        {
            Business.ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.HistorialTransacciones.FromSqlRaw($"HistorialGetById {IdCliente}").ToList();
                    result.Objetos = new List<Object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Business.Cliente cliente = new Business.Cliente();
                            cliente.Historial = new Business.HistorialTransacciones();

                            cliente.Historial.IdTransaccion = item.IdTransaccion;
                            cliente.Historial.IdTipoTransaccion = item.IdTipoTransaccion;
                            cliente.Historial.Transaccion = item.Transaccion;
                            cliente.Historial.Detalle = item.Detalle;
                            cliente.Historial.FechaTransaccion = item.FechaTransaccion.ToString();
                            cliente.Historial.IdNumeroCuenta = item.IdNumeroCuenta;
                            cliente.Historial.NombreDeCuenta = item.NombreDeCuenta;
                            cliente.Historial.Saldo = item.Saldo;
                            cliente.Historial.FechaCreacion = item.FechaCreacion.ToString();
                            cliente.Historial.IdCliente = item.IdCliente;
                            cliente.Historial.Nombre = item.Nombre;
                            cliente.Historial.ApellidoPaterno = item.ApellidoPaterno;
                            cliente.Historial.ApellidoMaterno = item.ApellidoMaterno;
                            cliente.Historial.NumeroCliente = item.NumeroCliente;
                            cliente.Historial.IdRol = (int)item.IdRol;
                            cliente.Historial.TipoRol = item.TipoRol;
                            cliente.Historial.MontoTransaccion = item.MontoTransaccion;
                            result.Objetos.Add(cliente);
                        }
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "No se encontro ninguna transaccion";
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
        public static Business.ControlResult GetByIdNumeroCuenta(int IdCNumeroCuenta)
        {
            Business.ControlResult result = new ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.HistorialTransacciones.FromSqlRaw($"HistorialGetByIdCuenta {IdCNumeroCuenta}").ToList();
                    result.Objetos = new List<Object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Business.Cliente cliente = new Business.Cliente();
                            cliente.Historial = new Business.HistorialTransacciones();

                            cliente.Historial.IdTransaccion = item.IdTransaccion;
                            cliente.Historial.IdTipoTransaccion = item.IdTipoTransaccion;
                            cliente.Historial.Transaccion = item.Transaccion;
                            cliente.Historial.Detalle = item.Detalle;
                            cliente.Historial.FechaTransaccion = item.FechaTransaccion.ToString();
                            cliente.Historial.IdNumeroCuenta = item.IdNumeroCuenta;
                            cliente.Historial.NombreDeCuenta = item.NombreDeCuenta;
                            cliente.Historial.Saldo = item.Saldo;
                            cliente.Historial.FechaCreacion = item.FechaCreacion.ToString();
                            cliente.Historial.IdCliente = item.IdCliente;
                            cliente.Historial.Nombre = item.Nombre;
                            cliente.Historial.ApellidoPaterno = item.ApellidoPaterno;
                            cliente.Historial.ApellidoMaterno = item.ApellidoMaterno;
                            cliente.Historial.NumeroCliente = item.NumeroCliente;
                            cliente.Historial.IdRol = (int)item.IdRol;
                            cliente.Historial.TipoRol = item.TipoRol;
                            cliente.Historial.MontoTransaccion = item.MontoTransaccion;
                            result.Objetos.Add(cliente);
                        }
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                        result.MensajeError = "No se encontro ninguna transaccion";
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
