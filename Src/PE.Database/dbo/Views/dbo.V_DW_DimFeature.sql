CREATE   VIEW [dbo].[V_DW_DimFeature]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          F.FeatureId AS DimFeatureKey, 
          F.FKAssetId AS DimAssetKey, 
          F.FKUnitOfMeasureId AS DimUOMKey, 
          F.FKDataTypeId AS DimDataTypeKey, 
          F.FeatureCode AS FeatureCode, 
          F.FeatureName, 
          F.FeatureDescription, 
          F.IsMaterialRelated AS FeatureIsMaterialRelated, 
          F.IsLengthRelated AS FeatureIsLengthRelated, 
          F.IsTrigger AS FeatureIsTrigger, 
          F.IsNewProcessingStep AS FeatureIsNewProcessingStep, 
          CAST(FE.MinValue AS NUMERIC(18, 8)) AS FeatureMinValue, 
          CAST(FE.MaxValue AS NUMERIC(18, 8)) AS FeatureMaxValue
   FROM dbo.MVHFeatures AS F
        LEFT OUTER JOIN dbo.MVHFeaturesEXT AS FE ON F.FeatureId = FE.FKFeatureId;