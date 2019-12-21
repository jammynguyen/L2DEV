CREATE TABLE [smf].[Roles] (
    [Id]            NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (256) NOT NULL,
    [Discriminator] NVARCHAR (128) NULL,
    [Description]   NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.SMFRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

