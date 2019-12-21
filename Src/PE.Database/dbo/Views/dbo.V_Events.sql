


CREATE   VIEW [dbo].[V_Events]
AS
     SELECT 
	 ISNULL(ROW_NUMBER() OVER (ORDER BY DOY.DateDay),0) AS Sorting,
	 CONCAT(ET.EventTypeId, '.', EventsQry.EventInternalId) AS EventIndex, 
            DOY.DateDay, 
            DOY.Year, 
            DOY.Quarter, 
            DOY.Month, 
            DOY.DayNumber, 
            DOY.WeekDayNumber, 
            DOY.WeekNumber, 
            ET.EventTypeId, 
            ET.EventTypeName, 
            EventsQry.ShiftCalendarId, 
            EventsQry.EventTimeFrom, 
            EventsQry.EventTimeTo, 
            EventsQry.EventName, 
            EventsQry.EventDescription, 
            EventsQry.EventAssetId, 
            EventsQry.EventMaterialId, 
            EventsQry.EventInternalId, 
            EventsQry.ColorCheck
     FROM dbo.DaysOfYear AS DOY
          INNER JOIN
     (
         SELECT CAST(SC.PlannedStartTime AS DATE) AS DateDay, 
                SC.ShiftCalendarId AS ShiftCalendarId, 
                'SC' AS EventTypeCode, 
                SC.PlannedStartTime AS EventTimeFrom, 
                SC.PlannedEndTime AS EventTimeTo, 
                SC.ShiftCalendarId AS EventInternalId, 
                NULL AS EventAssetId, 
                NULL AS EventMaterialId, 
                SD.ShiftCode AS EventName, 
                CONCAT('', SD.ShiftCode) AS EventDescription, 
                DENSE_RANK() OVER(
                ORDER BY SD.ShiftDefinitionId) ColorCheck
         FROM dbo.ShiftCalendar AS SC
              INNER JOIN dbo.ShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
         UNION ALL
         SELECT CAST(SC.PlannedStartTime AS DATE) AS DateDay, 
                SC.ShiftCalendarId AS ShiftCalendarId, 
                'CC' AS EventTypeCode, 
                SC.PlannedStartTime AS EventTimeFrom, 
                SC.PlannedEndTime AS EventTimeTo, 
                SC.FKCrewId AS EventInternalId, 
                NULL AS EventAssetId, 
                NULL AS EventMaterialId, 
                C.CrewName AS EventName, 
                CONCAT(C.Description, ' ', C.LeaderName) AS EventDescription, 
                DENSE_RANK() OVER(
                ORDER BY C.CrewId) ColorCheck
         FROM dbo.ShiftCalendar AS SC
              INNER JOIN dbo.Crews AS C ON SC.FKCrewId = C.CrewId
         UNION ALL
         SELECT CAST(D.CreatedTs AS DATE) AS DateDay, 
                dbo.FNGetShiftId(D.CreatedTs) AS ShiftCalendarId, 
                'MD' AS EventTypeCode, 
                D.CreatedTs AS EventTimeFrom, 
                D.CreatedTs AS EventTimeTo, 
                D.DefectId AS EventInternalId, 
                NULL AS EventAssetId, 
                D.FKRawMaterialId AS EventMaterialId, 
                D.DefectName AS EventName, 
                RM.RawMaterialName AS EventDescription, 
                1
         FROM dbo.MVHDefects AS D
              INNER JOIN dbo.MVHDefectCatalogue AS DC ON D.FKDefectCatalogueId = DC.DefectCatalogueId
              INNER JOIN dbo.MVHRawMaterials AS RM ON D.FKRawMaterialId = RM.RawMaterialId
         UNION ALL
         SELECT CAST(D.DelayStart AS DATE) AS DateDay, 
                dbo.FNGetShiftId(D.DelayStart) AS ShiftCalendarId, 
                'LD' AS EventTypeCode, 
                D.DelayStart AS EventTimeFrom, 
                D.DelayEnd AS EventTimeTo, 
                D.DelayId AS EventInternalId, 
                NULL AS EventAssetId, 
                D.FKRawMaterialId AS EventMaterialId, 
                DC.DelayCatalogueName AS EventName, 
                DC.DelayCatalogueDescription AS EventDescription, 
                1
         FROM dbo.DLSDelays AS D
              INNER JOIN dbo.DLSDelayCatalogue AS DC ON D.FKDelayCatalogueId = DC.DelayCatalogueId
         UNION ALL
         SELECT CAST(S.PlannedStartTime AS DATE) AS DateDay, 
                dbo.FNGetShiftId(S.PlannedStartTime) AS ShiftCalendarId, 
                'PP' AS EventTypeCode, 
                S.PlannedStartTime AS EventTimeFrom, 
                S.PlannedEndTime AS EventTimeTo, 
                S.ScheduleId AS EventInternalId, 
                NULL AS EventAssetId, 
                NULL AS EventMaterialId, 
                CONCAT('Work Order Name:', WO.WorkOrderName) AS EventName, 
                CONCAT('Product Catalogue Name:', PC.ProductCatalogueName) AS EventDescription, 
                1
         FROM dbo.PPLSchedules AS S
              INNER JOIN dbo.PRMWorkOrders AS WO ON S.FKWorkOrderId = WO.WorkOrderId
              INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
         UNION ALL
         SELECT CAST(RMS.CreatedTs AS DATE) AS DateDay, 
                dbo.FNGetShiftId(RMS.CreatedTs) AS ShiftCalendarId, 
                'RMCG' AS EventTypeCode, 
                RMS.CreatedTs AS EventTimeFrom, 
                RMS.CreatedTs AS EventTimeTo, 
                RMS.RawMaterialStepId AS EventInternalId, 
                RMS.FKAssetId AS EventAssetId, 
                RMS.FKRawMaterialId AS EventMaterialId, 
                CONCAT(RM.RawMaterialName, ', Pass: ', CAST(RMS.PassNo AS NVARCHAR)) AS EventName, 
                CONCAT('Material length: ', CAST(RMS.Length AS NVARCHAR)) AS EventDescription, 
                1
         FROM dbo.MVHRawMaterialsSteps AS RMS
              INNER JOIN dbo.MVHRawMaterials AS RM ON RMS.FKRawMaterialId = RM.RawMaterialId
         WHERE(RMS.ProcessingStepNo = 1)
              AND (RMS.FKAssetId = 6)
         UNION ALL
         SELECT CAST(CreatedTs AS DATE) AS DateDay, 
                dbo.FNGetShiftId(CreatedTs) AS ShiftCalendarId, 
                'L3WO' AS EventTypeCode, 
                CreatedTs AS EventTimeFrom, 
                CreatedTs AS EventTimeTo, 
                CounterId AS EventInternalid, 
                NULL AS AssetId, 
                NULL AS EventMaterialId, 
                WorkOrderName AS EventName, 
                CONCAT('Product Name: ', ProductName, 'Heat Name:', HeatName) AS EventDescription, 
                1
         FROM xfr.L3L2WorkOrderDefinition AS L3WO
     ) AS EventsQry ON DOY.DateDay = EventsQry.DateDay
          INNER JOIN dbo.EventTypes AS ET ON EventsQry.EventTypeCode = ET.EventTypeCode;
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_Events';


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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_Events';

