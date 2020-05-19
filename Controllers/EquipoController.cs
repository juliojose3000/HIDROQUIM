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
    public class EquipoController : Controller
    {
        EquipoData equipoData = new EquipoData();
        static Equipo equipoAModificar = new Equipo();
        ProveedorDao proveedorDao = new ProveedorDao();
        // GET: Equipo
        public async Task<ActionResult> Index(string SearchString)
        {
            List<Equipo> listaEquipos = new List<Equipo>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaEquipos = equipoData.getEquipoByName(SearchString);
            }
            else
            {
                listaEquipos = equipoData.getAllEquipos();
            }
            return View(listaEquipos);
        }

        public async Task<ActionResult> IndexGetAll()
        {
            List<Equipo> listaEquipos = new List<Equipo>();

            listaEquipos = equipoData.getAllEquipos();

            return View(listaEquipos);
        }

        // GET: Equipo/Details/5
        public ActionResult Details(int id)
        {
            return View(equipoData.getEquipoById(id));
        }

        // GET: Equipo/Create
        public ActionResult Create()
        {
            var proveedores = proveedorDao.getAllProvedores();
            ViewData["proveedores"] = proveedores;
            var categories = equipoData.GetCategorias();
            ViewData["categories"] = categories;
            return View();
        }

        // POST: Equipo/Create
        [HttpPost]
        public ActionResult Create(String nombreProducto, String descripcion, int precio, int puntoReorden, String proveedor,
            String categoria, int cantidadDisponible)
        {
            try
            {
                Proveedor proveedorInsertar = proveedorDao.getProveedorByName2(proveedor);
                Categoria categoriaInsertar = equipoData.GetCategoriaByName(categoria);
                Producto producto = new Producto();
                producto.IdProducto = 0;
                producto.Nombre = nombreProducto;
                producto.Descripcion = descripcion;
                producto.Precio = (float)precio;
                producto.PuntoReorden = puntoReorden;
                producto.Categoria = categoriaInsertar;
                producto.Proveedor = proveedorInsertar;
                Equipo equipo = new Equipo();
                equipo.IdEquipo = 0;
                equipo.CantidadDisponible = cantidadDisponible;
                equipo.Producto = producto;
                equipoData.insertarEquipo(equipo);

                return RedirectToAction("Index");
            }
            catch(MySqlException ex)
            {

                return RedirectToAction("Error", "Equipo");
            }
        }

        // GET: Equipo/Edit/5
        public ActionResult Edit(int id)
        {
            equipoAModificar = equipoData.getEquipoById(id);
            return View(equipoAModificar);
        }

        // POST: Equipo/Edit/5
        [HttpPost]
        public ActionResult Edit(Equipo equipo)
        {
            try
            {
                equipoAModificar.Producto.Nombre = equipo.Producto.Nombre;
                equipoAModificar.Producto.Descripcion = equipo.Producto.Descripcion;
                equipoAModificar.Producto.Precio = equipo.Producto.Precio;
                equipoAModificar.CantidadDisponible = equipo.CantidadDisponible;
                equipoAModificar.Producto.PuntoReorden = equipo.Producto.PuntoReorden;

                equipoData.modificarEquipo(equipoAModificar);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error", "Equipo");
            }
        }

        // GET: Equipo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(equipoData.getEquipoById(id));
        }

        // POST: Equipo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                equipoData.suprimirEquipo(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {

                return RedirectToAction("Error","Equipo");
            }
        }
        
        public ActionResult Error()
        {
            return View();
        }
    }
}
