CREATE TABLE [smf].[HmiClientMenu] (
    [HmiClientMenuId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [DisplayName]     NVARCHAR (100) NOT NULL,
    [Controller]      NVARCHAR (100) NULL,
    [Method]          NVARCHAR (100) NULL,
    [ParentId]        BIGINT         NULL,
    [DisplayOrder]    INT            NULL,
    [MethodParameter] NVARCHAR (100) NULL,
    [IconName]        NVARCHAR (100) NULL,
    [Active]          BIT            NULL,
    [FKModuleId]      BIGINT         NULL,
    CONSTRAINT [PK_SMFHmiClientMenu] PRIMARY KEY CLUSTERED ([HmiClientMenuId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_SMFHmiClientMenu_ParentId] FOREIGN KEY ([FKModuleId]) REFERENCES [smf].[Module] ([Id]),
    CONSTRAINT [UQ_SMFHmiClientMenu_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);




GO
CREATE NONCLUSTERED INDEX [NCI_ParentId]
    ON [smf].[HmiClientMenu]([ParentId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ModuleId]
    ON [smf].[HmiClientMenu]([FKModuleId] ASC);

