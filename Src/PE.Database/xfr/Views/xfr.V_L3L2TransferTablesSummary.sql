CREATE VIEW [xfr].[V_L3L2TransferTablesSummary]
AS
     SELECT ISNULL(ROW_NUMBER() OVER(
            ORDER BY TransferTableName), 0) AS Sorting, 
            TransferTableName, 
            StatusNew, 
            StatusInProc, 
            StatusOK, 
            StatusValErr, 
            StatusProcErr
     FROM
     (
         SELECT 'L3L2WorkOrderDefinition' AS TransferTableName, 
                SUM(CASE
                        WHEN CommStatus = 0
                        THEN 1
                        ELSE 0
                    END) AS StatusNew, 
                SUM(CASE
                        WHEN CommStatus = 1
                        THEN 1
                        ELSE 0
                    END) AS StatusInProc, 
                SUM(CASE
                        WHEN CommStatus = 2
                        THEN 1
                        ELSE 0
                    END) AS StatusOK, 
                SUM(CASE
                        WHEN CommStatus = -1
                        THEN 1
                        ELSE 0
                    END) AS StatusValErr, 
                SUM(CASE
                        WHEN CommStatus = -2
                        THEN 1
                        ELSE 0
                    END) AS StatusProcErr
         FROM xfr.L3L2WorkOrderDefinition AS L3WOD
     ) AS QRY;