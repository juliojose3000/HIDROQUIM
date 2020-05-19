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
    public class PersonaController : Controller
    {
        PersonaDao personaDao = new PersonaDao();
        static Persona personaModificar = new Persona();
        EncargadoLogueadoData encargadoLogueadoData = new EncargadoLogueadoData();
        CorreoDao correoDao = new CorreoDao();

        // GET: Persona
        public async Task<ActionResult> Index(string SearchString)
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                List<Persona> personas = new List<Persona>();
                if (!String.IsNullOrEmpty(SearchString))
                {
                    personas = personaDao.getPersonaByNombre(SearchString);
                }
                else
                {
                    personas = personaDao.getAllPersonas(); 
                }
                return View(personas);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> IndexGetAll()
        {
            List<Persona> personas = new List<Persona>();

            personas = personaDao.getAllPersonas();

            return View(personas);
        }


        // GET: Persona/Create
        public ActionResult Create()
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Persona/Crear
        [HttpPost]
        public ActionResult Crear(String telefonoFijo, String telefonoCelular, String correo, String cedula
            , String nombre_completo, String username, String password)
        {
            try
            {
                PersonaDao personaDao = new PersonaDao();
                InformacionContacto informacionContacto = new InformacionContacto(correo, telefonoFijo, telefonoCelular);
                Cliente cliente = new Cliente(nombre_completo, username, password);
                Persona persona = new Persona(cliente, cedula, informacionContacto);
                personaDao.insertarPersona(persona);
                correoDao.generarCorreo(correo, username, password, nombre_completo);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Persona");
            }
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(String id)
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                personaModificar = personaDao.getPersonaById(id);
                return View(personaModificar);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Persona/Edit/5
        [HttpPost]
        public ActionResult Edit(Persona persona)
        {
            try
            {
                personaModificar.Cedula = persona.Cedula;
                personaModificar.Cliente.Nombre_cliente = persona.Cliente.Nombre_cliente;
                personaModificar.Cliente.Username = persona.Cliente.Username;
                personaModificar.Cliente.Password = persona.Cliente.Password;
                personaModificar.InformacionContacto.Correo = persona.InformacionContacto.Correo;
                personaModificar.InformacionContacto.TelefonoCelular = persona.InformacionContacto.TelefonoCelular;
                persona.InformacionContacto.TelefonoFijo = persona.InformacionContacto.TelefonoFijo;


                personaDao.modificarPersona(personaModificar);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(String id)
        {
           
         return View(personaDao.getPersonaById(id));
               
        }
          
        

        // POST: Persona/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            try
            {
                personaDao.suprimirPersona(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Persona");
            }
        
        }
        public ActionResult Error()
        {
            return View();
        }


    }
}

