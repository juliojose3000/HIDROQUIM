using HIDROQUIM.Data;
using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class AnalisisAguaResidualController : Controller
    {
        AnalisisAguaResidualDao analisisAguaDao = new AnalisisAguaResidualDao();
        ClienteData clienteData = new ClienteData();
        // GET: AnalisisAguaResidual
        public ActionResult Index()
        {
            return View(analisisAguaDao.getAll());
        }

        // GET: AnalisisAguaResidual/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalisisAguaResidual/Create
        public ActionResult Create(string ciiu)
        {
            CUII cuui = new CUII();
            string isExists = "";

            if (ciiu != null)
            {
                if (analisisAguaDao.existsCodCiiu(Int32.Parse(ciiu)))
                {
                    cuui = analisisAguaDao.GetCiuuByCod(Int32.Parse(ciiu));
                }
                else
                {
                    isExists = "El código " + ciiu + " no existe";
                }
            }

            ViewData["clients"] = clienteData.GetAllClients();
            ViewData["itNotExists"] = isExists;
            ViewData["ciiu"] = cuui;
            ViewData["parameters"] = analisisAguaDao.GetListParametersFromString(cuui.Parametros);

            return View();
        }

        // POST: AnalisisAguaResidual/Create
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


                CUII cuui = analisisAguaDao.GetCiuuByCod(Int32.Parse(collection["CIIU"].ToString()));

                List<string> listParameters = analisisAguaDao.GetListParametersFromString(cuui.Parametros);
                foreach (string parameter in listParameters)
                {
                    string parameterValue = collection[parameter].ToString();

                    int cuii = Int32.Parse(collection["CIIU"].ToString());

                    CUIIParametroValor cuiiParametroValor = new CUIIParametroValor(cuii, parameterValue, parameter);

                    cuui.ParametrosValores.Add(cuiiParametroValor);
                }

                AnalisisQuimicoAguaResidual analisisQuimicoAguaResidual = new AnalisisQuimicoAguaResidual();
                analisisQuimicoAguaResidual.Analisis = analisisQuimico;
                analisisQuimicoAguaResidual.Ciiu = cuui;
                analisisQuimicoAguaResidual.ResultadoAnalisisQuimicoAguaResidual = collection["resultadoAnalisis"].ToString();

                analisisAguaDao.insert(analisisQuimico, analisisQuimicoAguaResidual);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaResidual");
            }
        }

        // GET: AnalisisAguaResidual/Edit/5
        public ActionResult Edit(int id)
        {
            AnalisisQuimicoAguaResidual analisis = analisisAguaDao.getAnalisisResidualById(id);
            var valorP = analisis.Ciiu.ParametrosValores;
            ViewData["ValorP"] = valorP;
            return View(analisis);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                analisisAguaDao.suprimir(Int32.Parse(id));
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaResidual");
            }
        }

        // POST: AnalisisAguaResidual/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                analisisAguaDao.suprimir(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaResidual");
            }
        }

        public ActionResult GetParametersByCIIU(int ciiu)
        {
            CUII cuui = analisisAguaDao.GetCiuuByCod(ciiu);
            ViewData["ciiu"] = cuui;
            return RedirectToAction("Create(" + cuui + ")");
        }

        public async Task<ActionResult> GetListAsync(string ciiu)
        {
            /*CUII cuui = analisisAguaDao.GetParametersByCIIU(ciiu);
            //Create a stopwatch for getting excution time  
            var watch = new Stopwatch();
            watch.Start();
            var CIIU = GetCountryAsync();
            var state = GetStateAsync();
            var city = GetCityAsync();
            var content = await country;
            var count = await state;
            var name = await city;
            watch.Stop();
            ViewBag.WatchMilliseconds = watch.ElapsedMilliseconds;*/
            return View();
        }

        public List<Cliente> GetAllClients()
        {
            return clienteData.GetAllClients();
        }

        [HttpPost]
        public JsonResult GetClientByName(string clientName)
        {
            List<Cliente> clientes = clienteData.GetSpecificClients(clientName);

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getActividadCiiu(string ciiu)
        {
            CUII cuui = analisisAguaDao.GetCiuuByCod(Int32.Parse(ciiu));
            return Json(cuui.Actividad, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getParametersCiiu(string ciiu)
        {
            CUII cuui = analisisAguaDao.GetCiuuByCod(Int32.Parse(ciiu));
            List<string> parametersList = analisisAguaDao.GetListParametersFromString(cuui.Parametros);

            return Json(parametersList, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult Edit(DateTime ResultadoFechaMuestreo, int IdAguaResidual, DateTime FechaResultado, String Ubicacion, String Resultado, int IdCliente)
        {
            try
            {
                // TODO: Add update logic here
                AnalisisQuimicoAguaResidual analisispotable = new AnalisisQuimicoAguaResidual();
                analisispotable.IdAguaResidual = IdAguaResidual;
                analisispotable.ResultadoAnalisisQuimicoAguaResidual = Resultado;
                analisispotable.Analisis.FechaMuestreo = ResultadoFechaMuestreo;
                analisispotable.Analisis.FechaResultado = FechaResultado;
                analisispotable.Analisis.UbicacionGeografica = Ubicacion;
                Cliente c = new Cliente();
                c.IdCliente = IdCliente;
                analisispotable.Analisis.Cliente = c;
                analisisAguaDao.modificarAnalisisAguaPotable(analisispotable);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAguaResidual");
            }
        }

        public ActionResult Error()
        {
            return View();
        }


    }
}


