using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digiturno1.Controller;


namespace Digiturno1
{
    internal static class Program
    {
        
        static void Main()
        {


            UsuarioController usuarioController = new UsuarioController();
            ServicioController servicioController = new ServicioController();
            TurnoController turnoController = new TurnoController();

            while (true)
            {
                Console.WriteLine("\n==== DIGITURNO MENU ====");
                Console.WriteLine("1. Crear Servicio");
                Console.WriteLine("2. Generar turno");
                Console.WriteLine("3. Llamar turno");
                Console.WriteLine("4. Finalizar turno");
                Console.WriteLine("5. Cancelar turno");
                Console.WriteLine("6. Salir");
                Console.WriteLine("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        {
                            Console.Write("Nombre del servicio: ");
                            string nombre = Console.ReadLine();

                            Console.Write("Letra de turno (una sola letra): ");
                            char letra = Console.ReadKey().KeyChar;
                            Console.WriteLine();

                            var servicio = servicioController.CrearServicio(nombre, letra);
                            Console.WriteLine($"Servicio '{servicio.nombre}' creado.");
                            break;
                        }
                    case "2":
                        {
                            var listaServicios = servicioController.ListarServicios();

                            if (listaServicios.Count == 0)
                            {
                                Console.WriteLine("No hay servicios disponibles. Cree uno primero.");
                                break;
                            }

                            Console.WriteLine("Servicios disponibles: ");
                            foreach (var s in listaServicios)
                            {
                                Console.WriteLine($"- {s.nombre} ({s.letraTurno})");
                            }

                            Console.WriteLine("Ingrese la letra del servicio al que desea asignar al turno: ");
                            char letraSeleccionada = Console.ReadKey().KeyChar;
                            Console.WriteLine();

                            var servicioSeleccionado = listaServicios.FirstOrDefault(s => s.letraTurno == letraSeleccionada);

                            if (servicioSeleccionado == null)
                            {
                                Console.WriteLine("Servicio no encontrado con esa letra.");
                            }
                            else
                            {
                                turnoController.GenerarTurno(servicioSeleccionado);
                            }

                            break;
                        }
                    case "3":
                        {
                            var listaTurnos = turnoController.ListarTurnos();

                            if (listaTurnos.Count == 0)
                            {
                                Console.WriteLine("No hay turnos pendientes.");
                                break;
                            }

                            Console.WriteLine("Turnos pendientes.");
                            foreach (var t in listaTurnos)
                            {
                                Console.WriteLine($"- Código: {t.letra}{t.numero:D3}, Estado: {t.estado}");
                            }

                            Console.Write("Ingrese el código del turno a llamar: ");
                            string codigo = SolicitarCodigoTurno();

                            turnoController.LlamarTurno(codigo);
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Ingrese el código del turno a finalizar: ");
                            string codigo = SolicitarCodigoTurno();

                            turnoController.FinalizarTurno(codigo);
                            break;
                        }

                    case "5":
                        {
                            Console.Write("Ingrese el código del turno a cancelar: ");
                            string codigo = SolicitarCodigoTurno();

                            turnoController.CancelarTurno(codigo);
                        }
                        break;

                    case "6":
                        {
                            Console.Write("Salir");
                            break;
                        }
                }

            }
        }

        static string SolicitarCodigoTurno()
        {
            string codigo = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.WriteLine("Código inválido.");
                return null;
            }

            return codigo;
        }
    }
}
