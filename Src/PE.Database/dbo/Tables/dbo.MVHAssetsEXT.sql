CREATE TABLE [dbo].[MVHAssetsEXT] (
    [FKAssetId] BIGINT   NOT NULL,
    [CreatedTs] DATETIME CONSTRAINT [DF_AssetsEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [MaxPassNo] SMALLINT CONSTRAINT [DF_MVHAssetsEXT_MaxPassNo] DEFAULT ((1)) NOT NULL,
    [TimeIn]    SMALLINT NULL,
    [IsInitial] BIT      CONSTRAINT [DF_MVHAssetsEXT_IsInitial] DEFAULT ((0)) NULL,
    [IsLast]    BIT      CONSTRAINT [DF_MVHAssetsEXT_IsLast] DEFAULT ((0)) NULL,
    [EnumArea]  SMALLINT NULL,
    CONSTRAINT [PK_AssetsEXT] PRIMARY KEY CLUSTERED ([FKAssetId] ASC),
    CONSTRAINT [FK_AssetsEXT_Assets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]) ON DELETE CASCADE
);





