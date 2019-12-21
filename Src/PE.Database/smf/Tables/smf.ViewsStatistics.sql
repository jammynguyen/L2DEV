CREATE TABLE [smf].[ViewsStatistics] (
    [Name]          VARCHAR (100) NULL,
    [Time]          INT           NULL,
    [Records]       INT           NULL,
    [TimePerRecord] FLOAT (53)    NULL,
    [UsedInViews]   INT           NULL,
    [ViewsOwned]    INT           NULL,
    [Created]       DATE          NULL,
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_SMFViewsStatistics] PRIMARY KEY CLUSTERED ([Id] ASC)
);

