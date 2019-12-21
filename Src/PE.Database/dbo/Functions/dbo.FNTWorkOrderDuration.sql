CREATE FUNCTION [dbo].[FNTWorkOrderDuration](@WorkOrderId BIGINT)
RETURNS TABLE
AS
     RETURN
     --DECLARE @WorkOrderId BIGINT = 1265
     SELECT M.FKWorkOrderId AS WorkOrderId, 
            MIN(MV.CreatedTs) ProductionStart, 
            MAX(MV.CreatedTs) ProductionEnd, 
            24 * 60 * 60 * (CONVERT(FLOAT, MAX(MV.CreatedTs)) - CONVERT(FLOAT, MIN(MV.CreatedTs))) AS Duration, 
            MIN(CASE
                    WHEN F.EnumFeatureType = 4
                    THEN MV.CreatedTs
                    ELSE NULL
                END) AS FurnaceIn, 
            MAX(CASE
                    WHEN F.EnumFeatureType = 5
                    THEN MV.CreatedTs
                    ELSE NULL
                END) AS FurnaceOut, 
            MIN(CASE
               WHEN AEXT.EnumArea = 1
               THEN MV.CreatedTs
               ELSE NULL
           END) AS FurnaceZoneStart, 
       MAX(CASE
               WHEN AEXT.EnumArea = 1
               THEN MV.CreatedTs
               ELSE NULL
           END) AS FurnaceZoneEnd, 
       MIN(CASE
               WHEN AEXT.EnumArea = 2
               THEN MV.CreatedTs
               ELSE NULL
           END) AS RollingStart, 
       MAX(CASE
               WHEN AEXT.EnumArea = 2
               THEN MV.CreatedTs
               ELSE NULL
           END) AS RollingEnd, 
       MIN(CASE
               WHEN AEXT.EnumArea = 3
               THEN MV.CreatedTs
               ELSE NULL
           END) AS AfterRollingStart, 
       MAX(CASE
               WHEN AEXT.EnumArea = 3
               THEN MV.CreatedTs
               ELSE NULL
           END) AS AfterRollingEnd
     FROM [dbo].[MVHMeasurements] MV
          INNER JOIN MVHFeatures F ON MV.FKFeatureId = F.FeatureId
          INNER JOIN MVHAssets A ON F.FKAssetId = A.AssetId
          INNER JOIN MVHAssetsEXT AEXT ON A.AssetId = AEXT.FKAssetId
          INNER JOIN MVHRawMaterials RM ON MV.FKRawMaterialId = RM.RawMaterialId
          INNER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
     WHERE M.FKWorkOrderId = @WorkOrderId
     GROUP BY M.FKWorkOrderId;

/*
	 SELECT WO.WorkOrderId, 
            MIN(RMS.CreatedTs) ProductionStart, 
            MAX(RMS.LastUpdateTs) ProductionEnd, 
            24 * 60 * 60 * (CONVERT(FLOAT, MAX(RMS.LastUpdateTs)) - CONVERT(FLOAT, MIN(RMS.CreatedTs))) AS Duration
     FROM dbo.PRMWorkOrders AS WO
          LEFT OUTER JOIN dbo.PRMMaterials AS M
          LEFT OUTER JOIN dbo.MVHRawMaterials AS RM
          INNER JOIN dbo.MVHRawMaterialsSteps AS RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
                                                        AND RMS.ProcessingStepNo = 0 ON M.MaterialId = RM.FKMaterialId ON WO.WorkOrderId = M.FKWorkOrderId
	 WHERE WO.WorkOrderId = @WorkOrderId     
	 GROUP BY WO.WorkOrderId;
*/