using HIDROQUIM.Data;
using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class VistasClienteController : Controller
    {
        ClienteData clienteData = new ClienteData();
        AguaPotableDao aguaPotableDao = new AguaPotableDao();
        AnalisisAguaResidualDao aguaResidualDao = new AnalisisAguaResidualDao();
        AnalisisSueloDao analisisSuelo = new AnalisisSueloDao();
        AnalisisAireDao analisisAireDao = new AnalisisAireDao();
        // GET: VistasCliente
        public ActionResult VistaClienteIndex()
        {
            ClienteLogueado clienteLogueado = (ClienteLogueado)clienteData.getClienteLogueado();
            return View(clienteLogueado);
        }

        public ActionResult CerrarSesion()
        {
            ClienteLogueado clienteLogueado = clienteData.getClienteLogueado();
            clienteData.suprimirClienteLogueado(clienteLogueado);
            return RedirectToAction("Index", "Home");
        }

        /* FALTA JALARLE LOS NIVELES por implementar la busqueda por NIVEL 
        public ActionResult AnalisisAguaPotable(string SearchString)
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
             List<AnalisisQuimicoAguaPotable> lista = aguaPotableDao.getAnalisisPotableByIdCliente(idCliente);

            if (!String.IsNullOrEmpty(SearchString))
            {
               lista = aguaPotableDao.getAnalisisPotableByIdClienteAndParametro(SearchString, idCliente);
                return View(lista);
            }
            else
            {
                if (lista.Count > 0)
                {
                    return View(lista);
                }
                else
                    return RedirectToAction("Fallo");
            }
        }

        public ActionResult Fallo()
        {
            return View();
        }
        */

        public ActionResult AnalisisAguaResidual(string SearchString)
        {
            List<AnalisisQuimicoAguaResidual> listaAguaResidual = aguaResidualDao.getAnalisisResidualByIdCLiente(clienteData.getClienteLogueado().IdCliente);
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaAguaResidual = aguaResidualDao.getAnalisisAireByIdClienteAndActividad(SearchString, idCliente);
                return View(listaAguaResidual);
            }
            else
            {

                if (listaAguaResidual.Count > 0)
                {
                    return View(listaAguaResidual);
                }
                else
                    return RedirectToAction("Fallo");
            }
        }
        public ActionResult AnalisisSuelo(string SearchString)
        {
            List<AnalisisQuimicoSuelo> listaAnalisisSuelo = analisisSuelo.getAnalisisSueloByIdCliente(clienteData.getClienteLogueado().IdCliente);
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaAnalisisSuelo = analisisSuelo.getAnalisisAireByIdClienteAndDeterminante(SearchString, idCliente);
                return View(listaAnalisisSuelo);
            }
            else
            {
                if (listaAnalisisSuelo.Count > 0)
                {
                    return View(listaAnalisisSuelo);
                }
                return RedirectToAction("Fallo");
            }
        }
        public async Task<ActionResult> AnalisisAire(string SearchString)
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            List<AnalisisQuimicoAire> listaAnalisisAire = analisisAireDao.getAnalisisAireByIdCliente(idCliente);
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaAnalisisAire = analisisAireDao.getAnalisisAireByIdClienteAndContaminante(SearchString, idCliente);
                return View(listaAnalisisAire);
            }
            else
            {
                if (listaAnalisisAire.Count > 0)
                {
                    return View(listaAnalisisAire);
                }
                else

                    return RedirectToAction("Fallo");
            }
        }
        public async Task<ActionResult> AireGetAll()
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            List<AnalisisQuimicoAire> listaAnalisisAire = analisisAireDao.getAnalisisAireByIdCliente(idCliente);
            return View(listaAnalisisAire);
        }
        public async Task<ActionResult> SueloGetAll()
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            List<AnalisisQuimicoSuelo> listaAnalisisSuelo = analisisSuelo.getAnalisisSueloByIdCliente(clienteData.getClienteLogueado().IdCliente);
            return View(listaAnalisisSuelo);
        }
        /*
        public async Task<ActionResult> AguaPotableGetAll()
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            List<AnalisisQuimicoAguaPotable> lista = aguaPotableDao.getAnalisisPotableByIdCliente(clienteData.getClienteLogueado().IdCliente);
            return View(lista);
        }*/
        public async Task<ActionResult> AguaResidualGetAll()
        {
            int idCliente = clienteData.getClienteLogueado().IdCliente;
            List<AnalisisQuimicoAguaResidual> listaAguaResidual = aguaResidualDao.getAnalisisResidualByIdCLiente(clienteData.getClienteLogueado().IdCliente);
            return View(listaAguaResidual);
        }
        public ActionResult DetallesAnalisisSuelo(int id)
        {
            Pdf pdf = new Pdf();
            pdf.generarPdf(id);
            return View(analisisSuelo.getAnalisisSueloById(id));
        }
    }
}