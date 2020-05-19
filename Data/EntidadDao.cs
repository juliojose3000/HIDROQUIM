using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class EntidadDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public EntidadDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public List<Entidad> getAllEntidades()
        {
            List<Entidad> list = new List<Entidad>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select e.id_entidad, e.id_cliente,e.cedulaJuridica,e.idResponsable_legal, e.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular, c.nombre_completo, c.username, c.password, e.idResponsable_legal,r.idResponsable_legal , r.nombreCompleto, r.cedula, r.id_informacion_contacto,ir.id_informacion_contacto AS id_informacion_contactoResponsable, ir.correo AS correoResponsable,ir.telefonoFijo AS telefonoFijoResponsable,ir.telefonoCelular AS telefonoCelularResponsable, r.idResponsable_legal from " +
                               "informacion_contacto ir,entidad e, informacion_contacto i, cliente c, responsable_legal r where e.id_informacion_contacto= i.id_informacion_contacto and c.id_cliente= e.id_cliente and e.idResponsable_legal = r.idResponsable_legal and r.id_informacion_contacto =ir.id_informacion_contacto;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Entidad entidad = null;
                    InformacionContacto informacionContacto = null;
                    Cliente cliente = null;
                    ResponsableLegal responsableLegal = null;

                    while (reader.Read())
                    {
                        entidad = new Entidad();
                        entidad.IdEntidad = reader.GetInt32("id_entidad");
                        entidad.CedulaJuridica = reader.GetString("cedulaJuridica");
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
                        responsableLegal = new ResponsableLegal();
                        responsableLegal.Cedula = reader.GetString("cedula");
                        responsableLegal.NombreCompleto = reader.GetString("nombreCompleto");
                        responsableLegal.IdReponsableLegal = reader.GetInt32("idResponsable_legal");
                        responsableLegal.InformacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contactoResponsable");
                        responsableLegal.InformacionContacto.Correo = reader.GetString("correoResponsable");
                        responsableLegal.InformacionContacto.TelefonoFijo = reader.GetString("telefonoFijoResponsable");
                        responsableLegal.InformacionContacto.TelefonoCelular = reader.GetString("telefonoCelularResponsable");
                        entidad.InformacionContacto = informacionContacto;
                        entidad.Cliente = cliente;
                        entidad.ResponsableLegal = responsableLegal;
                        list.Add(entidad);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }
        public List<Entidad> getEntidadByName(String name)
        {

            InformacionContacto informacionContacto = null;
            Cliente cliente = null;
            ResponsableLegal responsable = null;
            Entidad entidad = null;
            List<Entidad> lista = new List<Entidad>();

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select e.id_entidad, e.id_cliente,e.cedulaJuridica,e.idResponsable_legal, e.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular, c.nombre_completo, c.username, c.password,r.idResponsable_legal , r.nombreCompleto, r.cedula,ir.id_informacion_contacto AS id_informacion_contactoResponsable, ir.correo AS correoResponsable,ir.telefonoFijo AS telefonoFijoResponsable,ir.telefonoCelular AS telefonoCelularResponsable, r.idResponsable_legal,  r.idResponsable_legal   " +
                  "from informacion_contacto ir, entidad e, informacion_contacto i, cliente c, responsable_legal r where e.id_informacion_contacto= i.id_informacion_contacto and c.id_cliente= e.id_cliente and e.idResponsable_legal = r.idResponsable_legal and r.id_informacion_contacto =ir.id_informacion_contacto and nombre_completo  LIKE '%" + name + "%';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {

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
                        responsable = new ResponsableLegal();
                        responsable.Cedula = reader.GetString("cedula");
                        responsable.NombreCompleto = reader.GetString("nombreCompleto");
                        responsable.IdReponsableLegal = reader.GetInt32("idResponsable_legal");
                        responsable.InformacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contactoResponsable");
                        responsable.InformacionContacto.Correo = reader.GetString("correoResponsable");
                        responsable.InformacionContacto.TelefonoFijo = reader.GetString("telefonoFijoResponsable");
                        responsable.InformacionContacto.TelefonoCelular = reader.GetString("telefonoCelularResponsable");
                        entidad = new Entidad();
                        entidad.IdEntidad = reader.GetInt32("id_entidad");
                        entidad.CedulaJuridica = reader.GetString("cedulaJuridica");
                        entidad.InformacionContacto = informacionContacto;
                        entidad.Cliente = cliente;
                        entidad.ResponsableLegal = responsable;
                        lista.Add(entidad);
                    }
                }
                sqlCon.Close();
            }
            return lista;
        }


        public Entidad getEntidadById(String id)
        {

            InformacionContacto informacionContacto = null;
            Cliente cliente = null;
            ResponsableLegal responsable = null;
            Entidad entidad = null;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select e.id_entidad, e.id_cliente,e.cedulaJuridica,e.idResponsable_legal, e.id_informacion_contacto, i.correo,i.telefonoFijo,i.telefonoCelular, c.nombre_completo, c.username, c.password,r.idResponsable_legal , r.nombreCompleto, r.cedula,ir.id_informacion_contacto AS id_informacion_contactoResponsable, ir.correo AS correoResponsable,ir.telefonoFijo AS telefonoFijoResponsable,ir.telefonoCelular AS telefonoCelularResponsable, r.idResponsable_legal,  r.idResponsable_legal   " +
                  "from informacion_contacto ir, entidad e, informacion_contacto i, cliente c, responsable_legal r where e.id_informacion_contacto= i.id_informacion_contacto and c.id_cliente= e.id_cliente and e.idResponsable_legal = r.idResponsable_legal and r.id_informacion_contacto =ir.id_informacion_contacto and cedulaJuridica ='" + id + "';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {

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
                        responsable = new ResponsableLegal();
                        responsable.Cedula = reader.GetString("cedula");
                        responsable.NombreCompleto = reader.GetString("nombreCompleto");
                        responsable.IdReponsableLegal = reader.GetInt32("idResponsable_legal");
                        responsable.InformacionContacto.IdInformacionContacto = reader.GetInt32("id_informacion_contactoResponsable");
                        responsable.InformacionContacto.Correo = reader.GetString("correoResponsable");
                        responsable.InformacionContacto.TelefonoFijo = reader.GetString("telefonoFijoResponsable");
                        responsable.InformacionContacto.TelefonoCelular = reader.GetString("telefonoCelularResponsable");
                        entidad = new Entidad();
                        entidad.IdEntidad = reader.GetInt32("id_entidad");
                        entidad.CedulaJuridica = reader.GetString("cedulaJuridica");
                        entidad.InformacionContacto = informacionContacto;
                        entidad.Cliente = cliente;
                        entidad.ResponsableLegal = responsable;
                    }

                }
                sqlCon.Close();

            }
            return entidad;
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
        public int getIdCliente(String nombreCompleto)
        {
            int idCliente = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select id_cliente from  cliente where nombre_completo='" + nombreCompleto + "'";
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
        public int getIdResponsableLegal(String cedula)
        {
            int idResponsableLegal = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select idResponsable_legal from  responsable_legal where cedula='" + cedula + "'";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idResponsableLegal = reader.GetInt32("idResponsable_legal");
                    }

                }
                sqlCon.Close();
            }
            return idResponsableLegal;
        }

        public void insertarEntidad(Entidad entidad)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Insert into informacion_contacto (correo,telefonoFijo,telefonoCelular) values ('" + entidad.InformacionContacto.Correo
                + "','" + entidad.InformacionContacto.TelefonoFijo + "','" + entidad.InformacionContacto.TelefonoCelular + "')";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();
                int idInformacionContacto = getIdInformacionContacto(entidad.InformacionContacto.Correo);

                String query2 = "Insert into cliente (nombre_completo,username,password) values ('" + entidad.Cliente.Nombre_cliente
                + "','" + entidad.Cliente.Username + "','" + entidad.Cliente.Password + "')";
                MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                sqlSelect2.ExecuteNonQuery();
                int idCliente = getIdCliente(entidad.Cliente.Nombre_cliente);

                String query3 = "Insert into informacion_contacto(correo,telefonoFijo,telefonoCelular) values ('" + entidad.ResponsableLegal.InformacionContacto.Correo +
                "','" + entidad.ResponsableLegal.InformacionContacto.TelefonoFijo + "','" + entidad.ResponsableLegal.InformacionContacto.TelefonoCelular + "')";
                MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                sqlSelect3.ExecuteNonQuery();
                int idInformacionContactoResponsableLegal = getIdInformacionContacto(entidad.ResponsableLegal.InformacionContacto.Correo);

                String query4 = "Insert into responsable_legal (nombreCompleto,cedula, id_informacion_contacto) values ('" + entidad.ResponsableLegal.NombreCompleto
                + "','" + entidad.ResponsableLegal.Cedula + "','" + idInformacionContactoResponsableLegal + "')";
                MySqlCommand sqlSelect4 = new MySqlCommand(query4, sqlCon);
                sqlSelect4.ExecuteNonQuery();
                int idResponsableLegal = getIdResponsableLegal(entidad.ResponsableLegal.Cedula);


                String query5 = "Insert into entidad (id_cliente,cedulaJuridica,idResponsable_legal,id_informacion_contacto) values (" + idCliente + ",'" + entidad.CedulaJuridica
                + "'," + idResponsableLegal + "," + idInformacionContacto + ")";
                MySqlCommand sqlSelect5 = new MySqlCommand(query5, sqlCon);
                sqlSelect5.ExecuteNonQuery();
                sqlCon.Close();


            }

        }

        public void suprimirEntidad(String id)
        {
            Entidad entidad = getEntidadById(id);
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Delete from informacion_contacto where id_informacion_contacto= " + entidad.InformacionContacto.IdInformacionContacto + ";";
                String query2 = "Delete from entidad  where id_entidad=" + entidad.IdEntidad + ";";
                String query3 = "Delete from responsable_legal  where idResponsable_legal =" + entidad.ResponsableLegal.IdReponsableLegal + ";";
                String query4 = "Delete from informacion_contacto  where id_informacion_contacto =" + entidad.ResponsableLegal.InformacionContacto.IdInformacionContacto + ";";
                String query5 = "Delete from cliente  where id_cliente =" + entidad.Cliente.IdCliente + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query2, sqlCon);
                sqlSelect.ExecuteNonQuery();

                MySqlCommand sqlSelect2 = new MySqlCommand(query, sqlCon);
                sqlSelect2.ExecuteNonQuery();

                MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                sqlSelect3.ExecuteNonQuery();

                MySqlCommand sqlSelect4 = new MySqlCommand(query4, sqlCon);
                sqlSelect4.ExecuteNonQuery();

                MySqlCommand sqlSelect5 = new MySqlCommand(query5, sqlCon);
                sqlSelect5.ExecuteNonQuery();

                sqlCon.Close();
            }
        }

        public void modificarEntidad(Entidad entidad)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();

                String query2 = "Update informacion_contacto set correo='" + entidad.InformacionContacto.Correo + "',telefonoFijo='" +
                entidad.InformacionContacto.TelefonoFijo + "',telefonoCelular='" + entidad.InformacionContacto.TelefonoCelular
                 + "' where id_informacion_contacto=" + entidad.InformacionContacto.IdInformacionContacto;

                String query3 = "Update cliente set nombre_completo='" + entidad.Cliente.Nombre_cliente + "',username='" +
               entidad.Cliente.Username + "',password='" + entidad.Cliente.Password
                 + "' where id_cliente=" + entidad.Cliente.IdCliente;

                String query4 = "Update responsable_legal set nombreCompleto='" + entidad.ResponsableLegal.NombreCompleto + "',cedula='" +
               entidad.ResponsableLegal.Cedula + "'" + "where idResponsable_legal=" + entidad.ResponsableLegal.IdReponsableLegal;

                String query5 = "Update informacion_contacto set correo='" + entidad.ResponsableLegal.InformacionContacto.Correo + "',telefonoFijo='" +
                     entidad.ResponsableLegal.InformacionContacto.TelefonoFijo + "',telefonoCelular='" + entidad.ResponsableLegal.InformacionContacto.TelefonoCelular
                      + "' where id_informacion_contacto=" + entidad.ResponsableLegal.InformacionContacto.IdInformacionContacto;



                MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                sqlSelect2.ExecuteNonQuery();

                MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                sqlSelect3.ExecuteNonQuery();

                MySqlCommand sqlSelect4 = new MySqlCommand(query4, sqlCon);
                sqlSelect4.ExecuteNonQuery();

                MySqlCommand sqlSelect5 = new MySqlCommand(query5, sqlCon);
                sqlSelect5.ExecuteNonQuery();



                sqlCon.Close();
            }
        }
    }
}

