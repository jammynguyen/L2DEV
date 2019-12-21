CREATE TABLE [dbo].[PRMSteelgradesEXT] (
    [FKSteelgradeId] BIGINT   NOT NULL,
    [CreatedTs]      DATETIME CONSTRAINT [DF_PRMSteelgradesEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMSteelgradesEXT] PRIMARY KEY CLUSTERED ([FKSteelgradeId] ASC),
    CONSTRAINT [FK_PRMSteelgradesEXT_PRMSteelgrades] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]) ON DELETE CASCADE
);

