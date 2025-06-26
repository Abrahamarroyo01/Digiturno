using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digiturno.Controller
{
    public class ServicioController
    {

        private List<Servicio> servicios = new List<Servicio>();
        private int idServicio = 1;

        public Servicio CrearServicio(string nombre, char letra) 
        {
            Servicio nuevoServicio = new Servicio 
            { 
                id = idServicio++,
                nombre = nombre,
                letraTurno = letra,
                tiempoEspera = DateTime.Now,
                bloqueado = false,
                alertaProductividad = false

            };

            servicios.Add(nuevoServicio);
            Console.WriteLine($"Servicio generado : {nuevoServicio.nombre} con identificador '{nuevoServicio.letraTurno}'.");
            return nuevoServicio;
        }

        public List<Servicio> ListarServicios()//Metodo para mostrar los Servicios
        {
            if (servicios.Count == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return servicios;
            }
            Console.WriteLine("Lista de servicios:");
            foreach (var servicio in servicios)
            {
                Console.WriteLine($"Nombre del servicio: {servicio.nombre}, Letra del servicio: {servicio.letraTurno}");
            }
            return servicios;
         
        }

        public Servicio BuscarPorId(int id)
        {
            return servicios.FirstOrDefault(u => u.id == id);
        }


        public bool EliminarServicio(int id)
        {
            var servicio = BuscarPorId(id);
            if (servicio != null)
            {
                servicios.Remove(servicio);
                Console.WriteLine($"Servicio eliminado: {servicio.nombre}");
                return true;
            }
            else
            {
                Console.WriteLine("Servicio no encontrado.");
                return false;
            }
        }


    }
}
