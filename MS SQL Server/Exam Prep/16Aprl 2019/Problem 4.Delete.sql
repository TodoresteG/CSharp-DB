UPDATE t
SET t.FlightId = NULL
FROM Tickets t
JOIN Flights f
ON t.FlightId = f.Id
WHERE f.Destination = 'Ayn Halagim'

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'