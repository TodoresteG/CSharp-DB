SELECT TOP(10) p.FirstName,
		p.LastName,
		t.Price
FROM Passengers p
JOIN Tickets t
ON p.Id = t.PassengerId
ORDER BY t.Price DESC, p.FirstName ASC, p.LastName ASC