//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web_agence_mysql.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cao_os
    {
        public int co_os { get; set; }
        public Nullable<int> nu_os { get; set; }
        public Nullable<int> co_sistema { get; set; }
        public string co_usuario { get; set; }
        public Nullable<int> co_arquitetura { get; set; }
        public string ds_os { get; set; }
        public string ds_caracteristica { get; set; }
        public string ds_requisito { get; set; }
        public Nullable<System.DateTime> dt_inicio { get; set; }
        public Nullable<System.DateTime> dt_fim { get; set; }
        public Nullable<int> co_status { get; set; }
        public string diretoria_sol { get; set; }
        public Nullable<System.DateTime> dt_sol { get; set; }
        public string nu_tel_sol { get; set; }
        public string ddd_tel_sol { get; set; }
        public string nu_tel_sol2 { get; set; }
        public string ddd_tel_sol2 { get; set; }
        public string usuario_sol { get; set; }
        public Nullable<System.DateTime> dt_imp { get; set; }
        public Nullable<System.DateTime> dt_garantia { get; set; }
        public Nullable<int> co_email { get; set; }
        public Nullable<int> co_os_prospect_rel { get; set; }
    }
}
