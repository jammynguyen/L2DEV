CREATE TABLE [dbo].[RLSRollSets] (
    [RollSetId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]          DATETIME       CONSTRAINT [DF_RollSets_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME       CONSTRAINT [DF_RollSets_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKUpperRollId]      BIGINT         NULL,
    [FKBottomRollId]     BIGINT         NULL,
    [FKThirdRollId]      BIGINT         NULL,
    [Status]             SMALLINT       NOT NULL,
    [RollSetType]        SMALLINT       NOT NULL,
    [Created]            DATETIME       NOT NULL,
    [RollSetName]        NVARCHAR (50)  NOT NULL,
    [RollSetDescription] NVARCHAR (100) NULL,
    [IsThirdRoll]        SMALLINT       NULL,
    CONSTRAINT [PK_RollSets] PRIMARY KEY CLUSTERED ([RollSetId] ASC),
    CONSTRAINT [FK_RollSets_RollsBottom] FOREIGN KEY ([FKBottomRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollSets_RollsThird] FOREIGN KEY ([FKThirdRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollSets_RollsUpper] FOREIGN KEY ([FKUpperRollId]) REFERENCES [dbo].[RLSRolls] ([RollId])
);

