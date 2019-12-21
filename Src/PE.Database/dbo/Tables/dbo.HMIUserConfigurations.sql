CREATE TABLE [dbo].[HMIUserConfigurations] (
    [ConfigurationId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [FKUserId]             NVARCHAR (128)  NULL,
    [UserName]             NVARCHAR (256)  NULL,
    [ConfigurationName]    NVARCHAR (50)   NULL,
    [ConfigurationContent] NVARCHAR (4000) NULL,
    [ConfigurationType]    NVARCHAR (50)   NULL,
    CONSTRAINT [PK_UserComparisonSchemes] PRIMARY KEY CLUSTERED ([ConfigurationId] ASC),
    CONSTRAINT [FK_HMIUserConfigurations_Users] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [NCI_UserId]
    ON [dbo].[HMIUserConfigurations]([FKUserId] ASC);

