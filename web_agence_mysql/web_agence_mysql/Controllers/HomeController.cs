using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agence_mysql.Models;

namespace web_agence_mysql.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult con_desempenho()
        {
            using (agencedbEntities dc = new agencedbEntities())
            {
                var tipos_usuarios = new List<decimal> { 0, 1, 2 };
                //var respuesta = (from c in dc.cao_usuario.OrderBy(c => c.no_usuario)
                //                 join p in dc.permissao_sistema on c.co_usuario equals p.co_usuario
                //                 where p.co_sistema == 1
                //                 && p.in_ativo == "S"
                //                 && tipos_usuarios.Contains(p.co_tipo_usuario)
                //                 select new {c.co_usuario, c.no_usuario }).ToList();


                var respuesta = (from c in dc.cao_usuario
                                 join p in dc.permissao_sistema on c.co_usuario equals p.co_usuario
                                 where p.co_sistema == 1
                                 && p.in_ativo == "S"
                                 && tipos_usuarios.Contains(p.co_tipo_usuario)
                                 orderby c.no_usuario
                                 select new { c.co_usuario, c.no_usuario }).ToList();


                List<consultores> lista_consultores = new List<consultores>();
                foreach(var item in respuesta)
                {
                    lista_consultores.Add(new consultores() { co_usuario = item.co_usuario, no_usuario = item.no_usuario, ac_seleccion = false });
                }

                ViewBag.consultores = lista_consultores;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Con_Desempenho()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Cargar_Relatorio(string mes_desde, string anio_desde, string mes_hasta, string anio_hasta, string split_co_usuarios)
        {
            bool confirmacion = false;
            List<relatorio> lista_relatorio = new List<relatorio>();

            using (agencedbEntities dc = new agencedbEntities())
            {
                string co_usuario = string.Empty;
                string no_usuario = string.Empty;

                string des_mes = string.Empty;
                string[] array_co_usuario = split_co_usuarios.Split(',');
                int mes_des = Convert.ToInt32(mes_desde);
                int mes_has = Convert.ToInt32(mes_hasta);
                int anio_des = Convert.ToInt32(anio_desde);
                int anio_has = Convert.ToInt32(anio_hasta);

                float total_receita_liquida = 0;
                float total_custo_fixo = 0;
                float total_comissao = 0;
                float total_lucro = 0;

                foreach (string cod_usu in array_co_usuario)
                {
                    total_receita_liquida = 0;
                    total_custo_fixo = 0;
                    total_comissao = 0;
                    total_lucro = 0;

                    string cd_us = cod_usu;
                    no_usuario = (from u in dc.cao_usuario
                                  where u.co_usuario == cd_us
                                  select new { u.no_usuario }).FirstOrDefault().no_usuario;

                    List<long> clientes_co_usuario = new List<long>();
                    var resultado_clientes = (from s in dc.cao_sistema
                                              where s.co_usuario == cd_us
                                              group s by s.co_cliente into grp
                                              select new { grp.Key }).ToList();
                    foreach(var item in resultado_clientes)
                    {
                        if (item.Key != null)
                        {
                            clientes_co_usuario.Add( Convert.ToInt64(item.Key));
                        }
                        
                    }

                    List<int> fechas_co_usuario = new List<int>();
                    var resultado_fechas = (from f in dc.cao_fatura
                                            where clientes_co_usuario.Contains(f.co_cliente)
                                            && f.data_emissao.Year >= anio_des && f.data_emissao.Year <= anio_has
                                            && f.data_emissao.Month >= mes_des && f.data_emissao.Month <= mes_has
                                            //group f by f.data_emissao.Year into grp
                                            //group f by f.data_emissao.Month into grp1
                                            //select new { grp.Key }).ToList();
                                            select new { mes_anio = (f.data_emissao.Year.ToString() + f.data_emissao.Month.ToString()) }).ToList();

                    var resultado_fec_ordenado = resultado_fechas.GroupBy(f => f.mes_anio).Select(x => x.First()).OrderBy(y => y.mes_anio).ToList();
                    foreach (var item in resultado_fec_ordenado)
                    {
                        if (item.mes_anio != string.Empty)
                        {
                            fechas_co_usuario.Add(Convert.ToInt32(item.mes_anio));
                        }
                    }

                    //Carga de relatorio
                    foreach (var item in fechas_co_usuario)
                    {
                        relatorio relatorio_usuario = new relatorio();
                        string mes_anio = item.ToString();
                        string anio = mes_anio.Substring(0,4);
                        string mes = mes_anio.Substring(4);

                        switch (Convert.ToInt32(mes))
                        {
                            case 1:
                                des_mes = "Janeiro";
                                break;
                            case 2:
                                des_mes = "Fevereiro";
                                break;
                            case 3:
                                des_mes = "Março";
                                break;
                            case 4:
                                des_mes = "Abril";
                                break;
                            case 5:
                                des_mes = "Maio";
                                break;
                            case 6:
                                des_mes = "Junho";
                                break;
                            case 7:
                                des_mes = "Julho";
                                break;
                            case 8:
                                des_mes = "Agosto";
                                break;
                            case 9:
                                des_mes = "Setembro";
                                break;
                            case 10:
                                des_mes = "Outubro";
                                break;
                            case 11:
                                des_mes = "Novembro";
                                break;
                            case 12:
                                des_mes = "Dezembro";
                                break;
                        }


                        //-----------------------------------------------

                        int mes_aux = Convert.ToInt32(mes);
                        int anio_aux = Convert.ToInt32(anio);

                        //Calculo Receita Líquida
                        float receita_liquida = 0;
                        receita_liquida = (from f in dc.cao_fatura
                                           where f.data_emissao.Year == anio_aux && f.data_emissao.Month == mes_aux
                                           && clientes_co_usuario.Contains(f.co_cliente)
                                           select ( (f.valor - f.total_imp_inc) )).Sum();

                        //Calculo Custo Fixo
                        float custo_fixo = 0;
                        custo_fixo = (from s in dc.cao_salario
                                      where s.co_usuario == cd_us
                                      select ( s.brut_salario )).FirstOrDefault();

                        //Calculo Comissao
                        float comissao = 0;
                        comissao = (from f in dc.cao_fatura
                                    where f.data_emissao.Year == anio_aux && f.data_emissao.Month == mes_aux
                                    && clientes_co_usuario.Contains(f.co_cliente)
                                    select ( (f.valor - (f.valor * f.total_imp_inc)) * f.comissao_cn )).Sum();
                        //Calculo Lucro
                        float lucro = 0;
                        lucro = (from f in dc.cao_fatura
                                 where f.data_emissao.Year == anio_aux && f.data_emissao.Month == mes_aux
                                 && clientes_co_usuario.Contains(f.co_cliente)
                                 select ( ((f.valor - f.total_imp_inc) - custo_fixo) + ((f.valor - (f.valor * f.total_imp_inc)) * f.comissao_cn) )).Sum();

                        total_receita_liquida = total_receita_liquida + receita_liquida;
                        total_custo_fixo = total_custo_fixo + custo_fixo;
                        total_comissao = total_comissao + comissao;
                        total_lucro = total_lucro + lucro;

                        //-----------------------------------------------

                        relatorio_usuario.co_usuario = cd_us;
                        relatorio_usuario.no_usuario = no_usuario;
                        relatorio_usuario.periodo = des_mes + " de " + anio;
                        relatorio_usuario.receita_liquida = receita_liquida;
                        relatorio_usuario.custo_fixo = custo_fixo;
                        relatorio_usuario.comissao = comissao;
                        relatorio_usuario.lucro = lucro;
                        relatorio_usuario.etiqueta_saldo = "SALDO";
                        relatorio_usuario.total_receita_liquida = 0;
                        relatorio_usuario.total_custo_fixo = 0;
                        relatorio_usuario.total_comissao = 0;
                        relatorio_usuario.total_lucro = 0;

                        lista_relatorio.Add(relatorio_usuario);
                    }

                    foreach (var item in lista_relatorio)
                    {
                        if (item.co_usuario == cd_us)
                        {
                            item.total_receita_liquida = total_receita_liquida;
                            item.total_custo_fixo = total_custo_fixo;
                            item.total_comissao = total_comissao;
                            item.total_lucro = total_lucro;
                        }
                    }

                    confirmacion = true;
                }
            }

            List<grafico_pie> lista = new List<grafico_pie>();
            var consolidado =
                (from g in lista_relatorio
                 group g by new
                 {
                    g.no_usuario,
                    g.total_receita_liquida,
                 } into gcs
                 select new 
                 {
                    no_usuario = gcs.Key.no_usuario,
                    total_receita_liquida = gcs.Key.total_receita_liquida,
                 }).ToList();
            foreach (var item in consolidado)
            {
                grafico_pie gp = new grafico_pie();
                gp.cantidad = item.total_receita_liquida;
                gp.descripcion = item.no_usuario;

                lista.Add(gp);
            }

            return new JsonResult { Data = new { confirmacion = confirmacion, lista_relatorio = lista_relatorio, lista_pie = lista } };
        }

    }
}