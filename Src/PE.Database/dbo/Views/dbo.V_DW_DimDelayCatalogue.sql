CREATE   VIEW [dbo].[V_DW_DimDelayCatalogue]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          DC.DelayCatalogueId AS DimDelayCatalogueKey, 
          DC.DelayCatalogueCode, 
          DC.DelayCatalogueName, 
          DC.DelayCatalogueDescription, 
          DC.StdDelayTime AS DelayStdTime, 
          DC.ParentDelayCatalogueId AS DimParentDelayCatalogueKey, 
          DCC.DelayCatalogueCategoryCode AS DelayCategoryCode, 
          DCC.DelayCatalogueCategoryName AS DelayCategoryName, 
          DCC.DelayCatalogueCategoryDescription AS DelayCategoryDescription
   FROM dbo.DLSDelayCatalogue AS DC
        LEFT OUTER JOIN dbo.DLSDelayCatalogueCategory AS DCC ON DC.FKDelayCatalogueCategoryId = DCC.DelayCatalogueCategoryId;
GO



GO


