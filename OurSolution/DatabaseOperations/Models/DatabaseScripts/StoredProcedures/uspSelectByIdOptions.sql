IF OBJECT_ID('uspSelectByIdOptions', 'P') IS NOT NULL 
	DROP PROC uspSelectByIdOptions;
GO
CREATE PROC uspSelectByIdOptions
	@Id INT
AS
	BEGIN 
		SELECT Id, Interval
		FROM Options
		WHERE Id = @Id
	END
GO

