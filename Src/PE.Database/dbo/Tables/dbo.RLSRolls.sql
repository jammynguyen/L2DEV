CREATE TABLE [dbo].[RLSRolls] (
    [RollId]             BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]          DATETIME       CONSTRAINT [DF_Rolls_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME       CONSTRAINT [DF_Rolls_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKRollTypeId]       BIGINT         NOT NULL,
    [RollName]           NVARCHAR (50)  NOT NULL,
    [RollDescription]    NVARCHAR (100) NULL,
    [Status]             SMALLINT       CONSTRAINT [DF_RLSRolls_Status] DEFAULT ((0)) NOT NULL,
    [InitialDiameter]    FLOAT (53)     NOT NULL,
    [ActualDiameter]     FLOAT (53)     NOT NULL,
    [MinimumDiameter]    FLOAT (53)     NULL,
    [Location]           SMALLINT       NULL,
    [GroovesNumber]      SMALLINT       CONSTRAINT [DF_Rolls_GroovesNumber] DEFAULT ((0)) NOT NULL,
    [Supplier]           NVARCHAR (50)  NULL,
    [ScrapReason]        SMALLINT       NULL,
    [ScrapDate]          DATETIME       NULL,
    [RollType]           SMALLINT       NULL,
    [DiameterOfMaterial] FLOAT (53)     NULL,
    [DiameterOfTool]     FLOAT (53)     NULL,
    CONSTRAINT [PK_Rolls] PRIMARY KEY CLUSTERED ([RollId] ASC),
    CONSTRAINT [FK_Rolls_RollTypes] FOREIGN KEY ([FKRollTypeId]) REFERENCES [dbo].[RLSRollTypes] ([RollTypeId]),
    CONSTRAINT [UQ_RollName] UNIQUE NONCLUSTERED ([RollName] ASC)
);

