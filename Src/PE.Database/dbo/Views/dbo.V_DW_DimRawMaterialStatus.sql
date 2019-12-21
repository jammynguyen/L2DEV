CREATE VIEW [dbo].[V_DW_DimRawMaterialStatus]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            EV.Value AS DimRawMaterialStatusKey, 
            SUBSTRING(EV.Keyword, 6, 50) AS RawMaterialStatusName
     FROM smf.EnumNames AS EN
          INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId
     WHERE(EN.EnumName = 'RawMaterialStatus');
GO



GO


