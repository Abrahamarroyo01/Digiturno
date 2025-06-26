using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digiturno
{
    public class Servicio
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public char letraTurno { get; set; }
        public DateTime tiempoEspera { get; set; }
        public bool bloqueado { get; set; }
        public bool alertaProductividad { get; set; }


    }
}
