
CREATE     VIEW [dbo].[V_RollHistory]
AS SELECT R.RollId, 
          R.RollName, 
          RT.RollTypeName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
		  R.[Status] AS RollStatus,
          RS.RollSetId, 
          RS.RollSetName, 
		  RS.[Status] AS RollSetStatus,
          RSH.RollSetHistoryId, 
          RSH.Mounted, 
          RSH.Dismounted, 
		  RSH.[Status] AS RollSetHistoryStatus,
          RGH.FKGrooveTemplateId, 
          RGH.[Status] AS GrooveStatus, 
          RGH.GrooveNumber, 
          GT.GrooveTemplateName, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RSH.AccWeightLimit,
		  'Upper' RollLocal
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKUpperRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                               AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
   UNION
   SELECT R.RollId, 
          R.RollName, 
          RT.RollTypeName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
		  R.[Status] AS RollStatus,
          RS.RollSetId, 
          RS.RollSetName, 
		  RS.[Status] AS RollSetStatus,
          RSH.RollSetHistoryId, 
          RSH.Mounted, 
          RSH.Dismounted, 
		  RSH.[Status] AS RollSetHistoryStatus,
          RGH.FKGrooveTemplateId, 
          RGH.[Status] AS GrooveStatus, 
          RGH.GrooveNumber, 
          GT.GrooveTemplateName, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RSH.AccWeightLimit,
		  'Bottom' RollLocal
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKBottomRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                               AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
   UNION
   SELECT R.RollId, 
          R.RollName, 
          RT.RollTypeName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
		  R.[Status] AS RollStatus,
          RS.RollSetId, 
          RS.RollSetName, 
		  RS.[Status] AS RollSetStatus,
          RSH.RollSetHistoryId, 
          RSH.Mounted, 
          RSH.Dismounted, 
		  RSH.[Status] AS RollSetHistoryStatus,
          RGH.FKGrooveTemplateId, 
          RGH.[Status] AS GrooveStatus, 
          RGH.GrooveNumber, 
          GT.GrooveTemplateName, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RSH.AccWeightLimit,
		  'Third' RollLocal
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKThirdRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                               AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId