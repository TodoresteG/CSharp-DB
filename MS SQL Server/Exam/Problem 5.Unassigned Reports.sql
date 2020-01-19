SELECT r.[Description],
		CONVERT(VARCHAR(10), r.OpenDate, 105) AS [OpenDate]
FROM Reports r
WHERE r.EmployeeId IS NULL
ORDER BY r.OpenDate ASC, r.[Description] ASC