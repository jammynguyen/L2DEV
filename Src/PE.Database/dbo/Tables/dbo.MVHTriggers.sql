CREATE TABLE [dbo].[MVHTriggers] (
    [TriggerId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [TriggerCode]        NVARCHAR (10)  NOT NULL,
    [TriggerName]        NVARCHAR (50)  NOT NULL,
    [TriggerDescription] NVARCHAR (100) NULL,
    [IsActive]           BIT            CONSTRAINT [DF_MVHTriggers_IsActive] DEFAULT ((1)) NOT NULL,
    [EnumTriggerType]    SMALLINT       NOT NULL,
    CONSTRAINT [PK_MVHTriggers] PRIMARY KEY CLUSTERED ([TriggerId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [NCI_TriggerType]
    ON [dbo].[MVHTriggers]([EnumTriggerType] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_TriggerCode]
    ON [dbo].[MVHTriggers]([TriggerCode] ASC);

