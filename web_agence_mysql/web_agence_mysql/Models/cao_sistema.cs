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
    
    public partial class cao_sistema
    {
        public int co_sistema { get; set; }
        public Nullable<long> co_cliente { get; set; }
        public string co_usuario { get; set; }
        public Nullable<long> co_arquitetura { get; set; }
        public string no_sistema { get; set; }
        public string ds_sistema_resumo { get; set; }
        public string ds_caracteristica { get; set; }
        public string ds_requisito { get; set; }
        public string no_diretoria_solic { get; set; }
        public string ddd_telefone_solic { get; set; }
        public string nu_telefone_solic { get; set; }
        public string no_usuario_solic { get; set; }
        public Nullable<System.DateTime> dt_solicitacao { get; set; }
        public Nullable<System.DateTime> dt_entrega { get; set; }
        public Nullable<int> co_email { get; set; }
    }
}