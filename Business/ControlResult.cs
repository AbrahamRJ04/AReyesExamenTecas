using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ControlResult 
    {
        public bool ProcesoCorrecto { get; set; }
        public string MensajeError { get; set; }
        public Object Objeto { get; set; }
        public List<object> Objetos { get; set; }
        public Exception exception { get; set; }
    }
}
