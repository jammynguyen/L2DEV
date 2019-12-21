CREATE TABLE [dbo].[PRMMaterialCatalogue] (
    [MaterialCatalogueId]       BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]                 DATETIME       CONSTRAINT [DF_RawMaterialCatalogue_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]              DATETIME       CONSTRAINT [DF_RawMaterialCatalogue_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsActive]                  BIT            CONSTRAINT [DF_RawMaterialCatalogue_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]                 BIT            CONSTRAINT [DF_RawMaterialCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [MaterialCatalogueName]     NVARCHAR (50)  NOT NULL,
    [FKMaterialCatalogueTypeId] BIGINT         NOT NULL,
    [FKShapeId]                 BIGINT         NOT NULL,
    [FKSteelgradeId]            BIGINT         NOT NULL,
    [Description]               NVARCHAR (100) NULL,
    [SAPNumber]                 NVARCHAR (20)  NULL,
    [Length]                    FLOAT (53)     NULL,
    [LengthMin]                 FLOAT (53)     NULL,
    [LengthMax]                 FLOAT (53)     NULL,
    [Thickness]                 FLOAT (53)     NOT NULL,
    [ThicknessMin]              FLOAT (53)     NULL,
    [ThicknessMax]              FLOAT (53)     NULL,
    [Width]                     FLOAT (53)     NOT NULL,
    [WidthMin]                  FLOAT (53)     NULL,
    [WidthMax]                  FLOAT (53)     NULL,
    [Weight]                    FLOAT (53)     NOT NULL,
    [WeightMin]                 FLOAT (53)     NULL,
    [WeightMax]                 FLOAT (53)     NULL,
    CONSTRAINT [PK_RawMaterialCatalogueId] PRIMARY KEY CLUSTERED ([MaterialCatalogueId] ASC),
    CONSTRAINT [FK_PRMMaterialCatalogue_PRMMaterialCatalogueTypes] FOREIGN KEY ([FKMaterialCatalogueTypeId]) REFERENCES [dbo].[PRMMaterialCatalogueTypes] ([MaterialCatalogueTypeId]),
    CONSTRAINT [FK_PRMMaterialCatalogue_PRMShapes] FOREIGN KEY ([FKShapeId]) REFERENCES [dbo].[PRMShapes] ([ShapeId]),
    CONSTRAINT [FK_RawMaterialCatalogue_SteelgradeId] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]),
    CONSTRAINT [UQ_RawMaterialCatalogue_Name] UNIQUE NONCLUSTERED ([MaterialCatalogueName] ASC)
);












GO
CREATE NONCLUSTERED INDEX [NCI_SteelgradeIdRef]
    ON [dbo].[PRMMaterialCatalogue]([FKSteelgradeId] ASC);




GO
CREATE NONCLUSTERED INDEX [NCI_ShapeId]
    ON [dbo].[PRMMaterialCatalogue]([FKShapeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialCatalogueTypeId]
    ON [dbo].[PRMMaterialCatalogue]([FKMaterialCatalogueTypeId] ASC);

