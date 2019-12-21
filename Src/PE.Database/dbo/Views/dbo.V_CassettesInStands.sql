
CREATE   VIEW [dbo].[V_CassettesInStands]
AS SELECT CT.CassetteTypeId, 
          C.CassetteId, 
          S.StandId, 
          CT.CassetteTypeName, 
          CT.CassetteTypeDescription, 
          CT.[Type], 
          CT.IsInterCassette, 
          C.Arrangement, 
          C.CassetteName, 
          C.NumberOfPositions, 
          C.[Status] AS CassetteStatus, 
          S.StandNo, 
          S.NumberOfRolls, 
          S.StandZoneName, 
          S.IsOnLine, 
          S.IsCalibrated, 
          S.Position, 
          S.[Status] AS StandStatus, 
          RS.RollSetId, 
          RSH.RollSetHistoryId, 
          RS.[Status] AS RollsetStatus, 
          RS.RollSetType, 
          RS.RollSetName, 
          RS.RollSetDescription AS RollSetDescription, 
          RSH.Mounted, 
          RSH.Dismounted, 
          RSH.[Status] AS RollsetHistoryStatus
   FROM dbo.RLSCassettes AS C
        INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId
        INNER JOIN dbo.RLSRollSetHistory AS RSH ON C.CassetteId = RSH.FKCassetteId
        INNER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
   WHERE(S.[Status] = 6);