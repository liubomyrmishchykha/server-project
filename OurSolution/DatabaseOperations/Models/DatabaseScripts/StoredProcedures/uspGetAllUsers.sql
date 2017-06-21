IF OBJECT_ID('uspGetAllUsers','P') IS NOT NULL
	DROP PROC uspGetAllUsers;
GO
CREATE PROC uspGetAllUsers
AS
	BEGIN
		SELECT Id, Name, Password
		FROM Users
	END
GO

