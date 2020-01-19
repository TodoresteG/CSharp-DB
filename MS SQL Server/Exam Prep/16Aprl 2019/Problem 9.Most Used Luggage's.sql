SELECT lp.[Type],
		COUNT(*) AS [MostUsedLuggage]
FROM LuggageTypes lp
LEFT OUTER JOIN Luggages l
ON lp.Id = l.LuggageTypeId
LEFT OUTER JOIN Passengers p
ON l.PassengerId = p.Id
GROUP BY lp.[Type]
ORDER BY MostUsedLuggage DESC, lp.[Type] ASC