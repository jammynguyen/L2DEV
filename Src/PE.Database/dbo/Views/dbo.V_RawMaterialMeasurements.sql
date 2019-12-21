

CREATE       VIEW [dbo].[V_RawMaterialMeasurements]
AS SELECT M.FKWorkOrderId, 
          MV.FKRawMaterialId RawMaterialId, 
          MV.MeasurementId, 
          M.FKHeatId, 
          F.FeatureId FeatureId, 
          F.FKUnitOfMeasureId UnitOfMeasureId, 
          A.AssetId AssetId, 
          WO.WorkOrderName, 
          RM.RawMaterialName, 
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
        LEFT JOIN [dbo].[MVHRawMaterials] RM
        LEFT JOIN [dbo].[PRMMaterials] M ON RM.FKMaterialId = M.MaterialId
        INNER JOIN [dbo].[PRMWorkOrders] WO ON M.FKWorkOrderId = WO.WorkOrderId ON MV.FKRawMaterialId = RM.RawMaterialId;