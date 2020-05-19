using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class EncargadoLogueadoData
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public EncargadoLogueadoData()
        {
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public EncargadoLogueado insertarEncargadoLogueado(Encargado encargado)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query1 = "Insert into encargado_logueado (id_encargado,user_name,password, nombre,apellidos) values ("
                    + encargado.IdEncargado + ",'" + encargado.UserName + "','" + encargado.Password 
                    +"','"+ encargado.Nombre+"','"+ encargado.Apellidos+"');";

                MySqlCommand sqlSelect = new MySqlCommand(query1, sqlCon);
                sqlSelect.ExecuteNonQuery();
                sqlCon.Close();
                EncargadoLogueado encargadoLogueado = new EncargadoLogueado(encargado.IdEncargado, encargado.UserName, encargado.Password, encargado.Nombre, encargado.Apellidos);
                return encargadoLogueado;
            }

        }

        public EncargadoLogueado getEncargadoLogin(string username, string password)
        {
            EncargadoLogueado encargado;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_encargado,user_name,password,nombre,apellidos from encargado_logueado where user_name='" + username +
                    "' and password='" + password + "';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    encargado = null;
                    while (reader.Read())
                    {
                        encargado = new EncargadoLogueado();
                        encargado.IdEncargado = reader.GetInt32("id_encargado");
                        encargado.UserName = reader.GetString("user_name");
                        encargado.Password = reader.GetString("password");
                        encargado.Nombre = reader.GetString("nombre");
                        encargado.Apellidos = reader.GetString("apellidos");

                    }
                    sqlCon.Close();
                }

            }
            return encargado;
        }

        public void suprimirEncargadoLogueado(int idEncargado)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                EncargadoLogueado encargadoLogueado = getEncargadoLogueadoByID(idEncargado);

                sqlCon.Open();
                String query = "Delete from encargado_logueado where id_encargado= " + encargadoLogueado.IdEncargado + ";";


                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();


                sqlCon.Close();
            }
        }
        public EncargadoLogueado getEncargadoLogueadoByID(int id)
        {
            EncargadoLogueado encargadoLogueado;
            using (MySqlConnection sqlCon = GetConnection())
            {

            sqlCon.Open();
            String query = "Select id_encargado,user_name,password,nombre,apellidos from encargado_logueado where id_encargado=" + id;




                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    encargadoLogueado = null;

                    while (reader.Read())
                    {
                        encargadoLogueado = new EncargadoLogueado();
                        encargadoLogueado.IdEncargado = reader.GetInt32("id_encargado");
                        encargadoLogueado.UserName = reader.GetString("user_name");
                        encargadoLogueado.Password = reader.GetString("password");
                        encargadoLogueado.Nombre = reader.GetString("nombre");
                        encargadoLogueado.Apellidos = reader.GetString("apellidos");


                    }
                    sqlCon.Close();
                }

            }
            return encargadoLogueado;

        }

        public EncargadoLogueado getEncargadoLogueado()
        {
            EncargadoLogueado encargadoLogueado;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select id_encargado,user_name,password,nombre,apellidos from encargado_logueado; ";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    encargadoLogueado = null;

                    while (reader.Read())
                    {
                        encargadoLogueado = new EncargadoLogueado();
                        encargadoLogueado.IdEncargado = reader.GetInt32("id_encargado");
                        encargadoLogueado.UserName = reader.GetString("user_name");
                        encargadoLogueado.Password = reader.GetString("password");
                        encargadoLogueado.Nombre = reader.GetString("nombre");
                        encargadoLogueado.Apellidos = reader.GetString("apellidos");


                    }
                    sqlCon.Close();
                }

            }
            return encargadoLogueado;

        }
        public Boolean existeUsuarioLogueado()
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select id_encargado,user_name,password from encargado_logueado; ";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                    sqlCon.Close();
                }

            }
            return false;
        }
    }
}