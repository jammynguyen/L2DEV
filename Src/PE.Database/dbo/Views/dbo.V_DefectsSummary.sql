CREATE   VIEW [dbo].[V_DefectsSummary]
AS SELECT DF.DefectId, 
          DF.CreatedTs, 
          DF.FKDefectCatalogueId, 
          DF.FKRawMaterialId, 
          DF.FKProductId, 
          DF.DefectName, 
          DF.DefectPosition, 
          DF.DefectFrequency, 
          DF.DefectScale, 
          DF.DefectDescription, 
		  DFC.DefectCatalogueCode,
          DFC.DefectCatalogueName,
		  DFCC.DefectCatalogueCategoryCode,
          DFCC.DefectCatalogueCategoryName, 
          CAST(CASE
                   WHEN DF.FKRawMaterialId IS NOT NULL
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsRawMaterialRelated, 
          CAST(CASE
                   WHEN DF.FKProductId IS NOT NULL
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsProductRelated, 
          RM.RawMaterialName, 
          PR.ProductName
   FROM dbo.MVHDefects AS DF
        INNER JOIN dbo.MVHDefectCatalogue AS DFC ON DF.FKDefectCatalogueId = DFC.DefectCatalogueId
        INNER JOIN dbo.MVHDefectCatalogueCategory AS DFCC ON DFC.FKDefectCatalogueCategoryId = DFCC.DefectCatalogueCategoryId
        LEFT OUTER JOIN dbo.MVHRawMaterials AS RM ON DF.FKRawMaterialId = RM.RawMaterialId
        LEFT OUTER JOIN dbo.PRMProducts AS PR ON DF.FKProductId = PR.ProductId;
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DefectsSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DefectsSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[21] 2[20] 3) )"
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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DF"
            Begin Extent = 
               Top = 0
               Left = 23
               Bottom = 130
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "DFC"
            Begin Extent = 
               Top = 153
               Left = 595
               Bottom = 283
               Right = 843
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "DFCC"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "RM"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PR"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 136
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_DefectsSummary';

