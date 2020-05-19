using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class ProveedorDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public ProveedorDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public List<Proveedor> getAllProvedores()
        {
            List<Proveedor> list = new List<Proveedor>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select p.id_proveedor,p.nombre,p.identificacion, p.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular from " +
                    " proveedor p, informacion_contacto i where p.id_informacion_contacto= i.id_informacion_contacto;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    InformacionContacto informacionContacto = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        proveedor.IdProveedor = reader.GetInt32("id_proveedor");
                        proveedor.Nombre = reader.GetString("nombre");
                        proveedor.Identificacion = reader.GetString("identificacion");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");

                        proveedor.InformacionContacto = informacionContacto;

                        list.Add(proveedor);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public Proveedor getProveedorById(int id)
        {
            Proveedor proveedor = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select p.id_proveedor,p.nombre,p.identificacion, p.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular from " +
                    " proveedor p, informacion_contacto i where p.id_informacion_contacto= i.id_informacion_contacto AND p.id_proveedor=" + id+";";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                   
                    InformacionContacto informacionContacto = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        proveedor.IdProveedor = reader.GetInt32("id_proveedor");
                        proveedor.Nombre = reader.GetString("nombre");
                        proveedor.Identificacion = reader.GetString("identificacion");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");

                        proveedor.InformacionContacto = informacionContacto;

                       
                    }
                    sqlCon.Close();
                }

            }
            return proveedor;
        }
        public List<Proveedor> getProveedorByName(string  name)
        {
            List<Proveedor> list = new List<Proveedor>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select p.id_proveedor,p.nombre,p.identificacion, p.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular from " +
                    " proveedor p, informacion_contacto i where p.id_informacion_contacto= i.id_informacion_contacto AND p.nombre LIKE '" + name+ "%';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    InformacionContacto informacionContacto = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        proveedor.IdProveedor = reader.GetInt32("id_proveedor");
                        proveedor.Nombre = reader.GetString("nombre");
                        proveedor.Identificacion = reader.GetString("identificacion");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");

                        proveedor.InformacionContacto = informacionContacto;

                        list.Add(proveedor);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public Proveedor getProveedorByName2(string name)
        {
            Proveedor proveedor;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select p.id_proveedor,p.nombre,p.identificacion, p.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular from " +
                    " proveedor p, informacion_contacto i where p.id_informacion_contacto= i.id_informacion_contacto AND p.nombre='" + name + "';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                     proveedor = null;
                    InformacionContacto informacionContacto = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        proveedor.IdProveedor = reader.GetInt32("id_proveedor");
                        proveedor.Nombre = reader.GetString("nombre");
                        proveedor.Identificacion = reader.GetString("identificacion");
                        informacionContacto = new InformacionContacto();
                        informacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contacto");
                        informacionContacto.Correo = reader.GetString("correo");
                        informacionContacto.TelefonoFijo = reader.GetString("telefonoFijo");
                        informacionContacto.TelefonoCelular = reader.GetString("telefonoCelular");

                        proveedor.InformacionContacto = informacionContacto;

                      
                    }
                    sqlCon.Close();
                }

            }
            return proveedor;
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

        public void insertarProveedor(Proveedor proveedor)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    String query = "Insert into informacion_contacto (correo,telefonoFijo,telefonoCelular) values ('" + proveedor.InformacionContacto.Correo
                    + "','" + proveedor.InformacionContacto.TelefonoFijo + "','" + proveedor.InformacionContacto.TelefonoCelular + "')";
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    int idInformacionContacto = getIdInformacionContacto(proveedor.InformacionContacto.Correo);
                    transaction2 = sqlCon.BeginTransaction();
                    String query2 = "Insert into proveedor (nombre,identificacion,id_informacion_contacto) values ('" + proveedor.Nombre + "','"
                    + proveedor.Identificacion + "'," + idInformacionContacto + ")";

                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();

                    transaction2.Commit();

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
        // trae id proveedor
        public void suprimirProveedor(int idProveedor)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                Proveedor proveedor = getProveedorById(idProveedor);
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    String query = "Delete from proveedor  where id_proveedor=" + idProveedor + ";";
                    String query2 = "Delete from informacion_contacto where id_informacion_contacto= " + proveedor.InformacionContacto.IdInformacionContacto + ";";
                    

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();

                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();

                    transaction2.Commit();
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

        public void modificarProveedor(Proveedor proveedor)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    String query = "Update informacion_contacto set correo='" + proveedor.InformacionContacto.Correo + "',telefonoFijo='" +
                proveedor.InformacionContacto.TelefonoFijo + "',telefonoCelular='" + proveedor.InformacionContacto.TelefonoCelular
                + "' where id_informacion_contacto=" + proveedor.InformacionContacto.IdInformacionContacto;
                   
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    String query2 = "Update proveedor set nombre='" + proveedor.Nombre + "'," + "identificacion='" + proveedor.Identificacion + "' " +
                       " where id_proveedor= " + proveedor.IdProveedor;

                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();
                 
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
