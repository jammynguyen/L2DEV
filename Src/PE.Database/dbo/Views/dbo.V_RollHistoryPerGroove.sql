CREATE   VIEW [dbo].[V_RollHistoryPerGroove]
AS SELECT TOP (100) PERCENT R.RollId, 
                            R.RollName, 
                            RSH.FKRollSetId, 
                            RSH.RollSetHistoryId, 
                            RGH.ActDiameter, 
                            RGH.GrooveNumber, 
                            RGH.AccWeight, 
                            RSH.AccWeightLimit, 
                            RGH.AccBilletCnt, 
                            RSH.[Status] AS RollSetHistoryStatus, 
                            RGH.[Status] AS GrooveStatus, 
                            GT.GrooveTemplateName, 
                            RSH.Mounted, 
                            RSH.Dismounted
   FROM dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSRollSetHistory AS RSH ON RGH.FKRollSetHistoryId = RSH.RollSetHistoryId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
   ORDER BY RSH.RollSetHistoryId DESC;