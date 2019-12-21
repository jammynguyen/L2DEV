CREATE PROCEDURE [dbo].[SPRawMaterialClear] @IRawMaterialId BIGINT   = NULL, 
                                           @IStatus        SMALLINT = NULL, 
                                           @IToDate        DATE     = NULL, 
                                           @IDayOffset     SMALLINT = NULL
AS
    BEGIN
        -- Check if param exist:
        DECLARE @DayOffset SMALLINT;
        SELECT @DayOffset = ValueInt
        FROM [smf].[Parameters]
        WHERE [Name] = 'DaysToClearData';
        IF @IDayOffset > 0
            BEGIN
                IF @IToDate IS NULL
                    SET @IToDate = GETDATE() - @IDayOffset;
        END;
            ELSE
            BEGIN
                IF @IToDate IS NULL
                    SET @IToDate = GETDATE() - ISNULL(@DayOffset, 5 * 365);
        END;
        IF OBJECT_ID('tempdb.dbo.#ControlTable1', 'U') IS NOT NULL
            DROP TABLE #ControlTable1;
        CREATE TABLE #ControlTable1(RawMaterialId BIGINT);
        IF @IRawMaterialId > 0
            BEGIN
                INSERT INTO #ControlTable1
                       SELECT RawMaterialId
                       FROM [dbo].[MVHRawMaterials]
                       WHERE RawMaterialId = @IRawMaterialId;
        END;
            ELSE
            IF @IStatus > 0
                BEGIN
                    INSERT INTO #ControlTable1
                           SELECT RawMaterialId
                           FROM [dbo].[MVHRawMaterials]
                           WHERE [Status] = @IStatus;
            END;
                ELSE
                BEGIN
                    INSERT INTO #ControlTable1
                           SELECT RawMaterialId
                           FROM [dbo].[MVHRawMaterials]
                           WHERE CreatedTs <= @IToDate;
            END;
        INSERT INTO #ControlTable1
               SELECT RawMaterialId
               FROM [dbo].[MVHRawMaterials]
               WHERE ParentRawMaterialId IN
               (
                   SELECT RawMaterialId
                   FROM #ControlTable1
               );
        DELETE FROM [dbo].[DLSDelays]
        WHERE FKRawMaterialId IN
        (
            SELECT RawMaterialId
            FROM #ControlTable1
        );
        DELETE FROM [dbo].[MVHDefects]
        WHERE FKRawMaterialId IN
        (
            SELECT RawMaterialId
            FROM #ControlTable1
        );
        DELETE FROM [dbo].[MVHMeasurements]
        WHERE FKRawMaterialId IN
        (
            SELECT RawMaterialId
            FROM #ControlTable1
        );
        DELETE FROM [dbo].[MVHRawMaterials]
        WHERE RawMaterialId IN
        (
            SELECT RawMaterialId
            FROM #ControlTable1
        );
        DROP TABLE #ControlTable1;
    END;