SELECT TOP(10) oi.OrderId,
		MAX(i.Price) AS [ExpensivePrice],
		MIN(i.Price) AS [CheapPrice]
FROM Items i
JOIN OrderItems oi
ON i.Id = oi.ItemId
GROUP BY oi.OrderId
ORDER BY ExpensivePrice DESC, oi.OrderId ASC