using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class CUIIParametroValor
    {
        private int codCuii;
        private string valor;
        private string parametro;

        public CUIIParametroValor(int codCuii, string valor, string parametro)
        {
            this.codCuii = codCuii;
            this.valor = valor;
            this.parametro = parametro;
        }

        public CUIIParametroValor()
        {
        }

        public int CodCuii { get => codCuii; set => codCuii = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Parametro { get => parametro; set => parametro = value; }
    }
}