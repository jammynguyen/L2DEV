CREATE TABLE [dbo].[TestTable2] (
    [Id]             INT        NOT NULL,
    [Name]           NCHAR (10) NULL,
    [FKTestTable1Id] INT        NOT NULL,
    CONSTRAINT [PK_TestTable2] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestTable2_TestTable1] FOREIGN KEY ([FKTestTable1Id]) REFERENCES [dbo].[TestTable1] ([Id])
);

