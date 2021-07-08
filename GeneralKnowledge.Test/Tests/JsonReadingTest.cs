using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic data retrieval from JSON test
    /// </summary>
    public class JsonReadingTest : ITest
    {
        public string Name { get { return "JSON Reading Test"; } }

        public void Run()
        {
            var jsonData = Resources.SamplePoints;
            Console.WriteLine(Name);
            // TODO: 
            // Determine for each parameter stored in the variable below, the average value, lowest and highest number.
            // Example output
            // parameter   LOW AVG MAX
            // temperature   x   y   z
            // pH            x   y   z
            // Chloride      x   y   z
            // Phosphate     x   y   z
            // Nitrate       x   y   z
            PrintOverview(jsonData);
        }

        private void PrintOverview(byte[] data)
        {
            try
            {
                string JSONData = Encoding.UTF8.GetString(data); //To read byte as string with appropriate encoding method..
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", ""));
                    // Create one xdocument file
                XElement root = new XElement("Root");
                root.Name = "samples";
                DataSet dataset = JsonConvert.DeserializeObject<DataSet>(JSONData);
                DataTable dataTable = dataset.Tables[0];
                root.Add(from row in dataTable.AsEnumerable()
                         select new XElement("Record",
                                             from column in dataTable.Columns.Cast<DataColumn>()
                                             select new XElement(column.ColumnName, row[column])
                                            )
                       ); // read line by line (data row and convert the same as XElements
                xmlDoc.Add(root);// Add individual rows fetched to xdocument
                var Records = from record in root.Descendants("Record")
                              select record;
                string[] Nodes = { "temperature", "pH", "chloride", "phosphate", "nitrate" };
                Console.WriteLine(string.Format("{0,-20}{1,10:##.00}{2,10:##.00}{3,10:##.00}", "parameter", "LOW", "AVG", "MAX"));
                foreach (var item in Nodes)
                {
                    var average = Records.Select(x => Convert.ToDecimal(String.IsNullOrWhiteSpace(x.Element(item).Value) ? "0" : x.Element(item).Value)).ToList().Average();
                    var low = Records.Where(y => !String.IsNullOrWhiteSpace(y.Element(item).Value)).Select(x => Convert.ToDecimal(String.IsNullOrWhiteSpace(x.Element(item).Value) ? "0" : x.Element(item).Value)).ToList().Min();
                    var high = Records.Where(y => !String.IsNullOrWhiteSpace(y.Element(item).Value)).Select(x => Convert.ToDecimal(String.IsNullOrWhiteSpace(x.Element(item).Value) ? "0" : x.Element(item).Value)).ToList().Max();
                    Console.WriteLine(string.Format("{0,-20}{1,10:##.00}{2,10:##.00}{3,10:##.00}", item, decimal.Parse(low.ToString("0.00")), decimal.Parse(average.ToString("0.00")), decimal.Parse(high.ToString("0.00"))));
                }
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
