CREATE TABLE [dbo].[PRMCustomers] (
    [CustomerId]      BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CustomerName]    NVARCHAR (100) NOT NULL,
    [CustomerAddress] NVARCHAR (200) NULL,
    [Email]           NVARCHAR (150) NULL,
    [Phone]           NVARCHAR (20)  NULL,
    [DocPatternName]  NVARCHAR (50)  NULL,
    [Country]         NVARCHAR (50)  NULL,
    [LogoName]        NVARCHAR (20)  NULL,
    [SAPKey]          NVARCHAR (20)  NULL,
    [IsActive]        BIT            CONSTRAINT [DF_PRMCustomers_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]       BIT            CONSTRAINT [DF_PRMCustomers_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CustomerName]
    ON [dbo].[PRMCustomers]([CustomerName] ASC);

