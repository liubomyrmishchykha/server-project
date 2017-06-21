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

