IF OBJECT_ID('uspUpdateUsers', 'P') IS NOT NULL
	DROP PROC uspUpdateUsers
GO
CREATE PROC uspUpdateUsers
	@Id INT = NULL,
    @Name VARCHAR(255),
	@Password VARCHAR(64)
AS
	BEGIN
		UPDATE Users 
		SET	Name = @Name,
			Password = @Password
		WHERE 
			Id = @Id
	END
GO

