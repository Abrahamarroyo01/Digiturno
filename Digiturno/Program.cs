using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digiturno.Controller;


namespace Digiturno
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
                Console.WriteLine("6. Crear Usuario");
                Console.WriteLine("7. Listar Usuario");
                Console.WriteLine("8. Actualizar Usuario");
                Console.WriteLine("9. Salir");
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
                            Console.WriteLine("Por favor ingrese el nombre del usuario:");
                            string nombre = Console.ReadLine();

                            Console.WriteLine("Por favor ingrese el usuario de acceso:");
                            string usuario = Console.ReadLine();

                            Console.WriteLine("Por favor ingrese la contraseña del usuario:");
                            string contraseña = Console.ReadLine();

                            Console.WriteLine("Seleccione un rol:");
                            Console.WriteLine("1. Administrador");
                            Console.WriteLine("2. Supervisor");
                            Console.WriteLine("3. Asesor");
                            string rolInput = Console.ReadLine();

                            Rol rolSeleccionado = rolInput switch
                            {
                                "1" => Rol.Administrador,
                                "2" => Rol.Supervisor,
                                "3" => Rol.Asesor,
                                _ => throw new InvalidOperationException("Rol inválido. No se puede continuar.")
                            };

                            var nuevoUsuario = usuarioController.CrearUsuario(nombre, usuario, contraseña,rolSeleccionado);

                        }
                        break;

                    case "7": 
                        {
                            var listaUsuarios = usuarioController.ListarUsuarios();
                        } 
                        break;

                    case "8":
                        {
                            Console.WriteLine("Ingrese el ID del usuario a actualizar");
                            if (!int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.WriteLine("ID inválido.");
                            }

                            var usuario = usuarioController.BuscarPorId(id);
                            if (usuario == null)
                            {
                                Console.WriteLine("Usuario no encontrado.");
                                break;
                            }

                            Console.WriteLine("\n Seleccione el dato que desea actualizar:");
                            Console.WriteLine("1. Nombre");
                            Console.WriteLine("2. Usuario");
                            Console.WriteLine("3. Contraseña");
                            Console.WriteLine("4. Rol");
                            Console.Write("Opción: ");
                            string opcionActualizacion = Console.ReadLine();

                            string nuevoNombre = usuario.nombre;
                            string nuevoUsuario = usuario.usuario;
                            string nuevaContraseña = usuario.contraseña;
                            Rol nuevoRol = usuario.tipoRol;

                            switch (opcionActualizacion)
                            {
                                case "1":
                                    {
                                        Console.Write("Nuevo nombre: ");
                                        nuevoNombre = Console.ReadLine();
                                    }
                                    break;

                                case "2":
                                    {
                                        Console.Write("Nuevo usuario: ");
                                        nuevoUsuario = Console.ReadLine();
                                    }
                                    break;

                                case "3":
                                    {
                                        Console.Write("Nueva contraseña: ");
                                        nuevaContraseña = Console.ReadLine();
                                    }
                                    break;

                                case "4":
                                    {
                                        Console.WriteLine("Seleccione un nuevo rol:");
                                        Console.WriteLine("1. Administrador");
                                        Console.WriteLine("2. Supervisor");
                                        Console.WriteLine("3. Asesor");
                                        string rolInput = Console.ReadLine();
                                        nuevoRol = rolInput switch
                                        {
                                            "1" => Rol.Administrador,
                                            "2" => Rol.Supervisor,
                                            "3" => Rol.Asesor,
                                            _ => usuario.tipoRol // Mantener el rol actual si la opción es inválida
                                        };
                                    }
                                    break;  
                            }
                            usuarioController.ActualizarUsuario(id, nuevoNombre, nuevoUsuario, nuevaContraseña, nuevoRol);
                        }
                        break;

                    case "9":
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