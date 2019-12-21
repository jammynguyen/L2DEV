CREATE TABLE [dbo].[DLSDelays] (
    [DelayId]            BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]          DATETIME       CONSTRAINT [DF_Delays_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME       CONSTRAINT [DF_Delays_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKDelayCatalogueId] BIGINT         NOT NULL,
    [FKRawMaterialId]    BIGINT         NULL,
    [FKUserId]           NVARCHAR (128) NULL,
    [DelayStart]         DATETIME       NOT NULL,
    [DelayEnd]           DATETIME       NULL,
    [UserComment]        NVARCHAR (200) NULL,
    [IsPlanned]          BIT            CONSTRAINT [DF_Delays_IsPlanned] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Delays] PRIMARY KEY CLUSTERED ([DelayId] ASC),
    CONSTRAINT [FK_PEDelays_PEDelaysCatalogue] FOREIGN KEY ([FKDelayCatalogueId]) REFERENCES [dbo].[DLSDelayCatalogue] ([DelayCatalogueId]),
    CONSTRAINT [FK_PEDelays_PERawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId]),
    CONSTRAINT [FK_PEDelays_SMFUsers] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);








GO
CREATE NONCLUSTERED INDEX [NCI_UserId]
    ON [dbo].[DLSDelays]([FKUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[DLSDelays]([FKRawMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DelayCatalogueId]
    ON [dbo].[DLSDelays]([FKDelayCatalogueId] ASC)
    INCLUDE([CreatedTs], [FKRawMaterialId], [FKUserId], [DelayStart], [DelayEnd], [UserComment], [IsPlanned]);




GO
CREATE NONCLUSTERED INDEX [NCI_DelayStart]
    ON [dbo].[DLSDelays]([DelayStart] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DelayEnd]
    ON [dbo].[DLSDelays]([DelayEnd] ASC);

