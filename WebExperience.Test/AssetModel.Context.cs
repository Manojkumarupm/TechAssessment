//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebExperience.Test
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AssetsInformationEntities : DbContext
    {
        public AssetsInformationEntities()
            : base("name=AssetsInformationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
    
        public virtual ObjectResult<Usp_GetAllAssets_Result> Usp_GetAllAssets(Nullable<int> pageNo, Nullable<int> pageSize, string sortOrder)
        {
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Usp_GetAllAssets_Result>("Usp_GetAllAssets", pageNoParameter, pageSizeParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Usp_GetAllAssetsCount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Usp_GetAllAssetsCount");
        }
    }
}
