IF OBJECT_ID('uspUpdateTables', 'P') IS NOT NULL
	DROP PROC uspUpdateTables
GO
CREATE PROC uspUpdateTables
	@Id INT, 
	@Name VARCHAR(64),
	@ColumnCount INT,
	@DatabaseId INT
AS 
	BEGIN
		UPDATE Tables 
		SET Name = @Name, 
			ColumnCount = @ColumnCount,
			DatabaseId = @DatabaseId
		WHERE Id = @Id 			
	END
GO

