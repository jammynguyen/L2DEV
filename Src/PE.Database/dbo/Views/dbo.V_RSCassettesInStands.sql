CREATE   VIEW [dbo].[V_RSCassettesInStands]
AS SELECT TOP (100) PERCENT S.StandId, 
                            S.[Status], 
                            S.StandNo, 
                            S.StandZoneName, 
                            S.IsOnLine, 
                            S.Position, 
                            C.CassetteId, 
                            C.[Status] AS CassetteStatus, 
                            C.CassetteName, 
                            C.Arrangement, 
                            CT.CassetteTypeName, 
                            CT.[Type], 
                            CT.CassetteTypeId
   FROM dbo.RLSCassettes C
        INNER JOIN dbo.RLSCassetteTypes CT ON C.FKCassetteTypeId = CT.CassetteTypeId
        RIGHT OUTER JOIN dbo.RLSStands S ON C.FKStandId = S.StandId
   ORDER BY S.StandId;