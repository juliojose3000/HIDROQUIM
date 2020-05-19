using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisQuimicoAguaResidual
    {
        private int idAguaResidual;
        private AnalisisQuimico analisis;
        private CUII ciiu;
        private string resultadoAnalisisQuimicoAguaResidual;

        public AnalisisQuimicoAguaResidual()
        {
            this.analisis = new AnalisisQuimico();
            this.ciiu = new CUII();
        }

        public AnalisisQuimicoAguaResidual(int idAguaResidual, AnalisisQuimico analisis, CUII ciiu, string resultadoAnalisisQuimicoAguaResidual)
        {
            this.IdAguaResidual = idAguaResidual;
            this.Analisis = analisis;
            this.Ciiu = ciiu;
            this.ResultadoAnalisisQuimicoAguaResidual = resultadoAnalisisQuimicoAguaResidual;
        }

        public int IdAguaResidual { get => idAguaResidual; set => idAguaResidual = value; }
        public AnalisisQuimico Analisis { get => analisis; set => analisis = value; }
        public CUII Ciiu { get => ciiu; set => ciiu = value; }
        public string ResultadoAnalisisQuimicoAguaResidual { get => resultadoAnalisisQuimicoAguaResidual; set => resultadoAnalisisQuimicoAguaResidual = value; }
    }
}