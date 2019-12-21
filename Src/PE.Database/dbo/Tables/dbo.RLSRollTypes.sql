CREATE TABLE [dbo].[RLSRollTypes] (
    [RollTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]           DATETIME       CONSTRAINT [DF_RollTypes_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]        DATETIME       CONSTRAINT [DF_RollTypes_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [RollTypeName]        NVARCHAR (50)  NOT NULL,
    [RollTypeDescription] NVARCHAR (100) NULL,
    [DiameterMin]         FLOAT (53)     NULL,
    [DiameterMax]         FLOAT (53)     NULL,
    [RoughnessMin]        FLOAT (53)     NULL,
    [RoughnessMax]        FLOAT (53)     NULL,
    [YieldStrengthRef]    FLOAT (53)     NULL,
    [SteelgradeRoll]      NVARCHAR (30)  NULL,
    [Length]              FLOAT (53)     NULL,
    [DrawingName]         NVARCHAR (50)  NULL,
    [ChokeType]           NVARCHAR (20)  NULL,
    [AccBilletCntLimit]   BIGINT         NULL,
    [AccWeightLimit]      FLOAT (53)     NULL,
    [MatchingRollsetType] SMALLINT       NULL,
    CONSTRAINT [PK_RollTypes] PRIMARY KEY CLUSTERED ([RollTypeId] ASC),
    CONSTRAINT [UQ_RollType_Name] UNIQUE NONCLUSTERED ([RollTypeName] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'refers to PE.Core.Constants.RollSetType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSRollTypes', @level2type = N'COLUMN', @level2name = N'MatchingRollsetType';

