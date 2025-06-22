using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digiturno1
{
    public enum Rol
    {
        Aministrador,
        Supervisor,
        Asesor
    }

    public class Usuario
    {

        public int id {get; set;}
        public string nombre {get; set;}
        public string usuario {get; set;}
        public string contraseña {get; set;}
        public Rol tipoRol {get; set;}

        

    }

}
