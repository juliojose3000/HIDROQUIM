using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class CUII
    {
        private int codCUII;
        private int cuii;
        private string actividad;
        private List<CUIIParametroValor> parametrosValores;
        private String parametros;


        public CUII()
        {

            this.parametrosValores = new List<CUIIParametroValor>();
        }

        public CUII(int codCiiu, int ciiu, string actividad)
        {
            this.parametrosValores = new List<CUIIParametroValor>();
            this.codCUII = codCiiu;
            this.cuii = ciiu;
            this.actividad = actividad;
        }


        public int CodCUII { get => codCUII; set => codCUII = value; }
        public int Cuii { get => cuii; set => cuii = value; }
        public string Actividad { get => actividad; set => actividad = value; }
        public List<CUIIParametroValor> ParametrosValores { get => parametrosValores; set => parametrosValores = value; }
        public string Parametros { get => parametros; set => parametros = value; }
    }
}