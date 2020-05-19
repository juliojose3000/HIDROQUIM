using HIDROQUIM.Data;
using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class VistasAdministradorController : Controller
    {
        EncargadoLogueadoData encargadoDao = new EncargadoLogueadoData();
        // GET: AdministradorIndex
        public ActionResult AdministradorIndex()
        {
            EncargadoLogueado encargadoLogueado = (EncargadoLogueado)encargadoDao.getEncargadoLogueado();
            return View(encargadoLogueado);
        }

        // GET: GestionarAnalisisQuimico
        public ActionResult GestionarAnalisisQuimico()
        {
            return View();
        }

        // GET: GestionarCliente
        public ActionResult GestionarCliente()
        {
            return View();
        }
        public ActionResult CerrarSesion()
        {
            EncargadoLogueado encargadoLogueado = encargadoDao.getEncargadoLogueado();
            encargadoDao.suprimirEncargadoLogueado(encargadoLogueado.IdEncargado);
            return RedirectToAction("Index","Home");
        }
        public ActionResult Inventario()
        {
            return View();
        }

    }
}
