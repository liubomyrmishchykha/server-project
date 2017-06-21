IF DB_ID('testDb') IS NULL
	CREATE DATABASE testDb;
GO

USE testDb
IF OBJECT_ID('Servers', 'U') IS NULL
	CREATE TABLE Servers
	(
		Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
		ServerName varchar(64) NOT NULL,
		Storage varchar(50) NOT NULL,
		OS varchar(64) NOT NULL,
		RAM varchar(64) NOT NULL,
		CPU varchar(64) NOT NULL,
		CountOfInstanses int NULL
	)
		
GO
	
IF OBJECT_ID('Users', 'U') IS NULL
CREATE TABLE Users
(
	Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name varchar(255) NOT NULL,
	Password varchar(64) NOT NULL,
	AuthentificationMode int NOT NULL
)
GO

IF OBJECT_ID('Instances', 'U') IS NULL
	CREATE TABLE Instances
	(
		Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
		HostName varchar(64) NOT NULL,
		InstanceName varchar(50) NOT NULL,
		Status int NOT NULL,
		Version varchar(64) NOT NULL,
		Added datetime NOT NULL,
		Modified datetime NULL,
		UserId int CONSTRAINT FK_Instances_Users FOREIGN KEY REFERENCES Users(Id) NULL,
		ServerId int CONSTRAINT FK_Instances_Servers FOREIGN KEY REFERENCES Servers(Id) NULL,
		CountOfDataBases int NULL
	)
GO
IF OBJECT_ID('DataBases', 'U') IS NULL
	CREATE TABLE DataBases
	(
		Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
		DBName varchar(64) NOT NULL,
		Count int NOT NULL,
		InstancesId  int CONSTRAINT FK_DataBases_Instance FOREIGN KEY REFERENCES Instances(Id) NULL
	)
GO
IF OBJECT_ID('Tables', 'U') IS NULL
	CREATE TABLE Tables
	(
		Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
		TableName varchar(64) NOT NULL,
		ColumNumber  int NOT NULL,
		DataBaseId  int CONSTRAINT FK_Tables_DataBases FOREIGN KEY REFERENCES DataBases(Id) NULL
	)
GO
IF OBJECT_ID('Jobs', 'U') IS NULL
	CREATE TABLE Jobs
	(
		Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
		JobName varchar(64) NOT NULL,
		InstancesId  int CONSTRAINT  FK_Job_Instanses  FOREIGN KEY REFERENCES Instances(Id) NULL
	)
GO

--Name pattern for stored procedures usp + OperationName + TableName
--Example: uspAddUsers, uspCountInstances

IF OBJECT_ID('uspAddInstances', 'P') IS NOT NULL
	DROP PROCEDURE uspAddInstances;
GO
--Procedure add new Instance to table Instances
CREATE PROC uspAddInstances
	@Id int = null,
	@HostName varchar(64),
	@InstanceName varchar(50),
	@Status int,
	@Version varchar(64),
	@Added datetime,
	@Modified datetime = NULL,
	@UserId int = NULL
AS
    BEGIN
	INSERT INTO Instances
			(HostName
			,InstanceName
			,Status
			,Version
			,Added
			,Modified
			,UserId)
	VALUES
			(@HostName
			,@InstanceName
			,@Status
			,@Version
			,@Added
			,@Modified
			,@UserId)
    END
GO

IF OBJECT_ID('uspUpdateInstances','P') IS NOT NULL
	DROP PROC uspUpdateInstances;
GO
--Procedure updates specific instance
CREATE PROC uspUpdateInstances
	@Id int,
	@HostName varchar(64),
	@InstanceName varchar(50),
	@Status int,
	@Version varchar(64),
	@Added datetime,
	@Modified datetime,
	@UserId int = null
AS
BEGIN
	UPDATE Instances SET
		InstanceName = @InstanceName,
		Status = @Status,
		Version = @Version,
		--Added = @Added,
		Modified = @Modified,
		UserId = @UserId
	WHERE HostName = @HostName
END
GO

IF OBJECT_ID('uspDeleteInstances','P') IS NOT NULL
	DROP PROC uspDeleteInstances;
GO
--Procedure deleted specific instance from Instance table;
CREATE PROC uspDeleteInstances
	@Id int
AS
	BEGIN
	DELETE FROM Instances WHERE Id = @Id
	END
GO

IF OBJECT_ID('uspGetAllInstances', 'P') IS NOT NULL
	DROP PROCEDURE uspGetAllInstances;
GO
--Procedure gets all instances from the instances table
CREATE PROC uspGetAllInstances
AS
	BEGIN
		SELECT Id, HostName, InstanceName, Status, Version, Added, Modified, UserId 
		FROM Instances
	END
GO

IF OBJECT_ID('uspSelectByIdInstanceWithUserInfo','P') IS NOT NULL
	DROP PROC uspSelectByIdInstanceWithUserInfo;
GO
-- Updates the specific instance
CREATE PROC uspSelectByIdInstanceWithUserInfo
	@Id int
AS
BEGIN
	SELECT				Instances.Id
						,Instances.HostName
						,Instances.InstanceName
						,Instances.Status
						,Instances.Version
						,Instances.Added
						,Instances.Modified
						,Instances.UserId
						,Users.Name
						,Users.Password
						,Users.AuthentificationMode
						FROM Instances LEFT outer JOIN Users On (Instances.UserId = Users.Id)
						WHERE Instances.Id=@Id
END	
GO

IF OBJECT_ID('uspExistsInstances','P') IS NOT NULL
	DROP PROC uspExistsInstances;
GO
--Checks whether there is an instance with this name in a database
CREATE PROC uspExistsInstances
	@Id int = null,
	@HostName varchar(64),
	@InstanceName varchar(50),
	@Status int = null,
	@Version varchar(64) = null,
	@Added datetime = null,
	@Modified datetime = null,
	@UserId int = null
AS
BEGIN
	SELECT *
	FROM Instances
	WHERE 
	InstanceName LIKE @InstanceName	
	AND HostName LIKE @HostName
END
GO

IF OBJECT_ID('uspCountInstances','P') IS NOT NULL
	DROP PROC uspCountInstances;
GO

CREATE PROC uspCountInstances
    @RowsCount int = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Instances
	SELECT @RowsCount AS RowsCount
END
GO

--Users operations--
IF OBJECT_ID('uspAddUsers','P') IS NOT NULL
	DROP PROC uspAddUsers;
GO
CREATE PROC uspAddUsers
	@Id int = NULL,
    @Name varchar(255),
	@Password varchar(64),
	@AuthentificationMode int = NULL
AS
	BEGIN
		INSERT INTO Users
				(Name
				,Password
				,AuthentificationMode)
		VALUES
				(@Name
				,@Password
				,@AuthentificationMode)
	END
GO

IF OBJECT_ID('uspUpdateUsers', 'P') IS NOT NULL
	DROP PROC uspUpdateUsers
GO
CREATE PROC uspUpdateUsers
	@Id int = NULL,
    @Name varchar(255),
	@Password varchar(64),
	@AuthentificationMode int 
AS
	BEGIN
		UPDATE Users SET
		Name = @Name,
		Password = @Password,
		AuthentificationMode = @AuthentificationMode
		WHERE Id = @Id
	END
GO

IF OBJECT_ID('uspGetAllUsers','P') IS NOT NULL
	DROP PROC uspGetAllUsers;
GO
--Method returns all users from table Users
CREATE PROC dbo.uspGetAllUsers
AS
BEGIN
	SELECT Id, Name, Password, AuthentificationMode
	FROM Users
END
GO

IF OBJECT_ID('uspSelectByIdUsers', 'P') IS NOT NULL 
	DROP PROC uspSelectByIdUsers;
GO
CREATE PROC uspSelectByIdUsers
	@Id int
AS
BEGIN 
	SELECT Id, Name, Password, AuthentificationMode
	FROM Users
	WHERE Id = @Id
END
GO

IF OBJECT_ID('uspDeleteUsers','P') IS NOT NULL
	DROP PROCEDURE uspDeleteUsers;
GO
-- Deletes specific user from database
CREATE PROCEDURE uspDeleteUsers
	@Id int
AS 
BEGIN
	DELETE FROM Users WHERE Id = @Id
END
GO

IF OBJECT_ID('uspCountUsers','P') IS NOT NULL
	DROP PROC uspCountUsers;
GO

CREATE PROC uspCountUsers
	@RowsCount int = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Users
	SELECT @RowsCount as RowsCount
END
GO

IF OBJECT_ID('uspSearchByInstanceNameInstances', 'P') IS NOT NULL
	DROP PROC uspSearchByInstanceNameInstances
GO
--Proc searchs instances by Instance name 
CREATE PROC uspSearchByInstanceNameInstances
	@InstanceName varchar(50),
	@PageNumber int,
	@InstancesPerPage int,
	@OrderBy varchar(4)
AS
BEGIN
	DECLARE @SearchCriteria varchar(50)
	BEGIN 
		SET @SearchCriteria = '%' + @InstanceName + '%'
	END
		BEGIN
			SELECT *
			FROM
				(SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY InstanceName) AS NUMBER,
				 Id, HostName, InstanceName, Status, Version, Added, Modified, UserId 
					FROM Instances 
					WHERE InstanceName like @SearchCriteria
					ORDER BY InstanceName)
					AS TBL			    
			WHERE NUMBER BETWEEN ((@PageNumber - 1) * @InstancesPerPage + 1) and (@PageNumber * @InstancesPerPage)
			ORDER BY InstanceName
		END
END
GO

IF OBJECT_ID('uspGetAllInstanceInfo', 'P') IS NOT NULL
	DROP PROC uspGetAllInstanceInfo;
GO
--Proc which returns all instances with related user infos for data collection
CREATE PROC uspGetAllInstanceInfo
AS
BEGIN
	SELECT 	 
	      Instances.Id
		 ,Instances.HostName
		 ,Instances.InstanceName
		 ,Users.Name as UserName
		 ,Users.Password
					FROM Instances LEFT JOIN Users On (Instances.UserId = Users.Id)
					ORDER BY Instances.Id ASC

END
GO

IF OBJECT_ID('uspGetAllInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspGetAllInstanceWithUserInfo;
GO
--Proc which returns all instances with related user infos
CREATE PROC uspGetAllInstanceWithUserInfo
AS
BEGIN
	SELECT 	 
	      Instances.Id
		 ,Instances.HostName
		 ,Instances.InstanceName
		 ,Instances.Status
		 ,Instances.Version
		 ,Instances.Added
		 ,Instances.Modified
		 ,Instances.UserId
		 ,Users.Name
		 ,Users.Password
		 ,Users.AuthentificationMode 
					FROM Instances LEFT JOIN Users On (Instances.UserId = Users.Id)
					ORDER BY Instances.Id ASC

END
GO

IF OBJECT_ID('uspSearchInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspSearchInstanceWithUserInfo;
GO
CREATE PROC uspSearchInstanceWithUserInfo
    @PageNumber int=1,
    @RecordsPerPage int= 20, 
	@HostNameSearch varchar(64) = '%',
	@InstanceNameSearch varchar(50) = '%',
	@StatusSearch int = -9999,
	@VersionSearch varchar(64) = '%',
	@AddedSearch datetime = null,
	@ModifiedSearch datetime = null,
	@UserNameSearch varchar(255) = null,
	@AuthentificationModeSearch int = -9999,
	@OrderByField varchar(255)= '%',
	@SearchCriteria varchar (255) = '%',
	
	--Order by can be ASC or DESC
	@OrderBy bit = 1
AS
BEGIN

			SELECT *
			FROM
				(SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY InstanceName) AS NUMBER, 
						Instances.Id
						,Instances.HostName
						,Instances.InstanceName
						,Instances.Status
						,Instances.Version
						,Instances.Added
						,Instances.Modified
						,Instances.UserId
						,Users.Name
						,Users.AuthentificationMode 
						FROM dbo.Instances LEFT outer JOIN dbo.Users On (dbo.Instances.UserId = dbo.Users.Id)
						WHERE 						
						   InstanceName like '%'+@InstanceNameSearch+'%'
					    and HostName like '%'+@HostNameSearch+'%'
						and (Status =  @StatusSearch or @StatusSearch = -9999)
						and Version like '%'+@VersionSearch+'%'
						and (Added = @AddedSearch or @AddedSearch is null )
						and (Modified = @ModifiedSearch or @ModifiedSearch is null)
					    and (Users.Name like '%'+@UserNameSearch+'%' or @UserNameSearch is null  )
						and (Users.AuthentificationMode = @AuthentificationModeSearch or @AuthentificationModeSearch = -9999)
									
		   
						
						)
					AS TBL			    
			WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RecordsPerPage + 1) and (@PageNumber * @RecordsPerPage)
			ORDER BY 
			CASE WHEN @OrderBy = 1 THEN
					CASE @OrderByField
					  WHEN 'Id' THEN TBL.Id 
					--  WHEN 'InstanceName'THEN  TBL.InstanceName
					  WHEN 'Added' THEN TBL.Added 
				--	  WHEN 'Version' THEN TBL.Version 
		      		END
				END ASC
				, CASE WHEN @OrderBy = 0 THEN
				   CASE @OrderByField
					  WHEN 'Id' THEN TBL.Id 
					--  WHEN 'InstanceName' THEN  TBL.InstanceName
					  WHEN 'Added' THEN TBL.Added 
					--  WHEN 'Version' THEN TBL.Version 
		            END
			END DESC

END
GO

IF OBJECT_ID('uspCountInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspCountInstanceWithUserInfo
GO

CREATE PROC uspCountInstanceWithUserInfo
	@RowsCount int = 0
AS 
BEGIN
	With QueryResult AS (SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY InstanceName) AS NUMBER, 
						Instances.Id
						,Instances.HostName
						,Instances.InstanceName
						,Instances.Status
						,Instances.Version
						,Instances.Added
						,Instances.Modified
						,Instances.UserId
						,Users.Name
						,Users.AuthentificationMode 
						FROM dbo.Instances LEFT outer JOIN dbo.Users On (dbo.Instances.UserId = dbo.Users.Id))
						SELECT @RowsCount = Count(*) FROM QueryResult
						SELECT @RowsCount AS RowsCount
END
GO

IF OBJECT_ID('uspSearchInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspSearchInstanceWithUserInfo;
GO
CREATE PROC uspSearchInstanceWithUserInfo

	@PageNumber int,
	@RecordsPerPage int,
	@IdSearch int = NULL,
	@HostNameSearch varchar(64) = null,
	@InstanceNameSearch varchar(50)=  null,
	@StatusSearch int = null,
	@VersionSearch varchar(50) = null,
	@AddedSearch datetime = null,
	@ModifiedSearch datetime = null,
	@UserIdSearch int = null,
	@UserNameSearch varchar(255) = null,
	@AuthentificationModeSearch int = null,
	@OrderByField varchar(1000) = null,
	@OrderBy bit = 0,

AS
BEGIN
--Query parts
--WHERES
	DECLARE @WHERE varchar(max)
	DECLARE @AND varchar(max)

 
	DECLARE @IdWhere varchar(1000)
	DECLARE @HostNameWhere varchar(1000)
	DECLARE @InstanceNameWhere varchar(1000)
	DECLARE @StatusWhere varchar(1000)
	DECLARE @VersionWhere varchar(1000)
	DECLARE @AddedWhere varchar(1000) 
	DECLARE @ModifiedWhere varchar(1000)
	DECLARE @UserIdWhere varchar(1000)
	DECLARE @UserNameWhere varchar(1000)
	DECLARE @AuthentificationModeWhere varchar(1000)
	DECLARE @OrderBySubQuery varchar(1000)

	DECLARE @Select varchar(MAX)
	DECLARE @SelectPagination varchar(max)
	DECLARE @FullQuery varchar(MAX)


--********************SETING PART*********************************
SET @WHERE = ' WHERE '
SET @AND = ' AND '
SET @SelectPagination = 'SELECT * 
		FROM QueryResult 
		WHERE NUMBER BETWEEN (('+CONVERT(varchar(10),@PageNumber)+' - 1) * '+CONVERT(varchar(10),@RecordsPerPage)
		+' + 1) and ('+CONVERT(varchar(10),@PageNumber)+' * '+CONVERT(varchar(10),@RecordsPerPage)+')'

/*
This section checks if there is value in 
passed parameters and makes parts of where block

IF(There is no value)
	SET default value
ELSE 
	SET passed value
*/

--Instance Id search
	IF(@IdSearch IS NULL)
		BEGIN
		   SET @IdWhere = ' Instances.Id > 0 '
		END
	ELSE
		BEGIN
		   SET @IdWhere = ' Instances.Id = '+ convert(varchar,@IdSearch)
		END

	--HostName search
	IF(@HostNameSearch IS NULL)
		BEGIN
		   SET @HostNameWhere = ' HostName like ''%'' '
		END
	ELSE
		BEGIN
			SET @HostNameWhere = ' HostName like ''%' + @HostNameSearch + '%'' '
		END

	--InstanceName search
	IF(@InstanceNameSearch IS NULL)
		BEGIN
		   SET @InstanceNameWhere = ' InstanceName like ''%'' ' 
		END
	ELSE
		BEGIN
			SET @InstanceNameWhere = ' InstanceName like ''%' + @InstanceNameSearch + '%'' '
		END

	--Status Search
	IF(@StatusSearch IS NULL)
		BEGIN
			SET @StatusWhere = ' Status > -1 '
		END
	ELSE
		BEGIN
			SET @StatusWhere = ' Status = ' + CONVERT(varchar, @StatusSearch)
		END

	--Version search
	IF(@VersionSearch IS NULL)
		BEGIN
		   SET @VersionWhere = ' Version like ''%'' '
		END
	ELSE
		BEGIN
			SET @VersionWhere = ' Version like ''%' + @VersionSearch + '%'' '
		END

	--Added search
	IF(@AddedSearch IS NULL)
		BEGIN
			SET @AddedWhere = ' Added > convert(datetime, ''1900-01-01'') '
		END
	ELSE
		BEGIN
			SET @AddedWhere = ' Added = ' + CONVERT(nvarchar, @AddedSearch)
		END

	--Modified search
	IF(@ModifiedSearch IS NULL)
		BEGIN
			SET @ModifiedWhere = ' (Modified > convert(datetime, ''1900-01-01'') or Modified IS NULL) '
		END
	ELSE
		BEGIN
			SET @ModifiedWhere = ' Modified = convert(datetime, '' ' + CONVERT(varchar, @ModifiedSearch) + ' '')'
		END

	--UserId search
	IF(@UserIdSearch IS NULL)
		BEGIN
			SET @UserIdWhere = ' (UserId > 0 OR UserId IS NULL) '
		END
	ELSE
		BEGIN
			SET @UserIdWhere = ' UserId = ' + CONVERT(varchar, @UserIdSearch)
		END

	--UserName search
	IF(@UserNameSearch IS NULL)
		BEGIN
		   SET @UserNameWhere =  ' (Users.Name like ''%'' or Name is NULL) '
		END
	ELSE
		BEGIN
		   SET @UserNameWhere = ' Users.Name like ''%' +@UserNameSearch+ '%'' '
		END

	--Auth Mode Search
	IF(@AuthentificationModeSearch IS NULL)
		BEGIN
			SET @AuthentificationModeWhere = ' (AuthentificationMode >= 0 OR AuthentificationMode IS NULL) '
		END
	ELSE 
		BEGIN
			SET @AuthentificationModeWhere = ' AuthentificationMode = ' + CONVERT(varchar, @AuthentificationModeSearch)
		END

	--Order by
	IF(@OrderBy = 0)
		BEGIN
			IF(@OrderByField IS NULL)
				BEGIN
					SET @OrderBySubQuery = ' ORDER BY Instances.Id  ASC) ' 
				END
			ELSE
				BEGIN
					SET @OrderBySubQuery = ' ORDER BY ' + @OrderByField + '  ASC) ' 
				END
		END
	ELSE
		BEGIN
			IF(@OrderByField IS NULL)
				BEGIN
					SET @OrderBySubQuery = ' ORDER BY Instances.Id  DESC)' 
				END
			ELSE
				BEGIN
					SET @OrderBySubQuery = ' ORDER BY ' + @OrderByField + '  DESC) ' 
				END
		END
SET @Select = 'WITH QueryResult AS (SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY Instances.Id) AS NUMBER, 
						Instances.Id
						,Instances.HostName
						,Instances.InstanceName
						,Instances.Status
						,Instances.Version
						,Instances.Added
						,Instances.Modified
						,Instances.UserId
						,Users.Name
						,Users.Password
						,Users.AuthentificationMode 
						FROM Instances LEFT outer JOIN Users On (Instances.UserId = Users.Id)
					  '
					SET @FullQuery = 
					@Select + @WHERE 
					+ @IdWhere + @AND
					+ @HostNameWhere + @AND
					+ @InstanceNameWhere + @AND
					+ @StatusWhere + @AND
					+ @VersionWhere + @AND
					+ @AddedWhere + @AND
					+ @ModifiedWhere + @AND
					+ @UserIdWhere + @AND
					+ @UserNameWhere + @AND
					+ @AuthentificationModeWhere 
					+ @OrderBySubQuery
					+ @SelectPagination
EXECUTE(@FullQuery)
END

