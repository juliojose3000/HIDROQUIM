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
    public class AnalisisAireController : Controller
    {
        // GET: AnalisisAire


        AnalisisAireDao analisisAireDao = new AnalisisAireDao();
        ClienteData clienteDao = new ClienteData();
        static AnalisisQuimicoAire analisisQuimicoAireStatico = new AnalisisQuimicoAire();
        // GET: AnalisisAire
        public async Task<ActionResult> Index(string SearchString)
        {
            List<AnalisisQuimicoAire> listaAnalisisAire = new List<AnalisisQuimicoAire>();

            if (!String.IsNullOrEmpty(SearchString))
            {
                listaAnalisisAire = analisisAireDao.getAnalisisByName(SearchString);
            }
            else
            {
                listaAnalisisAire = analisisAireDao.getAllAnalisisAire();
            }
            return View(listaAnalisisAire);
        }

        public async Task<ActionResult> IndexGetAll()
        {
            List<AnalisisQuimicoAire> listaAnalisisAire = new List<AnalisisQuimicoAire>();

            listaAnalisisAire = analisisAireDao.getAllAnalisisAire();

            return View(listaAnalisisAire);
        }


        // GET: AnalisisAire/Create
        public ActionResult Create()
        {
            var clientes = clienteDao.getAllClientes();
            ViewData["clientes"] = clientes;
            return View();
        }

        // POST: AnalisisAire/Create
        [HttpPost]
        public ActionResult Create(DateTime fechaMuestreo, DateTime fechaResultado, String ubicacionGeografica, String cliente,
            string contaminante, string tipoEstandar, float valorReferencia, float tiempoPromedio, string metodoReferencia,
            string resultadoAnalisis)
        {
            try
            {
                // TODO: Add insert logic here
                Cliente clienteIngresar = clienteDao.getClienteByid(Int32.Parse(cliente));

                AnalisisQuimico analisisQuimico = new AnalisisQuimico(fechaMuestreo, fechaResultado, ubicacionGeografica, clienteIngresar);

                AnalisisQuimicoAire analisisQuimicoAire = new AnalisisQuimicoAire(contaminante, tipoEstandar, valorReferencia
                , tiempoPromedio, metodoReferencia, resultadoAnalisis, analisisQuimico);
                analisisQuimicoAire.AnalisisQuimico.FechaMuestreo.ToString("yyyy-MM-dd");
                analisisQuimicoAire.AnalisisQuimico.FechaResultado.ToString("yyyy-MM-dd");


                analisisAireDao.insertarNuevoAnalisisDeAire(analisisQuimicoAire);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAire");
            }
        }

        // GET: AnalisisAire/Edit/5
        public ActionResult Edit(int id)
        {
            analisisQuimicoAireStatico = analisisAireDao.getAnalisisAireById(id);

            return View(analisisQuimicoAireStatico);
        }

        // POST: AnalisisSuelo/Edit/5
        [HttpPost]
        public ActionResult Edit(AnalisisQuimicoAire analisisQuimicoAire)
        {
            try
            {
                analisisQuimicoAireStatico.Contaminante = analisisQuimicoAire.Contaminante;
                analisisQuimicoAireStatico.AnalisisQuimico.FechaMuestreo = analisisQuimicoAire.AnalisisQuimico.FechaMuestreo;
                analisisQuimicoAireStatico.AnalisisQuimico.FechaResultado = analisisQuimicoAire.AnalisisQuimico.FechaResultado;
                analisisQuimicoAireStatico.AnalisisQuimico.UbicacionGeografica = analisisQuimicoAire.AnalisisQuimico.UbicacionGeografica;
                analisisQuimicoAireStatico.TipoEstandar = analisisQuimicoAire.TipoEstandar;
                analisisQuimicoAire.ValorReferencia = analisisQuimicoAire.ValorReferencia;
                analisisQuimicoAireStatico.TiempoPromedio = analisisQuimicoAire.TiempoPromedio;
                analisisQuimicoAireStatico.MetodoReferencia = analisisQuimicoAire.MetodoReferencia;
                analisisQuimicoAireStatico.ResultadoAnalisis = analisisQuimicoAire.ResultadoAnalisis;
                analisisAireDao.modificarAnalisisAire(analisisQuimicoAireStatico);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAire");
            }
        }

        // GET: AnalisisAire/Delete/5
        public ActionResult Delete(int id)
        {
            AnalisisQuimicoAire analisis = analisisAireDao.getAnalisisAireById(id);

            return View(analisis);
        }

        // POST: AnalisisAire/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            try
            {
                analisisAireDao.suprimirAnalisisAire(id);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisAire");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
    