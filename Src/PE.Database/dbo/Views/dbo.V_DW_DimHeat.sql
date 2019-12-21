﻿
CREATE     VIEW [dbo].[V_DW_DimHeat]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          H.HeatId AS DimHeatKey, 
          MC.FKSteelgradeId AS DimSteelgradeKey, 
          H.HeatName, 
          H.Created AS HeatCreated, 
          HS.SupplierName AS HeatSupplierName, 
          HS.[Description] AS HeatSupplierDescription, 
          CAST(HA.Fe AS NUMERIC(18, 8)) AS HeatChFe, 
          CAST(HA.C AS NUMERIC(18, 8)) AS HeatChC, 
          CAST(HA.Mn AS NUMERIC(18, 8)) AS HeatChMn, 
          CAST(HA.Cr AS NUMERIC(18, 8)) AS HeatChCr, 
          CAST(HA.Mo AS NUMERIC(18, 8)) AS HeatChMo, 
          CAST(HA.V AS NUMERIC(18, 8)) AS HeatChV, 
          CAST(HA.Ni AS NUMERIC(18, 8)) AS HeatChNi, 
          CAST(HA.Co AS NUMERIC(18, 8)) AS HeatChCo, 
          CAST(HA.Si AS NUMERIC(18, 8)) AS HeatChSi, 
          CAST(HA.P AS NUMERIC(18, 8)) AS HeatChP, 
          CAST(HA.S AS NUMERIC(18, 8)) AS HeatChS, 
          CAST(HA.Cu AS NUMERIC(18, 8)) AS HeatChCu, 
          CAST(HA.Nb AS NUMERIC(18, 8)) AS HeatChNb, 
          CAST(HA.Al AS NUMERIC(18, 8)) AS HeatChAl, 
          CAST(HA.N AS NUMERIC(18, 8)) AS HeatChN, 
          CAST(HA.Ca AS NUMERIC(18, 8)) AS HeatChCa, 
          CAST(HA.B AS NUMERIC(18, 8)) AS HeatChB, 
          CAST(HA.Ti AS NUMERIC(18, 8)) AS HeatChTi, 
          CAST(HA.Sn AS NUMERIC(18, 8)) AS HeatChSn, 
          CAST(HA.O AS NUMERIC(18, 8)) AS HeatChO, 
          CAST(HA.H AS NUMERIC(18, 8)) AS HeatChH, 
          CAST(HA.W AS NUMERIC(18, 8)) AS HeatChW, 
          CAST(HA.Pb AS NUMERIC(18, 8)) AS HeatChPb, 
          CAST(HA.Zn AS NUMERIC(18, 8)) AS HeatChZn, 
          CAST(HA.[As] AS NUMERIC(18, 8)) AS HeatChAs, 
          CAST(HA.Mg AS NUMERIC(18, 8)) AS HeatChMg, 
          CAST(HA.Sb AS NUMERIC(18, 8)) AS HeatChSb, 
          CAST(HA.Bi AS NUMERIC(18, 8)) AS HeatChBi, 
          CAST(HA.Ta AS NUMERIC(18, 8)) AS HeatChTa, 
          CAST(HA.Zr AS NUMERIC(18, 8)) AS HeatChZr, 
          CAST(HA.Ce AS NUMERIC(18, 8)) AS HeatChCe, 
          CAST(HA.Te AS NUMERIC(18, 8)) AS HeatChTe
   FROM dbo.PRMHeats AS H
		INNER JOIN dbo.PRMMaterialCatalogue MC ON H.FKMaterialCatalogueId = MC.MaterialCatalogueId
        LEFT OUTER JOIN dbo.PRMHeatChemAnalysis AS HA ON H.HeatId = HA.FKHeatId
        LEFT OUTER JOIN dbo.PRMHeatSuppliers AS HS ON H.FKHeatSupplierId = HS.HeatSupplierId;