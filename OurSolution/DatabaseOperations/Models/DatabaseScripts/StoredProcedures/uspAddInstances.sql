IF OBJECT_ID('uspAddInstances', 'P') IS NOT NULL
	DROP PROCEDURE uspAddInstances;
GO
--Procedure add new Instance to table Instances
CREATE PROC uspAddInstances
	@Id INT = NULL,
	@HostName VARCHAR(64),
	@InstanceName VARCHAR(50) = NULL,
	@Status INT,
	@Version VARCHAR(64) = NULL,
	@Added DATETIME,
	@Modified DATETIME = NULL,
	@RAM INT = NULL,
	@CPUCount INT = NULL,
	@UserId INT = NULL
AS
    BEGIN
	INSERT INTO Instances
			(HostName
			,InstanceName
			,Status
			,Version
			,Added
			,Modified
			,RAM
			,CPUCount
			,UserId)
	VALUES
			(@HostName
			,@InstanceName
			,@Status
			,@Version
			,@Added
			,@Modified
			,@RAM
			,@CPUCount
			,@UserId)
    END
GO

