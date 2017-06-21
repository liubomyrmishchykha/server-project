IF OBJECT_ID('uspDeleteTables', 'P') IS NOT NULL
	DROP PROC uspDeleteTables
GO
CREATE PROC uspDeleteTables
	@Id INT 
AS 
	BEGIN
		DELETE FROM Tables WHERE Id = @Id
	END
GO

