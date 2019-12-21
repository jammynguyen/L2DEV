CREATE TABLE [dbo].[PRMSteelgrades] (
    [SteelgradeId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]              DATETIME       CONSTRAINT [DF_Steelgrades_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]           DATETIME       CONSTRAINT [DF_Steelgrades_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsDefault]              BIT            CONSTRAINT [DF_Steelgrades_IsDefault] DEFAULT ((0)) NOT NULL,
    [SteelgradeCode]         NVARCHAR (10)  NOT NULL,
    [SteelgradeName]         NVARCHAR (50)  NULL,
    [SteelgradeDescription]  NVARCHAR (100) NULL,
    [Density]                FLOAT (53)     NULL,
    [OvenRecipeTemperature]  FLOAT (53)     NULL,
    [QualitySpecification]   NVARCHAR (50)  NULL,
    [CommercialGroup]        NVARCHAR (50)  NULL,
    [CustomerUseCode]        NVARCHAR (10)  NULL,
    [CustomerUseDescription] NVARCHAR (100) NULL,
    [FKSteelGroupId]         BIGINT         NULL,
    [ParentSteelgradeId]     BIGINT         NULL,
    CONSTRAINT [PK_SteelgradeId] PRIMARY KEY CLUSTERED ([SteelgradeId] ASC),
    CONSTRAINT [FK_Steelgrades_Steelgrades] FOREIGN KEY ([ParentSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]),
    CONSTRAINT [FK_Steelgrades_SteelGroupId] FOREIGN KEY ([FKSteelGroupId]) REFERENCES [dbo].[PRMSteelGroups] ([SteelGroupId]),
    CONSTRAINT [UQ_SteelgradeCode] UNIQUE NONCLUSTERED ([SteelgradeCode] ASC)
);






GO
CREATE NONCLUSTERED INDEX [NCI_SteelGroupId]
    ON [dbo].[PRMSteelgrades]([FKSteelGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentSteelgradeId]
    ON [dbo].[PRMSteelgrades]([ParentSteelgradeId] ASC);

