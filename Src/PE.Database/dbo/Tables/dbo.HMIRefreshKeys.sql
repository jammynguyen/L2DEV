CREATE TABLE [dbo].[HMIRefreshKeys] (
    [HmiRefreshKeyId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEventId]       BIGINT         NOT NULL,
    [HmiRefreshKey]   NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_HmiRefreshKeys] PRIMARY KEY CLUSTERED ([HmiRefreshKeyId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [NCI_EventId]
    ON [dbo].[HMIRefreshKeys]([FKEventId] ASC);

