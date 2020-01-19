SELECT TOP(1) o.Id AS [OrderId],
		SUM(i.Price * oi.Quantity) AS [TotalPrice]
FROM Orders o
LEFT OUTER JOIN OrderItems oi
ON o.Id = oi.OrderId
LEFT OUTER JOIN Items i
ON oi.ItemId = i.Id
GROUP BY o.Id
ORDER BY TotalPrice DESC
