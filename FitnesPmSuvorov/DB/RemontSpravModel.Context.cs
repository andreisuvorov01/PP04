﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitnesPmSuvorov.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RemontSpravEntities : DbContext
    {
        public RemontSpravEntities()
            : base("name=RemontSpravEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RepairMessages> RepairMessages { get; set; }
        public virtual DbSet<PreviewPages> PreviewPages { get; set; }
    }
}
