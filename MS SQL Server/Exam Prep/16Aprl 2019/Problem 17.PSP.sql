SELECT p.[Name],
		p.Seats,
		COUNT(t.PassengerId) AS [Passengers Count]
FROM Planes p
LEFT OUTER JOIN Flights f
ON p.Id = f.PlaneId
LEFT OUTER JOIN Tickets t
ON f.Id = t.FlightId
GROUP BY p.[Name], p.Seats
ORDER BY [Passengers Count] DESC, p.[Name] ASC, p.Seats ASC