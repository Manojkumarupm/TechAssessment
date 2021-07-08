using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App
{
    public class AssetInformation
    {
        [Key]
        public string AssetId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Created_By { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

    }

    public class AssetInformationClassMap : ClassMap<AssetInformation>
    {
        public AssetInformationClassMap()
        {
            Map(x => x.AssetId).Name("asset_id");
            Map(x => x.Country).Name("country");
            Map(x => x.Created_By).Name("created_by");
            Map(x => x.Description).Name("description");
            Map(x => x.FileName).Name("file_name");
            Map(x => x.Email).Name("email");
            Map(x => x.MimeType).Name("mime_type");
            Map(x => x.CreatedOn).Ignore();
        }
    }
}
