CREATE   VIEW [dbo].[V_DW_FactRawMaterial]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          DATEPART(YEAR, RM.CreatedTs) AS DimYearKey, 
          CONVERT(BIGINT, CONVERT(VARCHAR(8), RM.CreatedTs, 112)) AS DimDateKey, 
          SC.ShiftCalendarId AS DimShiftKey, 
          M.FKWorkOrderId AS DimWorkOrderKey, 
          RM.RawMaterialId AS DimRawMaterialKey, 
          RM.FKMaterialId AS DimMaterialKey, 
          RM.FKProductId AS DimProductKey, 
          S.SteelgradeId AS DimSteelgradeKey, 
          S.FKSteelGroupId AS DimSteelGroupKey, 
          M.FKHeatId AS DimHeatKey, 
          RM.[Status] AS DimRawMaterialStatusKey, 
          RM.ParentRawMaterialId AS DimParentRawMaterialKey, 
          RM1.[Status] AS DimParentRawMaterialStatusKey, 
          RMS0.FKAssetId AS DimLastAssetKey, 
          C.CrewId AS DimCrewKey, 
          WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
          WO.FKProductCatalogueId AS DimProductCatalogueKey, 
          RM.RawMaterialName, 
          CONVERT(DATETIME2(3), RM.CreatedTs) AS RawMaterialCreated, 
          CONVERT(DATETIME2(3), RM.LastUpdateTs) AS RawMaterialUpdated, 
          RM.ExternalRawMaterialId AS ExternalRawMaterialId, 
          RM.LastProcessingStepNo AS LastProcessingStepNo, 
          RM.CuttingSeqNo AS CuttingSeqNo, 
          RM.ChildsNo AS ChildsNo, 
          RM.IsDummy AS IsDummy, 
          RM.IsVirtual AS IsVirtual, 
          ISNULL(CAST(CASE
                          WHEN RM.FKMaterialId IS NOT NULL
                          THEN 1
                          ELSE 0
                      END AS BIT), 0) AS IsMaterialAssigned, 
          ISNULL(CAST(CASE
                          WHEN RM.FKProductId IS NOT NULL
                          THEN 1
                          ELSE 0
                      END AS BIT), 0) AS IsProductAssigned, 
          RM.IsTransferred2DW AS IsTransferred2DW, 
          MC.MaterialCatalogueName AS MaterialCatalogueName, 
          PC.ProductCatalogueName AS ProductCatalogueName, 
          M.MaterialName, 
          H.HeatName, 
          S.SteelgradeCode, 
          S.SteelgradeName, 
          SG.SteelGroupCode, 
          SG.SteelGroupName, 
          WO.WorkOrderName AS WorkOrderName, 
          CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', ShiftCode) ShiftKey, 
          SD.ShiftCode ShiftCode, 
          C.CrewName, 
          A.AssetName, 
          RM1.RawMaterialName AS ParentRawMaterialName, 
          RM1.ExternalRawMaterialId AS ParentExternalRawMaterialId, 
          RM1.CuttingSeqNo AS ParentCuttingSeqNo, 
          RM1.ChildsNo AS ParentChildsNo, 
          CONVERT(NUMERIC(18, 8), RMS1.[Weight]) AS RawMaterialInitialWeight, 
          CONVERT(NUMERIC(18, 8), RMS1.[Length]) AS RawMaterialInitialLength, 
          CONVERT(NUMERIC(18, 8), RMS0.[Weight]) AS RawMaterialLastWeight, 
          CONVERT(NUMERIC(18, 8), RMS0.[Length]) AS RawMaterialLastLength
   FROM dbo.MVHRawMaterials AS RM
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS0 ON RM.RawMaterialId = RMS0.FKRawMaterialId
                                                       AND RMS0.ProcessingStepNo = 0
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS1 ON RM.RawMaterialId = RMS1.FKRawMaterialId
                                                       AND RMS1.ProcessingStepNo = 1
        LEFT OUTER JOIN dbo.MVHRawMaterials AS RM1 ON RM.ParentRawMaterialId = RM1.RawMaterialId
        INNER JOIN dbo.MVHAssets AS A ON RMS0.FKAssetId = A.AssetId
        LEFT OUTER JOIN dbo.PRMMaterials M
        INNER JOIN dbo.PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
        INNER JOIN [dbo].[PRMMaterialCatalogue] MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN [dbo].[PRMProductCatalogue] PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
        INNER JOIN [dbo].[PRMHeats] H ON M.FKHeatId = H.HeatId
        INNER JOIN [dbo].[PRMSteelgrades] S ON MC.FKSteelgradeId = S.SteelgradeId
        INNER JOIN [dbo].[PRMSteelGroups] SG ON S.FKSteelGroupId = SG.SteelGroupId ON RM.FKMaterialId = M.MaterialId
        INNER JOIN [dbo].[ShiftCalendar] SC ON dbo.FNGetShiftId(RM.CreatedTs) = SC.ShiftCalendarId
        INNER JOIN [dbo].[ShiftDefinitions] AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
        INNER JOIN [dbo].[Crews] C ON SC.FKCrewId = C.CrewId;