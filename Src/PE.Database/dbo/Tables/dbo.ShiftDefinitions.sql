CREATE TABLE [dbo].[ShiftDefinitions] (
    [ShiftDefinitionId]      BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]              DATETIME      CONSTRAINT [DF_ShiftDefinitions_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]           DATETIME      CONSTRAINT [DF_ShiftDefinitions_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [ShiftCode]              NVARCHAR (10) NOT NULL,
    [DefaultStartTime]       TIME (7)      NOT NULL,
    [DefaultEndTime]         TIME (7)      NOT NULL,
    [ShiftEndsNextDay]       BIT           NOT NULL,
    [ShiftStartsPreviousDay] BIT           NOT NULL,
    [NextShiftDefinitionId]  BIGINT        NOT NULL,
    CONSTRAINT [PK_ShiftDefinitions] PRIMARY KEY CLUSTERED ([ShiftDefinitionId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ShiftCode]
    ON [dbo].[ShiftDefinitions]([ShiftCode] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_NextShiftDefinitionId]
    ON [dbo].[ShiftDefinitions]([NextShiftDefinitionId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefaultStartTime]
    ON [dbo].[ShiftDefinitions]([DefaultStartTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefaultEndTime]
    ON [dbo].[ShiftDefinitions]([DefaultEndTime] ASC);

