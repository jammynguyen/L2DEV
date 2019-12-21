CREATE VIEW [dbo].[V_DW_FactRawMaterialHistory]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            DATEPART(YEAR, RMS.CreatedTs) AS DimYearKey, 
            CONVERT(BIGINT, CONVERT(VARCHAR(8), RMS.CreatedTs, 112)) AS DimDateKey, 
            GS.ShiftCalendarId AS DimShiftKey, 
            RMS.RawMaterialStepId AS DimRawMaterialStepKey, 
            RMS.FKAssetId AS DimAssetKey, 
            RM.RawMaterialId AS DimRawMaterialKey, 
            RM.[Status] AS DimRawMaterialStatusKey, 
            RM1.[Status] AS DimParentRawMaterialStatusKey, 
            RMS.ProcessingStepNo AS ProcessingStepNo, 
			CONVERT(DATETIME2(3), RMS.CreatedTs) AS RawMaterialStepCreated, 
            RM.RawMaterialName AS RawMaterialName, 
            RM.ExternalRawMaterialId AS ExternalRawMaterialId, 
            RMS.PassNo AS PassNo, 
            RMS.IsLastPass AS IsLastPass, 
            RMS.IsReversed AS IsReversed, 
            RMS.Thickness AS RawMaterialThickness, 
            RMS.[Weight] AS RawMaterialWeight, 
            RMS.[Length] AS RawMaterialLength, 
            RMS1.[Weight] AS RawMaterialInitialWeight, 
            RMS1.[Length] AS RawMaterialInitialLength, 
            RMS0.[Weight] AS RawMaterialLastWeight, 
            RMS0.[Length] AS RawMaterialLastLength, 
            RM.CuttingSeqNo AS CuttingSeqNo, 
            RM.ChildsNo AS ChildsNo, 
            RMS.EnumTypeOfCut AS TypeOfCut, 
            RMS.HeadPartLength, 
            RMS.TailPartLength, 
            RMS.HeadPartCumm, 
            RMS.TailPartCumm, 
            RMS.Elongation, 
            RMS.MotherOffset, 
            RMS.RelLength, 
            RM.ParentRawMaterialId AS DimParentRawMaterialKey, 
            RM1.RawMaterialName AS ParentRawMaterialName, 
            RM1.ExternalRawMaterialId AS ParentExternalRawMaterialId, 
            RM1.CuttingSeqNo AS ParentCuttingSeqNo, 
            RM1.ChildsNo AS ParentChildsNo, 
            RM.IsTransferred2DW AS IsTransferred2DW, 
            A.AssetName
     FROM dbo.MVHRawMaterials AS RM
          INNER JOIN dbo.MVHRawMaterialsSteps AS RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
          INNER JOIN dbo.MVHRawMaterialsSteps AS RMS0 ON RM.RawMaterialId = RMS0.FKRawMaterialId
                                                         AND RMS0.ProcessingStepNo = 0
          INNER JOIN dbo.MVHRawMaterialsSteps AS RMS1 ON RM.RawMaterialId = RMS1.FKRawMaterialId
                                                         AND RMS1.ProcessingStepNo = 1
          LEFT OUTER JOIN dbo.MVHRawMaterials AS RM1 ON RM.ParentRawMaterialId = RM1.RawMaterialId
          LEFT OUTER JOIN dbo.MVHAssets A ON RMS.FKAssetId = A.AssetId
          CROSS APPLY dbo.FNTGetShiftId(RMS.CreatedTs) GS;