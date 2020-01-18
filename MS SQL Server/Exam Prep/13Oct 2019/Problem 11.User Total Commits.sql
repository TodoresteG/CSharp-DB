CREATE FUNCTION udf_UserTotalCommits(@username VARCHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @count INT
	SET @count = (
		SELECT COUNT(c.ContributorId)
		FROM Users u
		JOIN Commits c
		ON u.Id = c.ContributorId
		WHERE u.Username = @username
	)

	RETURN @count
END