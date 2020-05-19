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
    public class EntidadController : Controller
    {
        EntidadDao entidadDao = new EntidadDao();
        CorreoDao correoDao = new CorreoDao();

        // GET: Entidad
        public async Task<ActionResult> Index(string SearchString)
        {
            List<Entidad> listaEntidades = new List<Entidad>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaEntidades = entidadDao.getEntidadByName(SearchString);
            }
            else
            {
                listaEntidades = entidadDao.getAllEntidades();
            }
            return View(listaEntidades);
        }

        public async Task<ActionResult> IndexGetAll()
        {
            List<Entidad> listaEntidades = new List<Entidad>();

            listaEntidades = entidadDao.getAllEntidades();

            return View(listaEntidades);
        }



        // GET: Entidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entidad/Create
        [HttpPost]
        public ActionResult Create(String telefonoFijo, String telefonoCelular, String cedulaJuridica, String correo, string nombre_cliente, string username, string password,
            String telefonoFijo2, String telefonoCelular2, String correo2, String nombreCompleto, string cedula)
        {
            try
            {


                InformacionContacto informacionContacto = new InformacionContacto(correo, telefonoFijo, telefonoCelular);
                Cliente cliente = new Cliente(nombre_cliente, username, password);
                InformacionContacto informacionContactoResponsable = new InformacionContacto(correo2, telefonoFijo2, telefonoCelular2);
                ResponsableLegal responsableLegal = new ResponsableLegal(nombreCompleto, cedula, informacionContactoResponsable);
                responsableLegal.InformacionContacto = informacionContactoResponsable;
                Entidad entidad = new Entidad();
                entidad.CedulaJuridica = cedulaJuridica;
                entidad.Cliente = cliente;
                entidad.ResponsableLegal = responsableLegal;
                entidad.InformacionContacto = informacionContacto;

                entidadDao.insertarEntidad(entidad);
                correoDao.generarCorreo(correo, username, password, nombreCompleto);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Entidad");
            }
        }

        // GET: Entidad/Edit/5
        public ActionResult Edit(String id)

        {
            Entidad entidad = entidadDao.getEntidadById(id);
            return View(entidad);
        }

        // POST: Entidad/Edit/5
        [HttpPost]
        public ActionResult Edit(Entidad entidad)
        {
            try
            {
                entidadDao.modificarEntidad(entidad);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Entidad");
            }
        }
        // GET: Entidad/Delete/5
        public ActionResult Delete(String id)
        {

            return View(entidadDao.getEntidadById(id));
        }

        // POST: Entidad/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            try
            {
                entidadDao.suprimirEntidad(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Entidad");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}