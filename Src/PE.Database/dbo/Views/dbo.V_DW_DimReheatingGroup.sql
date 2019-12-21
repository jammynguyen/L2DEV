CREATE VIEW [dbo].[V_DW_DimReheatingGroup]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            ReheatingGroupId AS DimReheatingGroupKey, 
            ReheatingGroupCode AS ReheatingGroupCode, 
            ReheatingGroupName AS ReheatingGroupName
     FROM dbo.PRMReheatingGroup AS RG;