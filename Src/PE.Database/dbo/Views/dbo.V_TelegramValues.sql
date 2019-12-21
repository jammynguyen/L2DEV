CREATE   VIEW [dbo].[V_TelegramValues]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY TS.Sort), 0) AS FakeIndex, 
          TS.TelegramStructureIndex, 
          TS.ElementId, 
          TS.ParentElementId, 
          TS.ElementCode, 
          TS.DataType, 
          TS.DataTypeId, 
          TS.TelegramStructureId, 
          TS.RootId, 
          TS.IsRoot, 
          TS.IsStructure, 
          TS.IsHeader, 
          TS.StructureGraph, 
          TS.StructurePath, 
          TS.StructureCode, 
          TS.StructureLevel, 
          TS.OrderSeq, 
          TS.Sort, 
          TV.[Value], 
          T.TelegramId, 
          T.TelegramCode, 
          T.Id, 
          T.[Port], 
          T.TcpIp
   FROM dbo.V_TelegramStructures AS TS
        LEFT OUTER JOIN dbo.STPTelegramValues AS TV ON TS.TelegramStructureIndex = TV.FKTelegramStructureIndex
        LEFT OUTER JOIN dbo.STPTelegrams AS T ON TS.RootId = T.FKTelegramStructureId
                                                 AND TV.FKTelegramId = T.TelegramId;