

CREATE VIEW [xfr].[V_TelegramStructuresImport]
AS
     SELECT ROW_NUMBER() OVER(
            ORDER BY EX.ParentElementCode, 
                     EX.SeqNo) AS Sort, 
            TE.TelegramElementId AS FKTelegramElementId, 
            TEP.TelegramElementId AS FKParentTelegramElementId, 
            EX.SeqNo AS SeqNo, 
            EX.ElementCode AS StructureCode, 
            EX.ElementCode AS StructureName, 
            EX.StructureDescription AS StructureDescription, 
            EX.StructureSource AS StructureSource, 
            EX.Prefix AS Prefix, 
            EX.Sufix AS Sufix, 
            TS.TelegramStructureId, 
            CAST(CASE
                     WHEN TS.TelegramStructureId IS NULL
                     THEN 1
                     ELSE 0
                 END AS BIT) IsNew
     FROM xfr.TelegramStructuresTT EX
	 --[ExcelImportTable]...[TelegramStructures$] EX
          LEFT JOIN STPTelegramElements TE ON EX.ElementCode COLLATE Latin1_General_CI_AS = TE.ElementCode
          LEFT JOIN STPTelegramElements TEP ON EX.ParentElementCode COLLATE Latin1_General_CI_AS = TEP.ElementCode
          LEFT JOIN STPTelegramStructures TS ON CONCAT(CAST(TEP.TelegramElementId AS NVARCHAR(10)), '.', CAST(TE.TelegramElementId AS NVARCHAR(10)), '.', CAST(EX.SeqNo AS NVARCHAR(10))) = CONCAT(CAST(TS.FKParentTelegramElementId AS NVARCHAR(10)), '.', CAST(TS.FKTelegramElementId AS NVARCHAR(10)), '.', CAST(TS.SeqNo AS NVARCHAR(10)));