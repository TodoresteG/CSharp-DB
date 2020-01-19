SELECT p.FirstName,
		p.LastName,
		p.Age
FROM Passengers p
FULL OUTER JOIN Tickets t
ON p.Id = t.PassengerId
WHERE t.PassengerId IS NULL
ORDER BY p.Age DESC, p.FirstName ASC, p.LastName ASC