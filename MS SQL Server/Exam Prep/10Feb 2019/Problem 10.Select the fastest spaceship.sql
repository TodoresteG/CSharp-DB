SELECT TOP(1) ss.[Name] AS [SpaceshipName],
		sp.[Name] AS [SpaceportName]
FROM Spaceships ss
JOIN Journeys j
ON ss.Id = j.SpaceshipId
JOIN Spaceports sp
ON j.DestinationSpaceportId = sp.Id
ORDER BY ss.LightSpeedRate DESC