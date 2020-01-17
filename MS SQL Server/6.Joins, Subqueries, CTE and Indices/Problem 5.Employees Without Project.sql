SELECT TOP(3) e.EmployeeID,
			e.FirstName
FROM Employees e
LEFT OUTER JOIN EmployeesProjects ep
ON e.EmployeeID = ep.EmployeeID
LEFT OUTER JOIN Projects p
ON ep.ProjectID = p.ProjectID
WHERE p.[Name] IS NULL
ORDER BY e.EmployeeID ASC