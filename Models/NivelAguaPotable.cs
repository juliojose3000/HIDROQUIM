using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class NivelAguaPotable
    {

        private String idNivel;
        private String nombre;
        List<Parametro> parametros;


        public NivelAguaPotable()
        {
        }

        public String IdNivel { get => idNivel; set => idNivel = value; }
        public String Nombre { get => nombre; set => nombre = value; }
        public List<Parametro> Parametros { get => parametros; set => parametros = value; }



    }
}