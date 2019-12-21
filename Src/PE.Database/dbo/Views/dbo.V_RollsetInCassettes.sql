CREATE   VIEW [dbo].[V_RollsetInCassettes]
AS SELECT RS.RollSetId, 
          RS.RollSetName, 
          RS.FKUpperRollId, 
          RS.FKBottomRollId, 
          RS.[Status], 
          RS.RollSetType, 
          RSH.FKCassetteId, 
          RSH.[Status] AS RollSetHistoryStatus, 
          RSH.PositionInCassette, 
          RSH.Mounted, 
          RSH.Dismounted, 
          C.[Status] AS CassetteStatus, 
          C.CassetteName, 
          C.FKStandId, 
          S.StandNo
   FROM dbo.RLSRollSets AS RS
        INNER JOIN dbo.RLSRollSetHistory AS RSH ON RS.RollSetId = RSH.FKRollSetId
        INNER JOIN dbo.RLSCassettes AS C ON RSH.FKCassetteId = C.CassetteId
        LEFT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
   WHERE(RS.[Status] = 6);