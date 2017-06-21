IF DB_ID('testDb') IS NULL
	CREATE DATABASE testDb
GO

USE testDb
GO


IF OBJECT_ID('Users', 'U') IS NULL
	CREATE TABLE Users
	(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(255) NOT NULL,
		Password VARCHAR(64) NOT NULL
	)
GO


IF OBJECT_ID('Instances', 'U') IS NULL
	CREATE TABLE Instances
	(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		HostName VARCHAR(64) NOT NULL,
		InstanceName VARCHAR(64) NULL,
		Status INT NULL,
		Version VARCHAR(64) NULL,
		Added DATETIME NOT NULL,
		Modified DATETIME NULL,
        RAM INT NULL,
        CPUCount INT NULL,
		UserId INT CONSTRAINT FK_Instances_Users FOREIGN KEY REFERENCES Users(Id) NULL
	)
GO


IF OBJECT_ID('Databases', 'U') IS NULL
	CREATE TABLE Databases
	(
		Id INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
		Name VARCHAR(64) NOT NULL, 
		CreateTime DATETIME NOT NULL, 
		TotalSize INT NOT NULL,
		DbState INT NOT NULL,
		InstanceId INT NOT NULL CONSTRAINT FK_Databases_Instances FOREIGN KEY REFERENCES Instances(Id)
	)
GO


IF OBJECT_ID('Tables', 'U') IS NULL
	CREATE TABLE Tables 
	(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		Name VARCHAR(64) NOT NULL, 
		ColumnCount INT NOT NULL,
		DatabaseId INT NOT NULL CONSTRAINT FK_Tables_Databases FOREIGN KEY REFERENCES Databases(Id)
	)
GO


IF OBJECT_ID('Options', 'U') IS NULL
	CREATE TABLE Options
	(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
		Interval INT NOT NULL DEFAULT 5
	)
GO

INSERT INTO Options(Interval)
			VALUES(2)
GO


IF OBJECT_ID('uspAddBriefDatabase', 'P') IS NOT NULL 
	DROP PROC uspAddBriefDatabase
GO
CREATE PROC uspAddBriefDatabase
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@CreateTime DATETIME, 
	@TotalSize INT,
	@DbState INT,
	@InstanceId INT
AS 
	BEGIN
		INSERT INTO Databases(Name, CreateTime, TotalSize, DbState, InstanceId)
		VALUES(@Name, @CreateTime, @TotalSize, @DbState, @InstanceId)
	END
GO


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


IF OBJECT_ID('uspAddTables', 'P') IS NOT NULL
	DROP PROC uspAddTables
GO
CREATE PROC uspAddTables
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@ColumnCount INT,
	@DatabaseId INT
AS
	BEGIN
		INSERT INTO Tables(Name, ColumnCount, DatabaseId)
		VALUES(@Name, @ColumnCount, @DatabaseId)
	END
GO



IF OBJECT_ID('uspAddUsers','P') IS NOT NULL
	DROP PROC uspAddUsers;
GO
CREATE PROC uspAddUsers
	@Id INT = NULL,
    @Name VARCHAR(255),
	@Password VARCHAR(64)
AS
	BEGIN
		INSERT INTO Users
				(Name
				,Password)
		VALUES
				(@Name
				,@Password)
	END
GO


IF OBJECT_ID('uspCountDatabases', 'P') IS NOT NULL
	DROP PROC uspCountDatabases
GO
CREATE PROC uspCountDatabases
	@RowsCount INT = 0
AS
	BEGIN
		WITH QueryResult AS(SELECT Id FROM Databases) 
		SELECT @RowsCount = Count(*) FROM QueryResult
		SELECT @RowsCount AS RowsCount
	END
GO


IF OBJECT_ID('uspCountInstances','P') IS NOT NULL
	DROP PROC uspCountInstances;
GO

CREATE PROC uspCountInstances
    @RowsCount INT = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Instances
	SELECT @RowsCount AS RowsCount
END
GO


IF OBJECT_ID('uspCountInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspCountInstanceWithUserInfo
GO

CREATE PROC uspCountInstanceWithUserInfo
	@RowsCount int = 0
AS 
BEGIN
	With QueryResult AS (SELECT Instances.Id
						FROM Instances LEFT OUTER JOIN Users On (Instances.UserId = Users.Id))
						SELECT @RowsCount = Count(*) FROM QueryResult
						SELECT @RowsCount AS RowsCount
END
GO


IF OBJECT_ID('uspCountUsers','P') IS NOT NULL
	DROP PROC uspCountUsers;
GO

CREATE PROC uspCountUsers
	@RowsCount INT = 0
AS
BEGIN
	SELECT @RowsCount = COUNT(*) FROM Users
	SELECT @RowsCount as RowsCount
END
GO


IF OBJECT_ID('uspDeleteDatabases', 'P') IS NOT NULL
	DROP PROC uspDeleteDatabases
GO
CREATE PROC uspDeleteDatabases
	@Id INT
AS 
	BEGIN
		DELETE FROM Databases WHERE Databases.Id = @Id
	END
GO


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


IF OBJECT_ID('uspDeleteTables', 'P') IS NOT NULL
	DROP PROC uspDeleteTables
GO
CREATE PROC uspDeleteTables
	@Id INT 
AS 
	BEGIN
		DELETE FROM Tables WHERE Id = @Id
	END
GO


IF OBJECT_ID('uspDeleteUsers','P') IS NOT NULL
	DROP PROCEDURE uspDeleteUsers;
GO
-- Deletes specific user from database
CREATE PROCEDURE uspDeleteUsers
	@Id INT
AS 
	BEGIN
		DELETE FROM Users WHERE Id = @Id
	END
GO


IF OBJECT_ID('uspExistsBriefDatabase', 'P') IS NOT NULL 
	DROP PROC uspExistsBriefDatabase
GO
CREATE PROC uspExistsBriefDatabase
	@Id INT = NULL,
	@Name VARCHAR(64), 
	@CreateTime DATETIME = NULL, 
	@TotalSize INT = NULL,
	@DbState INT = NULL,
	@InstanceId INT
AS 
	BEGIN
		SELECT Id FROM Databases 
		WHERE Name LIKE @Name AND InstanceId = @InstanceId
	END
GO


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


IF OBJECT_ID('uspExistsTables', 'P') IS NOT NULL
	DROP PROC uspExistsTables
GO
CREATE PROC uspExistsTables 
	@Id INT,
	@Name VARCHAR(64),
	@ColumnCount INT = NULL,
	@DatabaseId INT	
AS
	BEGIN
		SELECT Id FROM Tables
		WHERE DatabaseId = @DatabaseId AND Name = @Name
	END
GO


IF OBJECT_ID('uspGetAllDatabases', 'P') IS NOT NULL
	DROP PROC uspGetAllDatabases
GO
CREATE PROC uspGetAllDatabases
AS
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases
	END
GO


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


IF OBJECT_ID('uspGetAllUsers','P') IS NOT NULL
	DROP PROC uspGetAllUsers;
GO
CREATE PROC uspGetAllUsers
AS
	BEGIN
		SELECT Id, Name, Password
		FROM Users
	END
GO


IF OBJECT_ID('uspSearchInstanceWithUserInfo', 'P') IS NOT NULL
	DROP PROC uspSearchInstanceWithUserInfo;
GO
CREATE PROC uspSearchInstanceWithUserInfo
	@PageNumber INT,
	@RecordsPerPage INT,
	@IdSearch INT = NULL,
	@HostNameSearch VARCHAR(64) = NULL,
	@InstanceNameSearch VARCHAR(64) =  NULL,
	@StatusSearch INT = NULL,
	@VersionSearch VARCHAR(50) = NULL,
	@AddedFromSearch DATETIME = NULL,
	@AddedToSearch DATETIME = NULL, 
	@ModifiedFromSearch DATETIME = NULL,
	@ModifiedToSearch DATETIME = NULL,
	@RAMSearch INT = NULL,
	@CPUCountSearch INT = NULL,
	@UserNameSearch VARCHAR(255) = NULL,
	@OrderByField VARCHAR(1000) = NULL,
	@OrderBy BIT = 1,
	@RowsCount INT = 0
AS
BEGIN
	WITH QueryResult AS (SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY Instances.Id) AS NUMBER, 
						Instances.Id
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
						FROM Instances LEFT OUTER JOIN Users On (Instances.UserId = Users.Id)
						WHERE	(@IdSearch IS NULL OR Instances.Id = @IdSearch)
								AND (@HostNameSearch IS NULL OR Instances.HostName LIKE '%' + @HostNameSearch + '%')
								AND (@InstanceNameSearch IS NULL OR Instances.InstanceName LIKE '%' + @InstanceNameSearch + '%')
								AND (@StatusSearch IS NULL OR Instances.Status = @StatusSearch)
								AND (@VersionSearch IS NULL OR Instances.Version LIKE '%' + @VersionSearch + '%')
								AND (@RAMSearch IS NULL OR Instances.RAM = @RAMSearch)
								AND (@CPUCountSearch IS NULL OR Instances.CPUCount = @CPUCountSearch)
								AND (@UserNameSearch IS NULL OR Users.Name LIKE '%' + @UserNameSearch + '%')

								AND (@AddedFromSearch IS NULL OR Instances.Added > @AddedFromSearch)
								AND (@AddedToSearch IS NULL OR Instances.Added < @AddedToSearch)
						
								AND (@ModifiedFromSearch IS NULL OR Instances.Modified > @ModifiedFromSearch)
								AND (@ModifiedToSearch IS NULL OR  Instances.Modified < @ModifiedToSearch))
		SELECT * ,(SELECT MAX(NUMBER) FROM QueryResult) AS RowsCount
		FROM QueryResult 
		WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RecordsPerPage + 1) and (@PageNumber * @RecordsPerPage)
		ORDER BY 
			(CASE WHEN @OrderByField LIKE 'Id' AND @OrderBy = 1 THEN Id END) ASC,
			(CASE WHEN @OrderByField LIKE 'Id' AND @OrderBy = 0 THEN Id END) DESC,
			(CASE WHEN @OrderByField LIKE 'InstanceName' AND @OrderBy = 1 THEN InstanceName END) ASC,
			(CASE WHEN @OrderByField LIKE 'InstanceName' AND @OrderBy = 0 THEN InstanceName END) DESC,
			(CASE WHEN @OrderByField LIKE 'HostName' AND @OrderBy = 1 THEN HostName END) ASC,
			(CASE WHEN @OrderByField LIKE 'HostName' AND @OrderBy = 0 THEN HostName END) DESC,
			(CASE WHEN @OrderByField LIKE 'Added' AND @OrderBy = 1 THEN Added END) ASC,
			(CASE WHEN @OrderByField LIKE 'Added' AND @OrderBy = 0 THEN Added END) DESC,
			(CASE WHEN @OrderByField LIKE 'Modified' AND @OrderBy = 1 THEN Modified END) ASC,
			(CASE WHEN @OrderByField LIKE 'Modified' AND @OrderBy = 0 THEN Modified END) DESC,
			(CASE WHEN @OrderByField LIKE 'Status' AND @OrderBy = 1 THEN Status END) ASC,
			(CASE WHEN @OrderByField LIKE 'Status' AND @OrderBy = 0 THEN Status END) DESC,
			(CASE WHEN @OrderByField LIKE 'RAM' AND @OrderBy = 1 THEN RAM END) ASC,
			(CASE WHEN @OrderByField LIKE 'RAM' AND @OrderBy = 0 THEN RAM END) DESC,
			(CASE WHEN @OrderByFIeld LIKE 'CPUCount' AND @OrderBy = 1 THEN CPUCount END) ASC,
			(CASE WHEN @OrderByFIeld LIKE 'CPUCount' AND @OrderBy = 0 THEN CPUCount END) DESC,
			(CASE WHEN @OrderByField LIKE 'UserName' AND @OrderBy = 1 THEN Name END) ASC,
			(CASE WHEN @OrderByField LIKE 'UserName' AND @OrderBy = 0 THEN Name END) DESC,
			(CASE WHEN @OrderByField LIKE 'Version' AND @OrderBy = 1 THEN Version END) ASC,
			(CASE WHEN @OrderByField LIKE 'Version' AND @OrderBy = 0 THEN Version END) DESC,
			(CASE WHEN @OrderByField IS NULL AND @OrderBy = 1 THEN Id END) ASC	
END
GO


IF OBJECT_ID('uspSelectByDatabaseIdsTables', 'P') IS NOT NULL
	DROP PROC uspSelectByDatabaseIdsTables
GO

IF TYPE_ID(N'DatabaseList') IS NOT NULL
	DROP TYPE DatabaseList;
Go

CREATE TYPE DatabaseList
AS TABLE
(
  DatabaseId INT
);
GO

CREATE PROC uspSelectByDatabaseIdsTables
	@Ids AS DatabaseList READONLY
AS 
	BEGIN
		SELECT Id, Name, ColumnCount, DatabaseId
		FROM Tables 
		WHERE DatabaseId IN (SELECT DatabaseId FROM @Ids)
	END
GO


IF OBJECT_ID('uspSelectByIdDatabases', 'P') IS NOT NULL
	DROP PROC uspSelectByIdDatabases
GO
CREATE PROC uspSelectByIdDatabases
	@Id INT 
AS 
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases 
		WHERE Id = @Id
	END
GO


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


IF OBJECT_ID('uspSelectByIdUsers', 'P') IS NOT NULL 
	DROP PROC uspSelectByIdUsers;
GO
CREATE PROC uspSelectByIdUsers
	@Id INT
AS
	BEGIN 
		SELECT Id, Name, Password
		FROM Users
		WHERE Id = @Id
	END
GO


IF OBJECT_ID('uspSelectByInstanceIdDatabases', 'P') IS NOT NULL
	DROP PROC uspSelectByInstanceIdDatabases
GO
CREATE PROC uspSelectByInstanceIdDatabases
	@Id INT 
AS 
	BEGIN
		SELECT Id, Name, CreateTime, TotalSize, DbState, InstanceId 
		FROM Databases 
		WHERE InstanceId = @Id
	END
GO


IF OBJECT_ID('uspUpdateBriefDatabase', 'P') IS NOT NULL
	DROP PROC uspUpdateBriefDatabase
GO
CREATE PROC uspUpdateBriefDatabase
	@Id INT,
	@Name VARCHAR(64), 
	@CreateTime DATETIME, 
	@TotalSize INT,
	@DbState INT,
	@InstanceId INT
AS
	BEGIN
		UPDATE Databases SET
		Name = @Name,
		CreateTime = @CreateTime, 
		TotalSize = @TotalSize,
		DbState = @DbState,
		InstanceId = @InstanceId
		WHERE Databases.Id = @Id
	END
GO


IF OBJECT_ID('uspUpdateBriefInstance','P') IS NOT NULL
	DROP PROC uspUpdateBriefInstance;
GO
--Procedure updates specific instance to offline status
CREATE PROC uspUpdateBriefInstance
	@Id INT,
	@Status INT,
	@Modified DATETIME,
	@UserID INT = NULL
AS
BEGIN
	UPDATE Instances SET
		Status = @Status,
		Modified = @Modified,
		UserID = @UserID
	WHERE Id = @Id
END
GO


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


IF OBJECT_ID('uspUpdateTables', 'P') IS NOT NULL
	DROP PROC uspUpdateTables
GO
CREATE PROC uspUpdateTables
	@Id INT, 
	@Name VARCHAR(64),
	@ColumnCount INT,
	@DatabaseId INT
AS 
	BEGIN
		UPDATE Tables 
		SET Name = @Name, 
			ColumnCount = @ColumnCount,
			DatabaseId = @DatabaseId
		WHERE Id = @Id 			
	END
GO


IF OBJECT_ID('uspUpdateUsers', 'P') IS NOT NULL
	DROP PROC uspUpdateUsers
GO
CREATE PROC uspUpdateUsers
	@Id INT = NULL,
    @Name VARCHAR(255),
	@Password VARCHAR(64)
AS
	BEGIN
		UPDATE Users 
		SET	Name = @Name,
			Password = @Password
		WHERE 
			Id = @Id
	END
GO


