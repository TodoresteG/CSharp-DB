SELECT p.[Name] AS [PlanetName],
		COUNT(*) AS [JourneysCount]
FROM Planets p
JOIN Spaceports s
ON p.Id = s.PlanetId
JOIN Journeys j
ON s.Id = j.DestinationSpaceportId
GROUP BY p.[Name]
ORDER BY JourneysCount DESC, p.[Name] ASC