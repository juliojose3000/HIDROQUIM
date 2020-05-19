using HIDROQUIM.Data;
using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIDROQUIM.Controllers
{
    public class InicioSeccionClienteController : Controller
    {
        ClienteData clienteData = new ClienteData();
        // GET: InicioSeccionCliente
        public ActionResult Login()
        {
            return View();
        }

      

        // POST: InicioSeccionCliente/Create
        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            try
            {
                ClienteLogueado cliente = clienteData.getClienteLogin(username, password);
                if(cliente.Username== username && cliente.Password == password)
                {
                 ClienteLogueado clienteLogueado=clienteData.insertarClienteLogueado(cliente);
                    TempData["clienteLogueado"] = clienteLogueado;
                    return RedirectToAction("VistaClienteIndex", "VistasCliente");
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
