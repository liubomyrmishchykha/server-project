IF OBJECT_ID('uspDeleteInstances','P') IS NOT NULL
	DROP PROC uspDeleteInstances;
GO
--Procedure deleted specific instance from Instance table;
CREATE PROC uspDeleteInstances
	@Id INT
AS
	BEGIN
		DELETE FROM Instances WHERE Id = @Id
	END
GO

