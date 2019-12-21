CREATE VIEW [dbo].[V_DW_DimShift]
AS
     SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
            CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
            SC.ShiftCalendarId AS DimShiftKey, 
            CONVERT(BIGINT, CONVERT(VARCHAR(8), SC.PlannedStartTime, 112)) AS DimDateKey, 
            SC.FKCrewId AS DimCrewKey, 
            CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', ShiftCode) ShiftKey, 
            SD.ShiftCode, 
            CONVERT(DATETIME2(3), SC.PlannedStartTime) AS ShiftStartTime, 
            CONVERT(DATETIME2(3), SC.PlannedEndTime) AS ShiftEndTime, 
            CAST(24 * CONVERT(FLOAT, SC.PlannedEndTime - SC.PlannedStartTime) AS FLOAT) AS ShiftDurationH, 
            CAST(60 * 24 * CONVERT(FLOAT, SC.PlannedEndTime - SC.PlannedStartTime) AS FLOAT) AS ShiftDurationM, 
            CAST(60 * 60 * 24 * CONVERT(FLOAT, SC.PlannedEndTime - SC.PlannedStartTime) AS FLOAT) AS ShiftDurationS, 
            SD.ShiftEndsNextDay, 
            C.CrewName, 
            C.[Description] AS CrewDescription
     FROM dbo.ShiftCalendar AS SC
          INNER JOIN dbo.ShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN dbo.Crews AS C ON SC.FKCrewId = C.CrewId;