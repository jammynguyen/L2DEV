CREATE   VIEW [dbo].[V_RawMaterialOverview] AS 

SELECT ROW_NUMBER() OVER(
       ORDER BY AEXT.EnumArea, 
                A.OrderSeq, 
                RM.LastUpdateTs) Sorting, 
       AEXT.EnumArea Area, 
       A.AssetName, 
       A2.AssetName ParentAssetName, 
       RM.CreatedTs RawMaterialCreated, 
       RM.LastUpdateTs RawMaterialLastUpdated, 
       RMS0.CreatedTs, 
       RM.[Status] RawMaterialStatus, 
       RM.RawMaterialId, 
       RM.ExternalRawMaterialId AS RawMaterialExternalId, 
       RM.RawMaterialName, 
       RMS1.Weight AS RawMaterialDeclaredWeight, 
       RMS1.Length AS RawMaterialDeclaredLength, 
       RMS2.Weight AS RawMaterialInitialWeight, 
       RMS2.Length AS RawMaterialInitialLength, 
       RMS0.Weight AS RawMaterialLastWeight, 
       RMS0.Length AS RawMaterialLastLength, 
       RM.LastProcessingStepNo AS LastStepNo, 
       RM.IsVirtual AS RawMaterialIsVirtual, 
       CAST(CASE
                WHEN RM.FKMaterialId IS NOT NULL
                THEN 1
                ELSE 0
            END AS BIT) AS IsMaterialAssigned, 
       CAST(CASE
                WHEN RM.FKProductId IS NOT NULL
                THEN 1
                ELSE 0
            END AS BIT) AS IsProductAssigned, 
       ISNULL(M.MaterialName, RM.RawMaterialName) MaterialName, 
       H.HeatName, 
       S.SteelgradeCode, 
       S.SteelgradeName, 
       SG.SteelGroupCode, 
       SG.SteelGroupName, 
       WO.WorkOrderName WorkOrderName, 
       MC.[MaterialCatalogueName] MaterialCatalogueName, 
       PC.[ProductCatalogueName] ProductCatalogueName, 
       CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', ShiftCode) ShiftKey, 
       SD.ShiftCode ShiftCode, 
       C.CrewName, 
       RM.CuttingSeqNo AS RawMaterialCutNo, 
       RM.ChildsNo AS RawMaterialChildNo, 
       RM1.RawMaterialName AS ParentRawMaterialName, 
       RM1.ExternalRawMaterialId AS ParentRawMaterialExternalId, 
       RM1.CuttingSeqNo AS ParentRawMaterialCutNo, 
       RM1.ChildsNo AS ParentRawMaterialChildNo, 
       RM.IsTransferred2DW AS RawMaterialInDW, 
       SC.ShiftCalendarId AS DimShiftKey, 
       M.FKWorkOrderId AS DimWorkOrderKey, 
       RM.RawMaterialId AS DimRawMaterialKey, 
       RM.FKMaterialId AS DimMaterialKey, 
       RM.FKProductId AS DimProductKey, 
       S.FKSteelGroupId AS DimSteelGroupKey, 
       S.SteelgradeId AS DimSteelgradeKey, 
       H.HeatId AS DimHeatKey, 
       RM.[Status] AS DimRawMaterialStatusKey, 
       RM.ParentRawMaterialId AS DimParentRawMaterialKey, 
       RM1.[Status] AS DimParentRawMaterialStatusKey, 
       RMS0.FKAssetId AS DimLastAssetKey, 
       C.CrewId DimCrewKey, 
       WO.FKMaterialCatalogueId DimMaterialCatalogueKey, 
       WO.FKProductCatalogueId DimProductCatalogueKey, 
       VA29.[Weight] AS Asset29Weight
FROM dbo.MVHRawMaterials AS RM
     INNER JOIN dbo.MVHRawMaterialsSteps AS RMS0 ON RM.RawMaterialId = RMS0.FKRawMaterialId
                                                    AND RMS0.ProcessingStepNo = 0
     INNER JOIN dbo.MVHRawMaterialsSteps AS RMS1 ON RM.RawMaterialId = RMS1.FKRawMaterialId
                                                    AND RMS1.ProcessingStepNo = 1
     LEFT OUTER JOIN dbo.MVHRawMaterialsSteps AS RMS2 ON RM.RawMaterialId = RMS2.FKRawMaterialId
                                                         AND RMS2.ProcessingStepNo = 2
     LEFT OUTER JOIN dbo.MVHRawMaterials AS RM1 ON RM.ParentRawMaterialId = RM1.RawMaterialId
     INNER JOIN dbo.MVHAssets AS A ON RMS0.FKAssetId = A.AssetId
     LEFT JOIN dbo.MVHAssets A2 ON A.ParentAssetId = A2.AssetId
     INNER JOIN dbo.MVHAssetsEXT AS AEXT ON A.AssetId = AEXT.FKAssetId
     LEFT OUTER JOIN dbo.PRMMaterials M
     INNER JOIN dbo.PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
     INNER JOIN dbo.PRMMaterialCatalogue MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
     INNER JOIN dbo.PRMProductCatalogue PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
     INNER JOIN dbo.PRMHeats H ON M.FKHeatId = H.HeatId
     INNER JOIN dbo.PRMSteelgrades S ON MC.FKSteelgradeId = S.SteelgradeId
     LEFT OUTER JOIN dbo.PRMSteelGroups SG ON S.FKSteelGroupId = SG.SteelGroupId ON RM.FKMaterialId = M.MaterialId
     LEFT OUTER JOIN [dbo].[ShiftCalendar] SC
     INNER JOIN [dbo].[ShiftDefinitions] AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
     INNER JOIN [dbo].[Crews] AS C ON SC.FKCrewId = C.CrewId ON dbo.FNGetShiftId(RM.CreatedTs) = SC.ShiftCalendarId
     OUTER APPLY dbo.[FNTRawMaterialStepValueByAssetId](RM.RawMaterialId, 29) AS VA29;