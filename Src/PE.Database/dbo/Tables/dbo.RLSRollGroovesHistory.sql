CREATE TABLE [dbo].[RLSRollGroovesHistory] (
    [RollGroovesHistoryId] BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]            DATETIME   CONSTRAINT [DF_RollGrooves_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]         DATETIME   CONSTRAINT [DF_RollGrooves_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKRollId]             BIGINT     NOT NULL,
    [FKGrooveTemplateId]   BIGINT     NOT NULL,
    [FKRollSetHistoryId]   BIGINT     NULL,
    [GrooveNumber]         SMALLINT   CONSTRAINT [DF_RollGrooves_GrooveNumber] DEFAULT ((0)) NOT NULL,
    [Status]               SMALLINT   CONSTRAINT [DF_RLSRollGroovesHistory_Status] DEFAULT ((0)) NOT NULL,
    [Created]              DATETIME   NULL,
    [Deactivated]          DATETIME   NULL,
    [AccWeight]            FLOAT (53) CONSTRAINT [DF_RollGroovesHistory_AccWeight] DEFAULT ((0)) NOT NULL,
    [AccBilletCnt]         BIGINT     CONSTRAINT [DF_RollGroovesHistory_AccBilletCnt] DEFAULT ((0)) NOT NULL,
    [ActDiameter]          FLOAT (53) NULL,
    CONSTRAINT [PK_RollGroovesHistory] PRIMARY KEY CLUSTERED ([RollGroovesHistoryId] ASC),
    CONSTRAINT [FK_RollGrooves_GrooveTemplates] FOREIGN KEY ([FKGrooveTemplateId]) REFERENCES [dbo].[RLSGrooveTemplates] ([GrooveTemplateId]),
    CONSTRAINT [FK_RollGroovesHistory_Rolls] FOREIGN KEY ([FKRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollGroovesHistory_RollSetHistory] FOREIGN KEY ([FKRollSetHistoryId]) REFERENCES [dbo].[RLSRollSetHistory] ([RollSetHistoryId]) ON DELETE CASCADE
);



