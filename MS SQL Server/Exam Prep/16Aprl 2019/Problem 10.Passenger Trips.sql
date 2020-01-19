SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [FullName],
		f.Origin,
		f.Destination
FROM Passengers p
JOIN Tickets t
ON p.Id = t.PassengerId
JOIN Flights f
ON t.FlightId = f.Id
ORDER BY FullName ASC, f.Origin ASC, f.Destination ASC