IF OBJECT_ID('uspDeleteUsers','P') IS NOT NULL
	DROP PROCEDURE uspDeleteUsers;
GO
-- Deletes specific user from database
CREATE PROCEDURE uspDeleteUsers
	@Id INT
AS 
	BEGIN
		DELETE FROM Users WHERE Id = @Id
	END
GO

