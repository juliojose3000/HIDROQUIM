using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisQuimicoAguaPotable
    {
        private int idAguaPotable;
        private AnalisisQuimico analisisQuimico;
        private String resultado;
        List<NivelAguaPotable> nivelesAguaPotable;

        public AnalisisQuimicoAguaPotable()
        {
        }


        public int IdAguaPotable { get => idAguaPotable; set => idAguaPotable = value; }
        public AnalisisQuimico AnalisisQuimico { get => analisisQuimico; set => analisisQuimico = value; }
        public String Resultado { get => resultado; set => resultado = value; }
        public List<NivelAguaPotable> NivelesAguaPotable { get => nivelesAguaPotable; set => nivelesAguaPotable = value; }

    }
}