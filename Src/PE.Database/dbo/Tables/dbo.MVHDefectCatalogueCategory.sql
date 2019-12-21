CREATE TABLE [dbo].[MVHDefectCatalogueCategory] (
    [DefectCatalogueCategoryId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [DefectCatalogueCategoryCode]        NVARCHAR (20)  NOT NULL,
    [DefectCatalogueCategoryName]        NVARCHAR (50)  NULL,
    [DefectCatalogueCategoryDescription] NVARCHAR (100) NULL,
    [IsDefault]                          BIT            CONSTRAINT [DF_DefectCatalogueCategory_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DefectCatalogueCategory] PRIMARY KEY CLUSTERED ([DefectCatalogueCategoryId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefectCatalogueCategoryCode]
    ON [dbo].[MVHDefectCatalogueCategory]([DefectCatalogueCategoryCode] ASC);

