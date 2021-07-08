using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App
{
    class AssetDBInitializer : CreateDatabaseIfNotExists<AssetContext>
    {
        protected override void Seed(AssetContext context)
        {

        }
    }
}
