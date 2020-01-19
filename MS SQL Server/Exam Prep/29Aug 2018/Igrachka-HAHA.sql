SELECT k.FullName,
		DATEDIFF(HOUR, s.CheckIn, s.CheckOut) AS [WorkHours],
		k.TotalPrice
FROM (
	SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [FullName],
		MAX(x.HighestPrice) AS [TotalPrice]
		FROM (
			SELECT o.Id,
				SUM(oi.Quantity * i.Price) AS [HighestPrice]
			FROM Orders o
			JOIN OrderItems oi
			ON o.Id = oi.OrderId
			JOIN Items i
			ON oi.ItemId = i.Id
			GROUP BY o.Id
		) AS x
		JOIN Orders o
		ON x.Id = o.Id
		JOIN Employees e
		ON o.EmployeeId = e.Id
		GROUP BY e.FirstName, e.LastName	
) AS k
JOIN Employees e
ON k.FullName = CONCAT(e.FirstName, ' ', e.LastName)
JOIN Shifts s
ON e.Id = s.EmployeeId
ORDER BY k.FullName ASC, WorkHours DESC, k.TotalPrice

GO

SELECT FullName,
		AVG(WorkHours)
FROM (
	SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [FullName],
		DATEDIFF(HOUR, s.CheckIn, s.CheckOut) AS [WorkHours]
	FROM Employees e
	JOIN Shifts s
	ON e.Id = s.EmployeeId
	GROUP BY e.FirstName, e.LastName
	ORDER BY FullName ASC, WorkHours DESC
) AS x
GROUP BY FullName