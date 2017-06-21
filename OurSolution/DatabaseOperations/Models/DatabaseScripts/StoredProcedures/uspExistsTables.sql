IF OBJECT_ID('uspExistsTables', 'P') IS NOT NULL
	DROP PROC uspExistsTables
GO
CREATE PROC uspExistsTables 
	@Id INT,
	@Name VARCHAR(64),
	@ColumnCount INT = NULL,
	@DatabaseId INT	
AS
	BEGIN
		SELECT Id FROM Tables
		WHERE DatabaseId = @DatabaseId AND Name = @Name
	END
GO

