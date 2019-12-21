CREATE TABLE [smf].[EnumNames] (
    [EnumNameId] BIGINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EnumName]   VARCHAR (50) NOT NULL,
    [EnumType]   VARCHAR (10) NOT NULL,
    [IsSMF]      BIT          CONSTRAINT [DF_EnumNames_IsSMF] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_smf.EnumNames] PRIMARY KEY CLUSTERED ([EnumNameId] ASC),
    CONSTRAINT [ChkEnumType] CHECK ([EnumType]='long' OR [EnumType]='int' OR [EnumType]='short' OR [EnumType]='byte')
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EnumName]
    ON [smf].[EnumNames]([EnumName] ASC);

