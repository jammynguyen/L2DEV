CREATE TABLE [smf].[Languages] (
    [LanguageId]   BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]    DATETIME      CONSTRAINT [DF_SMFLanguages_CreatedTs_1] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs] DATETIME      CONSTRAINT [DF_SMFLanguages_LastUpdateTs_1] DEFAULT (getdate()) NOT NULL,
    [LanguageCode] NVARCHAR (10) NOT NULL,
    [IconName]     NVARCHAR (50) NULL,
    [LanguageName] NVARCHAR (50) NULL,
    [Active]       BIT           CONSTRAINT [DF_Languages_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]    BIT           CONSTRAINT [DF_Languages_IsDefault] DEFAULT ((0)) NOT NULL,
    [Order]        INT           CONSTRAINT [DF_Languages_Order] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SMFLanguages] PRIMARY KEY CLUSTERED ([LanguageId] ASC),
    CONSTRAINT [UQ_SMFLanguageCode] UNIQUE NONCLUSTERED ([LanguageCode] ASC)
);



