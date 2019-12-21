CREATE TABLE [dbo].[MVHAssets] (
    [AssetId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderSeq]         SMALLINT       CONSTRAINT [DF_Assets_OrderSeq] DEFAULT ((0)) NOT NULL,
    [IsCheckpoint]     BIT            CONSTRAINT [DF_Assets_IsCheckpoint] DEFAULT ((0)) NOT NULL,
    [IsReversible]     BIT            CONSTRAINT [DF_Assets_IsReversible] DEFAULT ((0)) NOT NULL,
    [AssetCode]        NVARCHAR (10)  NOT NULL,
    [AssetName]        NVARCHAR (50)  NOT NULL,
    [AssetDescription] NVARCHAR (100) NULL,
    [ParentAssetId]    BIGINT         NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED ([AssetId] ASC) WITH (FILLFACTOR = 90, PAD_INDEX = ON),
    CONSTRAINT [sprawdz] CHECK ([OrderSeq]>(0) OR [OrderSeq]=NULL),
    CONSTRAINT [FK_Assets_ParentAsset] FOREIGN KEY ([ParentAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [UQ_AssetName] UNIQUE NONCLUSTERED ([AssetName] ASC) WITH (FILLFACTOR = 90, PAD_INDEX = ON)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderSeq]
    ON [dbo].[MVHAssets]([OrderSeq] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AssetCode]
    ON [dbo].[MVHAssets]([AssetCode] ASC);

