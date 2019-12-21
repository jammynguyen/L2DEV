CREATE FUNCTION [dbo].[FNTGenerateDateDimensions](@Date DATETIME)
RETURNS TABLE
AS
     RETURN

     --DECLARE @Date DATETIME= '2019-07-30 23:59:59';
     SELECT CONVERT(DATETIME2(7), GETDATE()) AS LoadTs, 
            CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
            CONVERT(BIGINT, CONVERT(VARCHAR(8), @Date, 112)) AS DimDateKey, 
            DATEDIFF(MINUTE, CONVERT(TIME, '00:00:00', 108), CONVERT(TIME, @Date, 108)) + 1 AS DimTimeMinKey, 
            DATEPART(YEAR, @Date) AS DimYearKey, 
            dbo.FNGetShiftId(@Date) DimShiftKey, 
            CONVERT(INT,
                    CASE
                        WHEN DATEPART(QUARTER, @Date) <          = (2)
                        THEN '1'
                        ELSE '2'
                    END) AS [Half Of Year], 
            DATEPART(QUARTER, @Date) AS [Quarter Number], 
            DATEPART(MONTH, @Date) AS [Month Number], 
            DATEPART(WEEK, @Date) AS [Week Number], 
            DATEPART(ISO_WEEK, @Date) AS [Week ISO], 
            DATEPART(DAYOFYEAR, @Date) AS [Day Of Year], 
            DATEPART(DAY, @Date) AS [Day Of Month], 
            DATEPART(WEEKDAY, @Date) AS [Day Of Week], 
            CONVERT(BIT,
                    CASE
                        WHEN DATEPART(WEEKDAY, @Date) IN(1, 7)
                        THEN 1
                        ELSE 0
                    END) AS [IsWeekend], 
            DATENAME(MONTH, @Date) AS [Month Name], 
            CAST(SUBSTRING(DATENAME(month, @Date), 1, 3) AS NVARCHAR(3)) AS [Short Month Name], 
            CONVERT(VARCHAR(10), DATENAME(WEEKDAY, @Date)) AS [Week Day Name], 
            CONVERT(DATETIME2, @Date) AS [Full Date Time], 
            CONVERT(DATETIME, @Date) AS [Date Time], 
            CONVERT(DATE, @Date) [Date Day], 
            CONVERT(VARCHAR(10), @Date, 102) AS [Date ANSI], 
            CONVERT(VARCHAR(10), @Date, 101) AS [Date US], 
            CONVERT(VARCHAR(10), @Date, 103) AS [DateUK], 
            CONVERT(VARCHAR(10), @Date, 104) AS [DateDE], 
            CONVERT(VARCHAR(10), @Date, 105) AS [DateIT], 
            CONVERT(VARCHAR(8), @Date, 112) AS [DateISO], 
            CONVERT(DATE, DATEADD(YEAR, DATEDIFF(YEAR, 0, @Date), 0)) AS [Date First Of Year], 
            CONVERT(DATE, DATEADD(DD, -1, DATEADD(YY, DATEDIFF(YY, 0, @Date) + 1, 0))) AS [Date Last Of Year], 
            CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, @Date), 0)) AS [Date First Of Month], 
            CONVERT(DATE, EOMONTH(@Date)) AS [Date Last Of Month], 
            CAST(N'CY' + CAST(YEAR(@Date) AS NVARCHAR(4)) AS NVARCHAR) AS [Calendar Year Label], 
            CAST(N'CY' + CAST(YEAR(@Date) AS NVARCHAR(4)) + N'CQ' + CAST(DATEPART(QUARTER, @Date) AS NVARCHAR(1)) AS NVARCHAR(10)) AS [Calendar Quarter Label], 
            CAST(N'CY' + CAST(YEAR(@Date) AS NVARCHAR(4)) + N'CM' + CAST(MONTH(@Date) AS NVARCHAR(2)) AS NVARCHAR(10)) AS [Calendar Month Label], 
            @Date AS [Date];