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
    public class ReactivoController : Controller
    {
        ReactivoData reactivoData = new ReactivoData();
        ProveedorDao proveedorDao = new ProveedorDao();
        static Reactivo reactivoAModificar = new Reactivo();
        // GET: Reactivo
        public async Task<ActionResult> Index(string SearchString)
        {
            List<Reactivo> listaReactivos = new List<Reactivo>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                listaReactivos = reactivoData.getReactivoByName(SearchString);
            }
            else
            {
                listaReactivos = reactivoData.getAllReactivos();
            }
            return View(listaReactivos);
        }
        // GET
        public async Task<ActionResult> IndexGetAll( )
        {
            List<Reactivo> listaReactivos = new List<Reactivo>();
          
                listaReactivos = reactivoData.getAllReactivos();
            
            return View(listaReactivos);
        }
        // GET: Reactivo/Details/5
        public ActionResult Details(int id)
        {
            return View(reactivoData.getReactivoById(id));
        }

        // GET: Reactivo/Create
        public ActionResult Create()
        {
            var proveedores = proveedorDao.getAllProvedores();
            ViewData["proveedores"] = proveedores;
            var categories = reactivoData.GetCategorias();
            ViewData["categories"] = categories;
            return View();
        }

        // POST: Reactivo/Create
        [HttpPost]
        public ActionResult Create(String nombreProducto, String descripcion,int precio,int puntoReorden,String proveedor,
            String categoria,String unidadMedida,int cantidadDisponible,String estado)
        {
            try
            {
                Proveedor proveedorInsertar = proveedorDao.getProveedorByName2(proveedor);
                Categoria categoriaInsertar = reactivoData.GetCategoriaByName(categoria);
                Producto producto = new Producto();
                producto.IdProducto = 0;
                producto.Nombre = nombreProducto;
                producto.Descripcion = descripcion;
                producto.Precio = (float)precio;
                producto.PuntoReorden = puntoReorden;
                producto.Categoria = categoriaInsertar;
                producto.Proveedor = proveedorInsertar;
                Reactivo reactivo = new Reactivo();
                reactivo.IdReactivo = 0;
                reactivo.CantidadDisponible = cantidadDisponible;
                reactivo.Estado = estado;
                reactivo.UnidadMedida = unidadMedida;
                reactivo.Producto = producto;

                reactivoData.insertarReactivo(reactivo);


                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Reactivo");

            }
        }

        // GET: Reactivo/Edit/5
        public ActionResult Edit(int id)
        {
             reactivoAModificar = reactivoData.getReactivoById(id);
            return View(reactivoAModificar);
        }

        // POST: Reactivo/Edit/5
        [HttpPost]
        public ActionResult Edit(Reactivo reactivo)
        {
            try
            {
                reactivoAModificar.Producto.Nombre = reactivo.Producto.Nombre;
                reactivoAModificar.Producto.Descripcion = reactivo.Producto.Descripcion;
                reactivoAModificar.Producto.Precio = reactivo.Producto.Precio;
                reactivoAModificar.Producto.PuntoReorden = reactivo.Producto.PuntoReorden;
                reactivoAModificar.UnidadMedida = reactivo.UnidadMedida;
                reactivoAModificar.Estado = reactivo.Estado;
                reactivoAModificar.CantidadDisponible = reactivo.CantidadDisponible;

                reactivoData.modificarReactivo(reactivoAModificar);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Reactivo");

            }
        }

        // GET: Reactivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(reactivoData.getReactivoById(id));
        }

        // POST: Reactivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                reactivoData.suprimirReactivo(id);

                return RedirectToAction("Index");
            }
            catch (MySqlException ex)
            {
                return RedirectToAction("Error", "Reactivo");

            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
