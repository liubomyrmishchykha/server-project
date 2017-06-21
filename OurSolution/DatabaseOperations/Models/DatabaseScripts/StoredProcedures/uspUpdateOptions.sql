IF OBJECT_ID('uspUpdateOptions', 'P') IS NOT NULL
	DROP PROC uspUpdateOptions
GO
CREATE PROC uspUpdateOptions
	@Id INT = NULL,
    @Interval INT
AS
	BEGIN
		UPDATE Options 
		SET	Interval = @Interval
		WHERE 
			Id = @Id
	END
GO

