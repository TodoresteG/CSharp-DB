SELECT TOP(1) j.Id,
		p.[Name] AS [PlanetName],
		s.[Name] AS [SpaceportName],
		j.Purpose
FROM Planets p
JOIN Spaceports s
ON p.Id = s.PlanetId
JOIN Journeys j
ON s.Id = j.DestinationSpaceportId
ORDER BY DATEDIFF(MONTH, j.JourneyStart, j.JourneyEnd)