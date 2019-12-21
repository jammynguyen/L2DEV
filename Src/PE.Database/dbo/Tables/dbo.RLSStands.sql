CREATE TABLE [dbo].[RLSStands] (
    [StandId]       BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]     DATETIME      CONSTRAINT [DF_Stands_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]  DATETIME      CONSTRAINT [DF_Stands_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Status]        SMALLINT      CONSTRAINT [DF_RLSStands_Status] DEFAULT ((0)) NOT NULL,
    [StandNo]       SMALLINT      NOT NULL,
    [NumberOfRolls] SMALLINT      NULL,
    [StandZoneName] NVARCHAR (30) NULL,
    [IsOnLine]      SMALLINT      NULL,
    [IsCalibrated]  BIT           CONSTRAINT [DF_RLSStands_IsCalibrated] DEFAULT ((0)) NOT NULL,
    [Position]      SMALLINT      NULL,
    CONSTRAINT [PK_Stands] PRIMARY KEY CLUSTERED ([StandId] ASC)
);

