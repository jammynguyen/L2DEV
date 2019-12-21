CREATE   VIEW [dbo].[V_ActualStandConfiguration]
AS SELECT TOP (100) PERCENT S.StandNo, 
                            S.[Status], 
							S.StandId, 
                            S.NumberOfRolls, 
                            S.StandZoneName, 
                            S.IsOnLine, 
                            S.IsCalibrated, 
                            S.Position, 
                            C.CassetteName, 
                            C.NumberOfPositions, 
                            C.Arrangement, 
                            C.FKCassetteTypeId, 
                            C.CassetteId,
                            CO.RollsetsCnt
   FROM dbo.RLSCassettes AS C
        LEFT OUTER JOIN dbo.V_CassettesOverview AS CO ON C.CassetteId = co.CassetteId
        RIGHT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
                                               AND C.FKStandId = S.StandId
   ORDER BY S.StandId;