SELECT r.[Description],
		c.[Name] AS [CategoryName]
FROM Reports r
JOIN Categories c
ON r.CategoryId = c.Id
ORDER BY r.[Description] ASC, c.[Name] ASC