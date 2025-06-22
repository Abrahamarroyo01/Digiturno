using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digiturno1.Controller
{

    public class UsuarioController
    {

        private List<Usuario> usuarios = new List<Usuario>(); //Se crea la lista de usuarios
        private int contadorId = 1;


        public void CrearUsuario(string nombre, string usuario, string contraseña, Rol tipoRol)
        {
            Usuario nuevoUsuario = new Usuario//Se crea el usuario con todos sus atributos cada vez que se llama al metodo
            {
                id = contadorId++,
                nombre = nombre,
                usuario = usuario,
                contraseña = contraseña,
                tipoRol = tipoRol
            };

            // Validación de datos
            usuarios.Add(nuevoUsuario);//Se añade cada usuario que se crea a la lista usuarios
            Console.WriteLine($"Usuario creado: {nuevoUsuario.nombre}, Rol: {nuevoUsuario.tipoRol}");//Imprime el usuario creado
        }

        public void ListarUsuarios()//Metodo para mostrar los usuarios
        {
            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }
            Console.WriteLine("Lista de usuarios:");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"ID: {usuario.id}, Nombre: {usuario.nombre}, Usuario: {usuario.usuario}, Rol: {usuario.tipoRol}");
            }
        }

        public Usuario BuscarPorId(int id)
        {
            return usuarios.FirstOrDefault(u => u.id == id);
        }

        public Usuario BuscarPorNombre(string nombre)
        {
            return usuarios.FirstOrDefault(u => u.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public bool EliminarUsuario(int id)
        {
            var usuario = BuscarPorId(id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
                Console.WriteLine($"Usuario eliminado: {usuario.nombre}");
                return true;
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
                return false;
            }
        }



    } 
}