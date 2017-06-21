IF OBJECT_ID('uspCountInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspCountInstanceWithUserInfo
GO

CREATE PROC uspCountInstanceWithUserInfo
	@RowsCount int = 0
AS 
BEGIN
	With QueryResult AS (SELECT Instances.Id
						FROM Instances LEFT OUTER JOIN Users On (Instances.UserId = Users.Id))
						SELECT @RowsCount = Count(*) FROM QueryResult
						SELECT @RowsCount AS RowsCount
END
GO

