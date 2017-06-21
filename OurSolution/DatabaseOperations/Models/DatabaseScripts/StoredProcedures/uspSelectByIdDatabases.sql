IF OBJECT_ID('uspSelectByIdDatabases', 'P') IS NOT NULL
	DROP PROC uspSelectByIdDatabases
GO
CREATE PROC uspSelectByIdDatabases
	@Id INT 
AS 
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases 
		WHERE Id = @Id
	END
GO

