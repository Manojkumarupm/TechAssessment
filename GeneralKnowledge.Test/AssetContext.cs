using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App
{
    class AssetContext : DbContext
    {
        public AssetContext() : base("Asset-CodeFirst")
        {
            Database.SetInitializer<AssetContext>(new AssetDBInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.Configurations.Add(new AssetConfigurations());
            modelBuilder.Entity<AssetInformation>()
                .ToTable("Assets");
        }
        public DbSet<AssetInformation> Assets { get; set; }
    }
}
