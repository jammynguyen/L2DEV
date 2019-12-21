CREATE TABLE [smf].[Users] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Hometown]             NVARCHAR (MAX) NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [Discriminator]        NVARCHAR (128) NULL,
    [FirstName]            NVARCHAR (50)  NULL,
    [LastName]             NVARCHAR (50)  NULL,
    [JobPosition]          NVARCHAR (100) NULL,
    [LeaderId]             BIGINT         NULL,
    [HMIViewOrientation]   SMALLINT       NULL,
    [LanguageId]           BIGINT         NULL,
    [ReportUser]           SMALLINT       NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SMFUsers_SMFLanguages] FOREIGN KEY ([LanguageId]) REFERENCES [smf].[Languages] ([LanguageId])
);



