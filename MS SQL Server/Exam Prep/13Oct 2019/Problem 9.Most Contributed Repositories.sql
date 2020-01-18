SELECT TOP(5) r.Id,
		r.[Name],
		COUNT(*) AS [Commits]
FROM Repositories r
LEFT OUTER JOIN Commits c
ON r.Id = c.RepositoryId
LEFT OUTER JOIN RepositoriesContributors rc
ON r.Id = rc.RepositoryId
GROUP BY r.Id, r.[Name]
ORDER BY Commits DESC, r.Id ASC, r.[Name] ASC