CREATE TABLE [dbo].[DLSDelayCatalogue] (
    [DelayCatalogueId]           BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]                  DATETIME       CONSTRAINT [DF_DelayCatalogue_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]               DATETIME       CONSTRAINT [DF_DelayCatalogue_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [DelayCatalogueCode]         NVARCHAR (10)  NOT NULL,
    [DelayCatalogueName]         NVARCHAR (50)  NOT NULL,
    [DelayCatalogueDescription]  NVARCHAR (100) NULL,
    [IsActive]                   BIT            CONSTRAINT [DF_DelayCatalogue_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]                  BIT            CONSTRAINT [DF_DelayCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [FKDelayCatalogueCategoryId] BIGINT         NULL,
    [ParentDelayCatalogueId]     BIGINT         NULL,
    [StdDelayTime]               FLOAT (53)     CONSTRAINT [DF_DelayCatalogue_StdDelayTime] DEFAULT ((0)) NOT NULL,
    [IsPlanned]                  SMALLINT       CONSTRAINT [DF_DLSDelayCatalogue_IsPlanned] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DelayCatalogue] PRIMARY KEY CLUSTERED ([DelayCatalogueId] ASC),
    CONSTRAINT [FK_DelayCatalogue] FOREIGN KEY ([ParentDelayCatalogueId]) REFERENCES [dbo].[DLSDelayCatalogue] ([DelayCatalogueId]),
    CONSTRAINT [FK_DelayCatalogue_DelayCatalogueCategory] FOREIGN KEY ([FKDelayCatalogueCategoryId]) REFERENCES [dbo].[DLSDelayCatalogueCategory] ([DelayCatalogueCategoryId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DelayCatalogueCode]
    ON [dbo].[DLSDelayCatalogue]([DelayCatalogueCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentDelayCatalogueId]
    ON [dbo].[DLSDelayCatalogue]([ParentDelayCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DelayCatalogueCategoryId]
    ON [dbo].[DLSDelayCatalogue]([FKDelayCatalogueCategoryId] ASC);

