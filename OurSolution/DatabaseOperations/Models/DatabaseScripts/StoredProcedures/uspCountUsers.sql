IF OBJECT_ID('uspCountUsers','P') IS NOT NULL
	DROP PROC uspCountUsers;
GO

CREATE PROC uspCountUsers
	@RowsCount INT = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Users
	SELECT @RowsCount as RowsCount
END
GO

