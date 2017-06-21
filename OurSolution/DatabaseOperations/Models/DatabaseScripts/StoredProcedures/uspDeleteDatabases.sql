IF OBJECT_ID('uspDeleteDatabases', 'P') IS NOT NULL
	DROP PROC uspDeleteDatabases
GO
CREATE PROC uspDeleteDatabases
	@Id INT
AS 
	BEGIN
		DELETE FROM Databases WHERE Databases.Id = @Id
	END
GO

