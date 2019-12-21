CREATE TABLE [dbo].[STPTelegramValues] (
    [TelegramValueId]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKTelegramId]             BIGINT        NOT NULL,
    [FKTelegramStructureIndex] VARCHAR (100) NOT NULL,
    [Value]                    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TelegramLineValues] PRIMARY KEY CLUSTERED ([TelegramValueId] ASC),
    CONSTRAINT [FK_TelegramValues_Telegrams] FOREIGN KEY ([FKTelegramId]) REFERENCES [dbo].[STPTelegrams] ([TelegramId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_TelegramStructureIndex]
    ON [dbo].[STPTelegramValues]([FKTelegramStructureIndex] ASC)
    INCLUDE([FKTelegramId], [Value]);


GO
CREATE NONCLUSTERED INDEX [NCI_TelStructureIndex]
    ON [dbo].[STPTelegramValues]([FKTelegramStructureIndex] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_TelegramId]
    ON [dbo].[STPTelegramValues]([FKTelegramId] ASC);

