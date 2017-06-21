IF OBJECT_ID('uspSelectByInstanceIdDatabases', 'P') IS NOT NULL
	DROP PROC uspSelectByInstanceIdDatabases
GO
CREATE PROC uspSelectByInstanceIdDatabases
	@Id INT 
AS 
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases 
		WHERE InstanceId = @Id
	END
GO

