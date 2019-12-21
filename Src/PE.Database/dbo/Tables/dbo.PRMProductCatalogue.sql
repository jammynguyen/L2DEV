CREATE TABLE [dbo].[PRMProductCatalogue] (
    [ProductCatalogueId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]                DATETIME       CONSTRAINT [DF_ProductCatalogue_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]             DATETIME       CONSTRAINT [DF_ProductCatalogue_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_ProductCatalogue_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]                BIT            CONSTRAINT [DF_ProductCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [ProductCatalogueName]     NVARCHAR (50)  NOT NULL,
    [FKProductCatalogueTypeId] BIGINT         NOT NULL,
    [FKShapeId]                BIGINT         NOT NULL,
    [FKSteelgradeId]           BIGINT         NOT NULL,
    [Description]              NVARCHAR (100) NULL,
    [SAPNumber]                NVARCHAR (20)  NULL,
    [Length]                   FLOAT (53)     NULL,
    [LengthMin]                FLOAT (53)     NULL,
    [LengthMax]                FLOAT (53)     NULL,
    [Thickness]                FLOAT (53)     NULL,
    [ThicknessMin]             FLOAT (53)     NULL,
    [ThicknessMax]             FLOAT (53)     NULL,
    [Width]                    FLOAT (53)     NULL,
    [WidthMin]                 FLOAT (53)     NULL,
    [WidthMax]                 FLOAT (53)     NULL,
    [Weight]                   FLOAT (53)     CONSTRAINT [DF_ProductCatalogue_TargetProductWeight] DEFAULT ((0)) NULL,
    [WeightMin]                FLOAT (53)     NULL,
    [WeightMax]                FLOAT (53)     NULL,
    [Slitting]                 BIT            CONSTRAINT [DF_PRMProductCatalogue_Slitting] DEFAULT ((0)) NOT NULL,
    [ExitSpeed]                FLOAT (53)     NOT NULL,
    [StdProductivity]          FLOAT (53)     NOT NULL,
    [StdMetallicYield]         FLOAT (53)     NOT NULL,
    [StdQualityYield]          FLOAT (53)     NOT NULL,
    [StdUtilizationTime]       FLOAT (53)     NOT NULL,
    [StdGapTime]               FLOAT (53)     NOT NULL,
    [StdRollingTime]           FLOAT (53)     NOT NULL,
    [StdProductionTime]        FLOAT (53)     NULL,
    [MaxTensile]               FLOAT (53)     NULL,
    [MaxYieldPoint]            FLOAT (53)     NULL,
    [Ovality]                  FLOAT (53)     NULL,
    [MaxOvality]               FLOAT (53)     NULL,
    [ProfileToleranceMin]      FLOAT (53)     NULL,
    [ProfileToleranceMax]      FLOAT (53)     NULL,
    CONSTRAINT [PK_ProductCatalogueId] PRIMARY KEY CLUSTERED ([ProductCatalogueId] ASC),
    CONSTRAINT [FK_PRMProductCatalogue_PRMShapes] FOREIGN KEY ([FKShapeId]) REFERENCES [dbo].[PRMShapes] ([ShapeId]),
    CONSTRAINT [FK_PRMProductCatalogue_PRMSteelgrades] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]),
    CONSTRAINT [FK_ProductCatalogue_ProductTypeId] FOREIGN KEY ([FKProductCatalogueTypeId]) REFERENCES [dbo].[PRMProductCatalogueTypes] ([ProductCatalogueTypeId]),
    CONSTRAINT [UQ_ProductCatalogueName] UNIQUE NONCLUSTERED ([ProductCatalogueName] ASC)
);












GO
CREATE NONCLUSTERED INDEX [NCI_SteelgradeIdRef]
    ON [dbo].[PRMProductCatalogue]([FKSteelgradeId] ASC);




GO
CREATE NONCLUSTERED INDEX [NCI_ShapeId]
    ON [dbo].[PRMProductCatalogue]([FKShapeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ProductCatalogueTypeId]
    ON [dbo].[PRMProductCatalogue]([FKProductCatalogueTypeId] ASC);

