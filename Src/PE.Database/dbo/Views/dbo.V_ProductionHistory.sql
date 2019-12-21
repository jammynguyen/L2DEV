
CREATE     VIEW [dbo].[V_ProductionHistory]
AS SELECT dbo.PRMProducts.ProductId, 
          dbo.PRMProducts.CreatedTs AS ProductCreated, 
          dbo.PRMProducts.ProductName, 
          dbo.PRMProducts.Weight, 
          dbo.PRMProducts.QualityEnum, 
          dbo.PRMWorkOrders.WorkOrderId, 
          dbo.PRMWorkOrders.WorkOrderName, 
          dbo.PRMWorkOrders.WorkOrderStatus, 
          dbo.PRMProductCatalogue.ProductCatalogueId, 
          dbo.PRMProductCatalogue.ProductCatalogueName, 
          dbo.PRMProductCatalogue.FKShapeId, 
          dbo.PRMShapes.ShapeSymbol, 
          dbo.PRMShapes.ShapeName, 
          dbo.PRMProductCatalogue.Thickness, 
          dbo.PRMProductCatalogue.Width, 
          dbo.PRMProductCatalogue.Weight AS ProdCatRefWeight, 
          dbo.PRMSteelgrades.SteelgradeId, 
          dbo.PRMSteelgrades.SteelgradeCode, 
          dbo.PRMSteelgrades.SteelgradeName, 
          dbo.PRMHeats.HeatId, 
          dbo.PRMHeats.HeatName, 
          COUNT(dbo.MVHDefects.DefectId) AS NumDefects, 
          dbo.PRMProductCatalogueTypes.ProductCatalogueTypeId, 
          dbo.PRMProductCatalogueTypes.ProductCatalogueTypeSymbol, 
          dbo.PRMProductCatalogueTypes.ProductCatalogueTypeName
   FROM dbo.PRMSteelgrades
        INNER JOIN dbo.PRMProductCatalogue ON dbo.PRMSteelgrades.SteelgradeId = dbo.PRMProductCatalogue.FKSteelgradeId
        INNER JOIN dbo.PRMWorkOrders ON dbo.PRMProductCatalogue.ProductCatalogueId = dbo.PRMWorkOrders.FKProductCatalogueId
        INNER JOIN dbo.PRMMaterialCatalogue MC ON dbo.PRMWorkOrders.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN dbo.PRMHeats ON MC.MaterialCatalogueId = dbo.PRMHeats.FKMaterialCatalogueId
        INNER JOIN dbo.PRMProducts ON dbo.PRMWorkOrders.WorkOrderId = dbo.PRMProducts.FKWorkOrderId
        INNER JOIN dbo.PRMShapes ON dbo.PRMProductCatalogue.FKShapeId = dbo.PRMShapes.ShapeId
        INNER JOIN dbo.PRMProductCatalogueTypes ON dbo.PRMProductCatalogue.FKProductCatalogueTypeId = dbo.PRMProductCatalogueTypes.ProductCatalogueTypeId
        LEFT OUTER JOIN dbo.MVHDefects ON dbo.PRMProducts.ProductId = dbo.MVHDefects.FKProductId
   GROUP BY dbo.PRMProducts.ProductId, 
            dbo.PRMProducts.CreatedTs, 
            dbo.PRMProducts.ProductName, 
            dbo.PRMProducts.Weight, 
            dbo.PRMProducts.QualityEnum, 
            dbo.PRMWorkOrders.WorkOrderId, 
            dbo.PRMWorkOrders.WorkOrderName, 
            dbo.PRMWorkOrders.WorkOrderStatus, 
            dbo.PRMProductCatalogue.ProductCatalogueId, 
            dbo.PRMProductCatalogue.ProductCatalogueName, 
            dbo.PRMProductCatalogue.FKShapeId, 
            dbo.PRMShapes.ShapeSymbol, 
            dbo.PRMShapes.ShapeName, 
            dbo.PRMProductCatalogue.Thickness, 
            dbo.PRMProductCatalogue.Width, 
            dbo.PRMProductCatalogue.Weight, 
            dbo.PRMSteelgrades.SteelgradeId, 
            dbo.PRMSteelgrades.SteelgradeCode, 
            dbo.PRMSteelgrades.SteelgradeName, 
            dbo.PRMHeats.HeatId, 
            dbo.PRMHeats.HeatName, 
            dbo.PRMProductCatalogueTypes.ProductCatalogueTypeId, 
            dbo.PRMProductCatalogueTypes.ProductCatalogueTypeSymbol, 
            dbo.PRMProductCatalogueTypes.ProductCatalogueTypeName;
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_ProductionHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PRMProductCatalogueTypes"
            Begin Extent = 
               Top = 139
               Left = 48
               Bottom = 309
               Right = 319
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
      Begin ColumnWidths = 27
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 2925
         Alias = 1635
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_ProductionHistory';


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
         Begin Table = "PRMHeats"
            Begin Extent = 
               Top = 162
               Left = 361
               Bottom = 292
               Right = 541
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PRMProductCatalogue"
            Begin Extent = 
               Top = 15
               Left = 357
               Bottom = 145
               Right = 591
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "PRMProducts"
            Begin Extent = 
               Top = 14
               Left = 28
               Bottom = 144
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PRMSteelgrades"
            Begin Extent = 
               Top = 164
               Left = 44
               Bottom = 294
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "PRMWorkOrders"
            Begin Extent = 
               Top = 120
               Left = 689
               Bottom = 273
               Right = 902
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "PRMShapes"
            Begin Extent = 
               Top = 0
               Left = 734
               Bottom = 130
               Right = 915
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "MVHDefects"
            Begin Extent = 
               Top = 276
               Left = 579
               Bottom = 406
               Right = 779
          ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_ProductionHistory';

