CREATE TABLE [dbo].[PRMHeats] (
    [HeatId]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedTs]             DATETIME      CONSTRAINT [DF_Heats_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]          DATETIME      CONSTRAINT [DF_Heats_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [HeatName]              NVARCHAR (50) NOT NULL,
    [FKMaterialCatalogueId] BIGINT        NOT NULL,
    [FKHeatSupplierId]      BIGINT        NULL,
    [Created]               DATETIME      NULL,
    [HeatWeightRef]         FLOAT (53)    NULL,
    [IsDummy]               BIT           CONSTRAINT [DF_Heats_IsDummyHeat] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Heats] PRIMARY KEY CLUSTERED ([HeatId] ASC),
    CONSTRAINT [FK_PEHeats_PEHeatSuppliers] FOREIGN KEY ([FKHeatSupplierId]) REFERENCES [dbo].[PRMHeatSuppliers] ([HeatSupplierId]),
    CONSTRAINT [FK_PRMHeats_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId]),
    CONSTRAINT [UQ_UniqueHeatName] UNIQUE NONCLUSTERED ([HeatName] ASC)
);














GO



GO
CREATE NONCLUSTERED INDEX [NCI_HeatSupplierId]
    ON [dbo].[PRMHeats]([FKHeatSupplierId] ASC);

