SELECT e.Id,
		e.FirstName
FROM Employees e
WHERE e.Salary > 6500
ORDER BY e.FirstName ASC, e.Id ASC