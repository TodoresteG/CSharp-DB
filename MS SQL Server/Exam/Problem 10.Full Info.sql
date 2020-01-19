SELECT CASE
			WHEN CONCAT(e.FirstName, ' ', e.LastName) = '' THEN 'None'
			ELSE CONCAT(e.FirstName, ' ', e.LastName)
		END AS [Employee],
		ISNULL(d.[Name], 'None') AS [Department],
		ISNULL(c.[Name], 'None') AS [Category],
		ISNULL(r.[Description], 'None') AS [Description],
		ISNULL(CONVERT(VARCHAR(10), r.OpenDate, 104), 'None') AS [OpenDate],
		ISNULL(s.Label, 'None') AS [Status],
		ISNULL(u.[Name], 'None') AS [User]
FROM Employees e
LEFT OUTER JOIN Departments d
ON e.DepartmentId = d.Id
FULL OUTER JOIN Reports r
ON e.Id = r.EmployeeId
LEFT OUTER JOIN Categories c
ON r.CategoryId = c.Id
LEFT OUTER JOIN [Status] s
ON r.StatusId = s.Id
JOIN Users u
ON r.UserId = u.Id
ORDER BY e.FirstName DESC, e.LastName DESC, d.[Name] ASC, c.[Name] ASC, r.[Description] ASC, r.OpenDate ASC, s.Label ASC, u.[Name] ASC