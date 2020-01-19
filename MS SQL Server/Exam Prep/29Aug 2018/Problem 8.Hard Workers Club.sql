SELECT e.FirstName,
		e.LastName,
		AVG(DATEDIFF(HOUR, s.CheckIn, s.CheckOut)) AS [WorkHours]
FROM Employees e
JOIN Shifts s
ON e.Id = s.EmployeeId
GROUP BY e.FirstName, e.LastName, e.Id
HAVING AVG(DATEDIFF(HOUR, s.CheckIn, s.CheckOut)) > 7
ORDER BY WorkHours DESC, e.Id ASC