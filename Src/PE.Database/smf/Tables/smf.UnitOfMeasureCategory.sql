CREATE TABLE [smf].[UnitOfMeasureCategory] (
    [UnitCategoryId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryName]   NVARCHAR (255) NULL,
    [SIUnitId]       BIGINT         NOT NULL,
    [Description]    NVARCHAR (200) NULL,
    CONSTRAINT [PK_UnitCategoryId] PRIMARY KEY CLUSTERED ([UnitCategoryId] ASC),
    CONSTRAINT [FK_SMFUnitOfMeasureCategory_SMFUnitOfMeasure] FOREIGN KEY ([SIUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_SIUnitId]
    ON [smf].[UnitOfMeasureCategory]([SIUnitId] ASC);

