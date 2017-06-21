--Procedure which returns all instances with related user infos for data collection
IF OBJECT_ID('uspGetAllInstanceInfo') IS NOT NULL
	DROP PROC uspGetAllInstanceInfo
GO
CREATE PROC uspGetAllInstanceInfo
AS
	BEGIN
		SELECT 	 
			  Instances.Id
			 ,Instances.HostName
			 ,Instances.InstanceName
			 ,Instances.UserId
			 ,Users.Name as UserName
			 ,Users.Password
						FROM Instances LEFT JOIN Users On (Instances.UserId = Users.Id)
						ORDER BY Instances.Id ASC
	END
GO

