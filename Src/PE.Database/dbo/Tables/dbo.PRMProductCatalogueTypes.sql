CREATE TABLE [dbo].[PRMProductCatalogueTypes] (
    [ProductCatalogueTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductCatalogueTypeSymbol]      NCHAR (10)     NOT NULL,
    [ProductCatalogueTypeName]        NVARCHAR (100) NOT NULL,
    [ProductCatalogueTypeDescription] NVARCHAR (255) NULL,
    [IsDefault]                       BIT            CONSTRAINT [DF_ProductCatalogueTypes_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductCatalogueTypeId] PRIMARY KEY CLUSTERED ([ProductCatalogueTypeId] ASC),
    CONSTRAINT [UQ_ProductCatalogueTypesName] UNIQUE NONCLUSTERED ([ProductCatalogueTypeName] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductCatalogueTypeSymbol]
    ON [dbo].[PRMProductCatalogueTypes]([ProductCatalogueTypeSymbol] ASC);

