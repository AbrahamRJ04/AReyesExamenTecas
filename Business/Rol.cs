using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public  class Rol
    {
        /*ATRIBUTOS*/
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public List<Object> Roles { get; set; }

        /*METODOS*/
        public static Business.ControlResult GetAll()
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.Rols.FromSqlRaw($"RolGetAll").ToList();
                    result.Objetos = new List<object>();

                    if(query  != null)
                    {
                        foreach (var item in query)
                        {
                            Rol rol = new Rol();
                            rol.IdRol = item.IdRol;
                            rol.Nombre = item.Nombre;
                            result.Objetos.Add(rol);
                        }
                        result.ProcesoCorrecto = true;
                    }
                    else
                    {
                        result.ProcesoCorrecto = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.MensajeError = ex.Message;
                result.ProcesoCorrecto = false;
            }
            return result;
        }
    }
}
