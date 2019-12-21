CREATE   VIEW [dbo].[V_PassChangeDataActual]
AS SELECT RSH.RollSetHistoryId, 
          RSH.Mounted, 
		  RSH.[Status], 
		  RSH.PositionInCassette,
          RS.RollSetId, 
          RS.RollSetName, 
		  RS.RollSetType,
		  C.CassetteId,
          C.CassetteName, 
          C.Arrangement, 
          S.StandId, 
          S.StandNo, 
		  S.Position,
          S.[Status] AS StandStatus, 
          R.ActualDiameter, 
          RT.RollTypeName, 
          RT.AccBilletCntLimit, 
          RT.AccWeightLimit, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RGH.GrooveNumber, 
		  RGH.[Status] AS GrooveStatus,
          GT.GrooveTemplateId, 
          GT.GrooveTemplateName
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSCassettes AS C ON RSH.FKCassetteId = C.CassetteId
        INNER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
        INNER JOIN dbo.RLSRolls AS R ON RS.FKUpperRollId = R.RollId
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                       AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
   WHERE(RSH.[Status] = 1);