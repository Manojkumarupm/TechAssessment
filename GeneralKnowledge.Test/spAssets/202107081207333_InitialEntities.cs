namespace GeneralKnowledge.Test.App.spAssets
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                {
                    AssetId = c.String(nullable: false, maxLength: 50),
                    FileName = c.String(),
                    MimeType = c.String(),
                    Created_By = c.String(),
                    Email = c.String(),
                    Country = c.String(),
                    Description = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.AssetId);

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
            DropTable("dbo.Assets");
            DropStoredProcedure("dbo.Usp_GetAllAssetsCount");
            DropStoredProcedure("dbo.Usp_GetAllAssets");
        }
    }
}
