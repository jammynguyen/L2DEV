CREATE PROCEDURE [dbo].[SPClearRawMaterials]
AS
    BEGIN
        DECLARE @LogValue NVARCHAR(50);
        BEGIN TRY
            --Create temporary table
            IF OBJECT_ID('tempdb.dbo.#ControlTable', 'U') IS NOT NULL
                DROP TABLE #ControlTable;
            CREATE TABLE #ControlTable(RawMaterialId BIGINT);
            INSERT INTO #ControlTable
                   SELECT RawMaterialId
                   FROM MVHRawMaterials
                   WHERE [Status] < 8
                         AND [IsDummy] = 1;
            INSERT INTO #ControlTable
                   SELECT RawMaterialId
                   FROM [dbo].[MVHRawMaterials]
                   WHERE ParentRawMaterialId IN
                   (
                       SELECT RawMaterialId
                       FROM #ControlTable
                   );
            DECLARE @RowCount BIGINT;
            SELECT @RowCount = ISNULL(COUNT(RawMaterialId), 0)
            FROM #ControlTable;
            SET @LogValue = 'Found: ' + CAST(@RowCount AS NVARCHAR(10)) + ' materials';
            --Update Materials
            UPDATE [dbo].[PRMMaterials]
              SET 
                  IsAssigned = 0
            WHERE MaterialId IN
            (
                SELECT FKMaterialId
                FROM MVHRawMaterials
                WHERE RawMaterialId IN
                (
                    SELECT RawMaterialId
                    FROM #ControlTable
                )
            );
            --Delete Delays, Defects, Measurements
            DELETE FROM [dbo].[DLSDelays]
            WHERE FKRawMaterialId IN
            (
                SELECT RawMaterialId
                FROM #ControlTable
            );
            DELETE FROM [dbo].[MVHDefects]
            WHERE FKRawMaterialId IN
            (
                SELECT RawMaterialId
                FROM #ControlTable
            );
            DELETE FROM [dbo].[MVHMeasurements]
            WHERE FKRawMaterialId IN
            (
                SELECT RawMaterialId
                FROM #ControlTable
            );
            --Delete RawMaterials
            DELETE FROM [dbo].[MVHRawMaterials]
            WHERE RawMaterialId IN
            (
                SELECT RawMaterialId
                FROM #ControlTable
            );
            --Drop temporary table
            DROP TABLE #ControlTable;
            EXEC SPLogError 
                 'SP', 
                 '[SPClearRawMaterials]', 
                 @LogValue;
        END TRY
        BEGIN CATCH
            EXEC SPLogError 
                 'SP', 
                 '[SPClearRawMaterials]', 
                 @LogValue;
        END CATCH;
    END;