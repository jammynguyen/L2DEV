CREATE FUNCTION [dbo].[FNTGetShiftId](@DateTimeStamp DATETIME)
RETURNS TABLE
AS
     RETURN
     --DECLARE @ShiftCalendarId BIGINT;
     SELECT ShiftCalendarId, 
            SD.ShiftCode, 
            C.CrewName, 
            CONCAT(DOY.DateDay, ' ', SD.ShiftCode) ShiftKey
     --SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
     FROM ShiftCalendar S
          INNER JOIN [dbo].[DaysOfYear] DOY ON S.FKDaysOfYear = DOY.DaysOfYearId
          INNER JOIN [dbo].[ShiftDefinitions] SD ON S.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN [dbo].[Crews] C ON S.FKCrewId = C.CrewId
     --WHERE @DateTimeStamp BETWEEN StartTime AND EndTime;
     --IF @ShiftCalendarId IS NULL
     --    SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
     --    FROM ShiftCalendar
     WHERE @DateTimeStamp BETWEEN PlannedStartTime AND PlannedEndTime;

/*
		 SELECT * FROM ShiftCalendar
		 SELECT * FROM DaysOfYear
 */