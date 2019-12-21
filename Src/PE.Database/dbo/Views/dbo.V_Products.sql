/****** Script for SelectTopNRows command from SSMS  ******/
CREATE   VIEW V_Products AS
SELECT P.[ProductId]
      ,P.[CreatedTs]
      ,P.[LastUpdateTs]
      ,P.[IsDummy]
      ,P.[ProductName]
      ,P.[FKWorkOrderId]
      ,P.[IsAssigned]
      ,P.[Weight]
	  ,WO.WorkOrderName
	  ,GS.ShiftCalendarId
	  ,GS.ShiftKey
  FROM [dbo].[PRMProducts] P
  LEFT JOIN PRMWorkOrders WO ON P.FKWorkOrderId = WO.WorkOrderId
  OUTER APPLY dbo.FNTGetShiftId(P.[CreatedTs]) GS