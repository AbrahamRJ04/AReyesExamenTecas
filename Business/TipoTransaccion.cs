using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TipoTransaccion
    {
        public int IdTipoTransaccion { get; set; }
        public string Nombre { get; set; }
        public List<Object> TiposTrasaccion { get; set; }

        public static Business.ControlResult GetById(int IdTipoTransaccion)
        {
            Business.ControlResult result = new Business.ControlResult();
            try
            {
                using (DataAccess.AreyesTecasExamenContext context = new DataAccess.AreyesTecasExamenContext())
                {
                    var query = context.TipoTransaccions.FromSqlRaw($"TipoTransaccionGetById {IdTipoTransaccion}").AsEnumerable().FirstOrDefault();
                    result.Objetos = new List<object>();

                    if (query != null)
                    {

                        TipoTransaccion tipoTransaccion = new TipoTransaccion();

                        tipoTransaccion.IdTipoTransaccion = query.IdTipoTransaccion;
                        tipoTransaccion.Nombre = query.Nombre;

                        result.Objeto = tipoTransaccion;
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
    }
}
