IF OBJECT_ID('uspExistsInstances','P') IS NOT NULL
	DROP PROC uspExistsInstances;
GO
--Checks whether there is an instance with this name in a database
CREATE PROC uspExistsInstances
	@Id INT = NULL,
	@HostName VARCHAR(64),
	@InstanceName VARCHAR(64) = NULL,
	@Status INT = NULL,
	@Version VARCHAR(64) = NULL,
	@Added DATETIME = NULL,
	@Modified DATETIME = NULL,
	@RAM INT = NULL,
	@CPUCount INT = NULL,
	@UserId INT = NULL
AS
BEGIN
	SELECT Id
	FROM Instances
	WHERE (HostName LIKE @HostName)
END
GO

