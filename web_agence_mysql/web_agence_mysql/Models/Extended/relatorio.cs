using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_agence_mysql.Models
{
    public class relatorio
    {
        public string co_usuario { get; set; }
        public string no_usuario { get; set; }
        public string periodo { get; set; }
        public float receita_liquida { get; set; }
        public float custo_fixo { get; set; }
        public float comissao { get; set; }
        public float lucro { get; set; }
        public string etiqueta_saldo { get; set; }
        public float total_receita_liquida { get; set; }
        public float total_custo_fixo { get; set; }
        public float total_comissao { get; set; }
        public float total_lucro { get; set; }
    }
}