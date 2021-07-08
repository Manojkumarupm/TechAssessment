using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App
{
    public class AssetConfigurations : EntityTypeConfiguration<AssetInformation>
    {
        public AssetConfigurations() : base()
        {
            this.Property(a => a.AssetId).IsRequired().HasMaxLength(50);
        }
    }
}
