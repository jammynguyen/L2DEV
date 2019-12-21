CREATE VIEW [dbo].[V_DW_DimTimeH]
AS
     WITH digits(i)
          AS (SELECT 1 AS i
              UNION ALL
              SELECT 2 AS Expr1
              UNION ALL
              SELECT 3 AS Expr1
              UNION ALL
              SELECT 4 AS Expr1
              UNION ALL
              SELECT 5 AS Expr1
              UNION ALL
              SELECT 6 AS Expr1
              UNION ALL
              SELECT 7 AS Expr1
              UNION ALL
              SELECT 8 AS Expr1
              UNION ALL
              SELECT 9 AS Expr1
              UNION ALL
              SELECT 0 AS Expr1),
          sequence(i)
          AS (SELECT(D1.i + 10 * D2.i) + 100 * D3.i AS Expr1
              FROM digits AS D1
                   CROSS JOIN digits AS D2
                   CROSS JOIN digits AS D3)
          SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
                 CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
                 CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
                 CAST(i + 1 AS BIGINT) AS DimTimeHKey, 
                 DATEPART(hh, dateVal) AS TimeHours, 
                 DATEPART(hh, dateVal) % 12 + CASE
                                                  WHEN DATEPART(hh, dateval) % 12 = 0
                                                  THEN 12
                                                  ELSE 0
                                              END AS TimeHours12,
                 CASE
                     WHEN DATEPART(hh, dateval) BETWEEN 0 AND 11
                     THEN 'AM'
                     ELSE 'PM'
                 END AS TimeAmPm, 
                 RIGHT('0' + CAST(DATEPART(hh, dateVal) AS VARCHAR(2)), 2) AS TimeHourCode,
				 CONVERT(TIME(0),dateVal) AS TimeTime
          FROM
          (
              SELECT DATEADD(HOUR, i, '20000101') AS dateVal, 
                     i
              FROM sequence AS sequence
              WHERE(i BETWEEN 0 AND 23)
          ) AS dailyHours;