IF OBJECT_ID('uspSelectByIdUsers', 'P') IS NOT NULL 
	DROP PROC uspSelectByIdUsers;
GO
CREATE PROC uspSelectByIdUsers
	@Id INT
AS
	BEGIN 
		SELECT Id, Name, Password
		FROM Users
		WHERE Id = @Id
	END
GO

