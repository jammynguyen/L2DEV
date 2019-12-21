CREATE VIEW [dbo].[V_DW_DimUOM]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            UOM.UnitId AS DimUOMKey, 
            ISNULL(UOM.SIUnitId, UOM.UnitId) AS DimUOMSIKey, 
            UC.CategoryName AS UOMCategory, 
            UOM.UnitCategoryId AS UOMCategoryKey, 
            UOM.UnitSymbol AS UOMSymbol, 
            UOM.[Name] AS UOMName, 
            UOM.Factor AS UOMFactor, 
            ISNULL(USI.UnitSymbol, UOM.UnitSymbol) AS UOMSymbolSI, 
            ISNULL(USI.[Name], UOM.[Name]) AS UOMNameSI
     FROM smf.UnitOfMeasure AS UOM
          INNER JOIN smf.UnitOfMeasureCategory AS UC ON UOM.UnitCategoryId = UC.UnitCategoryId
          LEFT OUTER JOIN smf.UnitOfMeasure AS USI ON UOM.SIUnitId = USI.UnitId;