UPDATE t
SET t.Price += t.Price * 0.13
FROM Tickets t
JOIN Flights f
ON t.FlightId = f.Id
WHERE f.Destination = 'Carlsbad'