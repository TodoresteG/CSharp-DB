SELECT t.FlightId,
		SUM(t.Price) AS [TotalPrice]
FROM Tickets t
GROUP BY t.FlightId
ORDER BY TotalPrice DESC, t.FlightId ASC