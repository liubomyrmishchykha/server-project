IF OBJECT_ID('uspAddTables', 'P') IS NOT NULL
	DROP PROC uspAddTables
GO
CREATE PROC uspAddTables
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@ColumnCount INT,
	@DatabaseId INT
AS
	BEGIN
		INSERT INTO Tables(Name, ColumnCount, DatabaseId)
		VALUES(@Name, @ColumnCount, @DatabaseId)
	END
GO


