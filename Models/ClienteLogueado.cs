using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class ClienteLogueado
    {
        private int idCliente;
        private string nombreCompleto;
        private string username;
        private string password;

        public ClienteLogueado()
        {
        }

        public ClienteLogueado(int idCliente, string nombreCompleto, string username, string password)
        {
            this.idCliente = idCliente;
            this.nombreCompleto = nombreCompleto;
            this.username = username;
            this.password = password;
        }

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}