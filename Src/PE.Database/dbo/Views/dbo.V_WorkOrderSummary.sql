CREATE   VIEW [dbo].[V_WorkOrderSummary]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY WO.CreatedTs DESC), 0) AS Sorting, 
          WO.WorkOrderId, 
          WO.WorkOrderName, 
          WO.IsTestOrder, 
          WO.WorkOrderStatus, 
          PC.ProductCatalogueName, 
          MC.MaterialCatalogueName, 
          H.HeatName, 
          S.SteelgradeCode, 
          WO.CreatedInL3, 
          WO.ToBeCompletedBefore, 
          SP.ScheduleId, 
          SP.OrderSeq AS ScheduleOrderSeq, 
          SP.PlannedStartTime, 
          SP.PlannedEndTime, 
          WOD.ProductionStart, 
          WOD.ProductionEnd, 
          WO.TargetOrderWeight, 
          ISNULL(P.ProductsWeight, 0) AS ProductsWeight, 
          SUM(ISNULL(RMS.[Weight], 0)) AS MaterialsWeight, 
          ISNULL(P.ProductsWeight, 0) / (SUM(ISNULL(RMS.[Weight], 0)) + 0.0000000001) AS MetallicYield, 
          ISNULL(P.ProductsWeight, 0) / WO.TargetOrderWeight AS Completion, 
          ISNULL(P.ProductsNumber, 0) AS ProductsNumber, 
          COUNT(M.MaterialId) AS MaterialsPlanned, 
          COUNT(RM.RawMaterialId) AS MaterialsAssigned, 
          SUM(CASE
                  WHEN RM.[Status] BETWEEN 4 AND 8
                  THEN 1
                  ELSE 0
              END) AS MaterialsCharged, 
          SUM(CASE
                  WHEN RM.[Status] BETWEEN 5 AND 8
                  THEN 1
                  ELSE 0
              END) AS MaterialsDischarged, 
          SUM(CASE
                  WHEN RM.[Status] BETWEEN 7 AND 8
                  THEN 1
                  ELSE 0
              END) AS MaterialsRolled, 
          SUM(CASE
                  WHEN RM.[Status] BETWEEN 9 AND 9
                  THEN 1
                  ELSE 0
              END) AS MaterialsRejected, 
          SUM(CASE
                  WHEN RM.[Status] BETWEEN 10 AND 10
                  THEN 1
                  ELSE 0
              END) AS MaterialsScrapped,
          CASE
              WHEN SP.ScheduleId IS NOT NULL
                   AND WOD.ProductionStart > 0
              THEN 1
              ELSE 0
          END AS IsProducedNow,
          CASE
              WHEN SP.ScheduleId IS NOT NULL
              THEN 1
              ELSE 0
          END AS IsPlanned
   FROM dbo.PRMWorkOrders AS WO
        INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
        INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN dbo.PRMSteelgrades AS S ON MC.FKSteelgradeId = S.SteelgradeId
        LEFT OUTER JOIN PPLSchedules SP ON WO.WorkOrderId = SP.FKWorkOrderId
        INNER JOIN dbo.PRMMaterials AS M ON WO.WorkOrderId = M.FKWorkOrderId
        INNER JOIN dbo.PRMHeats AS H ON M.FKHeatId = H.HeatId
        LEFT OUTER JOIN dbo.MVHRawMaterials AS RM
        INNER JOIN dbo.MVHRawMaterialsSteps AS RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
                                                      AND RMS.ProcessingStepNo = 0 ON M.MaterialId = RM.FKMaterialId 
        LEFT OUTER JOIN
   (
       SELECT FKWorkOrderId, 
              SUM([Weight]) ProductsWeight, 
              COUNT(ProductId) ProductsNumber
       FROM dbo.PRMProducts
       GROUP BY FKWorkOrderId
   ) AS P ON WO.WorkOrderId = P.FKWorkOrderId
        OUTER APPLY dbo.FNTWorkOrderDuration(WO.WorkOrderId) WOD
   GROUP BY WO.CreatedTs, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            WO.IsTestOrder, 
            WO.TargetOrderWeight, 
            WO.CreatedInL3, 
            WO.ToBeCompletedBefore, 
            SP.ScheduleId, 
            SP.OrderSeq, 
            SP.PlannedStartTime, 
            SP.PlannedEndTime, 
            WO.WorkOrderStatus, 
            PC.ProductCatalogueName, 
            H.HeatName, 
            MC.MaterialCatalogueName, 
            S.SteelgradeCode, 
            P.ProductsNumber, 
            P.ProductsWeight, 
            WOD.WorkOrderId, 
            WOD.ProductionStart, 
            WOD.ProductionEnd;
GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_WorkOrderSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[2] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
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
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 33
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
      PaneHidden = 
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_WorkOrderSummary';

