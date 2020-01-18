SELECT c.Id,
		CONCAT(c.FirstName, ' ', c.LastName) AS [FullName],
		c.Ucn
FROM Colonists c
ORDER BY c.FirstName ASC, c.LastName ASC, c.Id ASC