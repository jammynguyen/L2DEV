CREATE TABLE [dbo].[ShiftCalendar] (
    [ShiftCalendarId]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [CreatedTs]           DATETIME CONSTRAINT [DF_ShiftCalendar_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]        DATETIME CONSTRAINT [DF_ShiftCalendar_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKShiftDefinitionId] BIGINT   NOT NULL,
    [FKCrewId]            BIGINT   NOT NULL,
    [FKDaysOfYear]        BIGINT   NOT NULL,
    [PlannedStartTime]    DATETIME NOT NULL,
    [PlannedEndTime]      DATETIME NOT NULL,
    [IsActualShift]       BIT      CONSTRAINT [DF_ShiftCalendar_IsActualShift] DEFAULT ((0)) NOT NULL,
    [StartTime]           DATETIME NULL,
    [EndTime]             DATETIME NULL,
    CONSTRAINT [PK_PEShiftCalendar] PRIMARY KEY CLUSTERED ([ShiftCalendarId] ASC),
    CONSTRAINT [FK_PEShiftCalendar_PECrews] FOREIGN KEY ([FKCrewId]) REFERENCES [dbo].[Crews] ([CrewId]),
    CONSTRAINT [FK_PEShiftCalendar_PEShiftDefinitions] FOREIGN KEY ([FKShiftDefinitionId]) REFERENCES [dbo].[ShiftDefinitions] ([ShiftDefinitionId]),
    CONSTRAINT [FK_ShiftCalendar_DaysOfYear] FOREIGN KEY ([FKDaysOfYear]) REFERENCES [dbo].[DaysOfYear] ([DaysOfYearId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedStartTime]
    ON [dbo].[ShiftCalendar]([PlannedStartTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedEndTime]
    ON [dbo].[ShiftCalendar]([PlannedEndTime] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ShiftDefinitionId]
    ON [dbo].[ShiftCalendar]([FKShiftDefinitionId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DaysOfYearId]
    ON [dbo].[ShiftCalendar]([FKDaysOfYear] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_CrewId]
    ON [dbo].[ShiftCalendar]([FKCrewId] ASC);

