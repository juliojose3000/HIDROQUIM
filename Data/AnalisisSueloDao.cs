using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class AnalisisSueloDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public AnalisisSueloDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<AnalisisQuimicoSuelo> getAllAnalisisSuelo()
        {
            List<AnalisisQuimicoSuelo> list = new List<AnalisisQuimicoSuelo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select codigo,determinacion,descripcion,cod_analisis_qumico from analisis_suelo;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimico analisisQuimico = null;
                    AnalisisQuimicoSuelo analisisQuimicoSuelo = null;
                    while (reader.Read())
                    {

                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();
                        analisisQuimicoSuelo.IdSuelo = reader.GetInt32("codigo");
                        analisisQuimicoSuelo.Determinacion = reader.GetString("determinacion");
                        analisisQuimicoSuelo.Descripcion = reader.GetString("descripcion");
                        int codAnalisisQuimico = reader.GetInt32("cod_analisis_qumico");

                        analisisQuimicoSuelo.AnalisisQuimico = getAnalisisQuimicoById(codAnalisisQuimico);

                        list.Add(analisisQuimicoSuelo);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public void insertarNuevoAnalisisDeSuelo(AnalisisQuimicoSuelo analisisQuimicoSuelo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    String query = "Insert into analisis_quimico (fecha_Muestreo,fechaRegistro_Sistema_Automatico, fecha_resultado, ubicacion_geografica, id_cliente) values ('" + analisisQuimicoSuelo.AnalisisQuimico.FechaMuestreo.ToString("yyyy/MM/dd HH:mm:ss")
               + "','" + analisisQuimicoSuelo.AnalisisQuimico.FechaRegistroSistemaAutomatico.ToString("yyyy/MM/dd HH:mm:ss") + "','" + analisisQuimicoSuelo.AnalisisQuimico.FechaResultado.ToString("yyyy/MM/dd HH:mm:ss") + "','" + analisisQuimicoSuelo.AnalisisQuimico.UbicacionGeografica +
               "','" + analisisQuimicoSuelo.AnalisisQuimico.Cliente.IdCliente + "');";

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();
                    int idAnalisisQuimico = getIdAnalisisQuimico();

                    String query2 = "insert into analisis_suelo (codigo,determinacion,descripcion,cod_analisis_qumico) values (" + analisisQuimicoSuelo.IdSuelo + ",'"
                    + analisisQuimicoSuelo.Determinacion + "','" + analisisQuimicoSuelo.Descripcion + "'," + idAnalisisQuimico + ");";

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
        public int getIdAnalisisQuimico()
        {
            int idAnalisisQuimico = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "select codigo from analisis_quimico order by codigo desc limit 1;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idAnalisisQuimico = reader.GetInt32("codigo");
                    }

                }
                sqlCon.Close();
            }
            return idAnalisisQuimico;
        }


        public void suprimirAnalisisSuelo(int id)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {


                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;

                try
                {
                    sqlCon.Open();
                    String query = "Delete from analisis_quimico where codigo= " + id + ";";
                    String query2 = "Delete from analisis_suelo  where codigo=" + id + ";";

                    transaction = sqlCon.BeginTransaction();

                    MySqlCommand sqlSelect = new MySqlCommand(query2, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query, sqlCon);
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


        public void modificarAnalisisSuelo(AnalisisQuimicoSuelo analisisQuimicoSuelo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();

                    String query = "Update analisis_quimico set fecha_Muestreo='" + analisisQuimicoSuelo.AnalisisQuimico.FechaMuestreo.ToString("yyyy/MM/dd HH:mm:ss") + "',fechaRegistro_Sistema_Automatico='" + analisisQuimicoSuelo.AnalisisQuimico.FechaRegistroSistemaAutomatico.ToString("yyyy/MM/dd HH:mm:ss") +
                        "',fecha_resultado='" + analisisQuimicoSuelo.AnalisisQuimico.FechaResultado.ToString("yyyy/MM/dd HH:mm:ss") + "',ubicacion_geografica='" + analisisQuimicoSuelo.AnalisisQuimico.UbicacionGeografica
                        + "' where codigo=" + analisisQuimicoSuelo.AnalisisQuimico.Codigo;

                    String query2 = "Update analisis_suelo set determinacion='" + analisisQuimicoSuelo.Determinacion + "',descripcion='" +
                     analisisQuimicoSuelo.Descripcion
                     + "' where codigo=" + analisisQuimicoSuelo.IdSuelo;

                    transaction = sqlCon.BeginTransaction();

                    MySqlCommand sqlSelect = new MySqlCommand(query2, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query, sqlCon);
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

        public AnalisisQuimicoSuelo getAnalisisSueloById(int id)
        {
            AnalisisQuimicoSuelo analisisQuimicoSuelo = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select codigo,determinacion,descripcion,cod_analisis_qumico from analisis_suelo where codigo = " + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();
                        analisisQuimicoSuelo.IdSuelo = reader.GetInt32("codigo");
                        analisisQuimicoSuelo.Determinacion = reader.GetString("determinacion");
                        analisisQuimicoSuelo.Descripcion = reader.GetString("descripcion");
                        int codAnalisisQuimico = reader.GetInt32("cod_analisis_qumico");

                        analisisQuimicoSuelo.AnalisisQuimico = getAnalisisQuimicoById(codAnalisisQuimico);
                    }
                    sqlCon.Close();
                }
                return analisisQuimicoSuelo;
            }
        }
        public List<AnalisisQuimicoSuelo> getAnalisisSueloByIdCliente(int idCliente)
        {
            List<AnalisisQuimicoSuelo> analisisSuelos = new List<AnalisisQuimicoSuelo>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                
                sqlCon.Open();
                String query = "select a.codigo as 'CodigoSuelo',a.determinacion,a.descripcion,aq.codigo, " +
                    " aq.fecha_Muestreo, aq.fechaRegistro_Sistema_Automatico,aq.fecha_resultado,aq.ubicacion_geografica,c.id_cliente," +
                    " c.nombre_completo, c.username, c.password " +
                    " from analisis_suelo a, analisis_quimico aq,cliente c" +
                    " where c.id_cliente= aq.id_cliente AND a.cod_analisis_qumico= aq.codigo AND c.id_cliente=" + idCliente+";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimicoSuelo analisisQuimicoSuelo = null;
                    AnalisisQuimico analisisQuimico = null;
                    Cliente cliente = null;
                    while (reader.Read())
                    {

                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();
                        analisisQuimico = new AnalisisQuimico();
                        cliente = new Cliente();

                        analisisQuimicoSuelo.IdSuelo = reader.GetInt32("CodigoSuelo");
                        analisisQuimicoSuelo.Determinacion = reader.GetString("determinacion");
                        analisisQuimicoSuelo.Descripcion = reader.GetString("descripcion");
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        cliente.IdCliente= reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisisQuimico.Cliente = cliente;
                        analisisQuimicoSuelo.AnalisisQuimico = analisisQuimico;
                        analisisSuelos.Add(analisisQuimicoSuelo);
                    }
                    sqlCon.Close();
                }
                return analisisSuelos;
            }
        }
        public List<AnalisisQuimicoSuelo> getAnalisisAireByIdClienteAndDeterminante(string determinante,int idCliente)
        {
            List<AnalisisQuimicoSuelo> analisisSuelos = new List<AnalisisQuimicoSuelo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select a.codigo as 'CodigoSuelo',a.determinacion,a.descripcion,aq.codigo, " +
                    " aq.fecha_Muestreo, aq.fechaRegistro_Sistema_Automatico,aq.fecha_resultado,aq.ubicacion_geografica,c.id_cliente," +
                    " c.nombre_completo, c.username, c.password " +
                    " from analisis_suelo a, analisis_quimico aq,cliente c" +
                    " where c.id_cliente= aq.id_cliente AND a.cod_analisis_qumico= aq.codigo AND c.id_cliente=" + idCliente +
                    " AND a.determinacion LIKE '"+ determinante + "%';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimicoSuelo analisisQuimicoSuelo = null;
                    AnalisisQuimico analisisQuimico = null;
                    Cliente cliente = null;
                    while (reader.Read())
                    {

                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();
                        analisisQuimico = new AnalisisQuimico();
                        cliente = new Cliente();

                        analisisQuimicoSuelo.IdSuelo = reader.GetInt32("CodigoSuelo");
                        analisisQuimicoSuelo.Determinacion = reader.GetString("determinacion");
                        analisisQuimicoSuelo.Descripcion = reader.GetString("descripcion");
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisisQuimico.Cliente = cliente;
                        analisisQuimicoSuelo.AnalisisQuimico = analisisQuimico;
                        analisisSuelos.Add(analisisQuimicoSuelo);
                    }
                    sqlCon.Close();
                }
                return analisisSuelos;
            }
        }
        public AnalisisQuimico getAnalisisQuimicoByIdCliente(int id, int idCliente)
        {
            AnalisisQuimico analisisQuimico;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select codigo,fecha_Muestreo,fechaRegistro_Sistema_Automatico,fecha_resultado,ubicacion_geografica from analisis_quimico where codigo=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    analisisQuimico = new AnalisisQuimico();
                    while (reader.Read())
                    {
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        //analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        //analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        //analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                   
                        analisisQuimico.Cliente = getClienteById(idCliente);
                    }
                    sqlCon.Close();
                }
            }
            return analisisQuimico;

        }

        public AnalisisQuimico getAnalisisQuimicoById(int id)
        {
            AnalisisQuimico analisisQuimico;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select codigo,fecha_Muestreo,fechaRegistro_Sistema_Automatico,fecha_resultado,ubicacion_geografica,Id_cliente from analisis_quimico where codigo=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    analisisQuimico = new AnalisisQuimico();
                    while (reader.Read())
                    {
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        //analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        //analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        //analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        int idCliente = reader.GetInt32("Id_cliente");

                        analisisQuimico.Cliente = getClienteById(idCliente);
                    }
                    sqlCon.Close();
                }
            }
            return analisisQuimico;

        }

        public Cliente getClienteById(int id)
        {
            Cliente cliente;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select id_cliente,nombre_completo from cliente where id_cliente =" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    cliente = new Cliente();
                    while (reader.Read())
                    {
                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");



                    }
                    sqlCon.Close();
                }
            }
            return cliente;

        }

        public List<AnalisisQuimicoSuelo> getAnalisisByName(String nombre)
        {
            List<AnalisisQuimicoSuelo> list = new List<AnalisisQuimicoSuelo>();
            AnalisisQuimicoSuelo analisisQuimicoSuelo = null;

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select codigo,determinacion,descripcion,cod_analisis_qumico from analisis_suelo where descripcion like '" + nombre + "%';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();

                        analisisQuimicoSuelo = new AnalisisQuimicoSuelo();
                        analisisQuimicoSuelo.IdSuelo = reader.GetInt32("codigo");
                        analisisQuimicoSuelo.Determinacion = reader.GetString("determinacion");
                        analisisQuimicoSuelo.Descripcion = reader.GetString("descripcion");
                        int codAnalisisQuimico = reader.GetInt32("cod_analisis_qumico");

                        analisisQuimicoSuelo.AnalisisQuimico = getAnalisisQuimicoById(codAnalisisQuimico);

                        list.Add(analisisQuimicoSuelo);
                    }
                    sqlCon.Close();
                }
               

            }
            return list;

        }
    }
}