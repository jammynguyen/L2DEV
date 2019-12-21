
CREATE   VIEW [dbo].[V_DW_DimProductCatalogue]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            PC.ProductCatalogueId AS DimProductCatalogueKey, 
            PC.FKSteelgradeId AS DimSteelgradeKey, 
            PC.ProductCatalogueName, 
            PC.[Description] AS ProductCatalogueDescription, 
            PCT.ProductCatalogueTypeSymbol, 
            PCT.ProductCatalogueTypeName, 
            S.ShapeSymbol AS ProductShapeSymbol, 
            S.ShapeName AS ProductShapeName, 
            SG.SteelgradeCode, 
            SG.SteelgradeName, 
            PC.SAPNumber AS ProductSAPNumber, 
            PC.IsActive, 
            CAST(PC.Length AS NUMERIC(18, 8)) AS ProductCatalogueLength, 
            CAST(PC.Width AS NUMERIC(18, 8)) AS ProductCatalogueWidth, 
            CAST(PC.Thickness AS NUMERIC(18, 8)) AS ProductCatalogueThickness, 
            CAST(PC.Weight AS NUMERIC(18, 8)) AS ProductCatalogueWeight, 
            CAST(PC.LengthMin AS NUMERIC(18, 8)) AS ProductCatalogueLengthMin, 
            CAST(PC.WidthMin AS NUMERIC(18, 8)) AS ProductCatalogueWidthMin, 
            CAST(PC.ThicknessMin AS NUMERIC(18, 8)) AS ProductCatalogueThicknessMin, 
            CAST(PC.WeightMin AS NUMERIC(18, 8)) AS ProductCatalogueWeightMin, 
            CAST(PC.LengthMax AS NUMERIC(18, 8)) AS ProductCatalogueLengthMax, 
            CAST(PC.WidthMax AS NUMERIC(18, 8)) AS ProductCatalogueWidthMax, 
            CAST(PC.ThicknessMax AS NUMERIC(18, 8)) AS ProductCatalogueThicknessMax, 
            CAST(PC.WeightMax AS NUMERIC(18, 8)) AS ProductCatalogueWeightMax, 
            CAST(PC.Slitting AS BIT) AS ProductCatalogueSlitting, 
            CAST(PC.ExitSpeed AS NUMERIC(18, 8)) AS ProductCatalogueExitSpeed, 
            CAST(PC.StdMetallicYield AS NUMERIC(18, 8)) AS ProductCatalogueStdMetallicYield, 
            CAST(PC.StdQualityYield AS NUMERIC(18, 8)) AS ProductCatalogueStdQualityYield, 
            CAST(PC.StdGapTime AS NUMERIC(18, 8)) AS ProductCatalogueStdGapTime, 
            CAST(PC.StdRollingTime AS NUMERIC(18, 8)) AS ProductCatalogueStdRollingTime, 
            CAST(PC.StdUtilizationTime AS NUMERIC(18, 8)) AS ProductCatalogueStdUtilizationTime, 
            CAST(PC.StdProductionTime AS NUMERIC(18, 8)) AS ProductCatalogueStdProductionTime, 
            CAST(PC.StdProductivity AS NUMERIC(18, 8)) AS ProductCatalogueStdProductivity, 
            CAST(PC.Ovality AS NUMERIC(18, 8)) AS ProductCatalogueOvality, 
            CAST(PC.MaxOvality AS NUMERIC(18, 8)) AS ProductCatalogueMaxOvality, 
            CAST(PC.MaxTensile AS NUMERIC(18, 8)) AS ProductCatalogueMaxTensile, 
            CAST(PC.MaxYieldPoint AS NUMERIC(18, 8)) AS ProductCatalogueMaxYieldPoint, 
            CAST(PC.ProfileToleranceMin AS NUMERIC(18, 8)) AS ProductCatalogueProfileToleranceMin, 
            CAST(PC.ProfileToleranceMax AS NUMERIC(18, 8)) AS ProductCatalogueProfileToleranceMax
     FROM dbo.PRMProductCatalogue AS PC
          INNER JOIN dbo.PRMProductCatalogueTypes AS PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId
          INNER JOIN dbo.PRMShapes AS S ON PC.FKShapeId = S.ShapeId
          LEFT OUTER JOIN dbo.PRMSteelgrades SG ON PC.FKSteelgradeId = SG.SteelgradeId;
GO



GO


