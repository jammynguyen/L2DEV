CREATE TABLE [dbo].[PRMReheatingGroup] (
    [ReheatingGroupId]    BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]           DATETIME      CONSTRAINT [DF_ReheatingGroup_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]        DATETIME      CONSTRAINT [DF_ReheatingGroup_UpdateTs] DEFAULT (getdate()) NOT NULL,
    [ReheatingGroupCode]  NVARCHAR (10) NOT NULL,
    [ReheatingGroupName]  NVARCHAR (50) NOT NULL,
    [UsedTime]            FLOAT (53)    NULL,
    [IsDefault]           BIT           CONSTRAINT [DF_PRMReheatingGroup_IsDefault] DEFAULT ((0)) NULL,
    [TotalHeatingTime]    FLOAT (53)    CONSTRAINT [DF_ReheatingGroup_TotalHeatingTime] DEFAULT ((0)) NULL,
    [MaxHeatingTime]      FLOAT (53)    NULL,
    [Zone1HeatingTime]    FLOAT (53)    NULL,
    [Zone2HeatingTime]    FLOAT (53)    NULL,
    [Zone3HeatingTime]    FLOAT (53)    NULL,
    [Zone4HeatingTime]    FLOAT (53)    NULL,
    [Zone5HeatingTime]    FLOAT (53)    NULL,
    [Zone6HeatingTime]    FLOAT (53)    NULL,
    [Zone7HeatingTime]    FLOAT (53)    NULL,
    [Zone1HeatingTempMin] FLOAT (53)    NULL,
    [Zone2HeatingTempMin] FLOAT (53)    NULL,
    [Zone3HeatingTempMin] FLOAT (53)    NULL,
    [Zone4HeatingTempMin] FLOAT (53)    NULL,
    [Zone5HeatingTempMin] FLOAT (53)    NULL,
    [Zone6HeatingTempMin] FLOAT (53)    NULL,
    [Zone7HeatingTempMin] FLOAT (53)    NULL,
    [Zone1HeatingTempMax] FLOAT (53)    NULL,
    [Zone2HeatingTempMax] FLOAT (53)    NULL,
    [Zone3HeatingTempMax] FLOAT (53)    NULL,
    [Zone4HeatingTempMax] FLOAT (53)    NULL,
    [Zone5HeatingTempMax] FLOAT (53)    NULL,
    [Zone6HeatingTempMax] FLOAT (53)    NULL,
    [Zone7HeatingTempMax] FLOAT (53)    NULL,
    CONSTRAINT [PK_ReheatingGroup] PRIMARY KEY CLUSTERED ([ReheatingGroupId] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ReheatingGroupCode]
    ON [dbo].[PRMReheatingGroup]([ReheatingGroupCode] ASC);

