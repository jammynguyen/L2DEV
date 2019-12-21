CREATE   VIEW [dbo].[V_MaterialsInFurnace]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY A.OrderSeq, 
                   RMS.LastUpdateTs), 0) Sorting,
          CASE
              WHEN ROW_NUMBER() OVER(PARTITION BY A.AssetId
                   ORDER BY RMS.LastUpdateTs ASC) = 1
              THEN 1
              ELSE 0
          END IsFirstOne,
          CASE
              WHEN ROW_NUMBER() OVER(PARTITION BY A.AssetId
                   ORDER BY RMS.LastUpdateTs DESC) = 1
              THEN 1
              ELSE 0
          END IsLastOne, 
          RM.RawMaterialId, 
          RM.ExternalRawMaterialId, 
          RM.RawMaterialName, 
          RM.[Status], 
          RMS.LastUpdateTs, 
          A.AssetId, 
          A.AssetName, 
          RMS.[Weight], 
          RMS.[Length], 
          RM.FKMaterialId L3MaterialId, 
          M.MaterialName, 
          WO.WorkOrderName, 
          H.HeatName, 
          S.SteelgradeCode, 
          WO.WorkOrderId, 
          H.HeatId, 
          S.SteelgradeId
   FROM MVHRawMaterials RM
        INNER JOIN MVHRawMaterialsSteps RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
                                               AND ProcessingStepNo = 0
        INNER JOIN MVHAssets A ON RMS.FKAssetId = A.AssetId
        INNER JOIN MVHAssetsEXT AE ON RMS.FKAssetId = AE.FKAssetId
                                      AND EnumArea = 1
        LEFT JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
        LEFT JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
        LEFT JOIN PRMHeats H ON M.FKHeatId = H.HeatId
        LEFT JOIN PRMMaterialCatalogue MC ON H.FKMaterialCatalogueId = MC.MaterialCatalogueId
        LEFT JOIN PRMSteelgrades S ON MC.FKSteelgradeId = S.SteelgradeId
   WHERE 1 = 1;
GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_MaterialsInFurnace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[14] 4[47] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[17] 2[33] 3) )"
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_MaterialsInFurnace';

