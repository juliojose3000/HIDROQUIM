using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Parametro
    {

        private int idNivelAguaPotable;
        private String nombreParametro;
        private String unidad;
        private String valorObtenido;
        private String valorRecomendado;
        private String valorMaximoAdmisible;

        public Parametro()
        {

        }

        public int IdNivelAguaPotable { get => idNivelAguaPotable; set => idNivelAguaPotable = value; }
        public String NombreParametro { get => nombreParametro; set => nombreParametro = value; }
        public String Unidad { get => unidad; set => unidad = value; }
        public String ValorObtenido { get => valorObtenido; set => valorObtenido = value; }
        public String ValorRecomendado { get => valorRecomendado; set => valorRecomendado = value; }
        public String ValorMaximoAdmisible { get => valorMaximoAdmisible; set => valorMaximoAdmisible = value; }


    }
}