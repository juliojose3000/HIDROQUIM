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
    public class ProveedorController : Controller
    {
        ProveedorDao proveedorDao = new ProveedorDao();
        EncargadoLogueadoData encargadoLogueadoData = new EncargadoLogueadoData();
        // GET: Proveedor
        public async Task<ActionResult> Index(string SearchString)
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                List<Proveedor> listaProveedores = new List<Proveedor>();
                if (!String.IsNullOrEmpty(SearchString))
                {
                    listaProveedores = proveedorDao.getProveedorByName(SearchString);
                }
                else
                {
                    listaProveedores = proveedorDao.getAllProvedores();
                }
                return View(listaProveedores);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> IndexGetAll()
        {
            
                List<Proveedor> listaProveedores = new List<Proveedor>();

                listaProveedores = proveedorDao.getAllProvedores();

                return View(listaProveedores);
           
        }



        // GET: Proveedor/Create
        public ActionResult Create()
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Proveedor/Crear
        [HttpPost]
        public ActionResult Crear(String telefonoFijo,String telefonoCelular,String correo,String nombre,String identificacion)
        {
            try
            {
               
                InformacionContacto informacionContacto = new InformacionContacto(correo, telefonoFijo, telefonoCelular);
                Proveedor proveedor = new Proveedor( identificacion,  nombre,  informacionContacto);
                proveedorDao.insertarProveedor(proveedor);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Proveedor");

            }
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                Proveedor proveedor = proveedorDao.getProveedorById(id);
            return View(proveedor);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(Proveedor proveedor)
        {
            try
            {
               
                proveedorDao.modificarProveedor(proveedor);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Proveedor");

            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            if (encargadoLogueadoData.existeUsuarioLogueado())
            {
                return View(proveedorDao.getProveedorById(id));
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Proveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                proveedorDao.suprimirProveedor(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Proveedor");

            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
