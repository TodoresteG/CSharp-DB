SELECT SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, 100) AS [EmailProvider],
		COUNT(*) AS [Number Of Users]
FROM Users u
GROUP BY SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, 100)
ORDER BY [Number Of Users] DESC, EmailProvider ASC