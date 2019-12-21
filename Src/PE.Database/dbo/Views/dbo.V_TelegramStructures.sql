CREATE   VIEW [dbo].[V_TelegramStructures]
AS WITH TREE
        AS (SELECT TS.FKTelegramElementId ElementId, 
                   CAST(NULL AS BIGINT) ParentElementId, 
                   CAST(TS.StructureCode AS VARCHAR(100)) StructureGraph, 
                   TS.StructureCode StructureCode, 
                   1 StructureLevel, 
                   TS.SeqNo, 
                   CAST(TS.StructureCode AS VARCHAR(100)) OrderSeq, 
                   CAST(TS.TelegramStructureId AS VARCHAR(4000)) TelegramStructureIndex, 
                   CAST(TS.StructureCode AS VARCHAR(100)) StructurePath, 
                   TS.TelegramStructureId, 
                   TS.IsRoot, 
                   TS.IsHeader, 
                   TS.TelegramStructureId AS RootId
            FROM STPTelegramStructures AS TS
            WHERE TS.FKParentTelegramElementId IS NULL
            UNION ALL
            SELECT TS.FKTelegramElementId ElementId, 
                   TREE.ElementId ParentElementId, 
                   CAST(REPLICATE('|---', TREE.StructureLevel) + TS.StructureCode AS VARCHAR(100)) StructureGraph, 
                   TS.StructureCode StructureCode, 
                   TREE.StructureLevel + 1 StructureLevel, 
                   TS.SeqNo, 
                   CAST(TREE.OrderSeq + '\' + RIGHT('0000000000' + CAST(ROW_NUMBER() OVER(
                                                    ORDER BY TS.SeqNo) AS VARCHAR(10)), 10) AS VARCHAR(100)) OrderSeq, 
                   CAST(TREE.TelegramStructureIndex + '.' + CAST(TS.TelegramStructureId AS VARCHAR(100)) AS VARCHAR(4000)) TelegramStructureIndex, 
                   CAST(TREE.StructurePath + '\' + TS.StructureCode AS VARCHAR(100)) StructurePath, 
                   TS.TelegramStructureId, 
                   TS.IsRoot, 
                   TS.IsHeader, 
                   TREE.RootId
            FROM TREE
                 INNER JOIN STPTelegramStructures AS TS ON TS.FKParentTelegramElementId = TREE.ElementId)
        SELECT ISNULL(ROW_NUMBER() OVER(
               ORDER BY OrderSeq), 0) Sort, 
               ElementId, 
               ParentElementId, 
               TE.ElementCode, 
               DT.DataTypeId, 
               DT.DataType, 
               TelegramStructureId, 
               TelegramStructureIndex, 
               RootId, 
               CAST(CASE
                        WHEN StructureLevel = 1
                        THEN 1
                        ELSE 0
                    END AS BIT) IsRoot, 
               IsStructure, 
               IsHeader, 
               StructureGraph, 
               StructurePath, 
               StructureCode, 
               StructureLevel, 
               OrderSeq
        FROM TREE
             INNER JOIN STPTelegramElements TE ON TREE.ElementId = TE.TelegramElementId
             INNER JOIN DataTypes DT ON TE.FKDataTypeId = DT.DataTypeId;