CREATE TABLE [dbo].[EventTypes] (
    [EventTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventTypeCode]        NVARCHAR (10)  NOT NULL,
    [EventTypeName]        NVARCHAR (50)  NOT NULL,
    [EventTypeDescription] NVARCHAR (100) NULL,
    [HMIIcon]              NVARCHAR (50)  NULL,
    [HMIColor]             NVARCHAR (100) NULL,
    [HMILink]              NVARCHAR (255) NULL,
    [IsEditable]           BIT            CONSTRAINT [DF_EventTypes_IsEditable] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED ([EventTypeId] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EventTypeCode]
    ON [dbo].[EventTypes]([EventTypeCode] ASC);

