CREATE VIEW [dbo].[V_Heats]
AS
     SELECT H.HeatId, 
            H.CreatedTs, 
            H.LastUpdateTs, 
            H.HeatName, 
            H.FKMaterialCatalogueId, 
            H.FKHeatSupplierId, 
            H.Created, 
            H.HeatWeightRef, 
            H.IsDummy, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            S.Density, 
            MC.MaterialCatalogueName, 
            HS.SupplierName
     FROM dbo.PRMHeats AS H
          INNER JOIN dbo.PRMMaterialCatalogue AS MC ON H.FKMaterialCatalogueId = MC.MaterialCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON MC.FKSteelgradeId = S.SteelgradeId
          LEFT OUTER JOIN dbo.PRMHeatSuppliers AS HS ON H.FKHeatSupplierId = HS.HeatSupplierId;