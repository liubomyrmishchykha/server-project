IF OBJECT_ID('uspGetAllDatabases', 'P') IS NOT NULL
	DROP PROC uspGetAllDatabases
GO
CREATE PROC uspGetAllDatabases
AS
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases
	END
GO

