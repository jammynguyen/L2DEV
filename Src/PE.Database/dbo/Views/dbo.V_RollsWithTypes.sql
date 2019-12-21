CREATE   VIEW [dbo].[V_RollsWithTypes]
AS SELECT R.RollId, 
          R.RollName, 
          R.[Status], 
          RT.RollTypeId, 
          RT.RollTypeName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
          R.Supplier, 
          R.GroovesNumber, 
          RT.RollTypeDescription, 
          RT.DiameterMin, 
          RT.DiameterMax, 
          RT.RoughnessMin, 
          RT.RoughnessMax, 
          RT.YieldStrengthRef, 
          RT.SteelgradeRoll, 
          RT.[Length], 
          RollSetUpper.RollSetName AS RollSetUpper, 
          RollSetBottom.RollSetName AS RollSetBottom, 
          R.ScrapReason, 
          R.ScrapDate, 
          R.RollDescription, 
          RollSetUpper.RollSetId AS RollSetIdUpper, 
          RollSetBottom.RollSetId AS RollSetIdBottom, 
          RollSetThird.RollSetId AS RollSetIdThird, 
          R.FKRollTypeId, 
          R.RollType, 
          RollSetThird.RollSetName AS RollSetThird, 
          RT.MatchingRollsetType
   FROM dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId
        LEFT OUTER JOIN dbo.RLSRollSets AS RollSetThird ON R.RollId = RollSetThird.FKThirdRollId
        LEFT OUTER JOIN dbo.RLSRollSets AS RollSetBottom ON R.RollId = RollSetBottom.FKBottomRollId
        LEFT OUTER JOIN dbo.RLSRollSets AS RollSetUpper ON R.RollId = RollSetUpper.FKUpperRollId;