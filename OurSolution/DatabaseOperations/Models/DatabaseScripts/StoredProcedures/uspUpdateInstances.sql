IF OBJECT_ID('uspUpdateInstances','P') IS NOT NULL
	DROP PROC uspUpdateInstances;
GO
--Procedure updates specific instance
CREATE PROC uspUpdateInstances
	@Id INT = NULL,
	@HostName VARCHAR(64),
	@InstanceName VARCHAR(50) = NULL,
	@Status INT,
	@Version VARCHAR(64) = NULL,
	@Added DATETIME = NULL,
	@Modified DATETIME = NULL,
	@RAM INT = NULL,
	@CPUCount INT = NULL,
	@UserId INT = NULL
AS
BEGIN
	UPDATE Instances SET
		HostName = @HostName,
		InstanceName = @InstanceName,
		Status = @Status,
		Version = @Version,
		Modified = @Modified,
		RAM = @RAM,
		CPUCount = @CPUCount,
		UserId = @UserId
	WHERE Id = @Id
END
GO

