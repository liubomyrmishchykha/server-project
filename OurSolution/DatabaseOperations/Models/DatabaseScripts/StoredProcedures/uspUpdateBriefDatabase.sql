IF OBJECT_ID('uspUpdateBriefDatabase', 'P') IS NOT NULL
	DROP PROC uspUpdateBriefDatabase
GO
CREATE PROC uspUpdateBriefDatabase
	@Id INT,
	@Name VARCHAR(64), 
	@CreateTime DATETIME, 
	@TotalSize INT,
	@DbState INT,
	@InstanceId INT
AS
	BEGIN
		UPDATE Databases SET
		Name = @Name,
		CreateTime = @CreateTime, 
		TotalSize = @TotalSize,
		DbState = @DbState,
		InstanceId = @InstanceId
		WHERE Databases.Id = @Id
	END
GO

