using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Categoria
    {
        private int idCategoria;
        private string nombre;

        public Categoria()
        {

        }

        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string Nombre { get => nombre; set => nombre = value; }

     
     
    }
}