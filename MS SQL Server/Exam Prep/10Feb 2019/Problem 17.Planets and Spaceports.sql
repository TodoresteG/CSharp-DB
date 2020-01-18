SELECT p.[Name],
		COUNT(s.Id) AS [Count]
FROM Planets p
FULL OUTER JOIN Spaceports s
ON p.Id = s.PlanetId
GROUP BY p.[Name]
ORDER BY [Count] DESC, p.[Name] ASC