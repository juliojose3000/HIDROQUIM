using HIDROQUIM.Data;
using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class InicioSesionEncargadoController : Controller
    {
        EncargadoLogueadoData encargadoLogueadoData = new EncargadoLogueadoData();
        EncargadoDao encargadoDao = new EncargadoDao();
        public ActionResult Login()
        {
            return View();
        }
      // POST: InicioSesionEncargado/Create
        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            try
            {
                Encargado encargado = encargadoDao.getEncargadoByUserNameAndPassword(username, password);
                if (encargado.UserName == username && encargado.Password == password)
                {
                   EncargadoLogueado encargadoLogueado= encargadoLogueadoData.insertarEncargadoLogueado(encargado);
                    TempData["encargadoLogueado"] = encargadoLogueado;
                    return RedirectToAction("AdministradorIndex", "VistasAdministrador");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

     
    }
}
