IF OBJECT_ID('uspAddUsers','P') IS NOT NULL
	DROP PROC uspAddUsers;
GO
CREATE PROC uspAddUsers
	@Id INT = NULL,
    @Name VARCHAR(255),
	@Password VARCHAR(64)
AS
	BEGIN
		INSERT INTO Users
				(Name
				,Password)
		VALUES
				(@Name
				,@Password)
	END
GO

