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
    public class AnalisisSueloController : Controller
    {

        AnalisisSueloDao analisisSueloDao = new AnalisisSueloDao();
        ClienteData clienteDao = new ClienteData();
        static AnalisisQuimicoSuelo analisisQuimicoSuelo = new AnalisisQuimicoSuelo();

        // GET: AnalisisSuelo
        public async Task<ActionResult> Index(string SearchString)
        {
            List<AnalisisQuimicoSuelo> listaAnalisisSuelo = new List<AnalisisQuimicoSuelo>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaAnalisisSuelo = analisisSueloDao.getAnalisisByName(SearchString);
            }
            else
            {
                listaAnalisisSuelo = analisisSueloDao.getAllAnalisisSuelo();
            }
            return View(listaAnalisisSuelo);
        }

        public async Task<ActionResult> IndexGetAll()
        {
            List<AnalisisQuimicoSuelo> listaAnalisisSuelo = new List<AnalisisQuimicoSuelo>();

            listaAnalisisSuelo = analisisSueloDao.getAllAnalisisSuelo();

            return View(listaAnalisisSuelo);
        }


        // GET: AnalisisSuelo/Create
        public ActionResult Create()
        {
            var clientes = clienteDao.getAllClientes();
            ViewData["clientes"] = clientes;
            return View();
        }

        // POST: AnalisisSuelo/Create
        [HttpPost]
        public ActionResult Create(String fechaMuestreo, String fechaResultado,
            String determinacion, String descripcion, String ubicacionGeografica, String cliente)
        {
            try
            {
                DateTime dateTimeFechaMuestreo = Convert.ToDateTime(fechaMuestreo);
                DateTime dateTimeFechaResultado = Convert.ToDateTime(fechaResultado);
                DateTime fechaRegistroSistemaAutomatico = DateTime.Today;

                Cliente clienteIngresar = clienteDao.getClienteByid(Int32.Parse(cliente));

                AnalisisQuimico analisisQuimico = new AnalisisQuimico(dateTimeFechaMuestreo, dateTimeFechaResultado, fechaRegistroSistemaAutomatico, ubicacionGeografica, clienteIngresar);
                AnalisisQuimicoSuelo analisisQuimicoSuelo = new AnalisisQuimicoSuelo(determinacion, descripcion, analisisQuimico);

                analisisSueloDao.insertarNuevoAnalisisDeSuelo(analisisQuimicoSuelo);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisSuelo");
            }
        }

        // GET: AnalisisSuelo/Edit/5
        public ActionResult Edit(int id)
        {
            analisisQuimicoSuelo = analisisSueloDao.getAnalisisSueloById(id);

            return View(analisisQuimicoSuelo);
        }

        // POST: AnalisisSuelo/Edit/5
        [HttpPost]
        public ActionResult Edit(AnalisisQuimicoSuelo analisisQuimicoSueloEditar)
        {
            try
            {

                analisisQuimicoSuelo.Descripcion = analisisQuimicoSueloEditar.Descripcion;
                analisisQuimicoSuelo.Determinacion = analisisQuimicoSueloEditar.Determinacion;
                analisisQuimicoSuelo.AnalisisQuimico.UbicacionGeografica = analisisQuimicoSueloEditar.AnalisisQuimico.UbicacionGeografica;
                analisisQuimicoSuelo.AnalisisQuimico.FechaResultado = analisisQuimicoSueloEditar.AnalisisQuimico.FechaResultado;


                analisisSueloDao.modificarAnalisisSuelo(analisisQuimicoSuelo); 

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisSuelo");
            }
        }

        // GET: AnalisisSuelo/Delete/5
        public ActionResult Delete(int id)
        {
            AnalisisQuimicoSuelo analisis = analisisSueloDao.getAnalisisSueloById(id);

            return View(analisis);
        }

        // POST: AnalisisSuelo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            try
            {
                analisisSueloDao.suprimirAnalisisSuelo(id);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "AnalisisSuelo");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}

    