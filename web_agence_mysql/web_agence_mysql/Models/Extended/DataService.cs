using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_agence_mysql.Models
{
    public static class DataService
    {
        public static List<DataPoint_Pie> GetPiePedidosFacturasCobros(object lista)
        {
            _dataPointsPie = new List<DataPoint_Pie>();
            if (lista is List<grafico_pie>)
            {
                List<grafico_pie> lista_sol_cot_pro = lista as List<grafico_pie>;

                foreach (var valor in lista_sol_cot_pro)
                {
                    _dataPointsPie.Add(new DataPoint_Pie(valor.cantidad, valor.descripcion));
                }
            }
            return _dataPointsPie;
        }

        private static List<DataPoint_Pie> _dataPointsPie;
    }
}