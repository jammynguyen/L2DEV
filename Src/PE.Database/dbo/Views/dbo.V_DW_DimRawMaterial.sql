


CREATE         VIEW [dbo].[V_DW_DimRawMaterial]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          RM.RawMaterialId AS DimRawMaterialKey, 
          RM.[Status] AS DimRawMaterialStatusKey, 
          RM.ParentRawMaterialId AS DimParentRawMaterialKey, 
          RM.FKMaterialId AS DimMaterialKey, 
          RM.FKProductId AS DimProductKey, 
          M.FKWorkOrderId AS DimWorkOrderKey, 
          M.FKHeatId AS DimHeatKey, 
          RM.RawMaterialName AS RawMaterialName, 
          CONVERT(DATETIME2(3), RM.CreatedTs) AS RawMaterialCreated, 
          CONVERT(DATETIME2(3), RM.LastUpdateTs) AS RawMaterialUpdated, 
          RM.LastProcessingStepNo AS LastProcessingStepNo, 
          RM.CuttingSeqNo AS CuttingSeqNo, 
          RM.ChildsNo AS ChildsNo, 
          RM.IsDummy AS IsDummy, 
          RM.ExternalRawMaterialId AS ExternalRawMaterialId, 
          RM.IsTransferred2DW AS IsTransferred2DW, 
          M.MaterialName, 
          CONVERT(NUMERIC(18, 8), M.[Weight]) AS MaterialWeight
   FROM MVHRawMaterials RM
        LEFT JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId;