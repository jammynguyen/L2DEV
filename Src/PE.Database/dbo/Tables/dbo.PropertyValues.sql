CREATE TABLE [dbo].[PropertyValues] (
    [PropertyValueId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKPropertyId]    BIGINT        NOT NULL,
    [Value]           NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PropertyValues] PRIMARY KEY CLUSTERED ([PropertyValueId] ASC),
    CONSTRAINT [FK_PropertyValues_Properties] FOREIGN KEY ([FKPropertyId]) REFERENCES [dbo].[Properties] ([PropertyId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [NCI_PropertyId]
    ON [dbo].[PropertyValues]([FKPropertyId] ASC);

