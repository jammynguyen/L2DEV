using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.L1S.Handlers.MillSetup;

namespace PE.L1S.Handlers
{
  public class AssetHandler
  {
    #region area initialization

    public async Task<List<MillAssetConfig>> GetAreaAssets(PEContext ctx, AssetsArea assetsArea)
    {
      List<MillAssetConfig> returnValue = new List<MillAssetConfig>();

      List<V_SimAssetSeq> asset = await ctx.V_SimAssetSeq.Where(x => x.EnumArea == assetsArea).OrderBy(x => x.SeqNo).ToListAsync();
      IEnumerable<long> assetIdList = asset.Select(x => x.AssetId);
      List<MVHAsset> listOfAssets = ctx.MVHAssets.Include(i => i.MVHAssetsEXT).Include(i => i.MVHFeatures).Where(x => assetIdList.Contains(x.AssetId)).ToList();
      foreach (MVHAsset mVHAsset in listOfAssets)
      {
        List<MVHFeature> listOfFeatures = await ctx.MVHFeatures.Include(i => i.MVHFeaturesEXT).Where(x => x.FKAssetId == mVHAsset.AssetId).ToListAsync();
        MillAssetConfig millAssetConfig = new MillAssetConfig(
            mVHAsset.AssetId,
            mVHAsset.AssetName,
            mVHAsset.MVHAssetsEXT.MaxPassNo,
            mVHAsset.MVHAssetsEXT.TimeIn.Value,
            (int)asset.Where(x => x.AssetId == mVHAsset.AssetId).Select(x => x.SeqNo).FirstOrDefault(),
            mVHAsset.MVHAssetsEXT.IsLast.Value
            ) ;

        foreach (MVHFeature mVHFeature in listOfFeatures)
        {
          if (!mVHFeature.IsActive)
            continue;
          MillFeatureConfig feature = new MillFeatureConfig(
            mVHFeature.FeatureId,
            mVHFeature.FeatureName,
            mVHFeature.MVHFeaturesEXT.MinValue,
            mVHFeature.MVHFeaturesEXT.MaxValue,
            FeatureActivation.Enter,
            true,
						mVHFeature.FeatureCode);
          millAssetConfig.AddFeature(feature);
        }
        returnValue.Add(millAssetConfig);
      }

      return returnValue.OrderBy(x=>x.AssetSequence).ToList();
    }

    public void DeleteOldMaterials(PEContext ctx)
    {
      if (ctx == null) { ctx = new PEContext(); }

      ctx.SPClearRawMaterials();
    }

    #endregion
  }
}
