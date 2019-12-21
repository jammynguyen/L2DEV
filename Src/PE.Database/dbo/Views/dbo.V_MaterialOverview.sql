

CREATE     VIEW [dbo].[V_MaterialOverview] AS
SELECT M.[MaterialId], 
       M.[CreatedTs], 
       M.[LastUpdateTs], 
       M.[IsDummy], 
       M.[MaterialName], 
       M.[FKWorkOrderId], 
       M.[Weight], 
       M.[IsAssigned], 
       M.[FKHeatId],
	   WO.WorkOrderName,
	   H.HeatName,
	   RM.RawMaterialName
FROM [dbo].[PRMMaterials] M
     INNER JOIN [dbo].[PRMWorkOrders] WO ON M.FKWorkOrderId = WO.WorkOrderId
     INNER JOIN [dbo].[PRMHeats] H ON M.FKHeatId = H.HeatId
     LEFT JOIN [dbo].[MVHRawMaterials] RM ON M.MaterialId = RM.FKMaterialId;