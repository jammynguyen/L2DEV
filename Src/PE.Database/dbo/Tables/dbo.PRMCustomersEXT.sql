CREATE TABLE [dbo].[PRMCustomersEXT] (
    [FKCustomerId] BIGINT   NOT NULL,
    [CreatedTs]    DATETIME CONSTRAINT [DF_PRMCustomersEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMCustomersEXT] PRIMARY KEY CLUSTERED ([FKCustomerId] ASC),
    CONSTRAINT [FK_PRMCustomersEXT_PRMCustomers] FOREIGN KEY ([FKCustomerId]) REFERENCES [dbo].[PRMCustomers] ([CustomerId]) ON DELETE CASCADE
);

