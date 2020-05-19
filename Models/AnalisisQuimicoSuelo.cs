using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisQuimicoSuelo
    {
        private int idSuelo;
        private string determinacion;
        private string descripcion;
        private AnalisisQuimico analisisQuimico;

        public AnalisisQuimicoSuelo()
        {
            AnalisisQuimico = new AnalisisQuimico();
        }

        public AnalisisQuimicoSuelo(int idSuelo, string determinacion, string descripcion, AnalisisQuimico analisisQuimico)
        {
            this.IdSuelo = idSuelo;
            this.Determinacion = determinacion;
            this.Descripcion = descripcion;
            this.AnalisisQuimico = analisisQuimico;
        }
        public AnalisisQuimicoSuelo( string determinacion, string descripcion, AnalisisQuimico analisisQuimico)
        {
            this.Determinacion = determinacion;
            this.Descripcion = descripcion;
            this.AnalisisQuimico = analisisQuimico;
        }



        public int IdSuelo { get => idSuelo; set => idSuelo = value; }
        public string Determinacion { get => determinacion; set => determinacion = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public AnalisisQuimico AnalisisQuimico { get => analisisQuimico; set => analisisQuimico = value; }
    }
}