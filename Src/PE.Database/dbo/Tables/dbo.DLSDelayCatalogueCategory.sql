CREATE TABLE [dbo].[DLSDelayCatalogueCategory] (
    [DelayCatalogueCategoryId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [DelayCatalogueCategoryCode]        NVARCHAR (10)  NOT NULL,
    [DelayCatalogueCategoryName]        NVARCHAR (50)  NOT NULL,
    [DelayCatalogueCategoryDescription] NVARCHAR (100) NULL,
    [IsDefault]                         BIT            CONSTRAINT [DF_DelayCatalogueCategory_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DelayCatalogueCategory] PRIMARY KEY CLUSTERED ([DelayCatalogueCategoryId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DelayCatalogueCategoryCode]
    ON [dbo].[DLSDelayCatalogueCategory]([DelayCatalogueCategoryCode] ASC);

