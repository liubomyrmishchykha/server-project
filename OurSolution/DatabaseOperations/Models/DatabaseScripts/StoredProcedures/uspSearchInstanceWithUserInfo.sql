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

