CREATE TABLE [smf].[EnumValues] (
    [EnumValueId]  BIGINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FkEnumNameId] BIGINT       NOT NULL,
    [Keyword]      VARCHAR (50) NOT NULL,
    [Value]        BIGINT       NOT NULL,
    CONSTRAINT [PK_EnumValues] PRIMARY KEY CLUSTERED ([EnumValueId] ASC),
    CONSTRAINT [Fk_EnumValues_EnumNames] FOREIGN KEY ([FkEnumNameId]) REFERENCES [smf].[EnumNames] ([EnumNameId])
);








GO
CREATE NONCLUSTERED INDEX [NCI_EnumNameId]
    ON [smf].[EnumValues]([FkEnumNameId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EnumNameId_Value]
    ON [smf].[EnumValues]([FkEnumNameId] ASC, [Value] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EnumNameId_Keyword]
    ON [smf].[EnumValues]([FkEnumNameId] ASC, [Keyword] ASC);

