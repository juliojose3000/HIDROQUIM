using HIDROQUIM.Models;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class AnalisisAguaResidualDao
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True";
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public List<AnalisisQuimicoAguaResidual> getAll()
        {
            List<AnalisisQuimicoAguaResidual> lista = new List<AnalisisQuimicoAguaResidual>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select ar.id_agua_residual, ar.resultado_analisis,ar.cod_ciiU,cu.ciiu, cu.actividad,cu.parametro, ar.cod_analisis_qumico,aq.fecha_Muestreo, aq.fecha_resultado,aq.fechaRegistro_Sistema_Automatico, aq.ubicacion_geografica, aq.Id_cliente, cl.nombre_completo,cl.username,cl.password from analisisagua_residual ar, analisis_quimico aq,ciiu cu, cliente cl where  ar.cod_analisis_qumico= aq.codigo AND  cu.cod_ciiU= ar.cod_ciiU AND aq.Id_cliente=  cl.id_cliente;";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimico analisisQuimico = null;
                    Cliente cliente = null;
                    AnalisisQuimicoAguaResidual analisisAguaResidual = null;
                    CUII cuii = new CUII();
                    while (reader.Read())
                    {
                        analisisQuimico = new AnalisisQuimico();
                        cliente = new Cliente();
                        analisisAguaResidual = new AnalisisQuimicoAguaResidual();
                        analisisAguaResidual.IdAguaResidual = reader.GetInt32("id_agua_residual");
                        analisisAguaResidual.ResultadoAnalisisQuimicoAguaResidual = reader.GetString("resultado_analisis");
                        cuii.CodCUII = reader.GetInt32("cod_ciiU");
                        cuii.Cuii = reader.GetInt32("ciiu");
                        cuii.Actividad = reader.GetString("actividad");
                        //cuii.Parametros = reader.GetString("parametros");*************************
                        analisisAguaResidual.Ciiu = cuii;
                        analisisQuimico.Codigo = reader.GetInt32("cod_analisis_qumico");

                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");

                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        cliente.IdCliente = reader.GetInt32("Id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisisQuimico.Cliente = cliente;
                        analisisAguaResidual.Analisis = analisisQuimico;
                        lista.Add(analisisAguaResidual);

                    }
                    sqlCon.Close();

                }
            }
            return lista;
        }

        internal void getParametersByCIIU(int? ciiu)
        {
            throw new NotImplementedException();
        }

        public AnalisisQuimicoAguaResidual getById(int id)
        {
            AnalisisQuimicoAguaResidual analisisAguaResidual = null;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select  ar.id_agua_residual, ar.resultado_analisis,ar.cod_ciiU,cu.ciiu, cu.actividad,cu.parametro, ar.cod_analisis_qumico,aq.fecha_Muestreo," +
             "aq.fecha_resultado,aq.fechaRegistro_Sistema_Automatico, aq.ubicacion_geografica, aq.Id_cliente, " +
             "cl.nombre_completo,cl.username,cl.password from analisisagua_residual ar, analisis_quimico aq,ciiu cu, cliente cl " +
             "where  ar.cod_analisis_qumico= aq.codigo AND  cu.cod_ciiU= ar.cod_ciiU AND aq.Id_cliente=  cl.id_cliente AND ar.id_agua_residual=" + id + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    AnalisisQuimico analisisQuimico = null;
                    Cliente cliente = null;

                    CUII cuii = new CUII();
                    while (reader.Read())
                    {
                        analisisQuimico = new AnalisisQuimico();
                        cliente = new Cliente();
                        analisisAguaResidual = new AnalisisQuimicoAguaResidual();
                        analisisAguaResidual.IdAguaResidual = reader.GetInt32("id_agua_residual");
                        analisisAguaResidual.ResultadoAnalisisQuimicoAguaResidual = reader.GetString("resultado_analisis");
                        cuii.CodCUII = reader.GetInt32("cod_ciiU");
                        cuii.Cuii = reader.GetInt32("ciiu");
                        cuii.Actividad = reader.GetString("actividad");
                        //cuii.Parametro = reader.GetString("parametros");**********************************************
                        analisisAguaResidual.Ciiu = cuii;
                        analisisQuimico.Codigo = reader.GetInt32("cod_analisis_qumico");
                        analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                        analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                        analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                        analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                        cliente.IdCliente = reader.GetInt32("Id_cliente");
                        cliente.Nombre_cliente = reader.GetString("nombre_completo");
                        cliente.Username = reader.GetString("username");
                        cliente.Password = reader.GetString("password");
                        analisisQuimico.Cliente = cliente;
                        analisisAguaResidual.Analisis = analisisQuimico;


                    }
                    sqlCon.Close();

                }
            }
            return analisisAguaResidual;
        }

        public void suprimir(int id)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                AnalisisQuimicoAguaResidual analisisQuimicoAguaResidual = getById(id);

                sqlCon.Open();
                String query = "Delete from ciiu_parametro where cod_ciiU = " + analisisQuimicoAguaResidual.Ciiu.CodCUII + " and id_agua_residual = " + id + ";";
                String query2 = "Delete from analisisagua_residual where id_agua_residual= " + id + ";";
                String query3 = "Delete from analisis_quimico  where codigo=" + analisisQuimicoAguaResidual.Analisis.Codigo + ";";


                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                sqlSelect2.ExecuteNonQuery();

                MySqlCommand sqlSelect3 = new MySqlCommand(query3, sqlCon);
                sqlSelect3.ExecuteNonQuery();



                sqlCon.Close();
            }
        }

        public CUII GetCiuuByCod(int codCiiu)
        {
            CUII cuii = new CUII();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from ciiu where ciiu =" + codCiiu + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        cuii.CodCUII = reader.GetInt32("cod_ciiU");
                        cuii.Actividad = reader.GetString("actividad");
                        cuii.Parametros = reader.GetString("parametro");
                        cuii.Cuii = reader.GetInt32("ciiu");


                    }
                    sqlCon.Close();

                }
            }
            return cuii;
        }

        public CUIIParametroValor GetCUIIParametroValor(string codCiiu)
        {
            CUIIParametroValor cuiiParametroValor = new CUIIParametroValor(0, "", null);
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from ciiu where ciiu =" + codCiiu + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        cuiiParametroValor.Parametro = reader.GetString("cod_ciiU");

                    }
                    sqlCon.Close();

                }
            }
            return cuiiParametroValor;
        }

        public List<string> GetListParametersFromString(string parameters)
        {
            List<string> listParameters = new List<string>();

            if (parameters != null)
            {

                string[] listaParameters = parameters.Split(',');
                foreach (string word in listaParameters)
                {
                    listParameters.Add(word);
                }
            }
            return listParameters;
        }

        public List<CUIIParametroValor> GetListParametersFromCIIU(int codCuii)
        {
            CUII cuii = GetCiuuByCod(codCuii);
            return cuii.ParametrosValores;
        }

        public Boolean existsCodCiiu(int codCiiu)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from ciiu where ciiu =" + codCiiu + ";";

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

        public void insert(AnalisisQuimico analisisQuimico, AnalisisQuimicoAguaResidual analisisQuimicoAguaResidual)
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

                query = "INSERT INTO analisisagua_residual(cod_analisis_qumico, cod_ciiU, resultado_analisis) " +
                    "values (" + codAnalisis + "," + analisisQuimicoAguaResidual.Ciiu.CodCUII + ",'" + analisisQuimicoAguaResidual.ResultadoAnalisisQuimicoAguaResidual + "');";

                sqlSelect = new MySqlCommand(query, sqlCon);

                sqlSelect.ExecuteNonQuery();

                //-----------------------------------------------------------------//

                int idAguaResidual = 0;

                query = "SELECT id_agua_residual FROM analisisagua_residual ORDER BY id_agua_residual DESC LIMIT 1;";

                sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idAguaResidual = reader.GetInt32("id_agua_residual");

                    }

                }

                //-------------------------------------------------------------------

                foreach (CUIIParametroValor cUIIParametroValor in analisisQuimicoAguaResidual.Ciiu.ParametrosValores)
                {
                    query = "insert into ciiu_parametro(cod_ciiU, parametro, valor, id_agua_residual) " +
                        "values (" + GetCiuuByCod(cUIIParametroValor.CodCuii).CodCUII + ",'" + cUIIParametroValor.Parametro + "','" + cUIIParametroValor.Valor + "'," + idAguaResidual + ");";

                    sqlSelect = new MySqlCommand(query, sqlCon);

                    sqlSelect.ExecuteNonQuery();
                }


            }
        }

        internal List<CUIIParametroValor> getParametersByCIIU(int ciiu)
        {
            List<CUIIParametroValor> parametros = new List<CUIIParametroValor>();
            CUIIParametroValor cuii = new CUIIParametroValor();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "select* from ciiu_parametro where id_agua_residual =" + ciiu + ";";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);

                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        cuii = new CUIIParametroValor();
                        cuii.CodCuii = reader.GetInt32("cuii_parametro_id");
                        cuii.Parametro = reader.GetString("parametro");
                        cuii.Valor = reader.GetString("valor");
                        parametros.Add(cuii);
                    }
                    sqlCon.Close();

                }
            }
            return parametros;
        }

        internal AnalisisQuimicoAguaResidual getAnalisisResidualById(int id)
        {
            {
                AnalisisQuimicoAguaResidual analisisAguaResidual = null;
                using (MySqlConnection sqlCon = GetConnection())
                {
                    sqlCon.Open();
                    String query = "Select  ar.id_agua_residual, ar.resultado_analisis,ar.cod_ciiU,cu.ciiu, cu.actividad,cu.parametro, ar.cod_analisis_qumico,aq.fecha_Muestreo," +
                 "aq.fecha_resultado,aq.fechaRegistro_Sistema_Automatico, aq.ubicacion_geografica, aq.Id_cliente, " +
                 "cl.nombre_completo,cl.username,cl.password from analisisagua_residual ar, analisis_quimico aq,ciiu cu, cliente cl " +
                 "where ar.id_agua_residual=" + id + " AND ar.cod_analisis_qumico= aq.codigo AND  cu.cod_ciiU= ar.cod_ciiU AND aq.Id_cliente=  cl.id_cliente; ";

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        AnalisisQuimico analisisQuimico = null;
                        Cliente cliente = null;
                        CUII cuii = new CUII();
                        while (reader.Read())
                        {
                            analisisQuimico = new AnalisisQuimico();
                            cliente = new Cliente();
                            analisisAguaResidual = new AnalisisQuimicoAguaResidual();
                            analisisAguaResidual.IdAguaResidual = reader.GetInt32("id_agua_residual");
                            analisisAguaResidual.ResultadoAnalisisQuimicoAguaResidual = reader.GetString("resultado_analisis");
                            cuii.CodCUII = reader.GetInt32("cod_ciiU");
                            cuii.Cuii = reader.GetInt32("ciiu");
                            cuii.Actividad = reader.GetString("actividad");
                            //cuii.Parametros = reader.GetString("parametros");*************************
                            analisisAguaResidual.Ciiu = cuii;
                            analisisQuimico.Codigo = reader.GetInt32("cod_analisis_qumico");
                            analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                            analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                            analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                            analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                            cliente.IdCliente = reader.GetInt32("Id_cliente");
                            cliente.Nombre_cliente = reader.GetString("nombre_completo");
                            cliente.Username = reader.GetString("username");
                            cliente.Password = reader.GetString("password");
                            analisisQuimico.Cliente = cliente;
                            analisisAguaResidual.Ciiu.ParametrosValores = getParametersByCIIU(analisisAguaResidual.IdAguaResidual);
                            analisisAguaResidual.Analisis = analisisQuimico;
                        }
                        sqlCon.Close();

                    }
                }
                return analisisAguaResidual;
            }
        }

        public void modificarAnalisisAguaResidual(AnalisisQuimicoAguaResidual analisis)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Update analisisagua_residual set resultado_analisis='" + analisis.ResultadoAnalisisQuimicoAguaResidual + "'where id_agua_residual=" + analisis.IdAguaResidual;

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                query = "Update analisis_quimico set fecha_Muestreo='" + analisis.Analisis.FechaMuestreo + "',fecha_resultado='" + analisis.Analisis.FechaResultado + "' ,ubicacion_geografica='" + analisis.Analisis.UbicacionGeografica + "'where Id_cliente=" + analisis.Analisis.Cliente.IdCliente + ";";

                sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                sqlCon.Close();
            }
        }




        public void modificarAnalisisAguaPotable(AnalisisQuimicoAguaResidual analisis)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Update analisisagua_residual set resultado_analisis='" + analisis.ResultadoAnalisisQuimicoAguaResidual + "'where id_agua_residual=" + analisis.IdAguaResidual;

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                query = "Update analisis_quimico set fecha_Muestreo='" + analisis.Analisis.FechaMuestreo + "',fecha_resultado='" + analisis.Analisis.FechaResultado + "' ,ubicacion_geografica='" + analisis.Analisis.UbicacionGeografica + "'where Id_cliente=" + analisis.Analisis.Cliente.IdCliente + ";";

                sqlSelect = new MySqlCommand(query, sqlCon);
                sqlSelect.ExecuteNonQuery();

                sqlCon.Close();
            }
        }
        public List<AnalisisQuimicoAguaResidual> getAnalisisResidualByIdCLiente(int idCliente)
        {
            {
                List<AnalisisQuimicoAguaResidual> listaAnalisisAguaResidual = new List<AnalisisQuimicoAguaResidual>();
                using (MySqlConnection sqlCon = GetConnection())
                {
                    sqlCon.Open();
                    String query = "Select  ar.id_agua_residual, ar.resultado_analisis,ar.cod_ciiU,cu.ciiu, cu.actividad,cu.parametro, ar.cod_analisis_qumico,aq.fecha_Muestreo," +
                 "aq.fecha_resultado,aq.fechaRegistro_Sistema_Automatico, aq.ubicacion_geografica, aq.Id_cliente, " +
                 "cl.nombre_completo,cl.username,cl.password from analisisagua_residual ar, analisis_quimico aq,ciiu cu, cliente cl " +
                 "where cl.id_cliente=" + idCliente + " AND ar.cod_analisis_qumico= aq.codigo AND  cu.cod_ciiU= ar.cod_ciiU AND aq.Id_cliente=  cl.id_cliente; ";

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        AnalisisQuimico analisisQuimico = null;
                        AnalisisQuimicoAguaResidual analisisAguaResidual = null;
                        Cliente cliente = null;
                        CUII cuii = new CUII();
                        while (reader.Read())
                        {
                            analisisQuimico = new AnalisisQuimico();
                            cliente = new Cliente();
                            analisisAguaResidual = new AnalisisQuimicoAguaResidual();
                            analisisAguaResidual.IdAguaResidual = reader.GetInt32("id_agua_residual");
                            analisisAguaResidual.ResultadoAnalisisQuimicoAguaResidual = reader.GetString("resultado_analisis");
                            cuii.CodCUII = reader.GetInt32("cod_ciiU");
                            cuii.Cuii = reader.GetInt32("ciiu");
                            cuii.Actividad = reader.GetString("actividad");
                            cuii.Parametros = reader.GetString("parametro"); //quitar este campo, se deja solo la lista parametroValor
                            analisisAguaResidual.Ciiu = cuii;
                            analisisQuimico.Codigo = reader.GetInt32("cod_analisis_qumico");
                            analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                            analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                            analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                            analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                            cliente.IdCliente = reader.GetInt32("Id_cliente");
                            cliente.Nombre_cliente = reader.GetString("nombre_completo");
                            cliente.Username = reader.GetString("username");
                            cliente.Password = reader.GetString("password");
                            analisisQuimico.Cliente = cliente;
                            analisisAguaResidual.Ciiu.ParametrosValores = getParametersByCIIU(analisisAguaResidual.IdAguaResidual);
                            analisisAguaResidual.Analisis = analisisQuimico;
                            listaAnalisisAguaResidual.Add(analisisAguaResidual);
                        }
                        sqlCon.Close();

                    }
                }
                return listaAnalisisAguaResidual;
            }
        }
        public List<AnalisisQuimicoAguaResidual> getAnalisisAireByIdClienteAndActividad(String actividad, int idCliente)
        {
            {
                List<AnalisisQuimicoAguaResidual> listaAnalisisAguaResidual = new List<AnalisisQuimicoAguaResidual>();
                using (MySqlConnection sqlCon = GetConnection())
                {
                    sqlCon.Open();
                    String query = "Selectar.id_agua_residual, ar.resultado_analisis,ar.cod_ciiU,cu.ciiu, cu.actividad,cu.parametro, ar.cod_analisis_qumico,aq.fecha_Muestreo," +
                 "aq.fecha_resultado,aq.fechaRegistro_Sistema_Automatico, aq.ubicacion_geografica, aq.Id_cliente, " +
                 "cl.nombre_completo,cl.username,cl.password from analisisagua_residual ar, analisis_quimico aq,ciiu cu, cliente cl " +
                 "where cl.id_cliente=" + idCliente + " AND ar.cod_analisis_qumico= aq.codigo AND  cu.cod_ciiU= ar.cod_ciiU " +
                 "AND aq.Id_cliente=  cl.id_cliente AND cu.actividad LIKE '" + actividad + "%';";

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        AnalisisQuimico analisisQuimico = null;
                        AnalisisQuimicoAguaResidual analisisAguaResidual = null;
                        Cliente cliente = null;
                        CUII cuii = new CUII();
                        while (reader.Read())
                        {
                            analisisQuimico = new AnalisisQuimico();
                            cliente = new Cliente();
                            analisisAguaResidual = new AnalisisQuimicoAguaResidual();
                            analisisAguaResidual.IdAguaResidual = reader.GetInt32("id_agua_residual");
                            analisisAguaResidual.ResultadoAnalisisQuimicoAguaResidual = reader.GetString("resultado_analisis");
                            cuii.CodCUII = reader.GetInt32("cod_ciiU");
                            cuii.Cuii = reader.GetInt32("ciiu");
                            cuii.Actividad = reader.GetString("actividad");
                            cuii.Parametros = reader.GetString("parametro"); //quitar este campo, se deja solo la lista parametroValor
                            analisisAguaResidual.Ciiu = cuii;
                            analisisQuimico.Codigo = reader.GetInt32("cod_analisis_qumico");
                            analisisQuimico.FechaMuestreo = reader.GetDateTime("fecha_Muestreo");
                            analisisQuimico.FechaResultado = reader.GetDateTime("fecha_resultado");
                            analisisQuimico.FechaRegistroSistemaAutomatico = reader.GetDateTime("fechaRegistro_Sistema_Automatico");
                            analisisQuimico.UbicacionGeografica = reader.GetString("ubicacion_geografica");
                            cliente.IdCliente = reader.GetInt32("Id_cliente");
                            cliente.Nombre_cliente = reader.GetString("nombre_completo");
                            cliente.Username = reader.GetString("username");
                            cliente.Password = reader.GetString("password");
                            analisisQuimico.Cliente = cliente;
                            analisisAguaResidual.Ciiu.ParametrosValores = getParametersByCIIU(analisisAguaResidual.IdAguaResidual);
                            analisisAguaResidual.Analisis = analisisQuimico;
                            listaAnalisisAguaResidual.Add(analisisAguaResidual);
                        }
                        sqlCon.Close();

                    }
                }
                return listaAnalisisAguaResidual;
            }
        }




    }
}