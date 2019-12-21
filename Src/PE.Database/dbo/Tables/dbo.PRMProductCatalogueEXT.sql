CREATE TABLE [dbo].[PRMProductCatalogueEXT] (
    [FKProductCatalogueId] BIGINT   NOT NULL,
    [CreatedTs]            DATETIME CONSTRAINT [DF_PRMProductCatalogueEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMProductCatalogueEXT] PRIMARY KEY CLUSTERED ([FKProductCatalogueId] ASC),
    CONSTRAINT [FK_PRMProductCatalogueEXT_PRMProductCatalogue] FOREIGN KEY ([FKProductCatalogueId]) REFERENCES [dbo].[PRMProductCatalogue] ([ProductCatalogueId]) ON DELETE CASCADE
);

