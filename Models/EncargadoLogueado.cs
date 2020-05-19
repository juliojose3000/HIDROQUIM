using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class EncargadoLogueado
    {
        private int idEncargado;
        private string userName;
        private string password;
        private string nombre;
        private string apellidos;
        public int IdEncargado { get => idEncargado; set => idEncargado = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }

        public EncargadoLogueado()
        {
        }

        public EncargadoLogueado(int idEncargado, string userName, string password, string nombre, string apellidos)
        {
            this.idEncargado = idEncargado;
            this.userName = userName;
            this.password = password;
            this.nombre = nombre;
            this.apellidos = apellidos;
        }
    }
}