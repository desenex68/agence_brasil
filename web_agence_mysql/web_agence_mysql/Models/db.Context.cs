﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class agencedbEntities : DbContext
    {
        public agencedbEntities()
            : base("name=agencedbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cao_fatura> cao_fatura { get; set; }
        public virtual DbSet<cao_os> cao_os { get; set; }
        public virtual DbSet<cao_salario> cao_salario { get; set; }
        public virtual DbSet<cao_sistema> cao_sistema { get; set; }
        public virtual DbSet<cao_usuario> cao_usuario { get; set; }
        public virtual DbSet<permissao_sistema> permissao_sistema { get; set; }
    }
}
