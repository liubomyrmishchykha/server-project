IF OBJECT_ID('uspCountInstances','P') IS NOT NULL
	DROP PROC uspCountInstances;
GO

CREATE PROC uspCountInstances
    @RowsCount INT = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Instances
	SELECT @RowsCount AS RowsCount
END
GO

