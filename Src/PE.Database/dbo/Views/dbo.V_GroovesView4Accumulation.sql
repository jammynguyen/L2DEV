CREATE   VIEW [dbo].[V_GroovesView4Accumulation]
AS SELECT RSH.FKRollSetId, 
          RSH.RollSetHistoryId, 
          RSH.Mounted, 
          RSH.[Status] AS RollSetHistoryStatus, 
          RSH.PositionInCassette, 
          RGH.RollGroovesHistoryId, 
          RGH.GrooveNumber, 
          RGH.FKGrooveTemplateId, 
          RGH.[Status] AS GrooveStatus, 
          RGH.AccBilletCnt, 
          RGH.AccWeight, 
          RGH.ActDiameter, 
          C.FKCassetteTypeId,
          C.CassetteName, 
          C.Arrangement, 
		  S.StandNo, 
          S.[Status] AS StandStatus, 
          S.StandId,
          S.NumberOfRolls, 
          RS.[Status] AS RollSetStatus, 
          RS.IsThirdRoll
   FROM dbo.RLSRollSetHistory RSH
        INNER JOIN dbo.RLSRollGroovesHistory RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
        INNER JOIN dbo.RLSCassettes C ON RSH.FKCassetteId = C.CassetteId
        INNER JOIN dbo.RLSStands S ON C.FKStandId = S.StandId
        INNER JOIN dbo.RLSRollSets RS ON RSH.FKRollSetId = RS.RollSetId;