CREATE   VIEW [dbo].[V_DW_DimDefectCatalogue]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          DC.DefectCatalogueId AS DimDefectCatalogueKey, 
          DC.DefectCatalogueCode, 
          DC.DefectCatalogueName, 
          DC.DefectCatalogueDescription, 
          DC.ParentDefectCatalogueId AS DimParentDefectCatalogueKey, 
          DCC.DefectCatalogueCategoryCode AS DefectCategoryCode, 
          DCC.DefectCatalogueCategoryName AS DefectCategoryName, 
          DCC.DefectCatalogueCategoryDescription AS DefectCategoryDescription
   FROM dbo.MVHDefectCatalogue AS DC
        LEFT OUTER JOIN dbo.MVHDefectCatalogueCategory AS DCC ON DC.FKDefectCatalogueCategoryId = DCC.DefectCatalogueCategoryId;