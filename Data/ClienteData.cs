using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class ClienteData
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public ClienteData()
        {
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public ClienteLogueado getClienteLogin(string username,string password)
        {
            ClienteLogueado cliente;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_cliente,nombre_completo,username,password from cliente where username='"+username+
                    "' and password='"+ password+"';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                     cliente = null;
                    while (reader.Read())
                    {
                        cliente = new ClienteLogueado();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.NombreCompleto = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                       
                    }
                    sqlCon.Close();
                }

            }
            return cliente;
        }
        public List<Cliente> GetSpecificClients(string letters)
        {
            Cliente cliente;
            List<Cliente> clientList = new List<Cliente>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "SELECT* from cliente where nombre_completo like '%" + letters + "%';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        clientList.Add(cliente);

                    }
                    sqlCon.Close();

                }
            }
            return clientList;
        }
        public List<Cliente> GetAllClients()
        {
            Cliente cliente;
            List<Cliente> clientList = new List<Cliente>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from cliente;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        clientList.Add(cliente);

                    }
                    sqlCon.Close();

                }
            }
            return clientList;
        }
        public Cliente GetClientById(int idClient)
        {
            Cliente client = new Cliente();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from cliente where id_cliente = '" + idClient + "';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        client.IdCliente = reader.GetInt32("id_cliente");
                        client.Nombre_cliente = reader.GetString("nombre_completo");
                        client.Username = reader.GetString("username");
                        client.Password = reader.GetString("password");


                    }
                    sqlCon.Close();

                }
            }
            return client;
        }
        public ClienteLogueado getClienteLogueado()
        {
            ClienteLogueado clienteLogueado;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_cliente,username,password,nombre_completo from cliente_logueado; ";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    clienteLogueado = null;

                    while (reader.Read())
                    {
                        clienteLogueado = new ClienteLogueado();
                        clienteLogueado.IdCliente = reader.GetInt32("id_cliente");
                        clienteLogueado.Username = reader.GetString("username");
                        clienteLogueado.Password = reader.GetString("password");
                        clienteLogueado.NombreCompleto = reader.GetString("nombre_completo");
                       

                    }
                    sqlCon.Close();
                }

            }
            return clienteLogueado;

        }
        public ClienteLogueado insertarClienteLogueado(ClienteLogueado cliente)
        {

            using (MySqlConnection sqlCon = GetConnection())
            {
                
                    sqlCon.Open();
                    String query = "Insert into cliente_logueado (id_cliente,nombre_completo,username,password) values ("+
                    cliente.IdCliente+",'"+cliente.NombreCompleto+"','"+cliente.Username+"','"+cliente.Password+"');";
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.ExecuteNonQuery();
                    sqlCon.Close();
                ClienteLogueado clienteLogueado = new ClienteLogueado(cliente.IdCliente, cliente.Username, cliente.Password, cliente.NombreCompleto);
                return clienteLogueado;
            }

            }

        public void suprimirClienteLogueado (ClienteLogueado clienteLogueado)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Delete from  cliente_logueado where id_cliente="+clienteLogueado.IdCliente;
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();
                sqlCon.Close();
            }
        }
        public int getIdCliente(string nombreCliente)
        {
            int id = -1;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select c.id_cliente from cliente c where nombre_completo='" + nombreCliente + "';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Cliente cliente = null;
                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        id = cliente.IdCliente;
                    }
                    sqlCon.Close();
                }

            }
            return id;
        }

        public List<Cliente> getAllClientes()
        {
            List<Cliente> list = new List<Cliente>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select a.id_cliente,a.nombre_completo from cliente a;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Cliente cliente = null;
                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");

                        list.Add(cliente);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public Cliente getClienteByid(int idCliente)
        {
            Cliente cliente = null;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select id_cliente, nombre_completo from Cliente where id_cliente=" + idCliente + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");

                    }
                    sqlCon.Close();
                }

            }
            return cliente;
        }

        public List<Cliente> getClienteByName(String SearchString)
        {
            List<Cliente> list = new List<Cliente>();
            Cliente cliente = null;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select id_cliente, nombre_completo from Cliente where nombre_completo like '" + SearchString + "%';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");

                        list.Add(cliente);

                    }
                    sqlCon.Close();
                }

            }
            return list;
        }
    }

}
       
    
