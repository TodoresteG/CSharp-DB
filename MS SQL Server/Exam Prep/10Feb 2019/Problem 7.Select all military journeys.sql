SELECT j.Id,
		CONVERT(VARCHAR(10), j.JourneyStart, 103) AS [JourneyStart],
		CONVERT(VARCHAR(10), j.JourneyEnd, 103) AS [JourneyEnd]
FROM Journeys j
WHERE j.Purpose = 'Military'
ORDER BY j.JourneyStart