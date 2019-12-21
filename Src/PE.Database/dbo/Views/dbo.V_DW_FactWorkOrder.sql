


CREATE       VIEW [dbo].[V_DW_FactWorkOrder]
AS
     SELECT CONVERT(DATETIME2(7), GETDATE()) AS FactLoadTs, 
            CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            DATEPART(YEAR, WO.CreatedTs) AS DimYearKey, 
            CONVERT(BIGINT, CONVERT(VARCHAR(8), WO.CreatedTs, 112)) AS DimDateKey, 
            dbo.FNGetShiftId(WO.CreatedTs) AS DimShiftKey, 
            WO.WorkOrderId AS DimWorkOrderKey, 
            dbo.FNGetShiftId(RM.ProductionStarted) AS DimShiftKeyStart, 
            dbo.FNGetShiftId(RM.ProductionFinished) AS DimShiftKeyEnd, 
            S.FKSteelGroupId AS DimSteelGroupKey, 
            S.SteelgradeId AS DimSteelgradeKey, 
            H.HeatId AS DimHeatKey, 
            MC.FKMaterialCatalogueTypeId AS DimMaterialCatalogueTypeKey, 
            WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
            MC.FKShapeId AS DimMaterialShapeKey, 
            PC.FKProductCatalogueTypeId AS DimProductCatalogueTypeKey, 
            WO.FKProductCatalogueId AS DimProductCatalogueKey, 
            PC.FKShapeId AS DimProductShapeKey, 
            WO.FKCustomerId AS DimCustomerKey, 
            WO.FKReheatingGroupId AS DimReheatingGroupKey, 
            WO.WorkOrderStatus AS DimWorkOrderStatusKey, 
            WO.WorkOrderName, 
            WO.CreatedTs AS WorkOrderCreated, 
            WO.CreatedInL3, 
            WO.ToBeCompletedBefore, 
            RM.ProductionStarted AS ProductionStart, 
            RM.ProductionFinished AS ProductionEnd, 
            CAST(ISNULL(RM.ProductionFinished - RM.ProductionStarted, 0) AS FLOAT) AS WorkOrderDurationD, 
            CAST(ISNULL(RM.ProductionFinished - RM.ProductionStarted, 0) AS FLOAT) * 24 AS WorkOrderDurationH, 
            CAST(ISNULL(RM.ProductionFinished - RM.ProductionStarted, 0) AS FLOAT) * 24 * 60 AS WorkOrderDurationM, 
            CAST(ISNULL(RM.ProductionFinished - RM.ProductionStarted, 0) AS FLOAT) * 24 * 60 * 60 AS WorkOrderDurationS, 
            WO.IsTestOrder,
            CASE
                WHEN SCH.FKWorkOrderId IS NULL
                THEN 0
                ELSE 1
            END AS IsScheduledNow,
            CASE
                WHEN dbo.FNGetShiftId(RM.ProductionStarted) = dbo.FNGetShiftId(RM.ProductionFinished)
                THEN 1
                ELSE 0
            END AS IsOnOneShift, 
            WO.TargetOrderWeight AS TargetWeight, 
            WO.TargetOrderWeightMin AS TargetMinWeight, 
            WO.TargetOrderWeightMax AS TargetMaxWeight, 
            Materials.MaterialsCount, 
            Materials.MaterialsWeight, 
            P_1.ProductsCount, 
            P_1.ProductsWeight, 
            0 Completion, 
            0 MetallicYield, 
            0 QualityYield, 
            WO.NextAggregate, 
            WO.OperationCode, 
            WO.QualityPolicy, 
            WO.ExtraLabelInformation, 
            WO.ExternalSteelgradeCode, 
            SCH.PlannedTime, 
            SCH.PlannedStartTime, 
            SCH.PlannedEndTime
     FROM dbo.PRMWorkOrders AS WO
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON MC.FKSteelgradeId = S.SteelgradeId
		  LEFT OUTER JOIN dbo.PRMMaterials M 
          INNER JOIN dbo.PRMHeats AS H ON M.FKHeatId = H.HeatId ON M.FKWorkOrderId = WO.WorkOrderId

          LEFT OUTER JOIN
     (
         SELECT FKWorkOrderId, 
                COUNT(MaterialId) AS MaterialsCount, 
                SUM(Weight) AS MaterialsWeight
         FROM dbo.PRMMaterials AS M
         GROUP BY FKWorkOrderId
     ) AS Materials ON WO.WorkOrderId = Materials.FKWorkOrderId
          LEFT OUTER JOIN
     (
         SELECT FKWorkOrderId, 
                COUNT(ProductId) AS ProductsCount, 
                SUM(Weight) AS ProductsWeight
         FROM dbo.PRMProducts AS P
         GROUP BY FKWorkOrderId
     ) AS P_1 ON WO.WorkOrderId = P_1.FKWorkOrderId
          LEFT OUTER JOIN dbo.PPLSchedules AS SCH ON WO.WorkOrderId = SCH.FKWorkOrderId
          LEFT OUTER JOIN
     (
         SELECT M.FKWorkOrderId, 
                MIN(RM1.CreatedTs) AS ProductionStarted, 
                MAX(RM1.LastUpdateTs) AS ProductionFinished
         FROM dbo.MVHRawMaterials AS RM1
              INNER JOIN dbo.PRMMaterials AS M ON RM1.FKMaterialId = M.MaterialId
         GROUP BY M.FKWorkOrderId
     ) AS RM ON WO.WorkOrderId = RM.FKWorkOrderId;
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DW_FactWorkOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd
         Begin Table = "SCH"
            Begin Extent = 
               Top = 6
               Left = 1047
               Bottom = 136
               Right = 1230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RM"
            Begin Extent = 
               Top = 6
               Left = 1268
               Bottom = 119
               Right = 1460
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DW_FactWorkOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "WO"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "H"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 258
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PC"
            Begin Extent = 
               Top = 6
               Left = 502
               Bottom = 136
               Right = 736
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MC"
            Begin Extent = 
               Top = 6
               Left = 774
               Bottom = 136
               Right = 1009
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "M_1"
            Begin Extent = 
               Top = 6
               Left = 289
               Bottom = 119
               Right = 464
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P_1"
            Begin Extent = 
               Top = 120
               Left = 289
               Bottom = 233
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DW_FactWorkOrder';

