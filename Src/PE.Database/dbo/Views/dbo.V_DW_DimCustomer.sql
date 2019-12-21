CREATE   VIEW [dbo].[V_DW_DimCustomer]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          CustomerId AS DimCustomerKey, 
          CustomerName AS CustomerName, 
          CustomerAddress, 
          Email AS CustomerEmail, 
          Phone AS CustomerPhone
   FROM dbo.PRMCustomers AS C;