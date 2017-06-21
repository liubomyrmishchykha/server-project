IF OBJECT_ID('uspUpdateBriefInstance','P') IS NOT NULL
	DROP PROC uspUpdateBriefInstance;
GO
--Procedure updates specific instance to offline status
CREATE PROC uspUpdateBriefInstance
	@Id INT,
	@Status INT,
	@Modified DATETIME,
	@UserID INT = NULL
AS
BEGIN
	UPDATE Instances SET
		Status = @Status,
		Modified = @Modified,
		UserID = @UserID
	WHERE Id = @Id
END
GO

