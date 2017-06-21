IF OBJECT_ID('uspGetAllInstances', 'P') IS NOT NULL
	DROP PROCEDURE uspGetAllInstances;
GO
--Procedure gets all instances from the instances table
CREATE PROC uspGetAllInstances
AS
	BEGIN
		SELECT Id, HostName, InstanceName, Status, Version, Added, Modified, RAM, CPUCount, UserId 
		FROM Instances
	END
GO

