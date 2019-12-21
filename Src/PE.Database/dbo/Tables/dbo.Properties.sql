CREATE TABLE [dbo].[Properties] (
    [PropertyId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [PropertyCode]        NVARCHAR (10)  NOT NULL,
    [PropertyName]        NVARCHAR (50)  NOT NULL,
    [PropertyDescription] NVARCHAR (100) NULL,
    [FKDataTypeId]        BIGINT         NOT NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
    CONSTRAINT [FK_Properties_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DataTypes] ([DataTypeId])
);




GO
CREATE NONCLUSTERED INDEX [NCI_DataTypeId]
    ON [dbo].[Properties]([FKDataTypeId] ASC);

