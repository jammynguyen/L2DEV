CREATE TABLE [smf].[AlarmCategories] (
    [AlarmCategoryId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [Description]     NVARCHAR (150) NULL,
    [DisplayFlag]     SMALLINT       CONSTRAINT [DF_AlarmCategories_DisplayFlag] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AlarmCategoryId] PRIMARY KEY CLUSTERED ([AlarmCategoryId] ASC)
);

