

CREATE       VIEW [dbo].[V_L1L3MaterialAssignment]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY M.CreatedTs DESC, 
                   M.MaterialId DESC), 0) AS Sorting, 
          M.MaterialId, 
          M.MaterialName, 
          M.CreatedTs, 
          M.LastUpdateTs, 
          M.FKHeatId, 
          M.FKWorkOrderId, 
          M.[Weight], 
          M.IsAssigned, 
          M.IsDummy, 
          RM.RawMaterialId, 
          RM.RawMaterialName, 
          RM.[Status] AS [Status], 
          dbo.FNGetEnumKeyword('RawMaterialStatus', RM.[Status]) AS StatusName
   FROM dbo.PRMMaterials M
        LEFT OUTER JOIN dbo.MVHRawMaterials RM ON M.MaterialId = RM.FKMaterialId;