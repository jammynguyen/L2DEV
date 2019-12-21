CREATE TABLE [xfr].[DelayCatalogueTT] (
    [DelayCode]                  NVARCHAR (255) NULL,
    [DelayName]                  NVARCHAR (255) NULL,
    [DelayDescription]           NVARCHAR (255) NULL,
    [IsActive]                   SMALLINT       NULL,
    [IsDefault]                  SMALLINT       NULL,
    [StdDelayTime]               FLOAT (53)     NULL,
    [ParentDelayCatalogueCode]   NVARCHAR (255) NULL,
    [DelayCatalogueCategoryCode] NVARCHAR (255) NULL,
    [DelayCatalogueCategoryName] NVARCHAR (255) NULL
);

