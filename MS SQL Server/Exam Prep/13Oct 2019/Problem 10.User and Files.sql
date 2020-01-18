SELECT u.Username,
		AVG(f.Size) AS [Size]
FROM Users u
INNER JOIN Commits c
ON u.Id = c.ContributorId
INNER JOIN Files f
ON c.Id = f.CommitId
GROUP BY u.Username
ORDER BY Size DESC, u.Username ASC