1. Instructions to set up the code first approach before start with executing the project.

Please configure the database name in the place of updateInfo in order to use local SQL DB. If set up is not required kindly remove the configuration from the App.config

<add name="Asset-CodeFirst" connectionString="Server=UpdateInfo;Database=AssetsInformation;Integrated Security=SSPI;persist security info=True;" providerName="System.Data.SqlClient" />

Once after the successful execution kindly proceed with executing this line in the project set up
This will generate the necessary stored procedure to proceed further. Stored procedure information attached seperately for reference.


Update-Database -configuration:TechnicalAssessment.DataImport.spAssets.Configuration -Verbose



Migration creation steps listed below. Not required as of now because it's already been created
1. enable-Migrations -ContextTypeName GeneralKnowledge.Test.App.AssetContext -MigrationsDirectory:spAssets

2.Add-Migration -configuration GeneralKnowledge.Test.App.spAssets.Configuration InitialEntities

3. Update-Database -configuration:GeneralKnowledge.Test.App.spAssets.Configuration -Verbose

