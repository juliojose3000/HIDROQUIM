using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class EquipoData
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True"; ProveedorDao proveedorDao = new ProveedorDao();
        public EquipoData()
        {
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public List<Categoria> GetCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select id_categoria,nombre from categoria;";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Categoria categeoria = null;
                    while (reader.Read())
                    {
                        categeoria = new Categoria();
                        categeoria.IdCategoria = reader.GetInt32("id_categoria");
                        categeoria.Nombre = reader.GetString("nombre");
                        categorias.Add(categeoria);
                    }
                    sqlCon.Close();

                }

            }
            return categorias;

        }
        public Categoria GetCategoriaByName(string name)
        {
            Categoria categoria;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select id_categoria,nombre from categoria where nombre='" + name + "';";
                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    categoria = null;
                    while (reader.Read())
                    {
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("nombre");

                    }
                    sqlCon.Close();

                }

            }

            return categoria;

        }

        public List<Equipo> getAllEquipos()
        {
            List<Equipo> list = new List<Equipo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario, e.id_equipo, e.cantidad_disponible from categoria c,proveedor pro," +
                " producto p, equipo e where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=e.id_producto;";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    Equipo equipo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        equipo = new Equipo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        equipo.IdEquipo = reader.GetInt32("id_equipo");
                        equipo.CantidadDisponible = reader.GetInt32("cantidad_disponible");
                      

                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        equipo.Producto = producto;


                        list.Add(equipo);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }
        public Equipo getEquipoById(int id)
        {
            Equipo equipo;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario,  e.id_equipo, e.cantidad_disponible from categoria c,proveedor pro," +
                " producto p, equipo e where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=e.id_producto AND e.id_equipo=" + id + ";";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    equipo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        equipo = new Equipo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        equipo.IdEquipo = reader.GetInt32("id_equipo");
                        equipo.CantidadDisponible = reader.GetInt32("cantidad_disponible");


                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        equipo.Producto = producto;



                    }
                    sqlCon.Close();
                }

            }
            return equipo;
            
        }


        public List<Equipo> getEquipoByName(string name)
        {
            List<Equipo> list = new List<Equipo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario, e.id_equipo, e.cantidad_disponible from categoria c,proveedor pro," +
                " producto p, equipo e where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=e.id_producto AND p.nombre LIKE '" + name + "%';";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    Equipo equipo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        equipo = new Equipo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        equipo.IdEquipo = reader.GetInt32("id_equipo");
                        equipo.CantidadDisponible = reader.GetInt32("cantidad_disponible");


                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        equipo.Producto = producto;


                        list.Add(equipo);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }

        public int getIDProducto(string name)
        {
            int idProducto = 0;
            using (MySqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                String query = "Select p.id_producto from  producto p WHERE p.nombre='" + name + "';";

                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idProducto = reader.GetInt32("id_producto");

                    }
                    sqlCon.Close();
                }

            }
            return idProducto;
        }

        public void insertarEquipo(Equipo equipo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
               
                String query1 = "Insert into producto (precio_unitario,nombre,descripcion,id_proveedor,id_Categoria,punto_reorden) values ("
                    + equipo.Producto.Precio + ",'" + equipo.Producto.Nombre + "','" + equipo.Producto.Descripcion + "'," +
                    +equipo.Producto.Proveedor.IdProveedor + "," + equipo.Producto.Categoria.IdCategoria + "," + equipo.Producto.PuntoReorden + ")";
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;

                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect = new MySqlCommand(query1, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();
                    int idProducto = getIDProducto(equipo.Producto.Nombre);
                    transaction2 = sqlCon.BeginTransaction();
                    String query2 = "Insert into equipo (id_producto,cantidad_disponible) values (" +
                       idProducto + "," + equipo.CantidadDisponible + ");";

                      MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                     sqlSelect2.Transaction = transaction2;

                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();


                }
                catch(MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    if (transaction2 != null)
                    {
                        transaction2.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                 {
                    if(sqlCon != null)
                    {
                        sqlCon.Close();
                   }
                }
                }

        }

        public void modificarEquipo(Equipo equipo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;

                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
           
                    String query = "Update producto set nombre='" + equipo.Producto.Nombre + "',descripcion='" +
                     equipo.Producto.Descripcion + "',precio_unitario=" + equipo.Producto.Precio + ",punto_reorden=" + equipo.Producto.PuntoReorden
                     + " where id_producto=" + equipo.Producto.IdProducto;

                    String query2 = "Update equipo set cantidad_disponible=" + equipo.CantidadDisponible +
                    " where id_equipo= " + equipo.IdEquipo;

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();

                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();

                }
                catch (MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    if (transaction2 != null)
                    {
                        transaction2.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlCon != null)
                    {
                        sqlCon.Close();
                    }
                }
            }
        
        }

        public void suprimirEquipo(int idEquipo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;

                try
                {

                    Equipo equipo = getEquipoById(idEquipo);

                    sqlCon.Open();

                    transaction = sqlCon.BeginTransaction();

                    String query = "Delete from equipo  where id_equipo=" + idEquipo + ";";
                    String query2 = "Delete from producto where id_producto= " + equipo.Producto.IdProducto + ";";
                  

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect2.ExecuteNonQuery();
                    transaction2.Commit();


                }
                catch (MySqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    if (transaction2 != null)
                    {
                        transaction2.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlCon != null)
                    {
                        sqlCon.Close();
                    }
                }
            }
        }


    }

}