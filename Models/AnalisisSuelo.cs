using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisSuelo
    {
        private int idSuelo;
        private string descripcion;
        private string determinacion;

        public int IdSuelo { get => idSuelo; set => idSuelo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Determinacion { get => determinacion; set => determinacion = value; }
        public AnalisisSuelo()
        { }
    }
}