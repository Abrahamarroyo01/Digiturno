using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digiturno1.Controller
{
    public class TurnoController
    {

        private List<Turno> turnos = new List<Turno>(); //Se crea la lista de turnos
        private Dictionary<char, int> consecutivosPorLetra = new Dictionary<char, int>(); //Se crea el diccionario para guardar la letra y el numero del turno

        private int idTurno = 1;

        public void GenerarTurno(Servicio servicio)
        {
            //validación del servicio si es nulo
            if (servicio == null) 
            {
                Console.WriteLine("No se encontró el servicio");
                return;
            }

            //Valicadión del consecutivoPorLetra, si no hay ninguna se crea el primer turno en el diccionario 
            if (!consecutivosPorLetra.ContainsKey(servicio.letraTurno))
            {
                consecutivosPorLetra[servicio.letraTurno] = 1;
            }

            //Para utilizar despues en caso de requerir
            int numero = consecutivosPorLetra[servicio.letraTurno]++;
            string codigo = $"{servicio.letraTurno}{numero:D3}";

            //Se crea el objeto de turno con todos sus atributos
            Turno nuevo = new Turno 
            {
                id = idTurno++,
                letra = servicio.letraTurno,
                numero = numero,
                estado = EstadoTurno.EnEspera,
                fechaHoraSolicitud = DateTime.Now,
                servicioAsignado = servicio,
                bloqueado = false
            };

            //Asigna el turno a la lista "turnos"
            turnos.Add(nuevo);
            Console.WriteLine($"Turno generado : {codigo} para servicio '{servicio.nombre}'.");//Imprime el turno generado
        }

        public List<Turno> ListarTurnos()//Metodo para mostrar los turnos
        {
            if (turnos.Count == 0)
            {
                Console.WriteLine("No hay turnos registrados.");
                return turnos;
            }
            Console.WriteLine("Lista de servicios:");
            foreach (var turno in turnos)
            {
                Console.WriteLine($"Nombre del turno: {turno.codigo}, Estado del turno: {turno.estado}");
            }
            return turnos;

        }

        private Turno BuscarTurno(string codigo)
        {
            var turno = turnos.FirstOrDefault(t => t.codigo == codigo); //Busca en la lista "turnos" por codigo
            return turno;
        }

        public void LlamarTurno(string codigo)
        {
            var turno = BuscarTurno(codigo);

            //Validación en caso de estar vacía la variable turno
            if (turno == null) 
            {
                Console.WriteLine("Turno no encontrado.");
                return;
            }

            //Validación en caso de no tener hora de atención
            if (turno.fechaHoraAtencion != null) 
            {
                Console.WriteLine("Este turno ya fue llamado anteriormente");
                return;
            }

            turno.fechaHoraAtencion = DateTime.Now; //Crea la hora de atención = hora en la que se llama al paciente
            turno.estado = EstadoTurno.EnEspera; //Se asigna el estado del turno = en espera

            //Imprime el turno con su letra y numero
            Console.WriteLine($"Turno {turno.codigo} en atención.");

        }

        public void FinalizarTurno(string codigo) { 
            var turno = BuscarTurno(codigo);

            if (turno == null)
            {
                Console.WriteLine("Turno no encontrado.");
                return;
            }

            switch (turno.estado)
            {
                case EstadoTurno.Finalizado:
                    Console.WriteLine("Este turno ya fue finalizado.");
                    return;

                case EstadoTurno.Cancelado:
                    Console.WriteLine("Este turno está cancelado y no se puede finalizar.");
                    return;
            }

            turno.fechaHoraFinalizacion = DateTime.Now;
            turno.estado = EstadoTurno.Finalizado;

            Console.WriteLine($"Turno {turno.codigo} finalizado a las {turno.fechaHoraFinalizacion.Value:T}");

            if (turno.fechaHoraAtencion.HasValue)
            {
                TimeSpan duracion = turno.fechaHoraFinalizacion.Value - turno.fechaHoraAtencion.Value;
                Console.WriteLine($"Duración de atención: {duracion.Minutes} minutos y {duracion.Seconds} segundos");
            }
            else
            {
                Console.WriteLine("No se había registrado la hora de atención. ");
            }
        }

        public void CancelarTurno( string codigo)
        {
            var turno = BuscarTurno(codigo);

            if(turno == null)
            {
                Console.WriteLine("Turno no encontrado.");
                return;
            }

            switch (turno.estado)
            {
                case EstadoTurno.Finalizado:
                    Console.WriteLine("Este turno ya fue finalizado.");
                    return;

                case EstadoTurno.Cancelado:
                    Console.WriteLine("Este turno está cancelado y no se puede finalizar.");
                    return;
            }

            turno.estado = EstadoTurno.Cancelado;
            turno.fechaHoraFinalizacion = DateTime.Now;

            Console.WriteLine($"Turno {turno.codigo} cancelado correctamente.");
        }

    }
}
