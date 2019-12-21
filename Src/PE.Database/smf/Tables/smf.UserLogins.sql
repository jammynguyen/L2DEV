CREATE TABLE [smf].[UserLogins] (
    [LoginProvider]   NVARCHAR (128) NOT NULL,
    [ProviderKey]     NVARCHAR (128) NOT NULL,
    [UserId]          NVARCHAR (128) NOT NULL,
    [IdentityUser_Id] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [smf].[Users] ([Id]) ON DELETE CASCADE
);

