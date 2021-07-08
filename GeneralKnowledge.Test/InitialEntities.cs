using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App
{

    class InitialEntities : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
            "dbo.Usp_GetAllAssets",
             p => new
             {
                 PageNo = p.Int(),
                 PageSize = p.Int(),
                 SortOrder = p.String(),
             },
            body:
                @"Select * From   (Select ROW_NUMBER() Over (  
    Order by Country ) AS 'RowNum', *  
         from   Assets 
        )t  where t.RowNum Between ((@PageNo-1)*@PageSize +1) AND (@PageNo*@pageSize)  
        "
        );
            CreateStoredProcedure(
            "dbo.Usp_GetAllAssetsCount",
            body:
                @"select count(AssetId) from   Assets"
        );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.Usp_GetAllAssetsCount");
            DropStoredProcedure("dbo.Usp_GetAllAssets");
        }
    }
}
