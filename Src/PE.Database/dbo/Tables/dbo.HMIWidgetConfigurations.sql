CREATE TABLE [dbo].[HMIWidgetConfigurations] (
    [WidgetConfigurationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [WidgetName]            NVARCHAR (50)  NOT NULL,
    [IsActive]              BIT            CONSTRAINT [DF_WidgetConfigurations_IsActive] DEFAULT ((1)) NOT NULL,
    [OrderSeq]              SMALLINT       NULL,
    [WidgetFileName]        NVARCHAR (200) NULL,
    [FKUserId]              NVARCHAR (128) NULL,
    CONSTRAINT [PK_PEWidgetConfigurations] PRIMARY KEY CLUSTERED ([WidgetConfigurationId] ASC),
    CONSTRAINT [FK_PEWidgetConfigurations_SMFUsers] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WidgetName]
    ON [dbo].[HMIWidgetConfigurations]([WidgetName] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderSeq]
    ON [dbo].[HMIWidgetConfigurations]([OrderSeq] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_UserId]
    ON [dbo].[HMIWidgetConfigurations]([FKUserId] ASC);

