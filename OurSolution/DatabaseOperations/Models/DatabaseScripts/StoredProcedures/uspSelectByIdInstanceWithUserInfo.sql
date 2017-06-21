IF OBJECT_ID('uspSelectByIdInstanceWithUserInfo','P') IS NOT NULL
	DROP PROC uspSelectByIdInstanceWithUserInfo;
GO
-- Updates the specific instance
CREATE PROC uspSelectByIdInstanceWithUserInfo
	@Id INT
AS
BEGIN
	SELECT	Instances.Id
			,Instances.HostName
			,Instances.InstanceName
			,Instances.Status
			,Instances.Version
			,Instances.Added
			,Instances.Modified
			,Instances.RAM
			,Instances.CPUCount
			,Instances.UserId
			,Users.Name
			,Users.Password
	FROM Instances LEFT outer JOIN Users On (Instances.UserId = Users.Id)
	WHERE Instances.Id=@Id
END	
GO

