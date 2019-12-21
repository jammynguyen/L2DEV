CREATE   VIEW [V_AlarmDefinitions] AS
SELECT CAST([AlarmDefinitionId] AS INT) [AlarmDefinitionId]
      ,[AlarmCode]
      ,[Description]
      ,[AlarmType]
      ,[ToConfirm]
      ,CAST([AlarmCategoryId] AS INT) [AlarmCategoryId]
      ,[ShowPopup]
  FROM [smf].[AlarmDefinitions]