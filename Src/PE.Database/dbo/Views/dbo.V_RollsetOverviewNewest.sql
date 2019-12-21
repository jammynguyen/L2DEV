CREATE   VIEW [dbo].[V_RollsetOverviewNewest]
AS SELECT TOP (100) PERCENT RSO.RollSetId, 
                            RSO.RollSetStatus, 
                            RSO.RollSetType, 
                            RSO.RollSetName, 
                            RSO.RollIdUpper, 
                            RSO.DiameterUpper, 
                            RSO.RollNameUpper, 
                            RSO.RollTypeIdUpper, 
                            RSO.RollTypeUpper, 
                            RSO.RollIdBottom, 
                            RSO.DiameterBottom, 
                            RSO.RollNameBottom, 
                            RSO.RollTypeIdBottom, 
                            RSO.RollTypeBottom, 
                            RSO.RollSetHistoryId, 
                            RSO.RollSetHistoryStatus, 
                            C.CassetteId, 
                            C.CassetteName, 
                            RSH.PositionInCassette, 
                            S.StandNo, 
                            RSH.Mounted, 
                            RSH.Dismounted, 
                            C.[Status] AS CassetteStatus, 
                            RSO.RollIdThird, 
                            RSO.DiameterThird, 
                            RSO.RollNameThird, 
                            RSO.RollTypeIdThird, 
                            RSO.RollTypeThird, 
                            RSO.IsThirdRoll, 
                            S.StandId, 
                            S.IsOnLine, 
                            C.FKCassetteTypeId, 
                            RSH.[Status], 
                            RSH.FKCassetteId, 
                            CT.[Type]
   FROM dbo.RLSCassettes AS C
        RIGHT OUTER JOIN dbo.V_RollSetOverview AS RSO
        INNER JOIN dbo.RLSRollSetHistory AS RSH ON RSO.RollSetHistoryId = RSH.RollSetHistoryId ON C.CassetteId = RSH.FKCassetteId
        LEFT OUTER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId
        LEFT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
   WHERE(RSO.RollSetHistoryId IN
   (
       SELECT MAX(RollSetHistoryId) AS Expr1
       FROM dbo.RLSRollSetHistory AS RSH
       WHERE(FKRollSetId = RSO.RollSetId)
   ))
   /*
   GROUP BY RSO.RollSetId, 
            RSO.RollSetStatus, 
            RSO.RollSetType, 
            RSO.RollSetName, 
            RSO.RollIdUpper, 
            RSO.DiameterUpper, 
            RSO.RollNameUpper, 
            RSO.RollTypeUpper, 
            RSO.RollIdBottom, 
            RSO.DiameterBottom, 
            RSO.RollNameBottom, 
            RSO.RollTypeBottom, 
            C.CassetteId, 
            C.CassetteName, 
            RSH.PositionInCassette, 
            S.StandNo, 
            RSH.Mounted, 
            RSH.Dismounted, 
            RSO.RollSetHistoryStatus, 
            RSO.RollSetHistoryId, 
            C.[Status], 
            RSO.RollTypeIdBottom, 
            RSO.RollTypeIdUpper, 
            RSO.RollIdThird, 
            RSO.DiameterThird, 
            RSO.RollNameThird, 
            RSO.RollTypeIdThird, 
            RSO.RollTypeThird, 
            RSO.IsThirdRoll, 
            S.StandId,
            S.IsOnLine, 
            C.FKCassetteTypeId, 
            RSH.[Status],
            RSH.FKCassetteId, 
            CT.[Type]
			*/
   ORDER BY RSO.RollSetName;