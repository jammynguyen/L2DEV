
CREATE   VIEW [dbo].[V_DW_DimDate]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          CONVERT(BIGINT, CONVERT(VARCHAR(8), DateDay, 112)) AS DimDateKey, 
          DATEPART(YEAR, DateDay) AS DimYearKey, 
          CONVERT(INT,
                  CASE
                      WHEN DATEPART(QUARTER, [DateDay]) <          = (2)
                      THEN '1'
                      ELSE '2'
                  END) AS DimHalfOfYearKey, 
          DATEPART(QUARTER, DateDay) AS DimQuarterKey, 
          DATEPART(MONTH, DateDay) AS DimMonthKey, 
          DATEPART(WEEK, DateDay) AS DimWeekKey, 
          DATEPART(ISO_WEEK, DateDay) AS DimWeekISOKey, 
          DATEPART(DAYOFYEAR, DateDay) AS DateDayOfYear, 
          DATEPART(DAY, DateDay) AS DimDayOfMonthKey, 
          DATEPART(WEEKDAY, DateDay) AS DimDayOfWeekKey, 
          CONVERT(BIGINT, DaysOfYearId) AS DimCalendarKey, 
          CONVERT(BIT,
                  CASE
                      WHEN DATEPART(WEEKDAY, [DateDay]) IN(1, 7)
                      THEN 1
                      ELSE 0
                  END) AS IsWeekend, 
          DATENAME(MONTH, DateDay) AS [MonthName], 
          CONVERT(VARCHAR(10), DATENAME(WEEKDAY, DateDay)) AS WeekDayName, 
          CONVERT(DATETIME, DateDay) AS FullDateTime, 
          CONVERT(DATETIME2(3), DateDay) AS FullDateTime2, 
          CONVERT(DATE, DateDay) FullDate, 
          DateDay, 
          CONVERT(VARCHAR(10), DateDay, 102) AS DateANSI, 
          CONVERT(VARCHAR(10), DateDay, 101) AS DateUS, 
          CONVERT(VARCHAR(10), DateDay, 103) AS DateUK, 
          CONVERT(VARCHAR(10), DateDay, 104) AS DateDE, 
          CONVERT(VARCHAR(10), DateDay, 105) AS DateIT, 
          CONVERT(VARCHAR(8), DateDay, 112) AS DateISO, 
          CONVERT(DATE, DATEADD(YEAR, DATEDIFF(YEAR, 0, DateDay), 0)) AS DateFirstOfYear, 
          CONVERT(DATE, DATEADD(DD, -1, DATEADD(YY, DATEDIFF(YY, 0, DateDay) + 1, 0))) AS DateLastOfYear, 
          CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, DateDay), 0)) AS DateFirstOfMonth, 
          CONVERT(DATE, EOMONTH(DateDay)) AS DateLastOfMonth
   FROM dbo.DaysOfYear AS DOY;