
CREATE   VIEW [dbo].[V_DW_DimMaterialCatalogue]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            MC.MaterialCatalogueId AS DimMaterialCatalogueKey, 
            MC.FKSteelgradeId AS DimSteelgradeKey, 
            MC.MaterialCatalogueName, 
            MC.[Description] AS MaterialCatalogueDescription, 
            MCT.MaterialCatalogueTypeSymbol, 
            MCT.MaterialCatalogueTypeName, 
            S.ShapeSymbol AS MaterialShapeSymbol, 
            S.ShapeName AS MaterialShapeName, 
            SG.SteelgradeCode, 
            SG.SteelgradeName, 
            MC.SAPNumber AS MaterialSAPNumber, 
            MC.IsActive, 
            CAST(MC.Length AS NUMERIC(18, 8)) AS MaterialCatalogueLength, 
            CAST(MC.Width AS NUMERIC(18, 8)) AS MaterialCatalogueWidth, 
            CAST(MC.Thickness AS NUMERIC(18, 8)) AS MaterialCatalogueThickness, 
            CAST(MC.Weight AS NUMERIC(18, 8)) AS MaterialCatalogueWeight, 
            CAST(MC.LengthMin AS NUMERIC(18, 8)) AS MaterialCatalogueLengthMin, 
            CAST(MC.WidthMin AS NUMERIC(18, 8)) AS MaterialCatalogueWidthMin, 
            CAST(MC.ThicknessMin AS NUMERIC(18, 8)) AS MaterialCatalogueThicknessMin, 
            CAST(MC.WeightMin AS NUMERIC(18, 8)) AS MaterialCatalogueWeightMin, 
            CAST(MC.LengthMax AS NUMERIC(18, 8)) AS MaterialCatalogueLengthMax, 
            CAST(MC.WidthMax AS NUMERIC(18, 8)) AS MaterialCatalogueWidthMax, 
            CAST(MC.ThicknessMax AS NUMERIC(18, 8)) AS MaterialCatalogueThicknessMax, 
            CAST(MC.WeightMax AS NUMERIC(18, 8)) AS MaterialCatalogueWeightMax
     FROM dbo.PRMMaterialCatalogue AS MC
          INNER JOIN dbo.PRMMaterialCatalogueTypes AS MCT ON MC.FKMaterialCatalogueTypeId = MCT.MaterialCatalogueTypeId
          INNER JOIN dbo.PRMShapes AS S ON MC.FKShapeId = S.ShapeId
          LEFT OUTER JOIN dbo.PRMSteelgrades SG ON MC.FKSteelgradeId = SG.SteelgradeId;
GO



GO


