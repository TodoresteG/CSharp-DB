SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [FullName],
		pl.[Name] AS [PlaneName],
		CONCAT(f.Origin, ' - ', f.Destination) AS [Trip],
		lp.[Type] AS [LuggageType]
FROM Passengers p
JOIN Tickets t
ON p.Id = t.PassengerId
JOIN Flights f
ON t.FlightId = f.Id
JOIN Luggages l
ON t.LuggageId = l.Id
JOIN LuggageTypes lp
ON l.LuggageTypeId = lp.Id
JOIN Planes pl
ON f.PlaneId = pl.Id
ORDER BY FullName ASC, pl.[Name] ASC, f.Origin ASC, f.Destination ASC, lp.[Type] ASC