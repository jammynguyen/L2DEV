CREATE TABLE [smf].[DefaultLanguageUOM] (
    [DefaultLanguageUOMId] BIGINT IDENTITY (1, 1) NOT NULL,
    [FkLanguageId]         BIGINT NOT NULL,
    [FkUnitCategoryId]     BIGINT NOT NULL,
    [FkUnitId]             BIGINT NOT NULL,
    CONSTRAINT [PK_DefaultLanguageUOM] PRIMARY KEY CLUSTERED ([DefaultLanguageUOMId] ASC),
    CONSTRAINT [FK_DefaultLanguageUOM_Languages] FOREIGN KEY ([FkLanguageId]) REFERENCES [smf].[Languages] ([LanguageId]),
    CONSTRAINT [FK_DefaultLanguageUOM_UnitOfMeasure] FOREIGN KEY ([FkUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_DefaultLanguageUOM_UnitOfMeasureCategory] FOREIGN KEY ([FkUnitCategoryId]) REFERENCES [smf].[UnitOfMeasureCategory] ([UnitCategoryId])
);




GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [smf].[DefaultLanguageUOM]([FkUnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_UnitCategoryId]
    ON [smf].[DefaultLanguageUOM]([FkUnitCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_LanguageId]
    ON [smf].[DefaultLanguageUOM]([FkLanguageId] ASC);

