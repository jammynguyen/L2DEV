CREATE TABLE [dbo].[PRMMaterialCatalogueTypes] (
    [MaterialCatalogueTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaterialCatalogueTypeSymbol]      NCHAR (10)     NOT NULL,
    [MaterialCatalogueTypeName]        NVARCHAR (100) NOT NULL,
    [MaterialCatalogueTypeDescription] NVARCHAR (255) NULL,
    [IsDefault]                        BIT            CONSTRAINT [DF_MaterialCatalogueTypes_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MaterialCatalogueTypeId] PRIMARY KEY CLUSTERED ([MaterialCatalogueTypeId] ASC),
    CONSTRAINT [UQ_MaterialCatalogueTypeName] UNIQUE NONCLUSTERED ([MaterialCatalogueTypeName] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaterialCatalogueTypeSymbol]
    ON [dbo].[PRMMaterialCatalogueTypes]([MaterialCatalogueTypeSymbol] ASC);

