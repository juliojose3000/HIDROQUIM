using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public  class Cliente
    
    {
        private int idCliente;
        private string nombre_cliente;
        private string username;
        private string password;

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string Nombre_cliente { get => nombre_cliente; set => nombre_cliente = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public Cliente()
        {

        }

        public Cliente(int idCliente, string nombre_cliente)
        {
            IdCliente = idCliente;
            Nombre_cliente = nombre_cliente;
        }

        public Cliente(int idCliente, string nombre_cliente, string username, string password)
        {
            this.idCliente = idCliente;
            this.nombre_cliente = nombre_cliente;
            this.username = username;
            this.password = password;
        }
        public Cliente( string nombre_cliente, string username, string password)
        {
            
            this.nombre_cliente = nombre_cliente;
            this.username = username;
            this.password = password;
        }
    }
}