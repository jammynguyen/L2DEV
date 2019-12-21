CREATE TABLE [dbo].[STPTelegrams] (
    [TelegramId]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKTelegramStructureId] BIGINT        NOT NULL,
    [TelegramCode]          NVARCHAR (12) NOT NULL,
    [TelegramName]          NVARCHAR (50) NULL,
    [TelegramDescription]   NCHAR (100)   NULL,
    [Port]                  SMALLINT      NULL,
    [TcpIp]                 NVARCHAR (50) NULL,
    [LastSent]              DATETIME      NULL,
    [Id]                    SMALLINT      NULL,
    CONSTRAINT [PK_TelegramHeaders] PRIMARY KEY CLUSTERED ([TelegramId] ASC),
    CONSTRAINT [FK_Telegrams_TelegramStructures] FOREIGN KEY ([FKTelegramStructureId]) REFERENCES [dbo].[STPTelegramStructures] ([TelegramStructureId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TelegramCode]
    ON [dbo].[STPTelegrams]([TelegramCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_TelegramStructureId]
    ON [dbo].[STPTelegrams]([FKTelegramStructureId] ASC);

