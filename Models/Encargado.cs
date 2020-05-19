using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Encargado
    {
        private int idEncargado;
        private string cedula;
        private string nombre;
        private string apellidos;
        private string correo;
        private string telefono;
        private string userName;
        private string password;
        private Rol rol;

        public Encargado()
        {
        }

        public Encargado(int idEncargado, string cedula, string nombre, string apellidos, string correo, string telefono, string userName, string password, Rol rol)
        {
            this.idEncargado = idEncargado;
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.correo = correo;
            this.telefono = telefono;
            this.userName = userName;
            this.password = password;
            this.rol = rol;
        }

        public int IdEncargado { get => idEncargado; set => idEncargado = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public Rol Rol { get => rol; set => rol = value; }
    }
}