IF OBJECT_ID('uspCountDatabases', 'P') IS NOT NULL
	DROP PROC uspCountDatabases
GO
CREATE PROC uspCountDatabases
	@RowsCount INT = 0
AS
	BEGIN
		WITH QueryResult AS(SELECT Id FROM Databases) 
		SELECT @RowsCount = Count(*) FROM QueryResult
		SELECT @RowsCount AS RowsCount
	END
GO

