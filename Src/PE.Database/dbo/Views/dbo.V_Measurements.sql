
CREATE     VIEW [dbo].[V_Measurements]
AS SELECT MV.MeasurementId, 
          F.FeatureId FeatureId, 
          F.FKUnitOfMeasureId UnitOfMeasureId, 
          A.AssetId AssetId, 
          F.FeatureCode, 
          F.FeatureName, 
          A.AssetName, 
          A2.AssetName ParentAssetName, 
          MV.PassNo, 
          MV.IsLastPass, 
          MV.IsValid, 
          MV.CreatedTs MeasurementTime, 
          MV.ValueMin MeasurementValueMin, 
          MV.ValueAvg MeasurementValueAvg, 
          MV.ValueMax MeasurementValueMax, 
          UOM.UnitSymbol
   FROM [dbo].[MVHMeasurements] MV
        INNER JOIN [dbo].[MVHFeatures] F ON MV.FKFeatureId = F.FeatureId
        INNER JOIN [smf].[UnitOfMeasure] UOM ON F.FKUnitOfMeasureId = UOM.UnitId
        INNER JOIN [dbo].[MVHAssets] A ON F.FKAssetId = A.AssetId
        LEFT JOIN [dbo].[MVHAssets] A2 ON A.ParentAssetId = A2.AssetId