using CsvHelper;
using EntityFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// CSV processing test
    /// </summary>
    public class CsvProcessingTest : ITest
    {
        public void Run()
        {
            // TODO
            // Create a domain model via POCO classes to store the data available in the CSV file below
            // Objects to be present in the domain model: Asset, Country and Mime type
            // Process the file in the most robust way possible
            // The use of 3rd party plugins is permitted
            //var csvFile = Resources.AssetImport;

            using (var ctx = new AssetContext())
            {
                try
                {
                    IList<AssetInformation> assets = new List<AssetInformation>();
                    string fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AssetImport.csv");
                    if (File.Exists(fileLocation))
                    {
                        using (StreamReader sr = new StreamReader(fileLocation))
                        {
                            using (CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                            {
                                csv.Context.RegisterClassMap<AssetInformationClassMap>();
                                assets = csv.GetRecords<AssetInformation>().ToList();
                                foreach (var item in assets)
                                {
                                    item.CreatedOn = DateTime.Now;
                                }
                            }
                        }
                    }
                    EFBatchOperation.For(ctx, ctx.Assets).InsertAll(assets);
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }
    }

}
