SELECT p.FirstName,
		p.LastName,
		COUNT(t.FlightId) AS [TotalTrips]
FROM Passengers p
FULL OUTER JOIN Tickets t
ON p.Id = t.PassengerId
GROUP BY p.FirstName, p.LastName
ORDER BY TotalTrips DESC, p.FirstName ASC, p.LastName ASC