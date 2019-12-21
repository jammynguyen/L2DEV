CREATE TABLE [smf].[Module] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [ModuleName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SMFModule] PRIMARY KEY CLUSTERED ([Id] ASC)
);

