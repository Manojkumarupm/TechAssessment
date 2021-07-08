using log4net;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebExperience.Test.Controllers
{
    [EnableCors(origins: "http://locathost:4200", headers: "*", methods: "*")]
    public class AssetsController : ApiController
    {
        private readonly log4net.ILog logger = LogManager.GetLogger(typeof(AssetsController));
        private AssetsInformationEntities db = new AssetsInformationEntities();
        /// <summary>
        /// Get Assets information based on the paged data
        /// </summary>
        /// <param name="pageNo">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <returns></returns>
        [HttpGet]
        public object getAssetsForPage(int pageNo, int pageSize, string sortOrder)
        {
            try
            {
                logger.Info(string.Format("Inside getAssetsForPage with Page No: {0} , Page Size : {1}, sort order {2}", pageNo, pageSize, sortOrder));
                var oMyString = new ObjectParameter("totalCount", typeof(int));
                var AssetDetails = db.Usp_GetAllAssets(pageNo, pageSize, sortOrder).ToList();
                return AssetDetails;
            }
            catch (Exception e)
            {
                logger.Error(string.Format("Inside getAssetsForPage with Page No: {0} , Page Size : {1}, sort order {2} , Exception {3}", pageNo, pageSize, sortOrder, e.Message));
                return null;
            }

        }
        /// <summary>
        /// Get Aseet Information based on AssetId
        /// </summary>
        /// <param name="AssetId">AssetId</param>
        /// <returns></returns>
        [HttpGet]
        public object getAssetInformation(string AssetId)
        {
            Asset AssetDetails = null;
            try
            {
                logger.Info(string.Format("Inside getAssetInformation with AssetId: {0}", AssetId));
                AssetDetails = db.Assets.First(x => x.AssetId == AssetId);

            }
            catch (Exception e)
            {
                logger.Error(string.Format("Inside getAssetInformation with AssetId: {0} , Exception {1}", AssetId, e.Message));
            }
            return AssetDetails;
        }
        /// <summary>
        /// Get All Asset Count
        /// </summary>
        /// <returns>count of assets</returns>
        [HttpGet]
        public object getAllAssetsCount()
        {
            try
            {
                var AssetDetailsCount = db.Usp_GetAllAssetsCount().SingleOrDefault();
                logger.Info(string.Format("Inside getAllAssetsCount with Count: {0}", AssetDetailsCount));
                return AssetDetailsCount;
            }
            catch (Exception e)
            {
                logger.Error(string.Format("Inside getAllAssetsCount with Exception: {0}", e.Message));
                return 0;
            }

        }
        /// <summary>
        /// Create a new asset
        /// </summary>
        /// <param name="asset">AssetInformation</param>
        /// <returns></returns>
        [HttpPost]
        // POST: api/Weather
        public bool CreateAsset([FromBody] object asset)
        {
            bool Result = false;
            try
            {
                logger.Info(string.Format("Inside CreateAsset with Count: {0}", asset.ToString()));
                Asset assetInfo = JsonConvert.DeserializeObject<Asset>(asset.ToString());
                if (assetInfo != null)
                {
                    assetInfo.AssetId = Guid.NewGuid().ToString();
                    assetInfo.Created_By = String.IsNullOrEmpty(assetInfo.Created_By) ? "Admin" : assetInfo.Created_By;
                    assetInfo.CreatedOn = DateTime.Now;
                    db.Assets.Add(assetInfo);
                    db.SaveChanges();
                    Result = true;
                }
            }
            catch (Exception e)
            {
                logger.Error(string.Format("Inside CreateAsset with Exception: {0}", e.Message));
            }
            return Result;
        }
        /// <summary>
        /// Update Asset in database
        /// </summary>
        /// <param name="AssetId">AssetId</param>
        /// <param name="asset">asset</param>
        /// <returns></returns>
        [HttpPut]
        public bool UpdateAsset([FromBody] object asset)
        {
            bool Result = false;
            try
            {
                Asset assetInfo = JsonConvert.DeserializeObject<Asset>(asset.ToString());
                logger.Info(string.Format("Inside UpdateAsset with AssetId: {0} , Asset : {1}", assetInfo.AssetId, assetInfo.ToString()));
                Asset validate = db.Assets.First(x => x.AssetId == assetInfo.AssetId);
                if (validate.AssetId != null)
                {
                    validate.Country = assetInfo.Country;
                    validate.FileName = assetInfo.FileName;
                    validate.MimeType = assetInfo.MimeType;
                    validate.Email = assetInfo.Email;
                    validate.Created_By = assetInfo.Created_By;
                    Result = true;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                logger.Error(string.Format("Inside UpdateAsset with Exception: {0}", e.Message));
            }
            return Result;
        }
        /// <summary>
        /// Delete asset base don AssetId
        /// </summary>
        /// <param name="AssetId"></param>
        /// <returns></returns>
        [HttpDelete]
        public object DeleteAsset(string AssetId)
        {
            logger.Info(string.Format("Inside Delete with AssetId: {0} ", AssetId));
            Asset asset = db.Assets.First(x => x.AssetId == AssetId);
            if (asset != null)
            {
                db.Assets.Remove(asset);
            }
            return asset;
        }
    }
}
