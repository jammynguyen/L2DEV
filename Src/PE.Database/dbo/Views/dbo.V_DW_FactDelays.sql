
CREATE   VIEW [dbo].[V_DW_FactDelays]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            DATEPART(YEAR, D.DelayStart) AS DimYearKey, 
            CONVERT(BIGINT, CONVERT(VARCHAR(8), D.DelayStart, 112)) AS DimDateKey, 
            dbo.FNGetShiftId(D.CreatedTs) AS DimShiftKey, 
            D.DelayId AS DimDelayKey, 
            dbo.FNGetShiftId(D.DelayStart) AS DimShiftKeyStart, 
            dbo.FNGetShiftId(D.DelayEnd) AS DimShiftKeyEnd, 
            D.FKDelayCatalogueId AS DimDelayCatalogueKey, 
            DC.FKDelayCatalogueCategoryId AS DimDelayCatalogueCategoryKey, 
            D.FKRawMaterialId AS DimRawMaterialKey, 
            D.FKUserId AS DimUserKey, 
            CONVERT(DATETIME2(3), ISNULL(D.DelayStart,0)) AS DelayStart, 
            CONVERT(DATETIME2(3), D.DelayEnd) AS DelayEnd, 
            CAST(ISNULL(DelayEnd, SYSDATETIME()) - DelayStart AS NUMERIC(18, 8)) AS DelayDurationD, 
            CAST(ISNULL(DelayEnd, SYSDATETIME()) - DelayStart AS NUMERIC(18, 8)) * 24 AS DelayDurationH, 
            CAST(ISNULL(DelayEnd, SYSDATETIME()) - DelayStart AS NUMERIC(18, 8)) * 24 * 60 AS DelayDurationM, 
            CAST(ISNULL(DelayEnd, SYSDATETIME()) - DelayStart AS NUMERIC(18, 8)) * 24 * 60 * 60 AS DelayDuration, 
            CONCAT(CAST(FLOOR(CAST(ISNULL(DelayEnd, GETDATE()) - DelayStart AS NUMERIC(18, 8))) AS NVARCHAR(50)),
                                                                                                               CASE
                                                                                                                   WHEN FLOOR(CAST(ISNULL(DelayEnd, GETDATE()) - DelayStart AS FLOAT)) = 1
                                                                                                                   THEN ' day '
                                                                                                                   ELSE ' days '
                                                                                                               END, CONVERT(VARCHAR, ISNULL(DelayEnd, GETDATE()) - DelayStart, 8)) DelayDurationFD, 
            UserComment AS UserComment, 
            DC.IsPlanned AS IsPlanned, 
            ISNULL(CONVERT(BIT,
                           CASE
                               WHEN DelayEnd IS NULL
                               THEN 1
                               ELSE 0
                           END), 0) IsOpen
     FROM dbo.DLSDelays D
          INNER JOIN dbo.DLSDelayCatalogue DC ON D.FKDelayCatalogueId = DC.DelayCatalogueId;