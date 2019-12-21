CREATE TABLE [dbo].[RLSRollSetHistory] (
    [RollSetHistoryId]   BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]          DATETIME   CONSTRAINT [DF_RollSetHistory_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME   CONSTRAINT [DF_RollSetHistory_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Created]            DATETIME   NULL,
    [FKRollSetId]        BIGINT     NOT NULL,
    [FKCassetteId]       BIGINT     NULL,
    [Mounted]            DATETIME   NULL,
    [Dismounted]         DATETIME   NULL,
    [Status]             SMALLINT   CONSTRAINT [DF_RLSRollSetHistory_Status] DEFAULT ((0)) NOT NULL,
    [PositionInCassette] SMALLINT   NULL,
    [AccWeightLimit]     FLOAT (53) NULL,
    CONSTRAINT [PK_RollSetHistory] PRIMARY KEY CLUSTERED ([RollSetHistoryId] ASC),
    CONSTRAINT [FK_RollSetHistory_Cassettes] FOREIGN KEY ([FKCassetteId]) REFERENCES [dbo].[RLSCassettes] ([CassetteId]),
    CONSTRAINT [FK_RollSetHistory_RollSets] FOREIGN KEY ([FKRollSetId]) REFERENCES [dbo].[RLSRollSets] ([RollSetId])
);



