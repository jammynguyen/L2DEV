CREATE   VIEW [dbo].[V_RawMaterialList]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY RM.CreatedTs DESC), 0) Sorting, 
          RM.RawMaterialId, 
          ISNULL(M.MaterialName, RM.RawMaterialName) MaterialName, 
          RM.RawMaterialName, 
          M.MaterialName L3MaterialName, 
          RM.CreatedTs, 
          RM.LastUpdateTs, 
          RM.[Status] AS [Status], 
          dbo.FNGetEnumKeyword('RawMaterialStatus', RM.[Status]) AS StatusName
   FROM dbo.MVHRawMaterials AS RM
        LEFT OUTER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId;