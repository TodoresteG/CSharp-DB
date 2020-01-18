SELECT COUNT(*)
FROM Colonists c
JOIN TravelCards tc
ON c.Id = tc.ColonistId
JOIN Journeys j
ON tc.JourneyId = j.Id
WHERE j.Purpose = 'Technical'