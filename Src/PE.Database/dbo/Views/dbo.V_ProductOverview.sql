CREATE   VIEW V_ProductOverview
AS SELECT P.[ProductId], 
          P.[CreatedTs], 
          P.[LastUpdateTs], 
          P.[IsDummy], 
          P.[ProductName], 
          P.[FKWorkOrderId], 
          P.[IsAssigned], 
          P.[Weight], 
          WO.WorkOrderName, 
          WO.ExternalSteelgradeCode, 
          WO.ExtraLabelInformation, 
          WO.OperationCode, 
          WO.QualityPolicy, 
          PC.ProductCatalogueName, 
          PC.SAPNumber, 
          RM.RawMaterialName
   FROM [dbo].[PRMProducts] AS P
        LEFT JOIN [dbo].[PRMWorkOrders] AS WO
        INNER JOIN [dbo].[PRMProductCatalogue] AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId ON P.FKWorkOrderId = WO.WorkOrderId
        LEFT OUTER JOIN MVHRawMaterials RM ON P.ProductId = RM.FKProductId;