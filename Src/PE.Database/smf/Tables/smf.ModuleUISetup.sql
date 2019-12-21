CREATE TABLE [smf].[ModuleUISetup] (
    [ModuleUISetupId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NameResourceKey] NVARCHAR (50)  NOT NULL,
    [DisplayName]     NVARCHAR (100) NOT NULL,
    [Controller]      NVARCHAR (100) NOT NULL,
    [Method]          NVARCHAR (100) NOT NULL,
    [ModuleId]        BIGINT         NOT NULL,
    [DisplayOrder]    INT            NOT NULL,
    [MethodParameter] NVARCHAR (100) NULL,
    [IconName]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_SMFModuleUISetupId] PRIMARY KEY CLUSTERED ([ModuleUISetupId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [UQ_SMFModuleUISetupId_NameResourceKey] UNIQUE NONCLUSTERED ([NameResourceKey] ASC)
);



