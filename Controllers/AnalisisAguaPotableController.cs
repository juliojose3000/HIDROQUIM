using HIDROQUIM.Data;
using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class AnalisisAguaPotableController : Controller
    {
        AguaPotableDao aguaDAO = new AguaPotableDao();
        ClienteData clienteData = new ClienteData();
        // GET: AnalisisAguaPotable
        public ActionResult Index(String SearchString)
        {
            List<AnalisisQuimicoAguaPotable> listaAguas = new List<AnalisisQuimicoAguaPotable>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                //listaAguas = aguaDAO.getAnalisisPotableByClient(SearchString);
            }
            else
            {
                listaAguas = aguaDAO.getAllAguaPotable();
            }
            return View(listaAguas);
        }

        // GET: AnalisisAguaPotable/Details/5
        public ActionResult Details(int id)
        {
            AguaPotableDao aguaDAO = new AguaPotableDao();
            AnalisisQuimicoAguaPotable analisis = aguaDAO.getAnalisisPotableById(id);
            return View(analisis);
        }

        // GET: AnalisisAguaPotable/Create
        public ActionResult Create()
        {
            AguaPotableDao aguaDAO = new AguaPotableDao();
            var clientes = clienteData.GetAllClients();
            ViewData["clientes"] = clientes;
            return View();
        }

        [HttpPost]
        public JsonResult GetClientByName(string clientName)
        {
            List<Cliente> clientes = clienteData.GetSpecificClients(clientName);

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }


        // POST: AnalisisAguaPotable/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                //Analisis quimico
                AnalisisQuimico analisisQuimico = new AnalisisQuimico();
                DateTime fechaMuestreo;
                DateTime.TryParse(collection["fecha_muestreo"], out fechaMuestreo);
                analisisQuimico.FechaMuestreo = fechaMuestreo;
                analisisQuimico.FechaRegistroSistemaAutomatico = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                analisisQuimico.UbicacionGeografica = collection["terriorioGeografico"].ToString();
                analisisQuimico.Cliente = clienteData.GetClientById(Int32.Parse(collection["idClientSelected"].ToString()));

                List<Parametro> listaNivelUno = new List<Parametro>();
                List<Parametro> listaNivelDos = new List<Parametro>();
                List<Parametro> listaNivelTres = new List<Parametro>();

                Parametro parametro = new Parametro();

                foreach (String detalleParametro in collection)
                {

                    String valorDetalleParametro = collection[detalleParametro].ToString();//obtengo el valor del input llamado como detalleParametro

                    //--------------------------------LEVEL 1----------------------------------------//

                    if (detalleParametro.Contains("lv1"))
                    {

                        if (detalleParametro.Contains("Parametro_lv1_"))//esto significa que es un atributo de tipo de un parametro de un nivel x de agua
                        {
                            parametro.NombreParametro = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Unidad_lv1_"))
                        {
                            parametro.Unidad = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Obtenido_lv1_"))
                        {
                            parametro.ValorObtenido = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Recomendado_lv1_"))
                        {
                            parametro.ValorRecomendado = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Maximo_lv1_"))
                        {
                            parametro.ValorMaximoAdmisible = valorDetalleParametro;
                            listaNivelUno.Add(parametro);
                            parametro = new Parametro();
                        }

                    }

                    //---------------------------------LEVEL 2-----------------------------------//

                    if (detalleParametro.Contains("lv2"))
                    {

                        if (detalleParametro.Contains("Parametro_lv2_"))//esto significa que es un atributo de tipo de un parametro de un nivel x de agua
                        {
                            parametro.NombreParametro = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Unidad_lv2_"))
                        {
                            parametro.Unidad = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Obtenido_lv2_"))
                        {
                            parametro.ValorObtenido = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Recomendado_lv2_"))
                        {
                            parametro.ValorRecomendado = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Maximo_lv2_"))
                        {
                            parametro.ValorMaximoAdmisible = valorDetalleParametro;
                            listaNivelDos.Add(parametro);
                            parametro = new Parametro();
                        }

                    }

                    //---------------------------------LEVEL 3-----------------------------------//

                    if (detalleParametro.Contains("lv3"))
                    {

                        if (detalleParametro.Contains("Parametro_lv3_"))//esto significa que es un atributo de tipo de un parametro de un nivel x de agua
                        {
                            parametro.NombreParametro = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Unidad_lv3_"))
                        {
                            parametro.Unidad = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Obtenido_lv3_"))
                        {
                            parametro.ValorObtenido = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Recomendado_lv3_"))
                        {
                            parametro.ValorRecomendado = valorDetalleParametro;
                        }
                        else if (detalleParametro.Contains("Maximo_lv3_"))
                        {
                            parametro.ValorMaximoAdmisible = valorDetalleParametro;
                            listaNivelTres.Add(parametro);
                            parametro = new Parametro();
                        }

                    }


                }

                //------------------FIN DE LOS NIVELES--------------------//

                NivelAguaPotable nivelAguaPotable1 = new NivelAguaPotable();
                nivelAguaPotable1.Nombre = "Nivel 1";
                nivelAguaPotable1.Parametros = listaNivelUno;

                NivelAguaPotable nivelAguaPotable2 = new NivelAguaPotable();
                nivelAguaPotable2.Nombre = "Nivel 2";
                nivelAguaPotable2.Parametros = listaNivelDos;

                NivelAguaPotable nivelAguaPotable3 = new NivelAguaPotable();
                nivelAguaPotable3.Nombre = "Nivel 3";
                nivelAguaPotable3.Parametros = listaNivelTres;

                List<NivelAguaPotable> nivelesAguaPotable = new List<NivelAguaPotable>();
                nivelesAguaPotable.Add(nivelAguaPotable1);
                nivelesAguaPotable.Add(nivelAguaPotable2);
                nivelesAguaPotable.Add(nivelAguaPotable3);


                AnalisisQuimicoAguaPotable analisisQuimicoAguaPotable = new AnalisisQuimicoAguaPotable();
                analisisQuimicoAguaPotable.AnalisisQuimico = analisisQuimico;
                analisisQuimicoAguaPotable.NivelesAguaPotable = nivelesAguaPotable;
                analisisQuimicoAguaPotable.Resultado = collection["EstadoAnalisisAguaPotable"].ToString();

                aguaDAO.insert(analisisQuimico, analisisQuimicoAguaPotable);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaPotable");
            }
        }

        // GET: AnalisisAguaPotable/Edit/5
        public ActionResult Edit(int id)
        {
            AnalisisQuimicoAguaPotable analsis = aguaDAO.getAnalisisPotableById(id);
            return View(analsis);
        }

        // POST: AnalisisAguaPotable/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {

            try
            {
                AnalisisQuimicoAguaPotable analisisAguaPotable = new AnalisisQuimicoAguaPotable();
                string x = collection["idAnalisisQuimicoAguaPotable"].ToString();
                analisisAguaPotable = aguaDAO.getAnalisisPotableById(Int32.Parse(collection["idAnalisisQuimicoAguaPotable"].ToString()));

                //Fecha
                DateTime fechaMuestreo;
                DateTime.TryParse(collection["FechaResultado"], out fechaMuestreo);
                analisisAguaPotable.AnalisisQuimico.FechaResultado = fechaMuestreo;

                analisisAguaPotable.Resultado = collection["ResultadoAnalisisAguaPotable"].ToString();

                aguaDAO.modificarAnalisisAgua(analisisAguaPotable);


                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaPotable");
            }
        }

        // GET: AnalisisAguaPotable/Delete/5
        public ActionResult Delete(int id)
        {
            return View(aguaDAO.getAnalisisPotableById(id));
        }

        // POST: AnalisisAguaPotable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                aguaDAO.suprimirAnalisisAgua(id);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaPotable");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}