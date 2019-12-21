CREATE VIEW dbo.V_MaterialGenealogyUp
AS
WITH TREE AS (
SELECT        RMI.RawMaterialId MaterialId, RMI.RawMaterialName, ISNULL(RMI.ChildsNo, 0) ChildsNo, RMI.ParentRawMaterialId ParentId, RMI.CuttingSeqNo ParentsCut, RMS.[Length] RootInitialLength, 1 IsAncestor, 
                         CASE WHEN RMI.ChildsNo > 0 THEN 1 ELSE 0 END IsParent, [Level] = 1, [Path] = CAST(RMI.RawMaterialId AS VARCHAR), RootId = RMI.RawMaterialId
FROM            dbo.MVHRawMaterials RMI INNER JOIN
                         dbo.MVHRawMaterialsSteps RMS ON RMI.RawMaterialId = RMS.FKRawMaterialId AND RMS.ProcessingStepNo = 1
WHERE        1 = 1
UNION ALL
SELECT        RMI.RawMaterialId, RMI.RawMaterialName, ISNULL(RMI.ChildsNo, 0) ChildsNo, RMI.ParentRawMaterialId ParentId, RMI.CuttingSeqNo ParentsCut, TREE.RootInitialLength, 0 IsAncestor, 
                         CASE WHEN RMI.ChildsNo > 0 THEN 1 ELSE 0 END IsParent, [Level] = TREE.[Level] + 1, [Path] = CAST(TREE.Path + '.' + RIGHT('000000' + CAST(ROW_NUMBER() OVER (ORDER BY RMI.RawMaterialId) AS VARCHAR(10)), 6) 
AS VARCHAR), RootId = TREE.RootId
FROM            dbo.MVHRawMaterials RMI INNER JOIN
                         TREE ON TREE.ParentId = RMI.RawMaterialId)
    SELECT        [Path], [Level] LevelNo, RootId, MaterialId, ParentId, ChildsNo, IsParent, ParentsCut, RootInitialLength, RMS1.[Length] InitialLength, RMS0.[Length] ActualLength, RawMaterialName, 
                              REPLICATE('      ', [Level] - 1) + RawMaterialName AS [Level]
     FROM            TREE INNER JOIN
                              dbo.MVHRawMaterialsSteps RMS0 ON TREE.MaterialId = RMS0.FKRawMaterialId AND RMS0.ProcessingStepNo = 0 INNER JOIN
                              dbo.MVHRawMaterialsSteps RMS1 ON TREE.MaterialId = RMS1.FKRawMaterialId AND RMS1.ProcessingStepNo = 1     WHERE        1 = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_MaterialGenealogyUp';


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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_MaterialGenealogyUp';

