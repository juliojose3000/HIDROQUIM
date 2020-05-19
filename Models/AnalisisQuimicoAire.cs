using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisQuimicoAire
    {
        private int idAire;
        private string contaminante;
        private string tipoEstandar;
        private float valorReferencia;
        private float tiempoPromedio;
        private string metodoReferencia;
        private string resultadoAnalisis;
        private AnalisisQuimico analisisQuimico;

        public AnalisisQuimicoAire()
        {
            this.analisisQuimico = new AnalisisQuimico();
        }

        public AnalisisQuimicoAire(string contaminante, string tipoEstandar, float valorReferencia, float tiempoPromedio, string metodoReferencia, string resultadoAnalisis, AnalisisQuimico analisisQuimico)
        {
            this.contaminante = contaminante;
            this.tipoEstandar = tipoEstandar;
            this.valorReferencia = valorReferencia;
            this.tiempoPromedio = tiempoPromedio;
            this.metodoReferencia = metodoReferencia;
            this.resultadoAnalisis = resultadoAnalisis;
            this.analisisQuimico = analisisQuimico;
        }

        public int IdAire { get => idAire; set => idAire = value; }
        public string Contaminante { get => contaminante; set => contaminante = value; }
        public string TipoEstandar { get => tipoEstandar; set => tipoEstandar = value; }
        public float ValorReferencia { get => valorReferencia; set => valorReferencia = value; }
        public float TiempoPromedio { get => tiempoPromedio; set => tiempoPromedio = value; }
        public string MetodoReferencia { get => metodoReferencia; set => metodoReferencia = value; }
        public AnalisisQuimico AnalisisQuimico { get => analisisQuimico; set => analisisQuimico = value; }
        public string ResultadoAnalisis { get => resultadoAnalisis; set => resultadoAnalisis = value; }
    }
}