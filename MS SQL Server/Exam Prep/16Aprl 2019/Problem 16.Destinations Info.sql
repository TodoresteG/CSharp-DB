SELECT f.Destination,
		COUNT(t.FlightId) AS [FliesCount]
FROM Flights f
LEFT OUTER JOIN Tickets t
ON f.Id = t.FlightId
GROUP BY f.Destination
ORDER BY FliesCount DESC, f.Destination ASC