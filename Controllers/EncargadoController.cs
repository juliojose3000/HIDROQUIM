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
    public class EncargadoController : Controller
    {
        static Encargado encargadoModificar = new Encargado();
        EncargadoDao encargadoDao = new EncargadoDao();
        CorreoDao correoDao = new CorreoDao();

        // GET: Encargado
        public async Task<ActionResult> Index(string SearchString)
        {
            List<Encargado> listaEncargados = new List<Encargado>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaEncargados = encargadoDao.getEncargadosByName(SearchString);
            }
            else
            {
                listaEncargados = encargadoDao.getAllEncargados();
            }
            return View(listaEncargados);
        }


        public async Task<ActionResult> IndexGetAll()
        {
            List<Encargado> listaEncargados = new List<Encargado>();

            listaEncargados = encargadoDao.getAllEncargados();

            return View(listaEncargados);
        }

        // GET: Encargado/Create
        public ActionResult Create()
        {
            List<Rol> roles = encargadoDao.getAllRoles();
            ViewData["roles"] = roles;
            return View();
        }

        // POST: Encargado/Create
        [HttpPost]
        public ActionResult Create(string cedula, string nombre, string apellidos, string correo, string telefono, string userName, string password, string rol)
        {

            try
            {
                Rol rolEncargado = encargadoDao.getRoleByName(rol);
                Encargado encargadoInsertar = new Encargado(0, cedula, nombre, apellidos, correo, telefono, userName, password, rolEncargado);
                encargadoDao.insertarNuevoEmpleado(encargadoInsertar);
                correoDao.generarCorreo(correo, userName, password, nombre);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Encargado");
            }
        }

        // GET: Encargado/Edit/5
        public ActionResult Edit(int id)
        {
            encargadoModificar = encargadoDao.getEncargadoById(id);
            return View(encargadoModificar);
        }

        // POST: Encargado/Edit/5
        [HttpPost]
        public ActionResult Edit(Encargado encargado)
        {
            try
            {
                encargadoModificar.Cedula = encargado.Cedula;
                encargadoModificar.Apellidos = encargado.Apellidos;
                encargadoModificar.Nombre = encargado.Nombre;
                encargadoModificar.Correo = encargado.Correo;
                encargadoModificar.Telefono = encargado.Telefono;
                encargadoModificar.UserName = encargado.UserName;
                encargadoModificar.Password = encargado.Password;


                encargadoDao.modificarEncargado(encargadoModificar);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Encargado");
            }
        }

        // GET: Encargado/Delete/5
        public ActionResult Delete(int id)
        {
            Encargado encargado = encargadoDao.getEncargadoById(id);
            return View(encargado);
        }

        // POST: Encargado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                encargadoDao.suprimirEmpleado(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Encargado");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}