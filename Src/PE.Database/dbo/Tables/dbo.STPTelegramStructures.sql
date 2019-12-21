CREATE TABLE [dbo].[STPTelegramStructures] (
    [TelegramStructureId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKTelegramElementId]       BIGINT         NOT NULL,
    [FKParentTelegramElementId] BIGINT         NULL,
    [SeqNo]                     SMALLINT       CONSTRAINT [DF_TelegramLines_SeqNo] DEFAULT ((0)) NOT NULL,
    [StructureCode]             NVARCHAR (20)  NOT NULL,
    [StructureName]             NVARCHAR (50)  NULL,
    [StructureDescription]      NVARCHAR (100) NULL,
    [StructureSource]           NVARCHAR (50)  NULL,
    [Prefix]                    NVARCHAR (20)  NULL,
    [Sufix]                     NVARCHAR (20)  NULL,
    [IsActive]                  BIT            CONSTRAINT [DF_TelegramLines_IsActive] DEFAULT ((1)) NOT NULL,
    [IsVisible]                 BIT            CONSTRAINT [DF_TelegramLines_IsVisible] DEFAULT ((1)) NOT NULL,
    [IsRoot]                    AS             (CONVERT([bit],case when [FKParentTelegramElementId] IS NULL then (1) else (0) end)),
    [IsHeader]                  BIT            CONSTRAINT [DF_STPTelegramStructures_IsHeader] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TelegramLines] PRIMARY KEY CLUSTERED ([TelegramStructureId] ASC),
    CONSTRAINT [FK_STPTelegramStructures_STPTelegramElements] FOREIGN KEY ([FKParentTelegramElementId]) REFERENCES [dbo].[STPTelegramElements] ([TelegramElementId]),
    CONSTRAINT [FK_TelegramLines_TelegramStructures] FOREIGN KEY ([FKTelegramElementId]) REFERENCES [dbo].[STPTelegramElements] ([TelegramElementId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_TelegramElementId]
    ON [dbo].[STPTelegramStructures]([FKTelegramElementId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentElementId]
    ON [dbo].[STPTelegramStructures]([FKParentTelegramElementId] ASC);

