
CREATE PROCEDURE [dbo].[SPMaterialGenealogy] (@RootId bigint = 112836, @Direction char(1) = NULL)
AS
BEGIN

DECLARE @UseView nvarchar(50)
DECLARE @SQLQry nvarchar(4000)

IF @Direction IS NULL 
SET @Direction = 
	(SELECT CASE WHEN ParentRawMaterialId IS NULL THEN 'D' ELSE 'U' END FROM [dbo].[MVHRawMaterials] WHERE [RawMaterialId]=@RootId)

IF @Direction='D' SET @UseView = N'V_MaterialGenealogyDown'
			 ELSE SET @UseView = N'V_MaterialGenealogyUp'

SET @SQLQry = '
SELECT 
	[Path], LevelNo, RootId, MaterialId, ParentId, ChildsNo, IsParent, ParentsCut, RootInitialLength,
	InitialLength, ActualLength, RawMaterialName, [Level] 
FROM ' + @UseView + '
WHERE 1=1 
AND RootId = ' + CAST(@RootId AS NVARCHAR) + '
ORDER BY [Path]
'
PRINT @SQLQry
EXECUTE(@SQLQry)
END