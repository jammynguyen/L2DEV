CREATE   VIEW [dbo].[V_CassettesOverview]
AS SELECT C.CassetteId, 
          C.CassetteName, 
          C.[Status], 
          C.FKCassetteTypeId, 
          CT.CassetteTypeName, 
          C.NumberOfPositions, 
          S.StandId, 
          S.StandNo, 
          C.Arrangement, 
          NORIC.RollsetsCnt, 
          CT.NumberOfRolls, 
          CT.IsInterCassette, 
          CT.[Type]
   FROM dbo.RLSCassettes AS C
        INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId
        LEFT OUTER JOIN
   (
       SELECT FKCassetteId, 
              COUNT(FKRollSetId) AS RollsetsCnt
       FROM dbo.RLSRollSetHistory
       WHERE [Status] = 1
       GROUP BY FKCassetteId
   ) AS NORIC ON C.CassetteId = NORIC.FKCassetteId
        LEFT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId;