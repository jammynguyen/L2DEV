CREATE TABLE [dbo].[MVHTriggersFeatures] (
    [TriggersFeatureId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKTriggerId]       BIGINT        NOT NULL,
    [FKFeatureId]       BIGINT        NOT NULL,
    [PassNo]            SMALLINT      CONSTRAINT [DF_MVHTriggersFeatures_PassNo] DEFAULT ((1)) NOT NULL,
    [OrderSeq]          SMALLINT      CONSTRAINT [DF_MVHTriggersFeatures_OrderSeq] DEFAULT ((1)) NOT NULL,
    [Relations]         NVARCHAR (50) NULL,
    CONSTRAINT [PK_MVHTriggersFeatures] PRIMARY KEY CLUSTERED ([TriggersFeatureId] ASC),
    CONSTRAINT [FK_MVHTriggersFeatures_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHTriggersFeatures_MVHTriggers] FOREIGN KEY ([FKTriggerId]) REFERENCES [dbo].[MVHTriggers] ([TriggerId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_TriggerId]
    ON [dbo].[MVHTriggersFeatures]([FKTriggerId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_FeatureId]
    ON [dbo].[MVHTriggersFeatures]([FKFeatureId] ASC);

