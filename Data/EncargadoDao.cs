using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class EncargadoDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public EncargadoDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<Encargado> getAllEncargados()
        {
            List<Encargado> list = new List<Encargado>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select id_encargado, user_name, password, cedula, nombre,apellidos,correo, telefono,id_rol from encargado;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Encargado encargado = new Encargado();
                    Rol rol = new Rol();
                    while (reader.Read())
                    {
                        encargado = new Encargado();
                        encargado.IdEncargado = reader.GetInt32("id_encargado");
                        encargado.UserName = reader.GetString("user_name");
                        encargado.Password= reader.GetString("password");
                        encargado.Cedula = reader.GetString("cedula");
                        encargado.Nombre = reader.GetString("nombre");
                        encargado.Apellidos = reader.GetString("apellidos");
                        encargado.Correo = reader.GetString("correo");
                        encargado.Telefono = reader.GetString("telefono");
                        int idRol = reader.GetInt32("id_rol");
                   
                        encargado.Rol = getRoleById(idRol);

                        list.Add(encargado);
                    }
                    sqlCon.Close();
                }
            }
            return list;
        }

        public void insertarNuevoEmpleado(Encargado encargado)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "INSERT INTO encargado(id_encargado, user_name, password, cedula, nombre , apellidos, correo, telefono, id_rol)" +
                    " VALUES (0,'"+encargado.UserName+"', '"+encargado.Password+"', '"+encargado.Cedula+"','"+encargado.Nombre+
                    "','"+encargado.Apellidos+"','"+encargado.Correo+"','"+encargado.Telefono+"',"+encargado.Rol.IdRol+");";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();
                sqlCon.Close();
            }
        }

        public void suprimirEmpleado(int id)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Delete from encargado where id_encargado=" + id + ";";
                MySqlCommand sqlDelete = new MySqlCommand(query, sqlCon);
                sqlDelete.ExecuteNonQuery();
                sqlCon.Close();
            }
        }

        public void modificarEncargado(Encargado encargado)
        {
            using (MySqlConnection sqlCon = GetConnection()) {
                sqlCon.Open();
                String query = "Update encargado set user_name= '"+ encargado.UserName + "', password= '"+encargado.Password+ "', cedula='"+encargado.Cedula+"',"+
                    "nombre='"+encargado.Nombre+ "', apellidos= '"+encargado.Apellidos+ "', correo= '"+encargado.Correo+ "', telefono= '"+encargado.Telefono+"',"+
                    "id_rol= "+encargado.Rol.IdRol+ " where id_encargado = "+encargado.IdEncargado+";";

                MySqlCommand sqlModify = new MySqlCommand(query, sqlCon);
                sqlModify.ExecuteNonQuery();

                sqlCon.Close();

            }
        }

        public Encargado getEncargadoById(int id)
        {
            Encargado encargado = new Encargado();
            Rol rol = new Rol();

            using (MySqlConnection sqlCon = GetConnection()) {
                sqlCon.Open();

                String query = "select id_encargado, user_name, password, cedula, nombre,apellidos,correo, telefono,id_rol from encargado where id_encargado = " + id+";";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader()) {
                    while(reader.Read())
                    {
                        encargado = new Encargado();
                        encargado.IdEncargado = reader.GetInt32("id_encargado");
                        encargado.UserName = reader.GetString("user_name");
                        encargado.Password = reader.GetString("password");
                        encargado.Cedula = reader.GetString("cedula");
                        encargado.Nombre = reader.GetString("nombre");
                        encargado.Apellidos = reader.GetString("apellidos");
                        encargado.Correo = reader.GetString("correo");
                        encargado.Telefono = reader.GetString("telefono");
                        int idRol = reader.GetInt32("id_rol");

                        encargado.Rol = getRoleById(idRol);

                    }
                    sqlCon.Close();
                }
                return encargado;
            }
        }

        public Encargado getEncargadoByUserNameAndPassword(string userName,string password)
        {
            Encargado encargado = new Encargado();
            Rol rol = new Rol();

            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();

                String query = "select id_encargado, user_name, password, cedula, nombre,apellidos,correo, telefono,id_rol from encargado where user_name = '" +userName+
                    "' and password='"+password+"' ;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        encargado = new Encargado();
                        encargado.IdEncargado = reader.GetInt32("id_encargado");
                        encargado.UserName = reader.GetString("user_name");
                        encargado.Password = reader.GetString("password");
                        encargado.Cedula = reader.GetString("cedula");
                        encargado.Nombre = reader.GetString("nombre");
                        encargado.Apellidos = reader.GetString("apellidos");
                        encargado.Correo = reader.GetString("correo");
                        encargado.Telefono = reader.GetString("telefono");
                        int idRol = reader.GetInt32("id_rol");

                        encargado.Rol = getRoleById(idRol);

                    }
                    sqlCon.Close();
                }
                return encargado;
            }
        }

        public List<Rol> getAllRoles()
        {
            List<Rol> list = new List<Rol>();

            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select r.id_rol, r.rol_name from rol r;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Rol rol = null;
                    while (reader.Read())
                    {
                        rol = new Rol();
                        rol.IdRol = reader.GetInt32("id_rol");
                        rol.NombreRol = reader.GetString("rol_name");

                        list.Add(rol);

                    }
                    sqlCon.Close();
                }
            }
            return list;

        }

        public Rol getRoleById(int id)
        {
            Rol rol;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_rol, rol_name from rol where id_rol="+id+";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    rol = new Rol();
                    while (reader.Read())
                    {
                        rol.IdRol = reader.GetInt32("id_rol");
                        rol.NombreRol = reader.GetString("rol_name");
                        
                    }
                    sqlCon.Close();
                }
            }
            return rol;

        }
        public Rol getRoleByName(string name)
        {
            Rol rol;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_rol, rol_name from rol where rol_name='" + name + "';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    rol = new Rol();
                    while (reader.Read())
                    {
                        rol.IdRol = reader.GetInt32("id_rol");
                        rol.NombreRol = reader.GetString("rol_name");

                    }
                    sqlCon.Close();
                }
            }
            return rol;

        }
        public List<Encargado> getEncargadosByName(String nombre)
        {
            List<Encargado> list = new List<Encargado>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select id_encargado, user_name, password, cedula, nombre,apellidos,correo, telefono,id_rol from encargado where nombre like '" + nombre + "%';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Encargado encargado; ;

                    while (reader.Read())
                    {
                        encargado = new Encargado();
                        encargado.IdEncargado = reader.GetInt32("id_encargado");
                        encargado.UserName = reader.GetString("user_name");
                        encargado.Password = reader.GetString("password");
                        encargado.Cedula = reader.GetString("cedula");
                        encargado.Nombre = reader.GetString("nombre");
                        encargado.Apellidos = reader.GetString("apellidos");
                        encargado.Correo = reader.GetString("correo");
                        encargado.Telefono = reader.GetString("telefono");
                        int idRol = reader.GetInt32("id_rol");

                        encargado.Rol = getRoleById(idRol);

                        list.Add(encargado);
                    }
                    sqlCon.Close();
                }
            }
            return list;
        }


    }
}