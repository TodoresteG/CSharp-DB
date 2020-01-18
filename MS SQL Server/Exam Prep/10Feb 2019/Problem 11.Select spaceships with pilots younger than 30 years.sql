SELECT s.[Name],
		s.Manufacturer
FROM Spaceships s
JOIN Journeys j
ON s.Id = j.SpaceshipId
JOIN TravelCards tc
ON j.Id = tc.JourneyId
JOIN Colonists c
ON tc.ColonistId = c.Id
WHERE tc.JobDuringJourney = 'Pilot' AND DATEDIFF(YEAR, c.BirthDate, '01/01/2019') < 30
ORDER BY s.[Name] ASC
