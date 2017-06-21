IF OBJECT_ID('uspAddBriefDatabase', 'P') IS NOT NULL 
	DROP PROC uspAddBriefDatabase
GO
CREATE PROC uspAddBriefDatabase
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@CreateTime DATETIME, 
	@TotalSize INT,
	@DbState INT,
	@InstanceId INT
AS 
	BEGIN
		INSERT INTO Databases(Name, CreateTime, TotalSize, DbState, InstanceId)
		VALUES(@Name, @CreateTime, @TotalSize, @DbState, @InstanceId)
	END
GO

