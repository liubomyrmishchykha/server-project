IF OBJECT_ID('uspExistsBriefDatabase', 'P') IS NOT NULL 
	DROP PROC uspExistsBriefDatabase
GO
CREATE PROC uspExistsBriefDatabase
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@CreateTime DATETIME = NULL, 
	@TotalSize INT = NULL,
	@DbState INT = NULL,
	@InstanceId INT
AS 
	BEGIN
		SELECT Id FROM Databases 
		WHERE Name LIKE @Name AND InstanceId = @InstanceId
	END
GO

