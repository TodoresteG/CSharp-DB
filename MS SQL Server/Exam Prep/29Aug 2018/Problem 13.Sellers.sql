SELECT TOP(10) CONCAT(e.FirstName, ' ', e.LastName) AS [FullName],
				SUM(i.Price * oi.Quantity) AS [TotalPrice],
				SUM(oi.Quantity) AS [Items]
FROM Employees e
LEFT OUTER JOIN Orders o
ON e.Id = o.EmployeeId
LEFT OUTER JOIN OrderItems oi
ON o.Id = oi.OrderId
LEFT OUTER JOIN Items i
ON oi.ItemId = i.Id
WHERE o.[DateTime] < '2018-06-15'
GROUP BY e.FirstName, e.LastName
ORDER BY TotalPrice DESC, Items DESC