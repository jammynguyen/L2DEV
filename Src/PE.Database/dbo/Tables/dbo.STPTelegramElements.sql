CREATE TABLE [dbo].[STPTelegramElements] (
    [TelegramElementId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDataTypeId]       BIGINT         NOT NULL,
    [FKUnitId]           BIGINT         NULL,
    [ByteLength]         SMALLINT       NULL,
    [ElementCode]        NVARCHAR (20)  NOT NULL,
    [ElementName]        NVARCHAR (50)  NULL,
    [ElementDescription] NVARCHAR (100) NULL,
    [IsActive]           BIT            CONSTRAINT [DF_TelegramStructures_IsActive] DEFAULT ((1)) NOT NULL,
    [IsVisible]          BIT            CONSTRAINT [DF_TelegramStructures_IsVisible] DEFAULT ((1)) NOT NULL,
    [IsStructure]        BIT            CONSTRAINT [DF_TelegramElements_IsStructure] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TelegramStructures] PRIMARY KEY CLUSTERED ([TelegramElementId] ASC),
    CONSTRAINT [FK_TelegramElements_UnitOfMeasure] FOREIGN KEY ([FKUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_TelegramStructures_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DataTypes] ([DataTypeId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ElementCode]
    ON [dbo].[STPTelegramElements]([ElementCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [dbo].[STPTelegramElements]([FKUnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DataTypeId]
    ON [dbo].[STPTelegramElements]([FKDataTypeId] ASC);

