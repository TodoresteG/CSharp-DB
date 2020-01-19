SELECT u.Username,
		c.[Name] AS [CategoryName]
FROM Users u
FULL OUTER JOIN Reports r
ON u.Id = R.UserId
FULL OUTER JOIN Categories c
ON r.CategoryId = c.Id
WHERE DATEPART(DAY, r.OpenDate) = DATEPART(DAY, u.Birthdate)
ORDER BY u.Username ASC, c.[Name] ASC