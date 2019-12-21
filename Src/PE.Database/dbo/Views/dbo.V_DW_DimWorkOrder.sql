


CREATE       VIEW [dbo].[V_DW_DimWorkOrder]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            WO.WorkOrderId AS DimWorkOrderKey, 
            WO.WorkOrderStatus AS DimWorkOrderStatusKey, 
            M.FKHeatId AS DimHeatKey, 
            WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
            WO.FKProductCatalogueId AS DimProductCatalogueKey, 
            WO.FKCustomerId AS DimCustomerKey, 
            WO.FKReheatingGroupId AS DimReheatingGroupKey, 
            WO.WorkOrderName AS WorkOrderName, 
            WO.IsTestOrder AS IsTestOrder, 
            WO.TargetOrderWeight, 
            CONVERT(DATETIME2(3), WO.CreatedTs) AS WorkOrderCreated, 
            CONVERT(DATETIME2(3), WO.LastUpdateTs) AS WorkOrderLastUpdate, 
            CONVERT(DATETIME2(3), WO.CreatedInL3) AS CreatedInL3, 
            CONVERT(DATETIME2(3), WO.ToBeCompletedBefore) AS ToBeCompletedBefore
     FROM PRMWorkOrders WO
	 LEFT OUTER JOIN dbo.PRMMaterials M ON WO.WorkOrderId = M.FKWorkOrderId