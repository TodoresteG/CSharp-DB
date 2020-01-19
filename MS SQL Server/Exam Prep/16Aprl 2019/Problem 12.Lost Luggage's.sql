SELECT p.PassportId,
		p.[Address]
FROM Passengers p
FULL OUTER JOIN Luggages l
ON p.Id = l.PassengerId
WHERE l.PassengerId IS NULL
ORDER BY p.PassportId ASC, p.[Address] ASC