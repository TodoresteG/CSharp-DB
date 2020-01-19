SELECT r.FirstName,
		r.LastName,
		r.Destination,
		r.Price
FROM (
	SELECT DENSE_RANK() OVER (PARTITION BY p.Id ORDER BY t.Price DESC) AS [Rank],
			p.FirstName,
			p.LastName,
			t.Price,
			f.Destination
	FROM Tickets t
	JOIN Passengers p
	ON t.PassengerId = p.Id
	JOIN Flights f
	ON t.FlightId = f.Id
) AS r
WHERE r.[Rank] = 1
ORDER BY r.Price DESC, r.FirstName ASC, r.LastName ASC, r.Destination ASC