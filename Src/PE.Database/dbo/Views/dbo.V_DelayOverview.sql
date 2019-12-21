

CREATE       VIEW [dbo].[V_DelayOverview]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY D.DelayStart DESC), 0) AS Sorting, 
		  CONCAT(CONVERT(VARCHAR(10),CONVERT(DATE, D.DelayStart)) , ' - ', GS.ShiftCode, ' - ', GS.CrewName) DelayHeader,
          D.DelayId, 
		  CONVERT(DATE, D.DelayStart) DateDay,
          dbo.FNGetShiftId(D.DelayStart) AS ShiftIdDelayStart, 
          dbo.FNGetShiftId(D.DelayEnd) AS ShiftIdDelayEnd, 
		  GS.ShiftCode,
          DC.DelayCatalogueCode, 
          DC.DelayCatalogueName, 
          DCC.DelayCatalogueCategoryCode, 
          DCC.DelayCatalogueCategoryName, 
		  DC.StdDelayTime,
          D.DelayStart, 
          D.DelayEnd, 
          24 * 60 * 60 * (CONVERT(FLOAT, ISNULL(D.DelayEnd, GETDATE())) - CONVERT(FLOAT, D.DelayStart)) AS DelayDuration, 
          CONCAT(CAST(FLOOR(CAST(ISNULL(D.DelayEnd, GETDATE()) - D.DelayStart AS FLOAT)) AS VARCHAR),
                                                                                                   CASE
                                                                                                       WHEN FLOOR(CAST(ISNULL(D.DelayEnd, GETDATE()) - D.DelayStart AS FLOAT)) = 1
                                                                                                       THEN ' day '
                                                                                                       ELSE ' days '
                                                                                                   END, CONVERT(VARCHAR, ISNULL(D.DelayEnd, GETDATE()) - D.DelayStart, 8)) DelayDurationFD, 
          D.IsPlanned AS IsPlanned,
          CASE
              WHEN D.DelayEnd IS NULL
              THEN 1
              ELSE 0
          END IsOpen, 
          WO.WorkOrderId, 
          WO.WorkOrderName, 
          RM.RawMaterialId, 
          RM.RawMaterialName, 
          D.FKUserId, 
          U.UserName, 
          D.UserComment
   FROM DLSDelays D
        INNER JOIN DLSDelayCatalogue DC ON D.FKDelayCatalogueId = DC.DelayCatalogueId
        INNER JOIN DLSDelayCatalogueCategory DCC ON DC.FKDelayCatalogueCategoryId = DCC.DelayCatalogueCategoryId
        LEFT OUTER JOIN smf.Users U ON D.FKUserId = U.Id
        LEFT OUTER JOIN MVHRawMaterials RM ON D.FKRawMaterialId = RM.RawMaterialId
        LEFT OUTER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
        LEFT OUTER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
		OUTER APPLY dbo.FNTGetShiftId(D.DelayStart) GS