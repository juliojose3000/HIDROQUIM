using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class AnalisisAireDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        public AnalisisAireDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<AnalisisQuimicoAire> getAllAnalisisAire()
        {
            List<AnalisisQuimicoAire> list = new List<AnalisisQuimicoAire>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select q.codigo, q.fecha_Muestreo, q.fechaRegistro_Sistema_Automatico, q.fecha_resultado, q.ubicacion_geografica," +
                    " c.nombre_completo,c.id_cliente, a.contaminante,a.tipo_estandar, a.valor_referencia, a.tiempo_promedio, a.metodoReferencia,a.id_aire," +
                    " a.resultado_analisis from " +
                    " analisis_quimico q, cliente c, analisis_aire a where c.id_cliente = q.Id_cliente AND a.cod_analisis_qumico = q.codigo;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimico analisisQuimico = null;
                    AnalisisQuimicoAire analisisQuimicoAire = null;
                    while (reader.Read())
                    {
                        analisisQuimico = new AnalisisQuimico();
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        int codAnalisisQuimico = reader.GetInt32("codigo");
                        analisisQuimico.FechaMuestreo = getFechaMuestreo(codAnalisisQuimico);
                        analisisQuimico.FechaResultado = getFechaResultado(codAnalisisQuimico);
                        analisisQuimico.FechaRegistroSistemaAutomatico = getFechaSistema(codAnalisisQuimico);

                        analisisQuimicoAire = new AnalisisQuimicoAire();
                        analisisQuimicoAire.IdAire = reader.GetInt32("id_aire");
                        analisisQuimicoAire.Contaminante = reader.GetString("contaminante");
                        analisisQuimicoAire.TipoEstandar = reader.GetString("tipo_estandar");
                        analisisQuimicoAire.ValorReferencia = reader.GetFloat("valor_referencia");
                        analisisQuimicoAire.TiempoPromedio = reader.GetFloat("tiempo_promedio");
                        analisisQuimicoAire.MetodoReferencia = reader.GetString("metodoReferencia");
                        analisisQuimicoAire.ResultadoAnalisis = reader.GetString("resultado_analisis");

                        analisisQuimicoAire.AnalisisQuimico = analisisQuimico;

                        list.Add(analisisQuimicoAire);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public DateTime getFechaMuestreo(int id)
        {
            DateTime fechaMuestreo = new DateTime();

            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "select year(fecha_Muestreo) as year, month(fecha_Muestreo) as month, day(fecha_Muestreo) as day, hour(fecha_Muestreo) as hour, minute(fecha_Muestreo) as minutes, second(fecha_Muestreo) as seconds from analisis_quimico where codigo=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    fechaMuestreo = new DateTime();
                    while (reader.Read())
                    {
                        int year = reader.GetInt32("year");
                        int month = reader.GetInt32("month");
                        int date = reader.GetInt32("day");
                        int hour = reader.GetInt32("hour");
                        int minutes = reader.GetInt32("minutes");
                        int seconds = reader.GetInt32("seconds");
                        fechaMuestreo = new DateTime(year, month, date, hour, minutes, seconds);
                    }
                    sqlCon.Close();
                }
            }
            return fechaMuestreo;
        }

        public DateTime getFechaSistema(int id)
        {
            DateTime fechaSistema = new DateTime();

            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select year(fechaRegistro_Sistema_Automatico) as year, month(fechaRegistro_Sistema_Automatico) as month, day(fechaRegistro_Sistema_Automatico) as day, hour(fechaRegistro_Sistema_Automatico) as hour, minute(fechaRegistro_Sistema_Automatico) as minutes, second(fechaRegistro_Sistema_Automatico) as seconds from analisis_quimico where codigo=" + id + ";";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    fechaSistema = new DateTime();
                    while (reader.Read())
                    {
                        int year = reader.GetInt32("year");
                        int month = reader.GetInt32("month");
                        int date = reader.GetInt32("day");
                        int hour = reader.GetInt32("hour");
                        int minutes = reader.GetInt32("minutes");
                        int seconds = reader.GetInt32("seconds");
                        fechaSistema = new DateTime(year, month, date, hour, minutes, seconds);
                    }
                    sqlCon.Close();
                }
            }
            return fechaSistema;
        }

        public DateTime getFechaResultado(int id)
        {
            DateTime fechaResultado = new DateTime();

            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select year(fecha_resultado) as year, month(fecha_resultado) as month, day(fecha_resultado) as day, hour(fecha_resultado) as hour, minute(fecha_resultado) as minutes, second(fecha_resultado) as seconds from analisis_quimico where codigo=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    fechaResultado = new DateTime();
                    while (reader.Read())
                    {
                        int year = reader.GetInt32("year");
                        int month = reader.GetInt32("month");
                        int date = reader.GetInt32("day");
                        int hour = reader.GetInt32("hour");
                        int minutes = reader.GetInt32("minutes");
                        int seconds = reader.GetInt32("seconds");
                        fechaResultado = new DateTime(year, month, date, hour, minutes, seconds);
                    }
                    sqlCon.Close();
                }
            }
            return fechaResultado;
        }


       
        public void insertarNuevoAnalisisDeAire(AnalisisQuimicoAire analisisQuimicoAire)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    String query = "Insert into analisis_quimico (fecha_Muestreo,fechaRegistro_Sistema_Automatico, fecha_resultado, ubicacion_geografica, " +
                        "id_cliente) values ('" + analisisQuimicoAire.AnalisisQuimico.FechaMuestreo.ToString("yyyy/MM/dd")
                     + "',NOW(),'" + analisisQuimicoAire.AnalisisQuimico.FechaResultado.ToString("yyyy/MM/dd") + "','" + analisisQuimicoAire.AnalisisQuimico.UbicacionGeografica +
                     "'," + analisisQuimicoAire.AnalisisQuimico.Cliente.IdCliente + ");";

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();
                    int idAnalisisQuimico = getIdAnalisisQuimico(analisisQuimicoAire.AnalisisQuimico.UbicacionGeografica);

                    String query2 = "Insert into analisis_aire (cod_analisis_qumico, contaminante,tipo_estandar,valor_referencia,tiempo_promedio," +
                        "metodoReferencia,resultado_analisis) values (" + idAnalisisQuimico + ",'"
                    + analisisQuimicoAire.Contaminante + "','" + analisisQuimicoAire.TipoEstandar + "'," +
                    analisisQuimicoAire.ValorReferencia + "," + analisisQuimicoAire.TiempoPromedio + ",'" +
                    analisisQuimicoAire.MetodoReferencia + "','" +
                    analisisQuimicoAire.ResultadoAnalisis + "');";

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
        public int getIdAnalisisQuimico(string ubicacion)
        {
            int idAnalisisQuimico = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select codigo from  analisis_quimico where ubicacion_geografica='" + ubicacion + "'";
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

       

        public void suprimirAnalisisAire(int id)
        {
            AnalisisQuimicoAire analisisQuimicoAire = getAnalisisAireById(id);
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    String query = "Delete from analisis_quimico where codigo= " + analisisQuimicoAire.AnalisisQuimico.Codigo + ";";
                    String query2 = "Delete from analisis_aire  where id_aire=" + id + ";";

                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query2, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query, sqlCon);
                    sqlSelect2.Transaction = transaction;
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

        public void modificarAnalisisAire(AnalisisQuimicoAire analisisQuimicoAire)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;

                try
                {
                    sqlCon.Open();

                    String query = "Update analisis_quimico set fecha_Muestreo='" + analisisQuimicoAire.AnalisisQuimico.FechaMuestreo.ToString("yyyy/MM/dd") +
                        "',fecha_resultado='" + analisisQuimicoAire.AnalisisQuimico.FechaResultado.ToString("yyyy/MM/dd") + "',ubicacion_geografica='" + analisisQuimicoAire.AnalisisQuimico.UbicacionGeografica
                        + "' where codigo=" + analisisQuimicoAire.AnalisisQuimico.Codigo + ";";

                    String query2 = "Update analisis_aire set contaminante='" + analisisQuimicoAire.Contaminante + "',tipo_estandar='" +
                     analisisQuimicoAire.TipoEstandar + "',valor_referencia=" + analisisQuimicoAire.ValorReferencia +
                     ",tiempo_promedio=" + analisisQuimicoAire.TiempoPromedio + ",metodoReferencia='" + analisisQuimicoAire.MetodoReferencia + "',resultado_analisis='" + analisisQuimicoAire.ResultadoAnalisis
                     + "' where id_aire=" + analisisQuimicoAire.IdAire;

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
    

    public AnalisisQuimicoAire getAnalisisAireById(int id)
    {
        AnalisisQuimicoAire analisis = null;
        using (MySqlConnection sqlCon = GetConnection())
        {

            sqlCon.Open();

            String query = "Select  a.contaminante,a.tipo_estandar, a.valor_referencia, a.tiempo_promedio, a.metodoReferencia," +
                " a.resultado_analisis,a.id_aire,q.codigo,q.codigo, q.fecha_Muestreo, q.fechaRegistro_sistema_automatico, q.fecha_resultado, q.ubicacion_geografica, q.Id_cliente" +
                " from  analisis_aire a, analisis_quimico q where a.cod_analisis_qumico= q.codigo AND a.id_aire=" + id + "";
            MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
            using (MySqlDataReader reader = sqlSelect.ExecuteReader())

            {

                while (reader.Read())
                {
                    analisis = new AnalisisQuimicoAire();
                    analisis.IdAire = id;
                    analisis.Contaminante = reader.GetString("contaminante");
                    analisis.TipoEstandar = reader.GetString("tipo_estandar");
                    analisis.ValorReferencia = reader.GetFloat("valor_referencia");
                    analisis.TiempoPromedio = reader.GetFloat("tiempo_promedio");
                    analisis.MetodoReferencia = reader.GetString("metodoReferencia");
                    analisis.ResultadoAnalisis = reader.GetString("resultado_analisis");

                    analisis.AnalisisQuimico.Codigo = reader.GetInt32("codigo");
                    analisis.AnalisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                    analisis.AnalisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_sistema_automatico");
                    analisis.AnalisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                    analisis.AnalisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");

                }

            }
            sqlCon.Close();
        }
        return analisis;
    }

    public List<AnalisisQuimicoAire> getAnalisisAireByIdCliente(int idCliente)
        {
            List<AnalisisQuimicoAire> listaAnalisis = new List<AnalisisQuimicoAire>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select a.id_aire,  a.contaminante,a.tipo_estandar, a.valor_referencia, a.tiempo_promedio, a.metodoReferencia," +
                    " a.resultado_analisis,q.codigo, q.fecha_Muestreo, q.fechaRegistro_sistema_automatico, " +
                    "q.fecha_resultado, q.ubicacion_geografica, c.id_cliente, c.nombre_completo,c.username,c.password" +
                    " from  analisis_aire a, analisis_quimico q,cliente c where a.cod_analisis_qumico= q.codigo" +
                    " AND c.id_cliente=" + idCliente + " AND c.id_cliente = q.id_cliente";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())

                {
                    AnalisisQuimicoAire analisis = null;
                    Cliente cliente = null;
                    while (reader.Read())
                    {
                        analisis = new AnalisisQuimicoAire();
                        cliente = new Cliente();
                        analisis.IdAire = reader.GetInt32("id_aire");
                        analisis.Contaminante = reader.GetString("contaminante");
                        analisis.TipoEstandar = reader.GetString("tipo_estandar");
                        analisis.ValorReferencia = reader.GetFloat("valor_referencia");
                        analisis.TiempoPromedio = reader.GetFloat("tiempo_promedio");
                        analisis.MetodoReferencia = reader.GetString("metodoReferencia");
                        analisis.ResultadoAnalisis = reader.GetString("resultado_analisis");

                        analisis.AnalisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisis.AnalisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisis.AnalisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_sistema_automatico");
                        analisis.AnalisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisis.AnalisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");

                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisis.AnalisisQuimico.Cliente = cliente;

                        listaAnalisis.Add(analisis);

                    }

                }
                sqlCon.Close();
            }
            return listaAnalisis;
        }
        public List<AnalisisQuimicoAire> getAnalisisByName(String nombre)
        {
            List<AnalisisQuimicoAire> list = new List<AnalisisQuimicoAire>();
            AnalisisQuimicoAire analisis = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select  a.contaminante,a.tipo_estandar, a.valor_referencia, a.tiempo_promedio, a.metodoReferencia," +
                    " a.resultado_analisis,a.id_aire,q.codigo,q.codigo, q.fecha_Muestreo, q.fechaRegistro_sistema_automatico, q.fecha_resultado, q.ubicacion_geografica, q.Id_cliente" +
                    " from  analisis_aire a, analisis_quimico q where a.cod_analisis_qumico= q.codigo AND a.id_aire like '" + nombre + "%'";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())

                {

                    while (reader.Read())
                    {
                        analisis = new AnalisisQuimicoAire();
                        analisis.Contaminante = reader.GetString("contaminante");
                        analisis.TipoEstandar = reader.GetString("tipo_estandar");
                        analisis.ValorReferencia = reader.GetFloat("valor_referencia");
                        analisis.TiempoPromedio = reader.GetFloat("tiempo_promedio");
                        analisis.MetodoReferencia = reader.GetString("metodoReferencia");
                        analisis.ResultadoAnalisis = reader.GetString("resultado_analisis");

                        analisis.AnalisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisis.AnalisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisis.AnalisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_sistema_automatico");
                        analisis.AnalisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisis.AnalisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");

                    }

                }
                sqlCon.Close();
            }


            return list;

        }

        public List<AnalisisQuimicoAire> getAnalisisAireByIdClienteAndContaminante(String contaminante,int idCliente)
        {
            List<AnalisisQuimicoAire> listaAnalisis = new List<AnalisisQuimicoAire>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();

                String query = "Select a.id_aire,  a.contaminante,a.tipo_estandar, a.valor_referencia, a.tiempo_promedio, a.metodoReferencia," +
                    " a.resultado_analisis,q.codigo, q.fecha_Muestreo, q.fechaRegistro_sistema_automatico, " +
                    "q.fecha_resultado, q.ubicacion_geografica, c.id_cliente, c.nombre_completo,c.username,c.password" +
                    " from  analisis_aire a, analisis_quimico q,cliente c where a.cod_analisis_qumico= q.codigo" +
                    " AND c.id_cliente=" + idCliente + " AND c.id_cliente = q.id_cliente AND a.contaminante LIKE '"+ contaminante + "%';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())

                {
                    AnalisisQuimicoAire analisis = null;
                    Cliente cliente = null;
                    while (reader.Read())
                    {
                        analisis = new AnalisisQuimicoAire();
                        cliente = new Cliente();
                        analisis.IdAire = reader.GetInt32("id_aire");
                        analisis.Contaminante = reader.GetString("contaminante");
                        analisis.TipoEstandar = reader.GetString("tipo_estandar");
                        analisis.ValorReferencia = reader.GetFloat("valor_referencia");
                        analisis.TiempoPromedio = reader.GetFloat("tiempo_promedio");
                        analisis.MetodoReferencia = reader.GetString("metodoReferencia");
                        analisis.ResultadoAnalisis = reader.GetString("resultado_analisis");

                        analisis.AnalisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisis.AnalisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisis.AnalisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_sistema_automatico");
                        analisis.AnalisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisis.AnalisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");

                        cliente.IdCliente = reader.GetInt32("id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisis.AnalisisQuimico.Cliente = cliente;

                        listaAnalisis.Add(analisis);

                    }

                }
                sqlCon.Close();
            }
            return listaAnalisis;
        }

    }
}