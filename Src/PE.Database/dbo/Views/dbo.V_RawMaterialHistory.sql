CREATE   VIEW [dbo].[V_RawMaterialHistory]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY RM.RawMaterialId, 
                   RMS.ProcessingStepNo), 0) Sorting, 
          RM.RawMaterialId AS RawMaterialId, 
          RMS.RawMaterialStepId AS RawMaterialStepId, 
          dbo.FNGetShiftId(RMS.CreatedTs) AS ShiftId, 
          RMS.FKAssetId AS AssetId, 
          RMS.FKFeatureIdRef AS FeatureId, 
          RMS.CreatedTs AS CreatedTs, 
          RM.RawMaterialName, 
          A.AssetName, 
          F.FeatureName, 
          RMS.ProcessingStepNo AS RawMaterialStep, 
          RMS.PassNo AS RawMaterialPassNo, 
          RMS.IsLastPass AS RawMaterialIsLastPass, 
          RMS.IsReversed AS RawMaterialIsReversed, 
          RMS.Thickness AS RawMaterialThickness, 
          RMS.Weight AS RawMaterialWeight, 
          RMS.Length AS RawMaterialLength, 
          RMS1.Weight AS RawMaterialDeclaredWeight, 
          RMS1.Length AS RawMaterialDeclaredLength, 
          RMS0.Weight AS RawMaterialLastWeight, 
          RMS0.Length AS RawMaterialLastLength, 
          RMS.EnumTypeOfCut RawMaterialCutType, 
          RMS.HeadPartLength, 
          RMS.TailPartLength, 
          RMS.HeadPartCumm, 
          RMS.TailPartCumm, 
          RMS.Elongation, 
          RMS.MotherOffset, 
          RMS.RelLength
   FROM dbo.MVHRawMaterials AS RM
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS0 ON RM.RawMaterialId = RMS0.FKRawMaterialId
                                                       AND RMS0.ProcessingStepNo = 0
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS1 ON RM.RawMaterialId = RMS1.FKRawMaterialId
                                                       AND RMS1.ProcessingStepNo = 1
        LEFT JOIN dbo.MVHAssets A ON RMS.FKAssetId = A.AssetId
        LEFT JOIN dbo.MVHFeatures F ON RMS.FKFeatureIdRef = F.FeatureId;