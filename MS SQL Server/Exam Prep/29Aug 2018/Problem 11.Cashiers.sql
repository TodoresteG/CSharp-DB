SELECT DISTINCT e.Id,
		e.FirstName,
		e.LastName
FROM Employees e
JOIN Orders o
ON e.Id = o.EmployeeId
ORDER BY e.Id