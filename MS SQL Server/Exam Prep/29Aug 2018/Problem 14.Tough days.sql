SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [FullName],
		DATENAME(WEEKDAY, s.CheckIn) AS [DayOfWeek]
FROM Employees e
LEFT OUTER JOIN Orders o
ON e.Id = o.EmployeeId
LEFT OUTER JOIN Shifts s
ON e.Id = s.EmployeeId
WHERE o.EmployeeId IS NULL AND DATEDIFF(HOUR, s.CheckIn, s.CheckOut) > 12
ORDER BY e.Id