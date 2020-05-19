using HIDROQUIM.Models;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class AguaPotableDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        ClienteData clienteData = new ClienteData();
        //JUAN MODIFICO EL STRING DE CONEXION 
        public AguaPotableDao()
        {
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<AnalisisQuimicoAguaPotable> getAllAguaPotable()
        {
            List<AnalisisQuimicoAguaPotable> list = new List<AnalisisQuimicoAguaPotable>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.nombre_completo, c.id_cliente, a.id_agua_potable, a.resultado_analisis, q.fecha_Muestreo,q.fechaRegistro_Sistema_Automatico,q.fecha_resultado, q.ubicacion_geografica, q.codigo from analisisagua_potable a, analisis_quimico q, cliente c where q.codigo= a.cod_analisis_qumico and q.Id_cliente = c.id_cliente;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimico analisisQuimico = null;
                    AnalisisQuimicoAguaPotable analisisAgua = null;

                    while (reader.Read())
                    {

                        analisisQuimico = new AnalisisQuimico();
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");


                        Cliente cliente = clienteData.GetClientById(reader.GetInt32("id_cliente"));

                        analisisAgua = new AnalisisQuimicoAguaPotable();
                        analisisAgua.IdAguaPotable = reader.GetInt32("id_agua_potable");
                        analisisQuimico.Cliente = cliente;
                        analisisAgua.AnalisisQuimico = analisisQuimico;
                        analisisAgua.Resultado = reader.GetString("resultado_analisis");

                        list.Add(analisisAgua);
                    }
                    sqlCon.Close();
                }

            }
            return list;

        }

        public int getCodigoAnalisisQ(String fecha, int cliente)
        {
            int codigo = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                //las fechas son iguales pero se dan vuelta me parece
                String query = "Select codigo from  analisis_quimico where Fecha_Muestreo='" + fecha + "' and Id_cliente= '" + cliente + "'";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        codigo = reader.GetInt32("codigo");
                    }

                }
                sqlCon.Close();
            }
            return codigo;
        }

        public AnalisisQuimicoAguaPotable getAnalisisPotableById(int id)
        {
            AnalisisQuimico analisisQuimico = null;
            AnalisisQuimicoAguaPotable analisisAgua = null;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select a.id_agua_potable,a.resultado_analisis, q.codigo, q.fecha_Muestreo,q.fechaRegistro_Sistema_Automatico, q.fecha_resultado, q.ubicacion_geografica, c.nombre_completo from cliente c, analisisagua_potable a, analisis_quimico q where q.Id_cliente=c.id_cliente AND q.codigo= a.cod_analisis_qumico AND a.id_agua_potable=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        analisisQuimico = new AnalisisQuimico();
                        Cliente cliente = new Cliente();
                        analisisQuimico.Codigo = reader.GetInt32("codigo");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        analisisQuimico.Cliente = cliente;
                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        analisisAgua = new AnalisisQuimicoAguaPotable();
                        analisisAgua.IdAguaPotable = reader.GetInt32("id_agua_potable");
                        analisisAgua.Resultado = reader.GetString("resultado_analisis");
                        analisisAgua.AnalisisQuimico = analisisQuimico;

                    }
                    sqlCon.Close();
                }

            }
            return analisisAgua;
        }

        public void insertarAnalisisQuimicoAguaPotable(AnalisisQuimicoAguaPotable aguaPotable)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {

            }

        }

        public void suprimirAnalisisAgua(int id)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                String query = null;
                MySqlCommand sqlSelect = null;
                AnalisisQuimicoAguaPotable aguaPotable = getAnalisisPotableById(id);
                sqlCon.Open();
                query = "Delete from analisisagua_potable where id_agua_potable= " + aguaPotable.IdAguaPotable + ";";
                sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                query = "Delete from analisis_quimico  where codigo=" + aguaPotable.AnalisisQuimico.Codigo + ";";
                sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();
                sqlCon.Close();
            }
        }

        public void modificarAnalisisAgua(AnalisisQuimicoAguaPotable aguaPotable)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();

                String query = "Update analisis_quimico set fecha_resultado='" + aguaPotable.AnalisisQuimico.FechaResultado + "' where codigo=" + aguaPotable.AnalisisQuimico.Codigo + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                //---------------------------------------------------------------------------------//

                String query2 = "update analisisagua_potable set resultado_analisis='" + aguaPotable.Resultado + "' where id_agua_potable = " + aguaPotable.IdAguaPotable + ";";

                MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                sqlSelect2.ExecuteNonQuery();


                sqlCon.Close();
            }
        }






        public void insert(AnalisisQuimico analisisQuimico, AnalisisQuimicoAguaPotable analisisQuimicoAguaPotable)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "INSERT INTO analisis_quimico(fecha_Muestreo, fechaRegistro_Sistema_Automatico, ubicacion_geografica, Id_cliente) " +
                    "values ('" + analisisQuimico.FechaMuestreo.ToString("yyyy/MM/dd HH:mm:ss") + "','" + analisisQuimico.FechaRegistroSistemaAutomatico.ToString("yyyy/MM/dd H:mm:ss") + "','" + analisisQuimico.UbicacionGeografica + "'," + analisisQuimico.Cliente.IdCliente + ");";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                sqlSelect.ExecuteNonQuery();

                //-----------------------------------------------------------------//

                int codAnalisis = 0;

                query = "SELECT codigo FROM analisis_quimico ORDER BY codigo DESC LIMIT 1;";

                sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        codAnalisis = reader.GetInt32("codigo");

                    }

                }


                //-----------------------------------------------------------------//

                query = "INSERT INTO analisisagua_potable(cod_analisis_qumico, resultado_analisis) " +
                    "values (" + codAnalisis + ",'" + analisisQuimicoAguaPotable.Resultado + "');";

                sqlSelect = new MySqlCommand(query, sqlCon);

                sqlSelect.ExecuteNonQuery();

                //-----------------------------------------------------------------//

                int idAguaPotable = 0;

                query = "SELECT id_agua_potable FROM analisisagua_potable ORDER BY id_agua_potable DESC LIMIT 1;";

                sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idAguaPotable = reader.GetInt32("id_agua_potable");

                    }

                }

                //----------------------------------------------------------------------------------------//

                foreach (NivelAguaPotable nivelAguaPotable in analisisQuimicoAguaPotable.NivelesAguaPotable)
                {
                    query = "insert into nivel_agua_potable(nombre_nivel, id_agua_potable) " +
                        "values ('" + nivelAguaPotable.Nombre + "'," + idAguaPotable + ");";

                    sqlSelect = new MySqlCommand(query, sqlCon);

                    sqlSelect.ExecuteNonQuery();

                    //----------------------------Id Nivel----------------------------------//


                    int idNivel = 0;

                    query = "SELECT id_nivel_agua_potable FROM nivel_agua_potable ORDER BY id_nivel_agua_potable DESC LIMIT 1;";

                    sqlSelect = new MySqlCommand(query, sqlCon);

                    using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            idNivel = reader.GetInt32("id_nivel_agua_potable");

                        }

                    }


                    //---------------------------Parametros--------------------------------//

                    foreach (Parametro parametros in nivelAguaPotable.Parametros)
                    {
                        query = "insert into parametro_nivel_agua_potable(parametro, unidad, valor_Recomendado, valor_Maximo_Admisible, valor_obtenido, id_nivel) " +
                            "values ('" + parametros.NombreParametro + "','" + parametros.Unidad + "','" + parametros.ValorRecomendado + "','" + parametros.ValorMaximoAdmisible + "','" + parametros.ValorObtenido + "'," + idNivel + ");";

                        sqlSelect = new MySqlCommand(query, sqlCon);

                        sqlSelect.ExecuteNonQuery();

                    }



                }


            }
        }





    }
}