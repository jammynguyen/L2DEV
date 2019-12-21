
CREATE     VIEW [dbo].[V_RollSetOverview]
AS SELECT TOP (100) PERCENT RS.RollSetId, 
                            RS.RollSetName, 
                            RS.[Status] AS RollSetStatus, 
                            RS.RollSetType, 
                            RollsUpper.RollId AS RollIdUpper, 
                            RollsUpper.ActualDiameter AS DiameterUpper, 
                            RollsUpper.RollName AS RollNameUpper, 
                            RollTypesUpper.RollTypeId AS RollTypeIdUpper, 
                            RollTypesUpper.RollTypeName AS RollTypeUpper, 
                            RollsBottom.RollId AS RollIdBottom, 
                            RollsBottom.ActualDiameter AS DiameterBottom, 
                            RollsBottom.RollName AS RollNameBottom, 
                            RollTypesBottom.RollTypeId AS RollTypeIdBottom, 
                            RollTypesBottom.RollTypeName AS RollTypeBottom, 
                            RollsThird.RollId AS RollIdThird, 
                            RollsThird.ActualDiameter AS DiameterThird, 
                            RollsThird.RollName AS RollNameThird, 
                            RollTypesThird.RollTypeId AS RollTypeIdThird, 
                            RollTypesThird.RollTypeName AS RollTypeThird, 
                            RSH.RollSetHistoryId, 
                            RSH.[Status] AS RollSetHistoryStatus, 
                            RSH.Mounted, 
                            RSH.Dismounted, 
                            RSH.FKCassetteId AS CassetteId, 
                            RSH.PositionInCassette, 
                            C.CassetteName, 
                            C.FKStandId, 
                            C.Arrangement, 
                            S.StandId, 
                            S.StandNo, 
                            RS.IsThirdRoll
   FROM dbo.RLSStands S
        RIGHT OUTER JOIN dbo.RLSCassettes C ON S.StandId = C.FKStandId
        RIGHT OUTER JOIN dbo.RLSRollSets AS RS
        INNER JOIN dbo.RLSRollSetHistory AS RSH ON RS.RollSetId = RSH.FKRollSetId
        LEFT OUTER JOIN dbo.RLSRollTypes AS RollTypesBottom
        INNER JOIN dbo.RLSRolls AS RollsBottom ON RollTypesBottom.RollTypeId = RollsBottom.FKRollTypeId ON RS.FKBottomRollId = RollsBottom.RollId
        LEFT OUTER JOIN dbo.RLSRollTypes AS RollTypesThird
        INNER JOIN dbo.RLSRolls AS RollsThird ON RollTypesThird.RollTypeId = RollsThird.FKRollTypeId ON RS.FKThirdRollId = RollsThird.RollId
        LEFT OUTER JOIN dbo.RLSRollTypes AS RollTypesUpper
        INNER JOIN dbo.RLSRolls AS RollsUpper ON RollTypesUpper.RollTypeId = RollsUpper.FKRollTypeId ON RS.FKUpperRollId = RollsUpper.RollId ON C.CassetteId = RSH.FKCassetteId
   ORDER BY RS.RollSetName;