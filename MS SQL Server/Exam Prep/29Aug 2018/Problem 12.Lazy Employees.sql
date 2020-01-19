SELECT DISTINCT e.Id,
		CONCAT(e.FirstName, ' ', e.LastName) AS [FullName]
FROM Employees e
LEFT OUTER JOIN Shifts s
ON e.Id = s.EmployeeId
WHERE DATEDIFF(HOUR, s.CheckIn, s.CheckOut) < 4
ORDER BY e.Id