--DECLARE @DimRawMaterialKey BIGINT= 26760;
CREATE   VIEW V_DW_FactRawMaterialMeasurement
AS SELECT CONVERT(DATETIME2(7), GETDATE()) AS FactLoadTs, 
          CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          DATEPART(YEAR, M.CreatedTs) AS DimYearKey, 
          CONVERT(BIGINT, CONVERT(VARCHAR(8), M.CreatedTs, 112)) AS DimDateKey, 
          M.MeasurementId DimMeasurementKey, 
          M.FKRawMaterialId DimRawMaterialKey, 
          F.FeatureId DimFeatureKey, 
          A.AssetId DimAssetKey, 
          F.FKUnitOfMeasureId DimUOMKey, 
          F.FeatureCode, 
          F.FeatureName, 
          A.AssetCode, 
          A.AssetName, 
          A2.AssetName ParentAssetName, 
          M.PassNo, 
          M.IsLastPass, 
          M.IsValid, 
          M.ActualLength, 
          M.CreatedTs MeasurementTime, 
          M.ValueAvg MeasurementValue, 
          UOM.UnitSymbol
   FROM [dbo].[MVHMeasurements] M
        INNER JOIN [dbo].[MVHFeatures] F ON M.FKFeatureId = F.FeatureId
        INNER JOIN [dbo].[MVHAssets] A ON F.FKAssetId = A.AssetId
        LEFT JOIN [dbo].[MVHAssets] A2 ON A.ParentAssetId = A2.AssetId
        INNER JOIN [smf].[UnitOfMeasure] UOM ON F.FKUnitOfMeasureId = UOM.UnitId;
--WHERE FKRawMaterialId = @DimRawMaterialKey;