using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class PersonaDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public PersonaDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public List<Persona> getAllPersonas()
        {
            List<Persona> list = new List<Persona>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select p.id_cliente,p.cedula, p.id_informacion_contacto, i.correo, i.telefonoFijo, i.telefonoCelular, c.nombre_completo, c.username, c.password from " +
                    " persona p, informacion_contacto i, cliente c where p.id_informacion_contacto= i.id_informacion_contacto and p.id_cliente= c.id_cliente;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Persona persona = null;
                    InformacionContacto informacionContacto = null;
                    Cliente cliente = null;

                    while (reader.Read())
                    {
                        persona = new Persona();
                        persona.Cedula = reader.GetString("cedula");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        persona = new Persona();
                        persona.Cedula = reader.GetString("cedula");
                        persona.InformacionContacto = informacionContacto;
                        persona.Cliente = cliente;

                        list.Add(persona);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }
        public int getIdInformacionContacto(String correo)
        {
            int idInformacionContacto = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select id_informacion_contacto from  informacion_contacto where correo='" + correo + "'";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idInformacionContacto = reader.GetInt32("id_informacion_contacto");
                    }

                }
                sqlCon.Close();
            }
            return idInformacionContacto;
        }

        public int getIdCliente(String nombre_completo)
        {
            int idCliente = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select id_cliente from  cliente where nombre_completo='" + nombre_completo + "'";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idCliente = reader.GetInt32("id_cliente");
                    }

                }
                sqlCon.Close();
            }
            return idCliente;
        }
        public Persona getPersonaById(String id)
        {

            int idCliente = 0;
            Persona persona = null;
            InformacionContacto informacionContacto = null;
            Cliente cliente = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select p.cedula, c.nombre_completo,p.id_informacion_contacto,c.id_cliente, c.username, c.password , i.correo, i.telefonoFijo,i.telefonoCelular from  persona p, cliente c, informacion_contacto i" +
                    " where cedula ='" + id + "' and p.id_cliente= c.id_cliente and i.id_informacion_contacto= p.id_informacion_contacto";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        persona = new Persona();
                        persona.Cedula = reader.GetString("cedula");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        persona.InformacionContacto = informacionContacto;
                        persona.Cliente = cliente;
                    }
                }
                sqlCon.Close();
            }
            return persona;
        }

        public List<Persona> getPersonaByNombre(string SearchString)
        {
            List<Persona> personas = new List<Persona>();
            Persona persona = null;
            InformacionContacto informacionContacto = null;
            Cliente cliente = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select p.cedula, c.nombre_completo,p.id_informacion_contacto,c.id_cliente, c.username, c.password , i.correo, i.telefonoFijo,i.telefonoCelular from  persona p, cliente c, informacion_contacto i" +
                    " WHERE p.id_cliente= c.id_cliente AND p.id_informacion_contacto=i.id_informacion_contacto AND  nombre_completo like'" + SearchString + "%';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        persona = new Persona();
                        persona.Cedula = reader.GetString("cedula");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");
                        cliente = new Cliente();
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        persona.InformacionContacto = informacionContacto;
                        persona.Cliente = cliente;
                        personas.Add(persona);


                    }
                }
                sqlCon.Close();
            }
            return personas;
        }

    
    public void insertarPersona(Persona persona)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                MySqlTransaction transaction3 = null;

                try
                {
                    sqlCon.Open();
                    String query = "Insert into informacion_contacto (correo,telefonoFijo,telefonoCelular) values ('" + persona.InformacionContacto.Correo
               + "','" + persona.InformacionContacto.TelefonoFijo + "','" + persona.InformacionContacto.TelefonoCelular + "')";

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();
                    int idInformacionContacto = getIdInformacionContacto(persona.InformacionContacto.Correo);

                    transaction2 = sqlCon.BeginTransaction();
                    String query2 = "Insert into cliente (nombre_completo,username,password) values ('" + persona.Cliente.Nombre_cliente + "','"
                + persona.Cliente.Username + "','" + persona.Cliente.Password + "')";
                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();
                    int idCliente = getIdCliente(persona.Cliente.Nombre_cliente);

                    transaction3 = sqlCon.BeginTransaction();
                    String query3 = "Insert into persona (id_cliente,cedula,id_informacion_contacto) values (" + idCliente + ",'"
                + persona.Cedula + "'," + idInformacionContacto + ")";
                    MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                    sqlSelect3.Transaction = transaction3;
                    sqlSelect3.ExecuteNonQuery();

                    transaction3.Commit();
                }
                catch (MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                finally
                {
                    if (sqlCon != null)
                    {
                        sqlCon.Close();
                    }
                }
            }

        }


        public void suprimirPersona(String id)
        {
            Persona persona = getPersonaById(id);
            using (MySqlConnection sqlCon = GetConnection())
            {

                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                MySqlTransaction transaction3 = null;

                try
                {
                    sqlCon.Open();

                    String query = "Delete from informacion_contacto where id_informacion_contacto= " + persona.InformacionContacto.IdInformacionContacto + ";";
                    String query2 = "Delete from cliente  where id_cliente=" + persona.Cliente.IdCliente + ";";
                    String query3 = "Delete from persona  where cedula='" + persona.Cedula + "';";

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                    sqlSelect3.Transaction = transaction;
                    sqlSelect3.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();

                    transaction3 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query2, sqlCon);
                    sqlSelect.Transaction = transaction3;
                    sqlSelect.ExecuteNonQuery();
                    transaction3.Commit();

                }
                catch (MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                finally
                {
                    if (sqlCon != null)
                    {
                        sqlCon.Close();
                    }
                }
            }

        }


        public void modificarPersona(Persona persona)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                MySqlTransaction transaction3 = null;
                try
                {
                    sqlCon.Open();

                    String query = "Update persona set cedula='" + persona.Cedula + "' where id_informacion_contacto=" + persona.InformacionContacto.IdInformacionContacto;

                    String query2 = "Update informacion_contacto set correo='" + persona.InformacionContacto.Correo + "',telefonoFijo='" +
                    persona.InformacionContacto.TelefonoFijo + "',telefonoCelular='" + persona.InformacionContacto.TelefonoCelular
                     + "' where id_informacion_contacto=" + persona.InformacionContacto.IdInformacionContacto;

                    String query3 = "Update cliente set nombre_completo='" + persona.Cliente.Nombre_cliente + "',username='" +
                   persona.Cliente.Username + "',password='" + persona.Cliente.Password
                     + "' where id_cliente=" + persona.Cliente.IdCliente;

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();

                    transaction3 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                    sqlSelect3.Transaction = transaction3;
                    sqlSelect3.ExecuteNonQuery();
                    transaction3.Commit();
                }
                catch (MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    if (transaction2 != null)
                    {
                        transaction2.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlCon != null)
                    {
                        sqlCon.Close();
                    }
                }
            }

        }

    }
}
