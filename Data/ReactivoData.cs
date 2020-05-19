using HIDROQUIM.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class ReactivoData
    {
        String connectionString = "Server=35.184.232.105; Database=hidroquim; Uid=root; Pwd=jsss123; convert zero datetime=True"; ProveedorDao proveedorDao = new ProveedorDao();
        public ReactivoData()
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
        public List<Reactivo> getAllReactivos()
        {
            List<Reactivo> list = new List<Reactivo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario, r.id_reactivo, r.unidad_medida,r.cantidad_disponible,r.estado from categoria c,proveedor pro," +
                " producto p, reactivo r where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=r.id_producto;";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    Reactivo reactivo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        reactivo = new Reactivo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        reactivo.IdReactivo = reader.GetInt32("id_reactivo");
                        reactivo.UnidadMedida = reader.GetString("unidad_medida");
                        reactivo.CantidadDisponible = reader.GetFloat("cantidad_disponible");
                        reactivo.Estado = reader.GetString("estado");

                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        reactivo.Producto = producto;
                     


                        list.Add(reactivo);
                    }
                    sqlCon.Close();
                }

            }
            return list;
        }
        public Reactivo getReactivoById(int id)
        {
            Reactivo reactivo;
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario, r.id_reactivo, r.unidad_medida,r.cantidad_disponible,r.estado from categoria c,proveedor pro," +
                " producto p, reactivo r where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=r.id_producto AND r.id_reactivo=" + id + ";";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    reactivo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        reactivo = new Reactivo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        reactivo.IdReactivo = reader.GetInt32("id_reactivo");
                        reactivo.UnidadMedida = reader.GetString("unidad_medida");
                        reactivo.CantidadDisponible = reader.GetFloat("cantidad_disponible");
                        reactivo.Estado = reader.GetString("estado");

                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        reactivo.Producto = producto;
                      



                    }
                    sqlCon.Close();
                }

            }
            return reactivo;
            ;
        }


        public List<Reactivo> getReactivoByName(string name)
        {
            List<Reactivo> list = new List<Reactivo>();
            using (MySqlConnection sqlCon = GetConnection())
            {

                sqlCon.Open();
                String query = "Select c.id_categoria, c.nombre as 'NombreCategoria', pro.id_proveedor, p.id_producto, p.nombre as 'Nombre Producto'," +
                " p.descripcion, p.punto_reorden,p.precio_unitario, r.id_reactivo, r.unidad_medida,r.cantidad_disponible,r.estado from categoria c,proveedor pro," +
                " producto p, reactivo r where p.id_proveedor=pro.id_proveedor AND p.id_categoria=c.id_categoria AND" +
                " p.id_producto=r.id_producto AND p.nombre LIKE '" + name + "%';";



                MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                using (MySqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Proveedor proveedor = null;
                    Producto producto = null;
                    Reactivo reactivo = null;
                    Categoria categoria = null;
                    while (reader.Read())
                    {
                        proveedor = new Proveedor();
                        producto = new Producto();
                        reactivo = new Reactivo();
                        categoria = new Categoria();
                        categoria.IdCategoria = reader.GetInt32("id_categoria");
                        categoria.Nombre = reader.GetString("NombreCategoria");
                        proveedor = proveedorDao.getProveedorById(reader.GetInt32("id_proveedor"));
                        producto.IdProducto = reader.GetInt32("id_producto");
                        producto.Nombre = reader.GetString("Nombre Producto");
                        producto.Descripcion = reader.GetString("descripcion");
                        producto.PuntoReorden = reader.GetInt32("punto_reorden");
                        producto.Precio = reader.GetFloat("precio_unitario");
                        reactivo.IdReactivo = reader.GetInt32("id_reactivo");
                        reactivo.UnidadMedida = reader.GetString("unidad_medida");
                        reactivo.CantidadDisponible = reader.GetFloat("cantidad_disponible");
                        reactivo.Estado = reader.GetString("estado");

                        producto.Proveedor = proveedor;
                        producto.Categoria = categoria;
                        reactivo.Producto = producto;
                       


                        list.Add(reactivo);
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

        public void insertarReactivo(Reactivo reactivo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    String query1 = "Insert into producto (precio_unitario,nombre,descripcion,id_proveedor,id_Categoria,punto_reorden) values ("
                        + reactivo.Producto.Precio + ",'" + reactivo.Producto.Nombre + "','" + reactivo.Producto.Descripcion + "'," +
                        +reactivo.Producto.Proveedor.IdProveedor + "," + reactivo.Producto.Categoria.IdCategoria + "," + reactivo.Producto.PuntoReorden + ")";

                    MySqlCommand sqlSelect = new MySqlCommand(query1, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();
                    int idProducto = getIDProducto(reactivo.Producto.Nombre);
                    transaction2 = sqlCon.BeginTransaction();

                    String query2 = "Insert into reactivo (id_producto,unidad_medida,cantidad_disponible,estado) values (" +
                      idProducto + ",'" + reactivo.UnidadMedida + "'," + reactivo.CantidadDisponible + ",'" + reactivo.Estado + "');";

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

        public void modificarReactivo(Reactivo reactivo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();

                    String query = "Update producto set nombre='" + reactivo.Producto.Nombre + "',descripcion='" +
 reactivo.Producto.Descripcion + "',precio_unitario=" + reactivo.Producto.Precio + ",punto_reorden=" + reactivo.Producto.PuntoReorden
 + " where id_producto=" + reactivo.Producto.IdProducto;


                    String query2 = "Update reactivo set unidad_medida='" + reactivo.UnidadMedida + "'," + "estado='" + reactivo.Estado + "', " +
                        "cantidad_disponible=" + reactivo.CantidadDisponible + " where id_reactivo= " + reactivo.IdReactivo;



                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect.ExecuteNonQuery();
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

        public void suprimirReactivo(int idReactivo)
        {
            using (MySqlConnection sqlCon = GetConnection())
            {
                Reactivo reactivo = getReactivoById(idReactivo);
                MySqlTransaction transaction = null;
                MySqlTransaction transaction2 = null;
                try
                {
                    sqlCon.Open();
                    transaction = sqlCon.BeginTransaction();
                    String query = "Delete from reactivo  where id_reactivo=" + idReactivo + ";";
                    String query2 = "Delete from producto where id_producto= " + reactivo.Producto.IdProducto + ";";
                    

                    MySqlCommand sqlSelect = new MySqlCommand(query, sqlCon);
                    sqlSelect.Transaction = transaction;
                    sqlSelect.ExecuteNonQuery();
                    transaction.Commit();

                    transaction2 = sqlCon.BeginTransaction();
                    MySqlCommand sqlSelect2 = new MySqlCommand(query2, sqlCon);
                    sqlSelect2.Transaction = transaction2;
                    sqlSelect.ExecuteNonQuery();
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