CREATE TABLE [dbo].[PRMMaterialCatalogueEXT] (
    [FKMaterialCatalogueId] BIGINT   NOT NULL,
    [CreatedTs]             DATETIME CONSTRAINT [DF_PRMMaterialCatalogueEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMMaterialCatalogueEXT] PRIMARY KEY CLUSTERED ([FKMaterialCatalogueId] ASC),
    CONSTRAINT [FK_PRMMaterialCatalogueEXT_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId]) ON DELETE CASCADE
);

