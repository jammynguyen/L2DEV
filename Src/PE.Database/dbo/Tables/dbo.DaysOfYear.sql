CREATE TABLE [dbo].[DaysOfYear] (
    [DaysOfYearId]  BIGINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DateDay]       DATE   NOT NULL,
    [Year]          AS     (datepart(year,[DateDay])),
    [Quarter]       AS     (datepart(quarter,[DateDay])),
    [Month]         AS     (datepart(month,[DateDay])),
    [Day]           AS     (datepart(day,[DateDay])),
    [WeekNo]        AS     (datepart(week,[DateDay])),
    [IsWeekend]     AS     (CONVERT([bit],case when datepart(weekday,[DateDay])=(7) OR datepart(weekday,[DateDay])=(1) then (1) else (0) end)),
    [MonthName]     AS     (datename(month,[DateDay])),
    [WeekDayName]   AS     (datename(weekday,[DateDay])),
    [HalfOfYear]    AS     (case when datepart(quarter,[DateDay])<=(2) then (1) else (2) end),
    [DateANSI]      AS     (CONVERT([varchar](10),[DateDay],(102))),
    [DateUS]        AS     (CONVERT([varchar](10),[DateDay],(101))),
    [DateUK]        AS     (CONVERT([varchar](10),[DateDay],(103))),
    [DateDE]        AS     (CONVERT([varchar](10),[DateDay],(104))),
    [DateIT]        AS     (CONVERT([varchar](10),[DateDay],(105))),
    [DateISO]       AS     (CONVERT([varchar](8),[DateDay],(112))),
    [FirstOfMonth]  AS     (CONVERT([date],dateadd(month,datediff(month,(0),[DateDay]),(0)))),
    [LastOfMonth]   AS     (CONVERT([date],eomonth([DateDay]))),
    [FirstOfYear]   AS     (CONVERT([date],dateadd(year,datediff(year,(0),[DateDay]),(0)))),
    [LastOfYear]    AS     (CONVERT([date],dateadd(day,(-1),dateadd(year,datediff(year,(0),[DateDay])+(1),(0))))),
    [ISOWeekNumber] AS     (datepart(iso_week,[DateDay])),
    [WeekNumber]    AS     (datepart(week,[DateDay])),
    [YearNumber]    AS     (datepart(year,[DateDay])),
    [MonthNumber]   AS     (datepart(month,[DateDay])),
    [DayYearNumber] AS     (datepart(dayofyear,[DateDay])),
    [DayNumber]     AS     (datepart(day,[DateDay])),
    [WeekDayNumber] AS     (datepart(weekday,[DateDay])),
    CONSTRAINT [PK_DaysOfYear] PRIMARY KEY CLUSTERED ([DaysOfYearId] ASC)
);






GO



GO
CREATE NONCLUSTERED INDEX [NCI_DateDay]
    ON [dbo].[DaysOfYear]([DateDay] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Year_Month_Day]
    ON [dbo].[DaysOfYear]([Year] ASC, [Month] ASC, [Day] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_Year_Quarter]
    ON [dbo].[DaysOfYear]([Year] ASC, [Quarter] ASC);

